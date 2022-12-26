using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace LogicMaster.gameplay
{
    public abstract class LogicGate : LogicElement
    {
        public abstract string Name { get; }

        public abstract bool[] TruthTable { get; }

        public abstract Uri ImageURI { get; }

        protected LogicElement?[] inputElements;

        protected List<LogicElement> outputElements = new List<LogicElement>();

        protected bool output;

        public override bool State
        {
            get
            {
                return output;
            }
        }

        public override void HandleSignal()
        {
            output = GetOutputLogic();


            foreach (LogicElement logicElement in outputElements)
            {
                logicElement.HandleSignal();
            }
        }

        public override void AttachTo(LogicElement element)
        {
            if (element.HandleAttachment(this))
            {
                outputElements.Add(element);
                element.HandleSignal();
            }
        }

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
            output = GetOutputLogic();
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
