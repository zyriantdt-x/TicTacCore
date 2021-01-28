using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Fleck;
using TicTacToeServer.WebSocket.Handlers.Outgoing;

namespace TicTacToeServer.WebSocket.Utilities
{
    public class WebSocketClientManager
    {
        private List<WebSocketClient> _webSocketClients;

        public WebSocketClientManager()
        {
            _webSocketClients = new List<WebSocketClient>();
        }

        public WebSocketClient GetWebSocketClient(string Uuid)
        {
            return _webSocketClients.FirstOrDefault(x => x.Uuid == Uuid);
        }

        public WebSocketClient GetWebSocketClient(IWebSocketConnection Socket)
        {
            return _webSocketClients.FirstOrDefault(x => x.WebSocketConnection == Socket);
        }

        public void RegisterSocketClient(WebSocketClient Client)
        {
            _webSocketClients.Add(Client);
        }

        public void DeregisterSocketClient(WebSocketClient Client)
        {
            _webSocketClients.Remove(Client);
        }

        public void DeregisterSocketClient(IWebSocketConnection Socket)
        {
            var SocketClient = GetWebSocketClient(Socket);
            _webSocketClients.Remove(SocketClient);
        }

        public void Dispose()
        {
            foreach (var client in _webSocketClients)
            {
                client.Send(new ErrorHandler("Server is shutting down"));
                client.WebSocketConnection.Close();
            }
        }
    }
}
