using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Shapes;

namespace Snake.Game
{
    public class Cube
    {
        public int X { get; set; }
        public int Y { get; set; }

        public GameInfo Info { get; set; }
        public Rectangle Rect { get; set; }

        public Cube(int x, int y, Rectangle rect, GameInfo gameInfo)
        {
            X = x;
            Y = y;
            Rect = rect;
            Info = gameInfo;
        }
    }

    public enum GameInfo
    { 
        NaN,
        Wall,
        Apple,
        Poison,
        Dynamite,
        SnakeHead,
        SnakeBody
    }
}
