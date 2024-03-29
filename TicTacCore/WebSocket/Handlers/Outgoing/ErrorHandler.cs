﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicTacToeServer.WebSocket.Utilities;

namespace TicTacToeServer.WebSocket.Handlers.Outgoing
{
    public class ErrorHandler : OutgoingEvent
    {
        public ErrorHandler(string error)
            : base("ERROR")
        {
            AppendObject("message", error);
            AppendObject("time", DateTime.Now.ToString("HH::mm:ss"));
        }
    }
}
