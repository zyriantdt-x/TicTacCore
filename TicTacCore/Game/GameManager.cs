using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using log4net;
using TicTacToeServer.WebSocket.Utilities;

namespace TicTacToeServer.Game
{
    public class GameManager
    {
        private static readonly ILog log =
            LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        private List<Game> _games;

        public GameManager()
        {
            _games = new List<Game>();
            log.Info("GameManager -> LOADED!");
        }

        public Game CreateGame(WebSocketClient master)
        {
            var gameObject = new Game(master);
            _games.Add(gameObject);
            return gameObject;
        }

        public void DestroyGame(Game game)
        {
            _games.Remove(game);
        }

        public Game GetGame(string uuid)
        {
            return _games.FirstOrDefault(x => x.Uuid == uuid);
        }
    }
}
