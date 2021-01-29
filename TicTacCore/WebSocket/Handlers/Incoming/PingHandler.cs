using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicTacToeServer.WebSocket.Handlers.Outgoing;
using TicTacToeServer.WebSocket.Utilities;

namespace TicTacToeServer.WebSocket.Handlers.Incoming
{
    public class PingHandler : IncomingEvent
    {
        public string Identifier => "PING";

        public void Execute(WebSocketClient SocketClient, MessageObject Data)
        {
            SocketClient.Send(new PongHandler());
        }
    }
}
