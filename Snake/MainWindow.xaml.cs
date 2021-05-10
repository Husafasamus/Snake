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
using System.Windows.Resources;
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

            gameTimer.Tick += GameTimerEvent;
            gameTimer.Interval = TimeSpan.FromMilliseconds(350);
            ShowMenu();
        }


        private void ShowGameFieldStart()
        {
            game.Start();
            ShowGameField();
        }

        private void ShowGameField()
        {
            for (int x = 0; x < game.GameField.cRectanglesOnWidth; x++)
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
            GameField.Children.Clear();
            GameField.Focus();
            gameTimer.Start();
            ShowGameFieldStart();
        }

        private void Play()
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
            game.Start();
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
                Pause();
                ShowPauseMenu();
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
            Close();
        }

        private void MenuStartButton_Click(object sender, RoutedEventArgs e)
        {
            Start();
        }

        private void PauseMenuButton_Click(object sender, RoutedEventArgs e)
        {
            GameField.Children.Clear();
            Play();
        }

        private void ShowPauseMenu()
        {
            Button PauseResumeButton = new Button()
            {
                Content = "Resume",
                Height = 29,
                Width = 100
            };
            PauseResumeButton.Click += PauseMenuButton_Click;
            GameField.Children.Add(PauseResumeButton);
            Canvas.SetLeft(PauseResumeButton, 350);
            Canvas.SetTop(PauseResumeButton, 235);
        }

        private void ShowMenu()
        {
            StackPanel menuPanel = new StackPanel();
            GameField.Children.Add(menuPanel);

            Image img = new Image();
            img.Source = new BitmapImage(new Uri("/Game/Data/Menu/snake2.jpg", UriKind.Relative));
            menuPanel.Children.Add(img);

            GameField.Background = Brushes.Black;
            Button MenuStartButton = new Button()
            {
                Content = "Start new Game",
                Height = 50,
                Width = 130
            };
            MenuStartButton.FontWeight = FontWeights.Bold;   
            MenuStartButton.FontSize = 16;
            MenuStartButton.Foreground = Brushes.White;
            SolidColorBrush brush = new SolidColorBrush(Color.FromArgb(255, 113, 172,  30));
            MenuStartButton.Background = brush;
            MenuStartButton.BorderThickness = new Thickness(0);
            MenuStartButton.Click += MenuStartButton_Click;
            menuPanel.Children.Add(MenuStartButton);
            Button MenuExitButton = new Button()
            {
                Content = "Exit",
                Height = 30,
                Width = 50
            };
            MenuExitButton.FontWeight = FontWeights.Bold;
            MenuExitButton.FontSize = 16;
            MenuExitButton.BorderThickness = new Thickness(0);
            MenuExitButton.Foreground = Brushes.White;
            MenuExitButton.Background = Brushes.Red;
            MenuExitButton.Click += MenuExitButton_Click;
            menuPanel.Children.Add(MenuExitButton);
            MenuStartButton.Margin = new Thickness(5);

            MenuExitButton.Margin = new Thickness(5);
            Canvas.SetLeft(menuPanel, 40);
            Canvas.SetTop(menuPanel, 0);


        }


    }
}
