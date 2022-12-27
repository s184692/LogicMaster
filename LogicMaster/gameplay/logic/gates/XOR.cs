using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LogicMaster.gameplay.logic;

namespace LogicMaster.gameplay.logic.gates
{
    public class XOR : LogicGate
    {
        public override string Name
        {
            get
            {
                return "XOR";
            }
        }

        public override bool[] TruthTable
        {
            get
            {
                return new bool[] { false, true, true, false };
            }
        }

        public override Uri ImageURI
        {
            get
            {
                return new Uri("/resources/images/xor.png", UriKind.Relative);
            }
        }
    }
}
