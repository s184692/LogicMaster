using LogicMaster.gui.controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicMaster.gameplay
{
    public abstract class LogicElement
    {
        public abstract bool State { get; }
        public abstract void HandleSignal();
        public abstract void AttachTo(LogicElement element);
        public abstract bool HandleAttachment(LogicElement element);
        public abstract void DetachFrom(LogicElement element);
        public abstract void HandleDetachment(LogicElement element);
        public abstract void DetachAll();
    }
}
