using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using LogicMaster.gameplay.logic;

namespace LogicMaster.gameplay.logic.gates
{
    public class OR : LogicGate
    {
        private static readonly ImageSource _imageSource = ((Image)Application.Current.FindResource("ORImage")).Source;

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

        public override ImageSource GateImageSource
        {
            get
            {
                return _imageSource;
            }
        }
    }
}
