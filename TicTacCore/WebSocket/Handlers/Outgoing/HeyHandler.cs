using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicTacToeServer.WebSocket.Utilities;

namespace TicTacToeServer.WebSocket.Handlers.Outgoing
{
    public class HeyHandler : OutgoingEvent
    {
        public HeyHandler(string uuid)
            : base("HEY")
        {
            AppendObject("uuid", uuid);
        }
    }
}
