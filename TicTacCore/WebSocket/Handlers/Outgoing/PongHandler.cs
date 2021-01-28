using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicTacToeServer.WebSocket.Utilities;

namespace TicTacToeServer.WebSocket.Handlers.Outgoing
{
    public class PongHandler : OutgoingEvent
    {
        public PongHandler()
            : base("PONG")
        {
            base.AppendObject("date", DateTime.Now.ToString());
        }
    }
}
