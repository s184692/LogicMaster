using LogicMaster.gameplay;
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
    /// Interaction logic for LogicContainer.xaml
    /// </summary>
    public partial class LogicContainer : UserControl
    {
        public LogicElement? logicElement { get; private set; }

        private List<LogicContainer> inputLogicContainers = new List<LogicContainer>();

        private List<LogicContainer> outputLogicContainers = new List<LogicContainer>();

        private ImageSource? previous = null;

        public LogicContainer()
        {
            InitializeComponent();
            AllowDrop = true;
        }

        public LogicContainer(LogicElement element)
        {
            InitializeComponent();
            logicElement = element;
            AllowDrop = false;
            switch(logicElement)
            {
                case LogicGate logicGate:
                    imageContainer.Source = new BitmapImage(logicGate.ImageURI);
                    break;
                case LogicSource logicSource:
                    objectContainer.Children.Add(new SourceObject(logicSource));
                    break;
                case LogicTarget logicTarget:
                    objectContainer.Children.Add(new TargetObject(logicTarget));
                    break;
                default:
                    break;
            }
        }

        public void ConnectTo(LogicContainer container, bool asInput = true)
        {
            if (asInput)
            {
                this.outputLogicContainers.Add(container);
                container.inputLogicContainers.Add(this);
            }
            else
            {
                this.inputLogicContainers.Add(container);
                container.outputLogicContainers.Add(this);
            }
        }

        private void SetAndAttachLogicElement(LogicElement element)
        {
            logicElement = element;
            foreach (LogicContainer lc in inputLogicContainers)
            {
                if (lc.logicElement != null)
                {
                    lc.logicElement.AttachTo(logicElement);
                }
            }
            foreach (LogicContainer lc in outputLogicContainers)
            {
                if (lc.logicElement != null)
                {
                    logicElement.AttachTo(lc.logicElement);
                }
            }
        }

        private void DetachAndRemoveLogicElement()
        {
            if (logicElement != null)
            {
                logicElement.DetachAll();
                logicElement = null;
            }
        }

        protected override void OnDrop(DragEventArgs e)
        {
            base.OnDrop(e);

            if (e.Data.GetDataPresent("Type"))
            {
                if ((Type)e.Data.GetData("Type") == typeof(GateObject))
                {
                    LogicGate logicGate = (LogicGate)e.Data.GetData("LogicGate");
                    DetachAndRemoveLogicElement();
                    SetAndAttachLogicElement(logicGate);

                    imageContainer.Source = new BitmapImage(logicGate.ImageURI);
                    imageContainer.Opacity = 1.0;
                    e.Effects = DragDropEffects.Copy;
                }
            }

            e.Handled = true;
        }

        protected override void OnDragEnter(DragEventArgs e)
        {
            base.OnDragEnter(e);

            previous = imageContainer.Source;

            if (e.Data.GetDataPresent("Type"))
            {
                if ((Type)e.Data.GetData("Type") == typeof(GateObject))
                {
                    LogicGate logicGate = (LogicGate)e.Data.GetData("LogicGate");
                    imageContainer.Source = new BitmapImage(logicGate.ImageURI);
                    imageContainer.Opacity = 0.5;
                    e.Effects = DragDropEffects.Copy;
                }
            }

            e.Handled = true;
        }

        protected override void OnDragLeave(DragEventArgs e)
        {
            base.OnDragLeave(e);

            imageContainer.Source = previous;
            imageContainer.Opacity = 1.0;
            previous = null;
        }
    }
}
