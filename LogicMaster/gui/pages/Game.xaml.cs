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
            inventoryGrid.Children.Clear();

            inventoryGrid.Children.Add(new GateInventoryBox(new BUF(), 1, 1));
            inventoryGrid.Children.Add(new GateInventoryBox(new NOT(), 1, 3));
            inventoryGrid.Children.Add(new GateInventoryBox(new AND(), 3, 1));
            inventoryGrid.Children.Add(new GateInventoryBox(new NAND(), 3, 3));
            inventoryGrid.Children.Add(new GateInventoryBox(new OR(), 5, 1));
            inventoryGrid.Children.Add(new GateInventoryBox(new NOR(), 5, 3));
            inventoryGrid.Children.Add(new GateInventoryBox(new XOR(), 7, 1));
            inventoryGrid.Children.Add(new GateInventoryBox(new XNOR(), 7, 3));

            LogicTarget lt1 = new LogicTarget();
            LogicSource ls1 = new LogicSource(true);
            LogicSource ls2 = new LogicSource(false);

            LogicContainer lc1 = new LogicContainer(lt1);
            LogicContainer lc2 = new LogicContainer();
            LogicContainer lc3 = new LogicContainer(ls1);
            LogicContainer lc4 = new LogicContainer(ls2);

            lc3.ConnectTo(lc2);
            lc4.ConnectTo(lc2);
            lc2.ConnectTo(lc1);

            gameCanvas.AddContainer(lc1, new Point(0.5, 0.2));
            gameCanvas.AddContainer(lc2, new Point(0.5, 0.4));
            gameCanvas.AddContainer(lc3, new Point(0.4, 0.6));
            gameCanvas.AddContainer(lc4, new Point(0.6, 0.6));
        }

        private void exitButton_Click(object sender, RoutedEventArgs e)
        {
            MainWindow? mainWindow = App.Current.MainWindow as MainWindow;
            if (mainWindow != null)
            {
                inventoryGrid.Children.Clear();
                mainWindow.LoadMainMenu();
            }
        }

        private void restartButton_Click(object sender, RoutedEventArgs e)
        {
            
        }
    }
}
