using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using Fleck;
using log4net;
using Newtonsoft.Json;
using TicTacToeServer.WebSocket.Handlers;
using TicTacToeServer.WebSocket.Handlers.Outgoing;
using TicTacToeServer.WebSocket.Utilities;

namespace TicTacToeServer.WebSocket
{
    public class WebSocketManager
    {
        private static readonly ILog log =
            LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        private IWebSocketServer _webSocketServer;
        private WebSocketClientManager _webSocketClientManager;
        private WebSocketHandlerManager _webSocketHandlerManager;

        public WebSocketManager(string Host, uint Port)
        {
            string location = "ws://" + Host + ":" + Port;
            _webSocketServer = new WebSocketServer(location);
            FleckLog.Level = LogLevel.Error;

            _webSocketClientManager = new WebSocketClientManager();
            _webSocketHandlerManager = new WebSocketHandlerManager();

            _webSocketServer.Start(socket =>
            {
                socket.OnOpen = () =>
                {
                    var Uuid = Guid.NewGuid().ToString();
                    log.Info("Connection created -> " + socket.ConnectionInfo.ClientIpAddress + " (" + Uuid + ")");
                    var socketClient = new WebSocketClient(Uuid, socket);
                    _webSocketClientManager.RegisterSocketClient(socketClient);
                    socketClient.Send(new HeyHandler(Uuid));
                };

                socket.OnClose = () =>
                {
                    var socketClient = _webSocketClientManager.GetWebSocketClient(socket);
                    GameEnvironment.GetGameManager().DestroyGame(socketClient.Game);
                    log.Info("Connection destroyed -> " + socket.ConnectionInfo.ClientIpAddress + " (" + socketClient.Uuid + ")");
                    _webSocketClientManager.DeregisterSocketClient(socket);
                };
                socket.OnMessage = (message) =>
                {
                    try
                    {
                        var Payload = JsonConvert.DeserializeObject<MessageObject>(message);
                        if (Payload.Event == null) throw new Exception("No event handler was passed.");

                        var Handler = _webSocketHandlerManager.GetIncomingEventHandler(Payload.Event);
                        if(Handler == null) throw new Exception("No handler was found for specified identifier");

                        var SocketClient = _webSocketClientManager.GetWebSocketClient(socket);
                        if(SocketClient == null) throw new Exception("No SocketClient instance was found for the socket connection");

                        LogMessage(SocketClient.Uuid, message);

                        Handler.Execute(SocketClient, Payload);
                    }
                    catch (Exception ex)
                    {
                        log.Error(ex);
                    }
                };
            });

            log.Info("WebSocketManager (" + location + ") -> LOADED!");
        }

        public static void LogMessage(string uuid, string msg, bool IsIncoming = true)
        {
            if (GameEnvironment.IsDebug)
            {
                if (IsIncoming) log.Debug("Message received (" + uuid + ") -> " + msg);
                if (!IsIncoming) log.Debug("Message sent (" + uuid + ") -> " + msg);
            }
        }

        public void Dispose()
        {
            _webSocketClientManager.Dispose();
            _webSocketServer.Dispose();
            log.Info("WebSocketManager -> DISPOSED!");
        }

        public WebSocketClientManager GetWebSocketClientManager() => _webSocketClientManager;
    }
}
