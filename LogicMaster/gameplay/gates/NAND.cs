using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicMaster.gameplay.gates
{
    public class NAND : LogicGate
    {
        public override string Name
        {
            get
            {
                return "NAND";
            }
        }

        public override bool[] TruthTable
        {
            get
            {
                return new bool[] { true, false, false, false };
            }
        }

        public override Uri ImageURI
        {
            get
            {
                return new Uri("/resources/images/nand.png", UriKind.Relative);
            }
        }
    }
}
