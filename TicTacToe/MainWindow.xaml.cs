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

namespace TicTacToe
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        GameLogic gameLogic = new GameLogic();
        private bool botEnable = false;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void PlayerClicksSpace(object sender, RoutedEventArgs e)
        {
            var space = (Button)sender;
            if (String.IsNullOrWhiteSpace(space.Content?.ToString()))
            {
                if (gameLogic.CurrentPlayer == "O")
                {
                    space.Background = Brushes.Blue;
                }
                else
                {
                    space.Background = Brushes.Red;
                }
                space.Content = gameLogic.CurrentPlayer;

                var coordinates = space.Tag.ToString().Split(',');
                var xValue = int.Parse(coordinates[0]);
                var yValue = int.Parse(coordinates[1]);

                var buttonPosition = new Position() { x= xValue, y = yValue };
                gameLogic.UpdateBoard(buttonPosition, gameLogic.CurrentPlayer);

                if (gameLogic.PlayerWin())
                {
                    if (gameLogic.CurrentPlayer == "O")
                    {
                        WinScreen.Foreground = Brushes.Blue;
                    }
                    else
                    {
                        WinScreen.Foreground = Brushes.Red;
                    }
                    WinScreen.Text = $"{gameLogic.CurrentPlayer} ПОБЕЖДАЕТ";
                    WinScreen.Visibility = Visibility.Visible;
                    return;
                }

                if (botEnable)
                {
                    gameLogic.SetNextPlayer();
                    int rnd = new Random().Next(0, 8);
                    while (!String.IsNullOrWhiteSpace(((Button)gameField.Children[rnd]).Content?.ToString()))
                    {
                        rnd = new Random().Next(0, 8);
                    }

                    if (gameLogic.CurrentPlayer == "O")
                    {
                        ((Button)gameField.Children[rnd]).Background = Brushes.Blue;
                    }
                    else
                    {
                        ((Button)gameField.Children[rnd]).Background = Brushes.Red;
                    }

                    ((Button)gameField.Children[rnd]).Content = gameLogic.CurrentPlayer;
                    coordinates = ((Button)gameField.Children[rnd]).Tag.ToString().Split(',');
                    xValue = int.Parse(coordinates[0]);
                    yValue = int.Parse(coordinates[1]);

                    buttonPosition = new Position() { x = xValue, y = yValue };
                    gameLogic.UpdateBoard(buttonPosition, gameLogic.CurrentPlayer);
                }

                if (gameLogic.PlayerWin())
                {
                    if(gameLogic.CurrentPlayer == "O")
                    {
                        WinScreen.Foreground = Brushes.Blue;
                    }
                    else
                    {
                        WinScreen.Foreground = Brushes.Red;
                    }
                    WinScreen.Text = $"{gameLogic.CurrentPlayer} ПОБЕЖДАЕТ";
                    WinScreen.Visibility = Visibility.Visible;
                    return;
                }
                gameLogic.SetNextPlayer();
            }
        }

        private void BtnNewGame_Click(object sender, RoutedEventArgs e)
        {
            foreach (var track in gameField.Children)
            {
                if(track is Button)
                {
                    ((Button)track).Content = String.Empty;
                    ((Button)track).Background = (SolidColorBrush)new BrushConverter().ConvertFrom("#272530");
                }
            }
            BtnTeam.Content = "X";
            BtnTeam.Background = Brushes.Red;

            Array.Clear(gameLogic.Board, 0,9);
            WinScreen.Visibility = Visibility.Collapsed;
            gameLogic.CurrentPlayer = "X";
        }

        private void BtnBotEnable_Click(object sender, RoutedEventArgs e)
        {
            if (botEnable == true)
            {
                botEnable = false;
            }
            else
            {
                botEnable = true;
            }
            if (botEnable)
            {
                BtnBotEnable.Background = Brushes.Green;
                BtnBotEnable.Foreground = Brushes.White;
            }
            else
            {
                BtnBotEnable.Background = Brushes.White;
                BtnBotEnable.Foreground = Brushes.Black;
            }
        }

        private void BtnTeam_Click(object sender, RoutedEventArgs e)
        {
            var btnTeam = (Button)sender;

            gameLogic.SetNextPlayer();

            btnTeam.Content = gameLogic.CurrentPlayer;

            if (btnTeam.Content.ToString() == "X")
            {
                btnTeam.Background = Brushes.Red;
            }
            else
            {
                btnTeam.Background = Brushes.Blue;
            }
        }
    }
}
