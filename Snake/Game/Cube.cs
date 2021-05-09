using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
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

        public const string HeadImage = "/snakeHead.jpg";

        public const int CubeWidth = 40;
        public const int CubeHeight = 40;

        public Position Coordinates { get; }

        public GameInfo Info { get; set; }
        public Image Rect { get; set; }

        public Cube(int x, int y, Image rect, GameInfo gameInfo)
        {
            Coordinates = new Position(x, y);
            Rect = rect;
            Info = gameInfo;
        }

        public void SetSnakeHead()
        {
            Info = GameInfo.SnakeHead;
            Rect.Source = new BitmapImage(new Uri("/Game/Data/snakeHead.jpg", UriKind.Relative));
        }

        public void SetSnakeBody()
        {
            Info = GameInfo.SnakeBody;
            Rect.Source = new BitmapImage(new Uri("/Game/Data/snakeBody.jpg", UriKind.Relative));
        }


        public void SetToNaN()
        {
            Info = GameInfo.NaN;
            Rect.Source = new BitmapImage(new Uri("/Game/Data/nan.jpg", UriKind.Relative));
        }

        public void SetToApple()
        {
            Info = GameInfo.Apple;
            Rect.Source = new BitmapImage(new Uri("/Game/Data/apple.jpg", UriKind.Relative));
        }

        public void SetToWall()
        {
            Info = GameInfo.Wall;
            Rect.Source = new BitmapImage(new Uri("/Game/Data/wall.jpg", UriKind.Relative));
        }


        public static Cube GetNaNImage(int x, int y)
        {
            Image headimage = new Image()
            {
                Width = CubeWidth,
                Height = CubeHeight,
                
            };

            headimage.Source = new BitmapImage(new Uri("/Game/Data/nan.jpg", UriKind.Relative));

            return new Cube(x, y, headimage, GameInfo.NaN);
        }
    }

    public enum GameInfo
    { 
        NaN,
        Wall,
        Snail,
        Apple,
        Poison,
        Dynamite,
        SnakeHead,
        SnakeBody
    }
}
