using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace LogicMaster.gameplay
{
    public class LogicTarget : LogicElement
    {
        private bool active;

        private LogicElement? inputElement;

        public SolidColorBrush ActiveColor
        {
            get
            {
                if (active)
                {
                    return new SolidColorBrush(Color.FromRgb(255, 255, 0));
                }
                else
                {
                    return new SolidColorBrush(Color.FromRgb(0, 0, 0));
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
            if (inputElement != null)
            {
                active = inputElement.State;
            }
            else
            {
                active = false;
            }
        }

        public override void AttachTo(LogicElement element)
        {
            return;
        }

        public override bool HandleAttachment(LogicElement element)
        {
            if (inputElement == null)
            {
                inputElement = element;
                return true;
            }
            else
            {
                return false;
            }
        }

        public LogicTarget()
        {
            this.inputElement = null;
            HandleSignal();
        }
    }
}
