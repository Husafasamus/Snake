using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Media;
using System.Windows.Shapes;

namespace Snake.Game
{
    public class Cube
    {
        public const int CubeWidth = 40;
        public const int CubeHeight = 40;

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

        public static Cube GetSnakeHead(int x, int y)
        {
            Rectangle headRectangle =  new Rectangle()
            {
                Width = CubeWidth,
                Height = CubeHeight,
                Fill = Brushes.Purple
            };

            return new Cube(CubeWidth * x, CubeHeight * y, headRectangle, GameInfo.SnakeHead);
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
