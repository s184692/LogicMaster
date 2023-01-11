using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicMaster.gui.controls
{
    /// <summary>
    /// interfejs dla przeciagania i upuszczania bramek
    /// </summary>
    public interface IGateDragAndDrop
    {
        /// <summary>
        /// Funkcja wykonywana gdy bramka zaczyna byc przesuwana
        /// </summary>
        /// <param name="source">obiekt klasy opisujacej przesuwana bramke</param>
        void HandleDragLeave(GateObject source); // when GateObject starts draganddrop
        /// <summary>
        /// Funkcja wykonywana gdy bramka jest upuszczona w niedozwolone miejsce
        /// </summary>
        /// <param name="source">obiekt klasy opisujacej przesuwana bramke</param>
        void HandleDragCancel(GateObject source); // when GateObject is dropped in invalid spot
        /// <summary>
        /// Funkcja wykonywana gdy bramka zostaje upuszczona w dozwolone miejsce
        /// </summary>
        /// <param name="source">obiekt klasy opisujacej przesuwana bramke</param>
        void HandleDragSuccess(GateObject source); // when GateObject is dropped succesfully
    }
}
