using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicTacToeServer.Game;
using TicTacToeServer.Game.Utilities;
using TicTacToeServer.WebSocket.Handlers.Outgoing;
using TicTacToeServer.WebSocket.Utilities;

namespace TicTacToeServer.WebSocket.Handlers.Incoming
{
    public class ModifyBoxHandler : IncomingEvent
    {
        public string Identifier => "MODIFY_BOX";

        public void Execute(WebSocketClient SocketClient, MessageObject Data) // X, Y
        {
            var location = new Vector2(Convert.ToInt32(Data.Body["x"]), Convert.ToInt32(Data.Body["y"]));

            BoardType boardType = SocketClient.Game.MasterPlayer.Uuid == SocketClient.Uuid
                ? BoardType.NAUGHT
                : BoardType.CROSS;

            if (!SocketClient.Game.HasStarted)
            {
                SocketClient.Send(new ErrorHandler("Game hasn't started"));
                return;
            }

            if ((SocketClient.Game.MasterPlayer.Uuid == SocketClient.Uuid && SocketClient.Game.IsHostPlaying) ||
                (SocketClient.Game.SlavePlayer.Uuid == SocketClient.Uuid && !SocketClient.Game.IsHostPlaying))
            {

                if (SocketClient.Game.ModifyBox(location, boardType))
                {
                    SocketClient.Game.SendToPlayers(new UpdateBoardHandler(location.X, location.Y, boardType));

                    if (SocketClient.Game.CheckForWin())
                    {
                        SocketClient.Game.SendToPlayers(new PlayerWonHandler(SocketClient.Uuid, SocketClient.Nickname));
                        SocketClient.Game.Dispose();
                        return;
                    }

                    SocketClient.Game.NextPlayer();
                }
                else
                    SocketClient.Send(new ErrorHandler("Box is already filled"));
            }
            else
            {
                SocketClient.Send(new ErrorHandler("Not your go"));
            }
        }
    }
}
