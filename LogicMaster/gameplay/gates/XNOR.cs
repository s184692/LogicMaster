using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicMaster.gameplay.gates
{
    internal class XNOR : LogicGate
    {
        public override string Name
        {
            get
            {
                return "XNOR";
            }
        }

        public override bool[] TruthTable
        {
            get
            {
                return new bool[] { true, false, false, true };
            }
        }

        public override Uri ImageURI
        {
            get
            {
                return new Uri("/resources/images/xnor.png");
            }
        }
    }
}
