using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicMaster.gameplay.gates
{
    internal class AND : LogicGate
    {
        public override string Name 
        { 
            get 
            { 
                return "AND";
            } 
        }

        public override bool[] TruthTable {
            get
            {
                return new bool[] { false, false, false, true };
            }
        }

        public override Uri ImageURI
        {
            get
            {
                return new Uri("/resources/images/and.png");
            }
        }
    }
}
