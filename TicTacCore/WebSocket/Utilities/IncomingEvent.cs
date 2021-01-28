using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicTacToeServer.WebSocket.Utilities
{
    public interface IncomingEvent
    {
        string Identifier { get; }
        IncomingEventType Type { get; }
        void Execute(WebSocketClient SocketClient, MessageObject Data);
    }
    public enum IncomingEventType
    {
        HANDSHAKE,
        NOT_HANDSHAKE
    }
}
