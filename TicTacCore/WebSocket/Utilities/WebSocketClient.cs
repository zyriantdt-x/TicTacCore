using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Fleck;
using Newtonsoft.Json;

namespace TicTacToeServer.WebSocket.Utilities
{
    public class WebSocketClient
    {
        public string Uuid { get; set; }
        public IWebSocketConnection WebSocketConnection { get; set; }
        public Game.Game Game { get; private set; }

        public string Nickname { get; set; }
        public WebSocketClient(string Uuid, IWebSocketConnection Socket)
        {
            this.Uuid = Uuid;
            this.WebSocketConnection = Socket;
        }

        public void Send(OutgoingEvent Event)
        {
            WebSocketConnection.Send(JsonConvert.SerializeObject(Event.Body));
            WebSocketManager.LogMessage(Uuid, Event.ToString(), false);
        }

        public bool SetGame(Game.Game game)
        {
            if (Game != null) return false;
            Game = game;
            return true;
        }

        public void EndGame()
        {
            this.Game = null;
        }
    }
}
