using LogicMaster.gui.controls;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace LogicMaster.gameplay.logic
{
    public abstract class LogicElement : INotifyPropertyChanged
    {
        protected bool _state;

        public abstract void HandleSignal();

        public abstract void AttachTo(LogicElement element);

        public abstract bool HandleAttachment(LogicElement element);

        public abstract void DetachFrom(LogicElement element);

        public abstract void HandleDetachment(LogicElement element);

        public abstract void DetachAll();

        public event PropertyChangedEventHandler? PropertyChanged;

        public bool State
        {
            get
            {
                return _state;
            }
            protected set
            {
                _state = value;
                OnPropertyChanged();
            }
        }

        protected void OnPropertyChanged([CallerMemberName] string? name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }
}
