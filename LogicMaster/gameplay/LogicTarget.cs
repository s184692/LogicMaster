using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicMaster.gameplay
{
    internal class LogicTarget : LogicElement
    {
        private bool active;

        private LogicElement? inputElement;

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
