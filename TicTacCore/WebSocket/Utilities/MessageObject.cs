using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicTacToeServer.WebSocket.Utilities
{
    public class MessageObject
    {
        public string Event { get; }
        public Dictionary<string, object> Body { get; set; }

        public MessageObject(string Event)
        {
            this.Event = Event;
            this.Body = new Dictionary<string, object>();
        }
    }
}
