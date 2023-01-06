using LogicMaster.gameplay.logic.gates;
using LogicMaster.gameplay.logic;
using LogicMaster.gui.controls;
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
using System.Numerics;
using LogicMaster.gameplay;
using LogicMaster.generator;
using System.Media;

namespace LogicMaster.gui.pages
{
    /// <summary>
    /// Interaction logic for Game.xaml
    /// </summary>
    public partial class Game : Page
    {
        private static readonly SoundPlayer clickSound = new SoundPlayer(@"resources/sounds/button_click.wav");

        private GameManager gameManager { get; set; }

        public bool GameStarted { get { return gameManager.GameStarted; } }

        public Game()
        {
            InitializeComponent();
            gameManager = new GameManager(this);
        }

        public void LoadNewGame(GameSettings gameSettings)
        {
            gameSettings.MergeChance = 0.0;
            gameManager.LoadNewGame(gameSettings);
            gameManager.StartTimer();
        }

        public void ContinueGame()
        {
            gameManager.StartTimer();
        }

        private void exitButton_Click(object sender, RoutedEventArgs e)
        {
            clickSound.Play();

            MainWindow? mainWindow = App.Current.MainWindow as MainWindow;
            if (mainWindow != null)
            {
                gameManager.StopTimer();
                mainWindow.LoadMainMenu();
            }
        }

        private void restartButton_Click(object sender, RoutedEventArgs e)
        {
            clickSound.Play();

            gameManager.RestartCurrentGame();
        }
    }
}
