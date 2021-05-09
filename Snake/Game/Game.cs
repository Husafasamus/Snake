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
        //private GameField _gameField;
        //private List<Cube> _gameItems;
        private Direction _direction;
   
        public Snake Snake { get; set; }
        public GameField GameField { get; set; }

        public SnakeGame(int width, int height)
        {
            GameField = new GameField(width, height);          
        }

        public void Start()
        {
            Snake = new Snake();
            GenerateHead();  
        }

        private void GenerateHead()
        {
            // Random rnd = new Random();
            //int x = rnd.Next(0, GameField.cRectanglesOnWidth);
            //int y = rnd.Next(0, GameField.cRectanglesOnHeight);
            int x = 10;
            int y = 10;

            Snake.AddBody(x, y);
            
        }

        public void GenerateBody()
        {
            Snake.AddBody();
        }

        public void GenerateApple()
        {
            Random rnd = new Random();
            int x = rnd.Next(0, GameField.cRectanglesOnWidth);
            int y = rnd.Next(0, GameField.cRectanglesOnHeight);

            GameField.gameField[x, y].SetToApple();
            

        }

        public void Update()
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

        }

        private bool Collision()
        {
            var newHeadPos = Snake.GetHead();

            if (GameField.gameField[newHeadPos.X, newHeadPos.Y].Info == GameInfo.Apple)
            {
                GenerateBody();
            }

            return false;
        }

        private void UpdateSnakePosition(int x, int y)
        {
            for (int i = 0; i < Snake.Count(); i++)
            {
                var oldPos = Snake.GetBody(i);
                GameField.gameField[oldPos.X, oldPos.Y].SetToNaN();
            }

            Snake.UpDatePositions(x, y);
            Collision();
            GameField.gameField[Snake.GetHead().X, Snake.GetHead().Y].SetSnakeHead();
            if (Snake.Count() > 1)
            {
                for (int i = 1; i < Snake.Count(); i++)
                {
                    var oldPos = Snake.GetBody(i);
                    GameField.gameField[oldPos.X, oldPos.Y].SetSnakeBody();
                }
            }
        }

        public void Reset()
        {
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
