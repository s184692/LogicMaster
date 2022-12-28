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

        private LogicElement? outputElement = null;

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
            if (outputElement != null)
            {
                outputElement.HandleSignal();
            }
        }

        public override void AttachTo(LogicElement element)
        {
            if (outputElement == null)
            {
                if (element.HandleAttachment(this))
                {
                    outputElement = element;
                    outputElement.HandleSignal();
                }
            }
        }

        public override bool HandleAttachment(LogicElement element)
        {
            return false;
        }

        public override void DetachFrom(LogicElement element)
        {
            if (element == outputElement)
            {
                element.HandleDetachment(this);
                outputElement = null;
            }
        }

        public override void HandleDetachment(LogicElement element)
        {
            if (element == outputElement)
            {
                outputElement = null;
            }
        }

        public override void DetachAll()
        {
            if (outputElement != null)
            {
                DetachFrom(outputElement);
            }
        }

        public LogicSource(bool active)
        {
            State = active;
        }
    }
}
