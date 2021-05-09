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
        public const int CubeWidth = 40;
        public const int CubeHeight = 40;

        public const int SnakeSpeed = 8;
        public const int Speed = 10;
        DispatcherTimer gameTimer = new DispatcherTimer();
     

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
                    Canvas.SetLeft(game.GameField.gameField[x, y].Rect, game.GameField.gameField[x, y].Coordinates.X * CubeWidth);
                    Canvas.SetTop(game.GameField.gameField[x, y].Rect, game.GameField.gameField[x, y].Coordinates.Y * CubeHeight);
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
                    Canvas.SetLeft(game.GameField.gameField[x, y].Rect, game.GameField.gameField[x, y].Coordinates.X * CubeWidth);
                    Canvas.SetTop(game.GameField.gameField[x, y].Rect, game.GameField.gameField[x, y].Coordinates.Y * CubeHeight);
                }
            }
        }


        private void GameTimerEvent(object sender, EventArgs e)
        {
            RefreshGameField();
        }


        private void CreasteSnake()
        {

        }

        private void AddBodySnake()
        {
            game.GenerateApple();
        }

        private void SetSnakesBodyUp()
        {
     
        }


        private void Canvas_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Left)
                game.SetDirection(Direction.Left);
            
            if (e.Key == Key.Up)
                game.SetDirection(Direction.Up);   
            
            if (e.Key == Key.Right)    
                game.SetDirection(Direction.Right);
     
            if (e.Key == Key.Down)
                game.SetDirection(Direction.Down);

            if (e.Key == Key.H)
                AddBodySnake();
   
        }

        private void Canvas_KeyUp(object sender, KeyEventArgs e)
        {
   

        }
    }
}
