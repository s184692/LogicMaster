using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicMaster.gameplay
{
    internal class LogicSource : LogicElement
    {
        private readonly bool active;

        private LogicElement? outputElement;

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

        public LogicSource(bool active)
        {
            this.active = active;
            this.outputElement = null;
        }
    }
}
