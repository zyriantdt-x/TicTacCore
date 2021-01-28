using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using log4net;

namespace TicTacToeServer.Core
{
    public static class ConsoleCommandHandler
    {
        private static readonly ILog log =
            LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public static void Invoke(string input)
        {
            if (string.IsNullOrEmpty(input))
                return;

            try
            {
                string[] parameters = input.Split(' ');
                switch (parameters[0])
                {
                    case "toggledebug":
                    {
                        GameEnvironment.IsDebug = !GameEnvironment.IsDebug;
                        log.Info("Debugging mode toggled");
                        break;
                    }
                    case "playerdetails":
                    {
                        var UserUuid = parameters[1];
                        var UserInfo = GameEnvironment.GetWebSocketManager().GetWebSocketClientManager()
                            .GetWebSocketClient(UserUuid);
                        if (UserInfo == null) { log.Error("No user found"); break; }

                        log.Info("UUID: " + UserInfo.Uuid);
                        if (UserInfo.Game != null) log.Info("Game ID: " + UserInfo.Game.Uuid);
                        log.Info("IP: " + UserInfo.WebSocketConnection.ConnectionInfo.ClientIpAddress);
                        break;
                    }
                    case "gamedetails":
                    {
                        var GameUuid = parameters[1];
                        var GameInfo = GameEnvironment.GetGameManager().GetGame(GameUuid);
                        if (GameInfo == null) { log.Error("No game found"); break; }

                        log.Info("UUID: " + GameInfo.Uuid);
                        log.Info("Has Started: " + GameInfo.HasStarted);
                        if (GameInfo.MasterPlayer != null) log.Info("Master Player ID: " + GameInfo.MasterPlayer.Uuid);
                        if (GameInfo.SlavePlayer != null) log.Info("Slave Player ID: " + GameInfo.SlavePlayer.Uuid);
                        break;
                    }
                    default:
                    {
                        log.Error("Unknown console command!");
                        break;
                    }
                }
            }
            catch (Exception e)
            {
                log.Error(e);
            }
        }
    }
}
