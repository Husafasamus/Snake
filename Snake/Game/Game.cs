using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;
using System.Windows.Threading;
using System.Linq;

namespace Snake.Game
{
    public class SnakeGame
    {
        private Direction _direction;
     
        public bool GameStatus { get; set; }
        public Snake Snake { get; set; }
        public GameField GameField { get; set; }

        public SnakeGame(int width, int height)
        {
            GameField = new GameField(width, height);          
        }

        public void Start()
        {
            GameStatus = true;
            Snake = new Snake();
            GenerateHead();
            GenerateApple();
            GenerateWall();
        }

        private void GenerateHead()
        {
            int x = 10;
            int y = 10;

            Snake.AddBody(x, y);         
        }

        public void GenerateBody()
        {
            Snake.AddBody();
        }

        public void GenerateWall()
        {
            GameField.gameField[5, 5].SetToWall();
        }

        public void GenerateApple()
        {
            Random rnd = new Random();
            int x = rnd.Next(0, GameField.cRectanglesOnWidth);
            int y = rnd.Next(0, GameField.cRectanglesOnHeight);

            GameField.gameField[x, y].SetToApple();
        }

        public bool Update()
        {           
            int x = Snake.GetHead().X;
            int y = Snake.GetHead().Y;

            if (_direction == Direction.Up)
                y--;
            if (_direction == Direction.Down)
                y++;
            if (_direction == Direction.Left)
                x--;
            if (_direction == Direction.Right)
                x++;

            UpdateSnakePosition(x, y);

            return GameStatus;
        }

        private bool Collision()
        {
            var newHeadPos = Snake.GetHead();

            if (newHeadPos.X < 0 || newHeadPos.X >= GameField.cRectanglesOnWidth ||
                    newHeadPos.Y < 0 || newHeadPos.Y >= GameField.cRectanglesOnHeight)
            {
                return false;
            }

            if (GameField.gameField[newHeadPos.X, newHeadPos.Y].Info == GameInfo.Apple)
            {
                GenerateBody();
                GenerateApple();
            }
            if (GameField.gameField[newHeadPos.X, newHeadPos.Y].Info == GameInfo.SnakeBody)
            {
                return false;
            }

            if (GameField.gameField[newHeadPos.X, newHeadPos.Y].Info == GameInfo.Wall)
            {
                return false;
            }

            return true;
        }

        private bool UpdateSnakePosition(int x, int y)
        {
            if (GameStatus)
            {
                var tmpSnake = new Snake(Snake);
                

                Snake.UpDatePositions(x, y);
                
                GameStatus = Collision();
                
                if (!GameStatus)
                {
                    return false;
                }

                for (int i = 0; i < tmpSnake.Count(); i++)
                {
                    var pp = tmpSnake.GetBody(i);
                    GameField.gameField[pp.X, pp.Y].SetToNaN();
                }

                GameField.gameField[Snake.GetHead().X, Snake.GetHead().Y].SetSnakeHead();
                if (Snake.Count() > 1)
                {
                    for (int i = 1; i < Snake.Count(); i++)
                    {
                        var oldPos = Snake.GetBody(i);
                        GameField.gameField[oldPos.X, oldPos.Y].SetSnakeBody();
                    }
                }

                return true;
            }

            return false;
        }

        public void Reset()
        {
            Snake = new Snake();
            GameField.ResetField();
        }

        public void SetDirection(Direction newDirection)
        {
            _direction = newDirection;
        }
    }

    public enum Direction
    {
        Up,
        Down,
        Left,
        Right
    }
}
