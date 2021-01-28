using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicTacToeServer.WebSocket.Handlers.Outgoing;
using TicTacToeServer.WebSocket.Utilities;

namespace TicTacToeServer.WebSocket.Handlers.Incoming
{
    public class NewGameHandler : IncomingEvent
    {
        public string Identifier => "NEW_GAME";

        public IncomingEventType Type => IncomingEventType.NOT_HANDSHAKE;

        public void Execute(WebSocketClient SocketClient, MessageObject Data)
        {
            var gameObject = GameEnvironment.GetGameManager().CreateGame(SocketClient);
            var nickname = Convert.ToString(Data.Body["nickname"]);
            if (!SocketClient.SetGame(gameObject))
            {
                SocketClient.Send(new ErrorHandler("Already in game"));
                return;
            }

            SocketClient.Nickname = nickname;
            SocketClient.Send(new EstablishNewGameHandler(gameObject));
            SocketClient.Send(new UpdateBoardHandler(gameObject));
        }
    }
}
