using LogicMaster.generator;
using LogicMaster.gui.dialog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Media;
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

namespace LogicMaster.gui.pages
{
    /// <summary>
    /// Interaction logic for MainMenu.xaml
    /// </summary>
    public partial class MainMenu : Page
    {
        private static readonly SoundPlayer clickSound = new SoundPlayer(@"resources/sounds/button_click.wav");

        private Dictionary<string, GameSettings> difficultyLevels { get; } = new Dictionary<string, GameSettings>()
        {
            { "Easy", new GameSettings(GameSettings.DifficultyPresets.Easy) },
            { "Medium", new GameSettings(GameSettings.DifficultyPresets.Medium) },
            { "Hard", new GameSettings(GameSettings.DifficultyPresets.Hard) },
        };

        public MainMenu()
        {
            InitializeComponent();
        }

        private void newGameButton_Click(object sender, RoutedEventArgs e)
        {
            clickSound.Play();

            MainWindow? mainWindow = App.Current.MainWindow as MainWindow;
            if (mainWindow != null)
            {
                NewGameWindow window = new NewGameWindow("Difficulty:", difficultyLevels.Keys.ToArray());
                window.Owner = mainWindow;
                mainWindow.contentOverlay.Opacity = 0.5;
                window.ShowDialog();
                mainWindow.contentOverlay.Opacity = 0.0;
                
                if (difficultyLevels.ContainsKey(window.Result))
                    mainWindow.StartNewGame(difficultyLevels[window.Result]);
            }
        }

        private void continueButton_Click(object sender, RoutedEventArgs e)
        {
            clickSound.Play();

            MainWindow? mainWindow = App.Current.MainWindow as MainWindow;
            if (mainWindow != null)
                mainWindow.ContinueGame();
        }

        private void quitButton_Click(object sender, RoutedEventArgs e)
        {
            clickSound.Play();

            MainWindow? mainWindow = App.Current.MainWindow as MainWindow;
            if (mainWindow != null)
                mainWindow.Close();
        }
    }
}
