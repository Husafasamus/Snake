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

        
        DispatcherTimer gameTimer = new DispatcherTimer();
     
        SnakeGame game = new SnakeGame(800, 800);

        public MainWindow()
        {      
            InitializeComponent();

            //GameField.Focus();
            gameTimer.Tick += GameTimerEvent;
            gameTimer.Interval = TimeSpan.FromMilliseconds(350);
            //Start();
            ShowMenu();
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
            if (!game.Update())
            {
                Pause();
            }
            
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

        private void Start()
        {
            GameField.Focus();
            gameTimer.Start();
            ShowGameField();
        }

        private void Pause()
        {
            gameTimer.Stop();
        }

        private void Reset()
        {
            game.Reset();
            Start();
        }

        private void AddBodySnake()
        {
            game.GenerateApple();
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

            if (e.Key == Key.H) // Add Apple
                AddBodySnake();

            if (e.Key == Key.P) // Pause
            {
                ShowMenu();
            }

            if (e.Key == Key.Escape)
            {
                Pause();
                GameField.Children.Clear();
                game.Reset();
                ShowMenu();

            }
                

            if (e.Key == Key.R) // Pause
                Reset();
        }

        private void Canvas_KeyUp(object sender, KeyEventArgs e)
        { 
        }

        private void MenuExitButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void MenuStartButton_Click(object sender, RoutedEventArgs e)
        {
          //  MenuStartButton.IsEnabled = false;
          //  MenuExitButton.IsEnabled = false;
           // MenuTitleText.IsEnabled = false;
            Start();

        }

        private void ShowMenu()
        {
            Button MenuStartButton = new Button()
            {
                Content = "Start new Game",
                Height = 29,
                Width = 100
            };
            MenuStartButton.Click += MenuStartButton_Click;
            GameField.Children.Add(MenuStartButton);
            Canvas.SetLeft(MenuStartButton, 350);
            Canvas.SetTop(MenuStartButton, 235);

            Button MenuExitButton = new Button()
            {
                Content = "Exit",
                Height = 29,
                Width = 48
            };
            MenuExitButton.Click += MenuExitButton_Click;
            GameField.Children.Add(MenuExitButton);
            Canvas.SetTop(MenuExitButton, 269);
            Canvas.SetLeft(MenuExitButton, 376);

            TextBlock MenuTitleText = new TextBlock()
            {
                Text = "Snake",
                FontSize = 50,
                FontWeight = FontWeights.Bold
            };
            GameField.Children.Add(MenuTitleText);
            Canvas.SetTop(MenuTitleText, 157);
            Canvas.SetLeft(MenuTitleText, 328);

        }


    }
}
