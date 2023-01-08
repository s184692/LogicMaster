using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;

namespace LogicMaster.gameplay.logic
{
    public class LogicSource : LogicElement
    {
        private static readonly SolidColorBrush inactiveSourceColor = (SolidColorBrush)Application.Current.FindResource("SourceInactive");

        private static readonly SolidColorBrush activeSourceColor = (SolidColorBrush)Application.Current.FindResource("SourceActive");

        private List<LogicElement> outputElements = new List<LogicElement>();

        public SolidColorBrush ActiveColor
        {
            get
            {
                if (State)
                {
                    return activeSourceColor;
                }
                else
                {
                    return inactiveSourceColor;
                }
            }
        }

        public override void HandleSignal()
        {
            foreach (LogicElement logicElement in outputElements)
            {
                logicElement.HandleSignal();
            }
        }

        public override void AttachTo(LogicElement element)
        {
            if (element.HandleAttachment(this))
            {
                outputElements.Add(element);
                element.HandleSignal();
            }
        }

        public override bool HandleAttachment(LogicElement element)
        {
            return false;
        }

        public override void DetachFrom(LogicElement element)
        {
            element.HandleAttachment(this);
            outputElements.Remove(element);
        }

        public override void HandleDetachment(LogicElement element)
        {
            outputElements.Remove(element);
        }

        public override void DetachAll()
        {
            for (int i = 0; i < outputElements.Count; i++)
                DetachFrom(outputElements[i]);
        }

        public LogicSource(bool active)
        {
            State = active;
        }
    }
}
