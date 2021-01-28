using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicTacToeServer.WebSocket.Utilities;

namespace TicTacToeServer.WebSocket.Handlers.Outgoing
{
    public class PlayerJoinedHandler : OutgoingEvent
    {
        public PlayerJoinedHandler(string uuid)
            : base("PLAYER_JOINED")
        {
            AppendObject("uuid", uuid);
        }
    }
}
