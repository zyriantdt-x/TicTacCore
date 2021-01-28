using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicTacToeServer.WebSocket.Utilities;

namespace TicTacToeServer.WebSocket.Handlers.Outgoing
{
    public class PlayerWonHandler : OutgoingEvent
    {
        public PlayerWonHandler(string uuid, string player)
            : base("PLAYER_WON")
        {
            base.AppendObject("uuid", uuid);
            base.AppendObject("player", player);
        }
    }
}
