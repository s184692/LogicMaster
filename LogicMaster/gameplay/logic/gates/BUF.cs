using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LogicMaster.gameplay.logic;

namespace LogicMaster.gameplay.logic.gates
{
    public class BUF : LogicGate
    {
        public override string Name
        {
            get
            {
                return "BUF";
            }
        }

        public override bool[] TruthTable
        {
            get
            {
                return new bool[] { false, true };
            }
        }

        public override Uri ImageURI
        {
            get
            {
                return new Uri("/resources/images/buf.png", UriKind.Relative);
            }
        }
    }
}
