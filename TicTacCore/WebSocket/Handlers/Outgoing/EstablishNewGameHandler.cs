using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicTacToeServer.WebSocket.Utilities;

namespace TicTacToeServer.WebSocket.Handlers.Outgoing
{
    public class EstablishNewGameHandler : OutgoingEvent
    {
        public EstablishNewGameHandler(Game.Game game)
            : base("ESTABLISH_NEW_GAME")
        {
            AppendObject("uuid", game.Uuid);
        }
    }
}
