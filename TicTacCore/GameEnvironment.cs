using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using log4net;
using log4net.Repository.Hierarchy;
using TicTacToeServer.Game;
using TicTacToeServer.WebSocket;

namespace TicTacToeServer
{
    public static class GameEnvironment
    {
        public static bool IsDebug = true;

        private static readonly ILog log =
            LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        private static WebSocketManager _webSocketManager;
        private static GameManager _gameManager;

        public static void Initialise()
        {
            var currentColour = Console.ForegroundColor;
            Console.ForegroundColor = ConsoleColor.DarkRed;
            Console.BackgroundColor = ConsoleColor.White;
            Console.Clear();
            Console.WriteLine(@"  _______ _   _______      _______         ");
            Console.WriteLine(@" |__   __(_) |__   __|    |__   __|        ");
            Console.WriteLine(@"    | |   _  ___| | __ _  ___| | ___   ___ ");
            Console.WriteLine(@"    | |  | |/ __| |/ _` |/ __| |/ _ \ / _ \");
            Console.WriteLine(@"    | |  | | (__| | (_| | (__| | (_) |  __/");
            Console.WriteLine(@"    |_|  |_|\___|_|\__,_|\___|_|\___/ \___|");
            Console.WriteLine("\n > TicTacToe Server by Ellis <\n\n");
            Console.ForegroundColor = currentColour;

            _webSocketManager = new WebSocketManager("0.0.0.0", 1232);
            _gameManager = new GameManager();
            log.Info("TicTacToe Server has initialised successfully!\n");
        }

        public static void Dispose()
        {
            _webSocketManager.Dispose();
        }

        public static WebSocketManager GetWebSocketManager() => _webSocketManager;
        public static GameManager GetGameManager() => _gameManager;
    }
}
