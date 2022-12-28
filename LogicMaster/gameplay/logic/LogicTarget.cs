using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;

namespace LogicMaster.gameplay.logic
{
    public class LogicTarget : LogicElement
    {
        private static readonly SolidColorBrush inactiveTargetColor = (SolidColorBrush)Application.Current.FindResource("TargetInactive");

        private static readonly SolidColorBrush activeTargetColor = (SolidColorBrush)Application.Current.FindResource("TargetActive");

        private LogicElement? inputElement = null;

        public SolidColorBrush ActiveColor
        {
            get
            {
                if (State)
                {
                    return activeTargetColor;
                }
                else
                {
                    return inactiveTargetColor;
                }
            }
        }

        public override void HandleSignal()
        {
            if (inputElement != null)
            {
                State = inputElement.State;
            }
            else
            {
                State = false;
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
