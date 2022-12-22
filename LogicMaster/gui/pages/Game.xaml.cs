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

namespace LogicMaster.gui.pages
{
    /// <summary>
    /// Interaction logic for Game.xaml
    /// </summary>
    public partial class Game : Page
    {
        public Game()
        {
            InitializeComponent();
        }

        public void LoadNewGame()
        {
            Image andImg = (Image)Application.Current.FindResource("andGateImage");
            Image nandImg = (Image)Application.Current.FindResource("nandGateImage");
            Image orImg = (Image)Application.Current.FindResource("orGateImage");
            Image norImg = (Image)Application.Current.FindResource("norGateImage");
            Image xorImg = (Image)Application.Current.FindResource("xorGateImage");
            Image xnorImg = (Image)Application.Current.FindResource("xnorGateImage");
            Image bufImg = (Image)Application.Current.FindResource("bufGateImage");
            Image notImg = (Image)Application.Current.FindResource("notGateImage");

            andGateContainer.gateContainer.Children.Add(new GateObject(andImg));
            nandGateContainer.gateContainer.Children.Add(new GateObject(nandImg));
            orGateContainer.gateContainer.Children.Add(new GateObject(orImg));
            norGateContainer.gateContainer.Children.Add(new GateObject(norImg));
            xorGateContainer.gateContainer.Children.Add(new GateObject(xorImg));
            xnorGateContainer.gateContainer.Children.Add(new GateObject(xnorImg));
            bufGateContainer.gateContainer.Children.Add(new GateObject(bufImg));
            notGateContainer.gateContainer.Children.Add(new GateObject(notImg));
        }

        private void exitButton_Click(object sender, RoutedEventArgs e)
        {
            MainWindow? mainWindow = App.Current.MainWindow as MainWindow;
            if (mainWindow != null)
                mainWindow.LoadMainMenu();
        }

        private void restartButton_Click(object sender, RoutedEventArgs e)
        {
            
        }
    }
}
