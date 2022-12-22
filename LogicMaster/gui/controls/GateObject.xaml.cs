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
        public GateObject()
        {
            InitializeComponent();
        }

        public GateObject(Image img)
        {
            InitializeComponent();
            gateImage.Source = img.Source;
        }

        public GateObject(GateObject go)
        {
            InitializeComponent();
            gateImage.Source = go.gateImage.Source;
        }

        protected override void OnMouseMove(MouseEventArgs e)
        {
            base.OnMouseMove(e);
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                DataObject data = new DataObject();
                data.SetData("Source", gateImage.Source);
                data.SetData("Type", this.GetType());

                DragDrop.DoDragDrop(this, data, DragDropEffects.Move);
            }
        }
    }
}
