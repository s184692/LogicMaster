using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicMaster.gameplay.gates
{
    public class OR : LogicGate
    {
        public override string Name
        {
            get
            {
                return "OR";
            }
        }

        public override bool[] TruthTable
        {
            get
            {
                return new bool[] { false, true, true, true };
            }
        }

        public override Uri ImageURI
        {
            get
            {
                return new Uri("/resources/images/or.png", UriKind.Relative);
            }
        }
    }
}
