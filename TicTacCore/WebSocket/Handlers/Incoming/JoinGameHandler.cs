using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicTacToeServer.WebSocket.Handlers.Outgoing;
using TicTacToeServer.WebSocket.Utilities;

namespace TicTacToeServer.WebSocket.Handlers.Incoming
{
    public class JoinGameHandler : IncomingEvent
    {
        public string Identifier => "JOIN_GAME";

        public void Execute(WebSocketClient SocketClient, MessageObject Data) // GameId
        {
            var gameId = Convert.ToString(Data.Body["uuid"]);

            var gameObject = GameEnvironment.GetGameManager().GetGame(gameId);
            if (gameObject == null)
            {
                SocketClient.Send(new ErrorHandler("Game doesn't exist"));
                return;
            }

            if (!gameObject.AddSlavePlayer(SocketClient))
            {
                SocketClient.Send(new ErrorHandler("Game already has two players"));
                return;
            }

            SocketClient.SetGame(gameObject);
            SocketClient.Send(new EstablishNewGameHandler(gameObject));
            SocketClient.Send(new UpdateBoardHandler(gameObject));
            gameObject.MasterPlayer.Send(new PlayerJoinedHandler(SocketClient.Uuid));
        }
    }
}
