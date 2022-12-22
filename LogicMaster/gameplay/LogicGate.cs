using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace LogicMaster.gameplay
{
    internal abstract class LogicGate : LogicElement
    {
        public abstract string Name { get; }

        public abstract bool[] TruthTable { get; }

        public abstract Uri ImageURI { get; }

        protected readonly LogicElement?[] inputElements;

        protected LogicElement? outputElement;

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

        public LogicGate()
        {
            int inputCount = BitOperations.Log2((uint)TruthTable.Length);
            inputElements = new LogicElement[inputCount];
            outputElement = null;
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
