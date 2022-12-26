using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace LogicMaster.gameplay
{
    public class LogicTarget : LogicElement, INotifyPropertyChanged
    {
        private bool active = false;

        private LogicElement? inputElement = null;

        public event PropertyChangedEventHandler? PropertyChanged;

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
                    return new SolidColorBrush(Color.FromRgb(54, 54, 54));
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
            // sends notification to change color
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs("ActiveColor"));
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

        public override void DetachFrom(LogicElement element)
        {
            if (element == inputElement)
            {
                element.HandleDetachment(this);
                inputElement = null;
            }
        }

        public override void HandleDetachment(LogicElement element)
        {
            if (element == inputElement)
            {
                inputElement = null;
            }
        }

        public override void DetachAll()
        {
            if (inputElement != null)
            {
                DetachFrom(inputElement);
            }
        }

        public LogicTarget()
        {
            HandleSignal();
        }
    }
}
