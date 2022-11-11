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
    /// Interaction logic for GateContainer.xaml
    /// </summary>
    public partial class GateContainer : UserControl
    {
        private ImageSource? _previous = null;
        public GateContainer()
        {
            InitializeComponent();
        }

        protected override void OnDrop(DragEventArgs e)
        {
            base.OnDrop(e);

            if (e.Data.GetDataPresent("Type"))
            {
                if ((Type)e.Data.GetData("Type") == typeof(GateObject))
                {
                    gateImageContainer.Source = (ImageSource)e.Data.GetData("Source");
                    gateImageContainer.Opacity = 1.0;
                    e.Effects = DragDropEffects.Copy;
                }
            }

            e.Handled = true;
        }

        protected override void OnDragEnter(DragEventArgs e)
        {
            base.OnDragEnter(e);

            _previous = gateImageContainer.Source;

            if (e.Data.GetDataPresent("Type"))
            {
                if ((Type)e.Data.GetData("Type") == typeof(GateObject))
                {
                    gateImageContainer.Source = (ImageSource)e.Data.GetData("Source");
                    gateImageContainer.Opacity = 0.5;
                    e.Effects = DragDropEffects.Copy;
                }
            }

            e.Handled = true;
        }

        protected override void OnDragLeave(DragEventArgs e)
        {
            base.OnDragLeave(e);

            gateImageContainer.Source = _previous;
            gateImageContainer.Opacity = 1.0;
            _previous = null;
        }
    }
}
