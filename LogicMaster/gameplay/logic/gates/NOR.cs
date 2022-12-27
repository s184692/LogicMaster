using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LogicMaster.gameplay.logic;

namespace LogicMaster.gameplay.logic.gates
{
    public class NOR : LogicGate
    {
        public override string Name
        {
            get
            {
                return "NOR";
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
                return new Uri("/resources/images/nor.png", UriKind.Relative);
            }
        }
    }
}
