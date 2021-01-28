using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicTacToeServer.Game.Utilities
{
    public class Vector2
    {
        public int X { get; }
        public int Y { get; }

        public Vector2(int x, int y)
        {
            this.X = x;
            this.Y = y;
        }
    }
}
