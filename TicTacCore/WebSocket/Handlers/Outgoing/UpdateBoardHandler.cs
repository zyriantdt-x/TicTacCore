using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicTacToeServer.Game;
using TicTacToeServer.WebSocket.Utilities;

namespace TicTacToeServer.WebSocket.Handlers.Outgoing
{
    public class UpdateBoardHandler : OutgoingEvent
    {
        public UpdateBoardHandler(Game.Game gameObject)
            : base("UPDATE_BOARD")
        {
            AppendObject("board_data", gameObject.GetBoardData());
        }

        public UpdateBoardHandler(int x, int y, BoardType newType)
            : base("UPDATE_BOARD")
        {
            AppendObject("x", x);
            AppendObject("y", y);
            AppendObject("new_type", newType);
        }
    }
}
