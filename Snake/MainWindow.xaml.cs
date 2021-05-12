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
        DispatcherTimer gameTimer1 = new DispatcherTimer();


        SnakeGame game = new SnakeGame(800, 800);

        TextBlock counter = new TextBlock()
        {
            Text = "Size: 0",
            FontSize = 25,
            FontWeight = FontWeights.Bold
        };

        public MainWindow()
        {      
            InitializeComponent();

            gameTimer.Tick += GameTimerEvent;
            gameTimer.Interval = TimeSpan.FromMilliseconds(350);

            gameTimer1.Tick += GameTimer1Event;
            gameTimer1.Interval = TimeSpan.FromMilliseconds(250);

            ShowMenu();
        }


        private void ShowGameFieldStart()
        {
            game.Start();
            ShowGameField();
        }

        private void ShowGameField()
        {
            ShowCounter();

            for (int x = 0; x < game.GameField.cRectanglesOnWidth; x++)
            {
                for (int y = 0; y < game.GameField.cRectanglesOnHeight; y++)
                {
                    GameField.Children.Add(game.GameField.gameField[x, y].Rect);
                    Canvas.SetLeft(game.GameField.gameField[x, y].Rect, game.GameField.gameField[x, y].Coordinates.X * CubeWidth);
                    Canvas.SetTop(game.GameField.gameField[x, y].Rect, (game.GameField.gameField[x, y].Coordinates.Y * CubeHeight) + CubeHeight);
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
                    Canvas.SetTop(game.GameField.gameField[x, y].Rect, (game.GameField.gameField[x, y].Coordinates.Y * CubeHeight) + CubeHeight);
                }
            }
        }

        private void RefreshGameFieldEnemys()
        {
            game.UpdateEnemyPosition();
            for (int x = 0; x < game.GameField.cRectanglesOnWidth; x++)
            {
                for (int y = 0; y < game.GameField.cRectanglesOnHeight; y++)
                {
                    Canvas.SetLeft(game.GameField.gameField[x, y].Rect, game.GameField.gameField[x, y].Coordinates.X * CubeWidth);
                    Canvas.SetTop(game.GameField.gameField[x, y].Rect, (game.GameField.gameField[x, y].Coordinates.Y * CubeHeight) + CubeHeight);
                }
            }
        }

        private void GameTimerEvent(object sender, EventArgs e)
        {
            RefreshGameField();
            counter.Text = $"Size: {game.Snake.Count() - 1}";
        }

        private void GameTimer1Event(object sender, EventArgs e)
        {
            RefreshGameFieldEnemys();


        }

        private void Start()
        {
            GameField.Background = new SolidColorBrush(Color.FromArgb(255, 113, 172, 30));
            ShowCounter();
            GameField.Children.Clear();
            GameField.Focus();
            gameTimer.Start();
            gameTimer1.Start();
            ShowGameFieldStart();
        }

        private void Play()
        {
            GameField.Focus();
            gameTimer.Start();
            gameTimer1.Start();

            ShowGameField();
        }

        private void Pause()
        {
            gameTimer.Stop();
            gameTimer1.Stop();
            ShowGameOver();

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

        private void BackToMenuButton_Click(object sender, RoutedEventArgs e)
        {
            Pause();
            GameField.Children.Clear();
            game.Reset();
            ShowMenu();
        }

        private void MenuScoreBoardButton_Click(object sender, RoutedEventArgs e)
        {
            GameField.Children.Clear();
            ShowScoreBoard();
        }



        private void ShowGameOver()
        {
            ShowPauseMenu();
        }

        private void ShowScoreBoard()
        {
            GameField.Background = new SolidColorBrush(Color.FromArgb(255, 113, 172, 30));
            
            StackPanel scorePanel = new StackPanel();
            //SolidColorBrush brush = new SolidColorBrush(Color.FromArgb(255, 105, 76, 46));
            //scorePanel.Background = brush;
            GameField.Children.Add(scorePanel);
            Canvas.SetLeft(scorePanel, (GameField.ActualWidth - scorePanel.ActualWidth) / 2);
            Canvas.SetTop(scorePanel, 10);

            TextBlock txt = new TextBlock()
            {
                Text = $"SCORE BOARD",
                FontSize = 35,
                FontWeight = FontWeights.Bold,
                Margin = new Thickness(5)
            };
            txt.Foreground = new SolidColorBrush(Colors.White);
            scorePanel.Children.Add(txt);

            var scoreBoard = game.scoreBoard.GetScoreBoard();

            for (int i = 0; i < scoreBoard.Count; i++)
            {
                TextBlock playerScor_ = new TextBlock()
                {
                    Text = $"{i+1}. {scoreBoard[i]}",
                    FontSize = 14,
                    FontWeight = FontWeights.Bold
                };
                playerScor_.Foreground = new SolidColorBrush(Colors.White);
                scorePanel.Children.Add(playerScor_);
            }

            Button BackButton = new Button()
            {
                Content = "Back to menu",
                Height = 30,
                Width = 105
            };
            BackButton.FontWeight = FontWeights.Bold;
            BackButton.FontSize = 16;
            BackButton.Foreground = Brushes.White;
            BackButton.Background = Brushes.Red;
            BackButton.Click += BackButton_Click;
            BackButton.BorderThickness = new Thickness(0);
            GameField.Children.Add(BackButton);
            Canvas.SetLeft(BackButton, 5);
            Canvas.SetTop(BackButton, 5);


        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            ShowMenu();
        }

        // TODO: Gameover menu

        private void ShowPauseMenu()
        {
            StackPanel pausePanel = new StackPanel();
            
            Button PauseResumeButton = new Button()
            {
                Content = "Resume",
                Height = 29,
                Width = 100
            };
            PauseResumeButton.Click += PauseMenuButton_Click;
            PauseResumeButton.FontWeight = FontWeights.Bold;
            PauseResumeButton.FontSize = 16;
            PauseResumeButton.Foreground = Brushes.White;
            SolidColorBrush brush = new SolidColorBrush(Color.FromArgb(255, 113, 172, 30));
            PauseResumeButton.Background = brush;
            PauseResumeButton.BorderThickness = new Thickness(0);
            PauseResumeButton.Margin = new Thickness(5);

            Button ExitButton = new Button()
            {
                Content = "Exit",
                Height = 30,
                Width = 100
            };
            PauseResumeButton.FontWeight = FontWeights.Bold;
            PauseResumeButton.FontSize = 16;
            PauseResumeButton.Foreground = Brushes.White;
            PauseResumeButton.Background = brush;
            PauseResumeButton.BorderThickness = new Thickness(0);
            pausePanel.Children.Add(PauseResumeButton);


            Button MenuExitButton = new Button()
            {
                Content = "Back to menu",
                Height = 30,
                Width = 105
            };
            MenuExitButton.FontWeight = FontWeights.Bold;
            MenuExitButton.FontSize = 16;
            MenuExitButton.BorderThickness = new Thickness(0);
            MenuExitButton.Foreground = Brushes.White;
            brush = new SolidColorBrush(Color.FromArgb(255, 224, 25, 25));
            MenuExitButton.Background = brush;
            MenuExitButton.Click += BackToMenuButton_Click;
            pausePanel.Children.Add(MenuExitButton);

            MenuExitButton.Margin = new Thickness(5);

            GameField.Children.Add(pausePanel);
            pausePanel.Width = 150;
            pausePanel.Height = 80;
            brush = new SolidColorBrush(Color.FromArgb(255, 105, 76, 46));
            pausePanel.Background = brush;
            Canvas.SetLeft(pausePanel, ((GameField.ActualWidth - pausePanel.ActualWidth) / 2) - 60);
            Canvas.SetTop(pausePanel, ((GameField.ActualHeight - pausePanel.ActualHeight) / 2) - 20);
        }

        

        // TODO: Fix menu to center

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

            Button MenuScoreBoardButton = new Button()
            {
                Content = "Score Board", 
                Height = 50,
                Width = 130
            };
            MenuScoreBoardButton.FontWeight = FontWeights.Bold;
            MenuScoreBoardButton.FontSize = 16;
            MenuScoreBoardButton.Foreground = Brushes.White;
            MenuScoreBoardButton.Background = Brushes.YellowGreen;
            MenuScoreBoardButton.BorderThickness = new Thickness(0);
            MenuScoreBoardButton.Click += MenuScoreBoardButton_Click;
            menuPanel.Children.Add(MenuScoreBoardButton);

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
            Canvas.SetLeft(menuPanel, ((GameField.ActualWidth - menuPanel.Width ) / 2));
            Canvas.SetTop(menuPanel, ((GameField.ActualHeight - menuPanel.Height) / 2));
        }

        

        private void ShowCounter()
        {
            counter.Foreground = new SolidColorBrush(Colors.White);
            GameField.Children.Add(counter);
            Canvas.SetTop(counter, 0);
            Canvas.SetLeft(counter, 5);
        }

        


    }
}
