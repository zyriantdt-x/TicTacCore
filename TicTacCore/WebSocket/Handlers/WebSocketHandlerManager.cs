using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicTacToeServer.WebSocket.Handlers.Incoming;
using TicTacToeServer.WebSocket.Utilities;

namespace TicTacToeServer.WebSocket.Handlers
{
    public class WebSocketHandlerManager
    {
        private List<IncomingEvent> _incomingEvents;

        public WebSocketHandlerManager()
        {
            _incomingEvents = new List<IncomingEvent>();
            RegisterIncoming();
        }

        private void RegisterIncoming()
        {
            _incomingEvents.Add(new PingHandler());

            _incomingEvents.Add(new ModifyBoxHandler());

            _incomingEvents.Add(new NewGameHandler());
            _incomingEvents.Add(new JoinGameHandler());
        }

        public IncomingEvent GetIncomingEventHandler(string identifier)
        {
            return _incomingEvents.FirstOrDefault(x => x.Identifier == identifier);
        }
    }
}
