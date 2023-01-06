using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicMaster.gui.controls
{
    public interface IGateDragAndDrop
    {
        void HandleDragLeave(GateObject source); // when GateObject starts draganddrop
        void HandleDragCancel(GateObject source); // when GateObject is dropped in invalid spot
        void HandleDragSuccess(GateObject source); // when GateObject is dropped succesfully
    }
}
