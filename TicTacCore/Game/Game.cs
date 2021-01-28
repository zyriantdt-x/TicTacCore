using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicTacToeServer.Core;
using TicTacToeServer.Game.Utilities;
using TicTacToeServer.WebSocket.Utilities;

namespace TicTacToeServer.Game
{
    public class Game
    {
        public string Uuid { get; }
        private BoardType[,] _board;

        public bool IsHostPlaying { get; private set; }

        public WebSocketClient MasterPlayer { get; } // naughts
        public WebSocketClient SlavePlayer { get; private set; } // crosses

        public bool HasStarted;

        public Game(WebSocketClient masterPlayer)
        {
            Uuid = GameManager.RandomString(7);

            _board = new BoardType[3,3]
            {
                { BoardType.UNFILLED, BoardType.UNFILLED, BoardType.UNFILLED },
                { BoardType.UNFILLED, BoardType.UNFILLED, BoardType.UNFILLED },
                { BoardType.UNFILLED, BoardType.UNFILLED, BoardType.UNFILLED }
            };

            MasterPlayer = masterPlayer;
            HasStarted = false;
            IsHostPlaying = true;
        }

        public bool AddSlavePlayer(WebSocketClient slavePlayer)
        {
            if (SlavePlayer != null) return false;
            SlavePlayer = slavePlayer;
            HasStarted = true;
            return true;
        }

        public bool ModifyBox(Vector2 position, BoardType type, bool force = false)
        {
            if (_board[position.X, position.Y] != BoardType.UNFILLED && !force) return false;
            _board[position.X, position.Y] = type;
            return true;
        }

        public void SendToPlayers(OutgoingEvent handler)
        {
            MasterPlayer.Send(handler);
            SlavePlayer.Send(handler);
        }

        public void NextPlayer()
        {
            IsHostPlaying = !IsHostPlaying;
        }

        public bool CheckForWin()
        {
            var ArrayHandler = new ArrayHandling<BoardType>();

            for (int i = 0; i <= 2; i++)
            {
                var array = ArrayHandler.GetRow(_board, i);
                var item = array.FirstOrDefault();
                bool match = array.Skip(1).All(x => x == item && x != BoardType.UNFILLED);
                if (match) return true;
            }

            for (int i = 0; i <= 2; i++)
            {
                var array = ArrayHandler.GetColumn(_board, i);
                var item = array.FirstOrDefault();
                bool match = array.Skip(1).All(x => x == item && x != BoardType.UNFILLED);
                if (match) return true;
            }

            if (_board[0, 0] != BoardType.UNFILLED && _board[0, 0] == _board[1, 1] && _board[1, 1] == _board[2, 2]) return true;
            if (_board[0, 2] != BoardType.UNFILLED && _board[0, 2] == _board[1, 1] && _board[1, 1] == _board[2, 0]) return true;


            return false;
        }

        public void Dispose()
        {
            this.MasterPlayer.EndGame();
            this.SlavePlayer.EndGame();
        }

        public BoardType[,] GetBoardData() => _board;
    }

    public enum BoardType
    {
        UNFILLED,
        NAUGHT,
        CROSS
    }
}
