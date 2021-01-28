using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicTacCore.WebSocket.Handlers.Outgoing;
using TicTacToeServer.WebSocket.Handlers.Outgoing;
using TicTacToeServer.WebSocket.Utilities;

namespace TicTacCore.WebSocket.Handlers.Incoming
{
    public class SendMessageHandler : IncomingEvent
    {
        public string Identifier => "SEND_MESSAGE";

        public void Execute(WebSocketClient SocketClient, MessageObject Data)
        {
            var message = Convert.ToString(Data.Body["message"]);
            if (message.Contains('<') || message.Contains('>'))
            {
                SocketClient.Send(new ErrorHandler("Chat message can't contain special characters"));
                return;
            }

            if (!SocketClient.Game.HasStarted)
            {
                SocketClient.Send(new ErrorHandler("Game hasn't started"));
                return;
            }

            SocketClient.Game.SendToPlayers(new ReceiveMessageHandler(SocketClient.Nickname, message, DateTime.Now.ToString("HH:mm:ss")));
        }
    }
}
