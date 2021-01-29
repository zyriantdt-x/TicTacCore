using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicTacToeServer.WebSocket.Utilities;

namespace TicTacCore.WebSocket.Handlers.Incoming
{
    public class ModifyNicknameHandler : IncomingEvent
    {
        public string Identifier => "MODIFY_NICKNAME";

        public void Execute(WebSocketClient SocketClient, MessageObject Data)
        {
            SocketClient.Nickname = Convert.ToString(Data.Body["nickname"]);
        }
    }
}
