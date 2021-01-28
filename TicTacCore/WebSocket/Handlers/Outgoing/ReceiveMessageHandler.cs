using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicTacToeServer.WebSocket.Utilities;

namespace TicTacCore.WebSocket.Handlers.Outgoing
{
    public class ReceiveMessageHandler : OutgoingEvent
    {
        public ReceiveMessageHandler(string nickname, string message, string time)
            : base("RECEIVE_MESSAGE")
        {
            base.AppendObject("nickname", nickname);
            base.AppendObject("message", message);
            base.AppendObject("time", time);
        }
    }
}
