using LogicMaster.generator;
using LogicMaster.gui.pages;
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

namespace LogicMaster
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        //rozmiar okna jest większy o stałą wartość niż rozmiar treści
        private double windowVerticalMarkup = 40;
        private double windowHorizontalMarkup = 16;
        private double windowTargetAspectRatio;
        private double windowInitialMinWidth;
        private MainMenu mainMenu { get; } = new MainMenu();
        private Game game { get; } = new Game();
        /// <summary>
        /// Konstruktor okna gry
        /// </summary>
        public MainWindow()
        {
            InitializeComponent();
            MinHeight = Height + windowVerticalMarkup;
            MinWidth = Width + windowHorizontalMarkup;
            windowTargetAspectRatio = MinWidth / MinHeight;
            windowInitialMinWidth = MinWidth;

            LoadMainMenu();
        }
        /// <summary>
        /// Funkcja uruchumiajaca menu glowne
        /// </summary>
        public void LoadMainMenu()
        {
            if (game.GameStarted)
                mainMenu.continueButton.IsEnabled = true;
            else
                mainMenu.continueButton.IsEnabled = false;

            contentFrame.Navigate(mainMenu);
        }
        /// <summary>
        /// Funkcja rozpoczynajaca nowa gre
        /// </summary>
        /// <param name="settings">ustawienia trudnosci gry</param>
        public void StartNewGame(GameSettings settings)
        {
            contentFrame.Navigate(game);
            game.LoadNewGame(settings);
        }
        /// <summary>
        /// Funkcja wznawiajaca gre
        /// </summary>
        public void ContinueGame()
        {
            if (game.GameStarted)
            {
                contentFrame.Navigate(game);
                game.ContinueGame();
            }
        }
        /// <summary>
        /// Funkcja zmieniajaca rozmiar okna
        /// </summary>
        /// <param name="sizeInfo">informacje o rozmiarze okna</param>
        protected override void OnRenderSizeChanged(SizeChangedInfo sizeInfo)
        {
            base.OnRenderSizeChanged(sizeInfo);

            double targetWidth = sizeInfo.NewSize.Height * windowTargetAspectRatio;
            targetWidth = targetWidth > windowInitialMinWidth ? targetWidth : windowInitialMinWidth;
            MinWidth = targetWidth + windowHorizontalMarkup;
            Width = targetWidth > sizeInfo.NewSize.Width ? targetWidth : sizeInfo.NewSize.Width;
            contentFrame.Width = targetWidth;
        }
        
        private void Window_StateChanged(object sender, EventArgs e)
        {
            if (WindowState == WindowState.Normal)
            {
                Height = MinHeight;
                Width = MinHeight * windowTargetAspectRatio;
            }
        }
    }
}
