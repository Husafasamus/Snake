using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;
using System.Windows.Threading;

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
            Random rnd = new Random();
            int x = rnd.Next(0, GameField.cRectanglesOnWidth);
            int y = rnd.Next(0, GameField.cRectanglesOnHeight);

            Cube snakeHead = Cube.GetSnakeHead(x, y);
            Snake.AddBody(new Position(x, y));
            GameField.gameField[x, y] = snakeHead;
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

        private void UpdateSnakePosition(int x, int y)
        {
            var oldHeadPosition = Snake.GetHead();
            GameField.gameField[oldHeadPosition.X, oldHeadPosition.Y].Info = GameInfo.NaN;
            GameField.gameField[oldHeadPosition.X, oldHeadPosition.Y].Rect.Fill = Brushes.Black;

            GameField.gameField[x, y].Info = GameInfo.SnakeHead;
            GameField.gameField[x, y].Rect.Fill = Brushes.Purple;

            Snake.UpDatePositions(x, y);

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
