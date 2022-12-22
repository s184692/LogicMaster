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

        public MainWindow()
        {
            InitializeComponent();
            MinHeight = Height + windowVerticalMarkup;
            MinWidth = Width + windowHorizontalMarkup;
            windowTargetAspectRatio = MinWidth / MinHeight;
            windowInitialMinWidth = MinWidth;

            LoadMainMenu();
        }

        public void LoadMainMenu()
        {
            contentFrame.Navigate(mainMenu);
        }

        public void StartNewGame()
        {
            contentFrame.Navigate(game);
            game.LoadNewGame();
        }

        public void ContinueGame()
        {

        }

        protected override void OnRenderSizeChanged(SizeChangedInfo sizeInfo)
        {
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
