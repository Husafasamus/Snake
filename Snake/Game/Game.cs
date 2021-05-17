using System;


// TODO: 6. Refactoring
namespace Snake.Game
{
    public class SnakeGame
    {
        private const int SubSpeed = 20;
        private const int IncSpeed = 50;

        private Direction _direction;
        private int countEatedForSnail = 0;

        public bool GameStatus { get; set; }
        public Snake Snake { get; set; }
        public GameField GameField { get; set; }
        public Enemy Enemy { get; set; }
        public Enemy Enemy2 { get; set; }
        public ScoreBoard scoreBoard;
        public int SpeedMS { get; set; }
        public Snail Snail { get; set; }


        public SnakeGame(int width, int height)
        {
            GameField = new GameField(width, height);
            scoreBoard = new ScoreBoard();
            SpeedMS = 250;
        }

        public void Start()
        {
            GameStatus = true;
            Snake = new Snake();
            GenerateHead();
            GenerateApple();
            GenerateWall();
            GenerateEnemy();
            GenerateSnail();
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
            countEatedForSnail++;
            if (countEatedForSnail == 3)
            {
                countEatedForSnail = 0;
                GenerateSnail();
            }
        }

        public void GenerateWall()
        {
            GameField.gameField[5, 5].SetToWall();
            GameField.gameField[5, 6].SetToWall();
            GameField.gameField[6, 5].SetToWall();
            GameField.gameField[6, 6].SetToWall();
        }

        public void GenerateApple()
        {
            Random rnd = new Random();
            int x = 0;
            int y = 0;

            while (true)
            {
                x = rnd.Next(0, GameField.GetRectanglesOnWidth());
                y = rnd.Next(0, GameField.GetRectanglesOnHeight());

                if (GetGameInfo(x, y) == GameInfo.NaN)
                    break;
            }

            GameField.gameField[x, y].SetToApple();
        }

        private GameInfo GetGameInfo(int x, int y)
        {
            return GameField.gameField[x, y].Info;
        }


        public void GenerateSnail()
        {
            Random rnd = new Random();
            int x = 0;
            int y = 0;

            while (true)
            {
                x = rnd.Next(0, GameField.GetRectanglesOnWidth());
                y = rnd.Next(0, GameField.GetRectanglesOnHeight());

                if (GetGameInfo(x, y) == GameInfo.NaN)
                    break;
            }

            Snail = new Snail();
            Snail.SetPosition(x, y);
            GameField.gameField[Snail.Place.X, Snail.Place.Y].SetToSnail();
        }

        public void GenerateEnemy()
        {
            Enemy = new Enemy();
            Enemy.SetPosition(0, 0);
            GameField.gameField[Enemy.Place.X, Enemy.Place.Y].SetToEnemy();

            Enemy2 = new Enemy();
            Enemy2.SetPosition(19, 19);
            GameField.gameField[Enemy2.Place.X, Enemy2.Place.Y].SetToEnemy();
        }

        /// <summary>
        /// Aktualizovanie pozicie nepriatela.
        /// </summary>
        public void UpdateEnemyPosition()
        {

            GameField.gameField[Enemy.Place.X, Enemy.Place.Y].SetToNaN();
            int x = Enemy.Place.X += 1;
            Enemy.SetPosition(x, Enemy.Place.Y);
            if (GetGameInfo(Enemy.Place.X, Enemy.Place.Y) == GameInfo.Apple)
            {
                GenerateApple();
            }
            if (GetGameInfo(Enemy.Place.X, Enemy.Place.Y) == GameInfo.Snail)
            {
                GenerateSnail();
            }

            if (GetGameInfo(Enemy.Place.X, Enemy.Place.Y) == GameInfo.SnakeBody ||
                GetGameInfo(Enemy.Place.X, Enemy.Place.Y) == GameInfo.SnakeHead)
            {
                Enemy.Break = true;
            }


            GameField.gameField[Enemy.Place.X, Enemy.Place.Y].SetToEnemy();

            GameField.gameField[Enemy2.Place.X, Enemy2.Place.Y].SetToNaN();
            x = Enemy2.Place.X -= 1;
            Enemy2.SetPosition(x, Enemy2.Place.Y);
            if (GetGameInfo(Enemy2.Place.X, Enemy2.Place.Y) == GameInfo.Apple)
            {
                GenerateApple();
            }
            if (GetGameInfo(Enemy2.Place.X, Enemy2.Place.Y) == GameInfo.SnakeBody ||
                GetGameInfo(Enemy2.Place.X, Enemy2.Place.Y) == GameInfo.SnakeHead)
            {
                Enemy2.Break = true;
            }
            GameField.gameField[Enemy2.Place.X, Enemy2.Place.Y].SetToEnemy();

        }


        /// <summary>
        /// Aktualizacia pozicie hadika
        /// </summary>
        /// <returns>stav hry true/false</returns>
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

        /// <summary>
        /// Metoda, ktora kontroluje ci nastala kolizia
        /// </summary>
        /// <returns>true/false</returns>
        private bool Collision()
        {
            var newHeadPos = Snake.GetHead();

            if (GameField.gameField[newHeadPos.X, newHeadPos.Y].Info == GameInfo.Apple)
            {
                GenerateBody();
                GenerateApple();
                SpeedMS -= SubSpeed;
            }
            if (GameField.gameField[newHeadPos.X, newHeadPos.Y].Info == GameInfo.SnakeBody)
            {
                return false;
            }

            if (GameField.gameField[newHeadPos.X, newHeadPos.Y].Info == GameInfo.Wall)
            {
                return false;
            }
            if (GameField.gameField[newHeadPos.X, newHeadPos.Y].Info == GameInfo.Snail)
            {
                SpeedMS += IncSpeed;
            }


            if (Enemy.Break || Enemy2.Break)
            {
                return false;
            }


            return true;
        }


        /// <summary>
        /// aktualizacia pozicie hadika
        /// </summary>
        /// <param name="x">x os</param>
        /// <param name="y">y os</param>
        /// <param name="direction">smerovanie hlavy hadika</param>
        /// <returns>true/false</returns>
        private bool UpdateSnakePosition(int x, int y, Direction direction)
        {
            if (GameStatus)
            {
                var tmpSnake = new Snake(Snake);


                Snake.UpdatePosition(x, y, direction);

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

        public void AddPlayerToScoreBoard(string player, int size)
        {
            scoreBoard.AddPlayerScore(player, size);
            scoreBoard.PrintToCsv();
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
