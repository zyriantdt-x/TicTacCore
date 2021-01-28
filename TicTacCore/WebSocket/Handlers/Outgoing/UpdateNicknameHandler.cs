using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicTacToeServer.WebSocket.Utilities;

namespace TicTacCore.WebSocket.Handlers.Outgoing
{
    public class UpdateNicknameHandler : OutgoingEvent
    {
        public UpdateNicknameHandler()
            : base("UPDATE_NICKNAME")
        {

        }
    }
}
