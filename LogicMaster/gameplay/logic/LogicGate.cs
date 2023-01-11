using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;

namespace LogicMaster.gameplay.logic
{
    /// <summary>
    /// Klasa modelujaca bramki logiczne
    /// </summary>
    public abstract class LogicGate : LogicElement
    {
        public abstract string Name { get; }

        public abstract bool[] TruthTable { get; }

        public abstract ImageSource GateImageSource { get; }

        protected LogicElement?[] inputElements;

        protected List<LogicElement> outputElements = new List<LogicElement>();
        /// <summary>
        /// Funkcja odpowiadajaca za przeslanie stanu do bramki na podstawie poprzedniego
        /// </summary>
        public override void HandleSignal()
        {
            State = GetOutputLogic();

            foreach (LogicElement logicElement in outputElements)
            {
                logicElement.HandleSignal();
            }
        }
        /// <summary>
        /// Funkcja przylaczajaca bramke do inych elementow
        /// </summary>
        /// <param name="element">element do ktorego przylaczamy bramke</param>
        public override void AttachTo(LogicElement element)
        {
            if (element.HandleAttachment(this))
            {
                outputElements.Add(element);
                element.HandleSignal();
            }
        }
        /// <summary>
        /// Okreslanie stanu na wyjsciu po dolaczeniu elementu
        /// </summary>
        /// <param name="element">element ktory dolaczylismy</param>
        /// <returns>stan na wyjsciu elementu</returns>
        public override bool HandleAttachment(LogicElement element)
        {
            for (int i = 0; i < inputElements.Length; i++)
            {
                if (inputElements[i] == null)
                {
                    inputElements[i] = element;
                    return true;
                }
            }
            return false;
        }
        /// <summary>
        /// Funkcja odlaczajaca bramke od elementu
        /// </summary>
        /// <param name="element">element od ktorego odlaczamy bramke</param>
        public override void DetachFrom(LogicElement element)
        {
            element.HandleDetachment(this);
            for (int i = 0; i < inputElements.Length; i++)
            {
                if (inputElements[i] == element)
                {
                    inputElements[i] = null;
                    return;
                }
            }
            outputElements.Remove(element);
        }
        /// <summary>
        /// Funkcja okreslajaca stan na wyjsciu po odlaczeniu elementu
        /// </summary>
        /// <param name="element">element od ktorego odlaczamy bramke</param>
        public override void HandleDetachment(LogicElement element)
        {
            for (int i = 0; i < inputElements.Length; i++)
            {
                if (inputElements[i] == element)
                {
                    inputElements[i] = null;
                    return;
                }
            }
            outputElements.Remove(element);
        }

        public override void DetachAll()
        {
            for (int i = 0; i < inputElements.Length; i++)
            {
                if (inputElements[i] != null)
                {
                    DetachFrom(inputElements[i]);
                }
            }
            for (int i = outputElements.Count - 1; i >= 0; i--)
            {
                DetachFrom(outputElements[i]);
            }
        }
        public LogicGate()
        {
            int inputCount = BitOperations.Log2((uint)TruthTable.Length);
            inputElements = new LogicElement[inputCount];
            State = GetOutputLogic();
        }

        protected bool GetOutputLogic()
        {
            return TruthTable[InputsToBinary()];
        }

        protected int InputsToBinary()
        {
            int ret = 0;

            for (int i = 0; i < inputElements.Length; i++)
            {
                if (inputElements[i] != null && inputElements[i].State == true)
                {
                    ret += 1 << i;
                }
            }

            return ret;
        }
    }
}
