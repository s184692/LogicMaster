using LogicMaster.gameplay.logic;
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

namespace LogicMaster.gui.controls
{
    /// <summary>
    /// Interaction logic for GateInventoryBox.xaml
    /// </summary>
    public partial class GateInventoryBox : UserControl
    {
        public GateInventoryBox()
        {
            InitializeComponent();
        }

        public GateInventoryBox(LogicGate gate, int column, int row)
        {
            InitializeComponent();
            Grid.SetColumn(this, column);
            Grid.SetRow(this, row);
            titleLabel.Content = gate.Name;
            gateContainer.Children.Add(new GateObject(gate));
        }
    }
}
