using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace TicTacToeServer.WebSocket.Utilities
{
    public partial class OutgoingEvent
    {
        public MessageObject Body;

        public OutgoingEvent(string EventName)
        {
            Body = new MessageObject(EventName);
        }

        protected void AppendObject(string key, object val)
        {
            Body.Body.Add(key, val);
        }

        public override string ToString()
        {
            return JsonConvert.SerializeObject(Body);
        }
    }
}
