using LogicMaster.gameplay.logic;
using System;
using System.Collections.Generic;
using System.Diagnostics;
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
        private IGateDragAndDrop _parent { get; set; }

        public LogicGate? logicGate { get; private set; } = null;

        public bool Draggable { get; private set; } = false;

        public GateObject(IGateDragAndDrop parent, LogicGate gate, bool draggable = false)
        {
            InitializeComponent();
            _parent = parent;
            Draggable = draggable;
            logicGate = gate;
            gateImage.Source = gate.GateImageSource;
        }

        protected override void OnMouseMove(MouseEventArgs e)
        {
            base.OnMouseMove(e);
            if (Draggable && e.LeftButton == MouseButtonState.Pressed)
            {
                DataObject data = new DataObject();
                data.SetData("LogicGate", logicGate);
                data.SetData("Type", this.GetType());

                _parent.HandleDragLeave(this);
                if (DragDrop.DoDragDrop(this, data, DragDropEffects.Move) == DragDropEffects.None)
                    _parent.HandleDragCancel(this);
                else
                    _parent.HandleDragSuccess(this);
            }
        }
    }
}
