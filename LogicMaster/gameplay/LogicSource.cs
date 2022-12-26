using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;

namespace LogicMaster.gameplay
{
    public class LogicSource : LogicElement
    {
        private readonly bool active;

        private LogicElement? outputElement = null;

        public SolidColorBrush ActiveColor
        {
            get
            {
                if (active)
                {
                    return new SolidColorBrush(Color.FromRgb(0, 255, 0));
                }
                else
                {
                    return new SolidColorBrush(Color.FromRgb(255, 0, 0));
                }
            }
        }

        public override bool State
        {
            get
            {
                return active;
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
            this.active = active;
        }
    }
}
