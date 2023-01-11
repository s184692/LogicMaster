using LogicMaster.gameplay.logic;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Media;
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
    public partial class LogicContainer : UserControl, INotifyPropertyChanged, IGateDragAndDrop
    {
        private static readonly SoundPlayer dropSound = new SoundPlayer(@"resources/sounds/gate_drop.wav");

        public event PropertyChangedEventHandler? PropertyChanged;

        public LogicElement? logicElement { get; private set; } = null;

        public List<LogicContainer> inputLogicContainers { get; private set; } = new List<LogicContainer>();

        public List<LogicContainer> outputLogicContainers { get; private set; } = new List<LogicContainer>();

        private ImageSource? previous { get; set; } = null;

        public LogicContainer()
        {
            InitializeComponent();
            AllowDrop = true;
        }
        /// <summary>
        /// Konstruktor slotów do elementow logicznych
        /// </summary>
        /// <param name="element">element logiczny</param>
        public LogicContainer(LogicElement element)
        {
            InitializeComponent();
            SetAndAttachLogicElement(element);
        }

        private void LogicElement_PropertyChanged(object? sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "State" && logicElement != null)
            {
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(e.PropertyName));
            }
        }
        /// <summary>
        /// Funkcja przylaczajaca element
        /// </summary>
        /// <param name="container">miejsce na element</param>
        /// <param name="asInput">czy jest to wejscie</param>
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
        /// <summary>
        /// Funkcja umieszczającaelement logiczny w kontenerze
        /// </summary>
        /// <param name="element">jaki element logiczny</param>
        /// <param name="allowDrop">zmienna opisujaca czy mozna upuscic element</param>
        /// <param name="draggable">zmienna opisujaca czy element mozna przemiescic</param>
        public void SetAndAttachLogicElement(LogicElement element, bool allowDrop = false, bool draggable = false)
        {
            logicElement = element;
            logicElement.PropertyChanged += LogicElement_PropertyChanged;
            AllowDrop = allowDrop;
            mainBorder.Background = !allowDrop && !draggable ? (SolidColorBrush)App.Current.FindResource("Layer2") : mainBorder.Background;
            // attach logicelement to other elements
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
            // draw object
            switch (logicElement)
            {
                case LogicGate logicGate:
                    objectContainer.Children.Add(new GateObject(this, logicGate, draggable));
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
        /// <summary>
        /// Funkcja usuwajaca element z poprzedniego polozenia
        /// </summary>
        private void DetachAndRemoveLogicElement()
        {
            if (logicElement != null)
            {
                logicElement.DetachAll();
                objectContainer.Children.Clear();
                logicElement.PropertyChanged -= LogicElement_PropertyChanged;
                logicElement = null;
                foreach (LogicContainer lc in outputLogicContainers)
                    if (lc.logicElement != null)
                        lc.logicElement.HandleSignal();
            }
        }

        protected override void OnDrop(DragEventArgs e)
        {
            base.OnDrop(e);

            if (e.Data.GetDataPresent("Type"))
            {
                if ((Type)e.Data.GetData("Type") == typeof(GateObject))
                {
                    dropSound.Play();

                    LogicGate logicGate = (LogicGate)e.Data.GetData("LogicGate");
                    DetachAndRemoveLogicElement();
                    SetAndAttachLogicElement(logicGate, false, true);

                    previous = null;
                    imageContainer.Source = null;
                    imageContainer.Opacity = 1.0;
                    e.Effects = DragDropEffects.Move;
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
                    imageContainer.Source = logicGate.GateImageSource;
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

        void IGateDragAndDrop.HandleDragLeave(GateObject source)
        {
            DetachAndRemoveLogicElement();
            AllowDrop = true;
        }

        void IGateDragAndDrop.HandleDragCancel(GateObject source)
        {
            PropertyChanged?.Invoke(source, new PropertyChangedEventArgs("Parent"));
        }

        void IGateDragAndDrop.HandleDragSuccess(GateObject source)
        {
            return;
        }
    }
}
