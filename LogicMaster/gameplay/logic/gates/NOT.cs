﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LogicMaster.gameplay.logic;

namespace LogicMaster.gameplay.logic.gates
{
    public class NOT : LogicGate
    {
        public override string Name
        {
            get
            {
                return "NOT";
            }
        }

        public override bool[] TruthTable
        {
            get
            {
                return new bool[] { true, false };
            }
        }

        public override Uri ImageURI
        {
            get
            {
                return new Uri("/resources/images/not.png", UriKind.Relative);
            }
        }
    }
}