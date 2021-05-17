using System;
using System.Collections.Generic;
using System.Text;

namespace Snake.Game
{
    public class Position
    {
        public int X { get; set; }
        public int Y { get; set; }
        public Direction Facing { get; set; }

        public Position(int x, int y)
        {
            X = x;
            Y = y;
            Facing = Direction.Up;
        }

        public Position(int x, int y, Direction direction)
        {
            X = x;
            Y = y;
            Facing = direction;
        }
    }
}
