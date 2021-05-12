using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;
using System.Windows.Threading;
using System.Linq;


// TODO: 6. Refactoring
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

            Snake.AddBody(x, y, _direction);         
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
            int x = 0;
            int y = 0;

            while (true)
            {
                x = rnd.Next(0, GameField.cRectanglesOnWidth);
                y = rnd.Next(0, GameField.cRectanglesOnHeight);

                if (GetGameInfo(x, y) == GameInfo.NaN)
                    break;
            }

            GameField.gameField[x, y].SetToApple();
        }

        private GameInfo GetGameInfo(int x, int y)
        {
            return GameField.gameField[x, y].Info;
        }


        // TODO: 2. Dokoncit Generovanie Snail
        public void GenerateSnail()
        {

        }

       


        //TODO: 5. Generate enemy

        public bool Update()
        {           
            int x = Snake.GetHead().X;
            int y = Snake.GetHead().Y;
            Direction newDirection = Snake.GetHead().Facing;

            if (_direction == Direction.Up)
            { 
                y--; 
                newDirection = Direction.Up;
            }
            if (_direction == Direction.Down)
            {
                y++;
                newDirection = Direction.Down;
            }
            if (_direction == Direction.Left)
            {
                x--;
                newDirection = Direction.Left;
            }
            if (_direction == Direction.Right)
            {
                x++;
                newDirection = Direction.Right;
            }
                
            UpdateSnakePosition(x, y, newDirection);

            return GameStatus;
        }

        private bool Collision()
        {
            var newHeadPos = Snake.GetHead();
            
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

        private bool UpdateSnakePosition(int x, int y, Direction direction)
        {
            if (GameStatus)
            {
                var tmpSnake = new Snake(Snake);
                

                Snake.UpDatePositions(x, y, direction);
                
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

                GameField.gameField[Snake.GetHead().X, Snake.GetHead().Y].SetSnakeHead(direction);
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
