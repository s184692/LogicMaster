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
    /// Interaction logic for GateObject.xaml
    /// </summary>
    public partial class GateObject : UserControl
    {
        private LogicGate? logicGate = null;

        public GateObject()
        {
            InitializeComponent();
        }

        public GateObject(LogicGate gate)
        {
            InitializeComponent();
            logicGate = gate;
            gateImage.Source = new BitmapImage(gate.ImageURI);
        }

        protected override void OnMouseMove(MouseEventArgs e)
        {
            base.OnMouseMove(e);
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                DataObject data = new DataObject();
                data.SetData("LogicGate", logicGate);
                data.SetData("Type", this.GetType());

                DragDrop.DoDragDrop(this, data, DragDropEffects.Move);
            }
        }
    }
}
