using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Numerics;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;
using LogicMaster.gui.controls;

namespace LogicMaster.gameplay.visuals
{
    public class GameCanvas : Canvas
    {
        private static readonly SolidColorBrush inactiveWireColor = (SolidColorBrush)Application.Current.FindResource("WireInactive");

        private static readonly SolidColorBrush activeWireColor = (SolidColorBrush)Application.Current.FindResource("WireActive");

        private List<(LogicContainer lc, Point pos)> logicContainers { get; set; }

        public int containerSizeModifier { get; set; } = 10;

        private double containerSize
        {
            get
            {
                return Math.Min(ActualHeight, ActualWidth) / (1.5 * containerSizeModifier + 1);
            }
        }

        private double wireThickness
        {
            get
            {
                return containerSize / 10.0;
            }
        }

        public GameCanvas() : base()
        {
            logicContainers = new List<(LogicContainer lc, Point pos)>();
        }

        public void AddContainer(LogicContainer lc, Point pos)
        {
            logicContainers.Add((lc, pos));
            lc.PropertyChanged += AnyLogicContainer_PropertyChanged;
            Children.Add(lc);
        }

        private void SetContainerOnCanvas(LogicContainer lc, Point pos)
        {
            lc.Width = containerSize;
            lc.Height = containerSize;

            SetLeft(lc, pos.X * ActualWidth - (lc.Width / 2));
            SetTop(lc, pos.Y * ActualHeight - (lc.Height / 2));
        }

        private void SetContainerOnCanvas((LogicContainer lc, Point pos) pair)
        {
            SetContainerOnCanvas(pair.lc, pair.pos);
        }

        private Point GetContainerPositionOnCanvas(LogicContainer lc)
        {
            int index = logicContainers.FindIndex(0, logicContainers.Count, e => { return e.lc == lc; });
            Point pos = logicContainers[index].pos;
            return new Point(pos.X * ActualWidth - (containerSize / 2), pos.Y * ActualHeight - (containerSize / 2));
        }

        private void ClearWires()
        {
            for (int i = Children.Count - 1; i >= 0; i--)
            {
                if (Children[i] is Line)
                {
                    Children.RemoveAt(i);
                }
            }
        }

        private Point? GetConnectorPosition(LogicContainer lc, double fraction, bool input)
        {
            Point pos = GetContainerPositionOnCanvas(lc);
            pos.X += fraction * containerSize;
            pos.Y += input ? 0.9 * containerSize : 0.1 * containerSize;
            if (double.IsNaN(pos.X) || double.IsNaN(pos.Y))
            {
                return null;
            }
            else
            {
                return pos;
            }
        }

        private Line GetLine(Point p1, Point p2, double thickness, SolidColorBrush stroke)
        {
            Line line = new Line();

            line.X1 = p1.X;
            line.X2 = p2.X;
            line.Y1 = p1.Y;
            line.Y2 = p2.Y;
            line.StrokeThickness = thickness;
            line.Stroke = stroke;
            line.StrokeStartLineCap = PenLineCap.Round;
            line.StrokeEndLineCap = PenLineCap.Round;
            line.SnapsToDevicePixels = true;
            SetZIndex(line, -1);

            return line;
        }

        private void DrawWire(Point src, Point dst, bool active, double breakpoint = 0.5)
        {
            // if its a straight line
            if (src.X == dst.X || src.Y == dst.Y)
            {
                Children.Add(GetLine(src, dst, wireThickness, active ? activeWireColor : inactiveWireColor));
            }
            else
            {
                double middlepoint = Math.Min(src.Y, dst.Y) +  Math.Abs(src.Y - dst.Y) * breakpoint;
                Point srcBreak = new Point(src.X, middlepoint);
                Point dstExtra = new Point(dst.X, middlepoint);

                Children.Add(GetLine(src, srcBreak, wireThickness, active ? activeWireColor : inactiveWireColor));
                Children.Add(GetLine(srcBreak, dstExtra, wireThickness, active ? activeWireColor : inactiveWireColor));
                Children.Add(GetLine(dstExtra, dst, wireThickness, active ? activeWireColor : inactiveWireColor));

            }
        }

        private void DrawContainerOutputWires(LogicContainer sourceContainer)
        {
            bool sourceState = sourceContainer.logicElement != null ? sourceContainer.logicElement.State : false;
            int outputCount = sourceContainer.outputLogicContainers.Count;
            for (int outputIndex = 0; outputIndex < outputCount; outputIndex++)
            {
                int inputIndex = 0;
                LogicContainer targetContainer = sourceContainer.outputLogicContainers[outputIndex];
                int inputCount = targetContainer.inputLogicContainers.Count;
                // find connector slot index of target container
                for (; inputIndex < inputCount; inputIndex++)
                {
                    if (targetContainer.inputLogicContainers[inputIndex] == sourceContainer)
                    {
                        break;
                    }
                }
                // get connector positions
                Point? src = GetConnectorPosition(sourceContainer, (double)(outputIndex + 1) / (double)(outputCount + 1), false);
                Point? dst = GetConnectorPosition(targetContainer, (double)(inputIndex + 1) / (double)(inputCount + 1), true);
                if (src.HasValue && dst.HasValue)
                {
                    DrawWire(src.Value, dst.Value, sourceState, 1 - (double)(outputIndex + 1) / (double)(outputCount + 1));
                }
            }
        }

        private void DrawAllContainersWires()
        {
            foreach ((LogicContainer lc, Point pos) pair in logicContainers)
            {
                DrawContainerOutputWires(pair.lc);
            }
        }

        protected override void OnRenderSizeChanged(SizeChangedInfo sizeInfo)
        {
            base.OnRenderSizeChanged(sizeInfo);

            UpdateVisuals();
        }

        private void AnyLogicContainer_PropertyChanged(object? sender, PropertyChangedEventArgs e)
        {
            ClearWires();
            DrawAllContainersWires();
        }

        public void UpdateVisuals()
        {
            ClearWires();
            DrawAllContainersWires();
            foreach ((LogicContainer lc, Point pos) pair in logicContainers)
                SetContainerOnCanvas(pair);
        }

        public void ClearAllData()
        {
            ClearWires();
            Children.Clear();
            foreach (var (lc, pos) in logicContainers)
                lc.PropertyChanged -= AnyLogicContainer_PropertyChanged;
            logicContainers.Clear();
        }
    }
}
