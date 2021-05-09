using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Media;
using System.Windows.Shapes;

namespace Snake.Game
{
    public class Position
    {
        public int X { get; set; }
        public int Y { get; set; }

        public Position(int x, int y)
        {
            X = x;
            Y = y;
        }
    }

    public class Cube
    {
        public const int CubeWidth = 40;
        public const int CubeHeight = 40;

        public Position Coordinates { get; }

        public GameInfo Info { get; set; }
        public Rectangle Rect { get; set; }

        public Cube(int x, int y, Rectangle rect, GameInfo gameInfo)
        {
            Coordinates = new Position(x, y);
            Rect = rect;
            Info = gameInfo;
        }

        public void SetSnakeHead()
        {
            Info = GameInfo.SnakeHead;
            Rect.Fill = Brushes.Purple;
        }

        public void SetSnakeBody()
        {
            Info = GameInfo.SnakeBody;
            Rect.Fill = Brushes.Blue;
        }


        public void SetToNaN()
        {
            Info = GameInfo.NaN;
            Rect.Fill = Brushes.Black;
        }

        public void SetToApple()
        {
            Info = GameInfo.Apple;
            Rect.Fill = Brushes.Red;
        }

        public static Cube GetSnakeHead(int x, int y)
        {
            Rectangle headRectangle =  new Rectangle()
            {
                Width = CubeWidth,
                Height = CubeHeight,
                Fill = Brushes.Purple
            };

            return new Cube(x, y, headRectangle, GameInfo.SnakeHead);
        }

        public static Cube GetNaN(int x, int y)
        {
            Rectangle headRectangle = new Rectangle()
            {
                Width = CubeWidth,
                Height = CubeHeight,
                Fill = Brushes.Black
            };

            return new Cube(x, y, headRectangle, GameInfo.NaN);
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
