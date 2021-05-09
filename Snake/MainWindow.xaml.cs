using Snake.Game;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace Snake
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public const int SnakeSpeed = 8;
        public const int Speed = 10;
        DispatcherTimer gameTimer = new DispatcherTimer();

       

        int xPos = 10;
        int yPos = 10;

        int xPosb = 11;
        int yPosb = 10;


        //GameField gameField = new GameField(800, 800);
        SnakeGame game = new SnakeGame(800, 800);


        public MainWindow()
        {      
            InitializeComponent();

            GameField.Focus();
            gameTimer.Tick += GameTimerEvent;
            gameTimer.Interval = TimeSpan.FromMilliseconds(350);
            gameTimer.Start();
            ShowGameField();
        }


        private void ShowGameField()
        {
            game.Start();
            for (int x = 0; x <  game.GameField.cRectanglesOnWidth; x++)
            {
                for (int y = 0; y < game.GameField.cRectanglesOnHeight; y++)
                {
                    GameField.Children.Add(game.GameField.gameField[x, y].Rect);
                    Canvas.SetLeft(game.GameField.gameField[x, y].Rect, game.GameField.gameField[x, y].X);
                    Canvas.SetTop(game.GameField.gameField[x, y].Rect, game.GameField.gameField[x, y].Y);
                }
            }
            
        }

        private void RefreshGameField()
        {
            game.Update();
            for (int x = 0; x < game.GameField.cRectanglesOnWidth; x++)
            {
                for (int y = 0; y < game.GameField.cRectanglesOnHeight; y++)
                {
                    Canvas.SetLeft(game.GameField.gameField[x, y].Rect, game.GameField.gameField[x, y].X);
                    Canvas.SetTop(game.GameField.gameField[x, y].Rect, game.GameField.gameField[x, y].Y);
                }
            }
        }


        private void GameTimerEvent(object sender, EventArgs e)
        {
            RefreshGameField();
            //game.GameField.gameField[xPosb, yPosb].Rect.Fill = Brushes.Blue;
            //if (isLeft)
            //{
            //    gameField.gameField[xPos, yPos].Rect.Fill = Brushes.Blue;
            //    xPos -= 1;
            //    gameField.gameField[xPos, yPos].Rect.Fill = Brushes.Purple;
            //}
            //if (isRight)
            //{
            //    gameField.gameField[xPos, yPos].Rect.Fill = Brushes.Blue;
            //    xPos += 1;
            //    gameField.gameField[xPos, yPos].Rect.Fill = Brushes.Purple;
            //}
            //if (isUp)
            //{
            //    gameField.gameField[xPos, yPos].Rect.Fill = Brushes.Blue;
            //    yPos -= 1;
            //    gameField.gameField[xPos, yPos].Rect.Fill = Brushes.Purple;
            //}
            //if (isDown)
            //{
            //    gameField.gameField[xPos, yPos].Rect.Fill = Brushes.Blue;
            //    yPos += 1;
            //    gameField.gameField[xPos, yPos].Rect.Fill = Brushes.Purple;
            //}

            //UpdateGameField();
            //gameField.gameField[xPosb, yPosb].Rect.Fill = Brushes.Black;
            //if (isLeft)
            //{
            //    Canvas.SetLeft(snake[0], Canvas.GetLeft(snake[0]) - SnakeSpeed);
            //    this.SetSnakesBodyLeft();

            //}
            //if (isUp)
            //{
            //    Canvas.SetTop(snake[0], Canvas.GetTop(snake[0]) - SnakeSpeed);
            //    this.SetSnakesBodyUp();

            //}
            //if (isDown)
            //{
            //    Canvas.SetTop(snake[0], Canvas.GetTop(snake[0]) + SnakeSpeed);
            //    this.SetSnakesBodyDown();
            //}
            //if (isRight)
            //{
            //    Canvas.SetLeft(snake[0], Canvas.GetLeft(snake[0]) + SnakeSpeed);
            //    this.SetSnakesBodyRight();
            //}

        }


        private void CreasteSnake()
        {
            //Rectangle snakeHead = new Rectangle()
            //{
            //    Width = 40,
            //    Height = 40,
            //    Fill = Brushes.Purple
            //};


            //snake.Add(snakeHead);
            //GameField.Children.Add(snake[0]);
            
            //Canvas.SetLeft(snake[0], 100);
            //Canvas.SetTop(snake[0], 200);
        }

        private void AddBodySnake()
        {
            //Rectangle snakeBody = new Rectangle()
            //{
            //    Width = 40,
            //    Height = 40,
            //    Fill = Brushes.Green
            //};
            //snake.Add(snakeBody);

            //GameField.Children.Add(snake[snake.Count - 1]);
            //Canvas.SetLeft(snake[snake.Count - 1], Canvas.GetLeft(snake[0]));
            //Canvas.SetTop(snake[snake.Count - 1], Canvas.GetTop(snake[0]) + 40);       
        }

        private void SetSnakesBodyUp()
        {
        //    if (snake.Count > 1)
        //    {
        //        Canvas.SetTop(snake[snake.Count - 1], Canvas.GetTop(snake[snake.Count - 1]) - SnakeSpeed);

        //    }
        //}
        //private void SetSnakesBodyDown()
        //{
        //    if (snake.Count > 1)
        //    {
        //        Canvas.SetTop(snake[snake.Count - 1], Canvas.GetTop(snake[snake.Count - 1]) + SnakeSpeed);

        //    }
        //}
        //private void SetSnakesBodyLeft()
        //{
        //    if (snake.Count > 1)
        //    {
        //        Canvas.SetLeft(snake[snake.Count - 1], Canvas.GetLeft(snake[snake.Count - 1]) - SnakeSpeed);
        //    }
        //}
        //private void SetSnakesBodyRight()
        //{
        //    if (snake.Count > 1)
        //    {
        //        Canvas.SetLeft(snake[snake.Count - 1], Canvas.GetLeft(snake[snake.Count - 1]) + SnakeSpeed);

        //    }
        }


        private void Canvas_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Left)
            {
                game.SetDirection(Direction.Left);
            }
            if (e.Key == Key.Up)
            {
                game.SetDirection(Direction.Up);
            }
            if (e.Key == Key.Right)
            {
                game.SetDirection(Direction.Right);
            }
            if (e.Key == Key.Down)
            {
                game.SetDirection(Direction.Down);
            }



            //if (e.Key == Key.H)
            //{
            //    AddBodySnake();
            //}
        }

        private void Canvas_KeyUp(object sender, KeyEventArgs e)
        {
   

        }
    }
}
