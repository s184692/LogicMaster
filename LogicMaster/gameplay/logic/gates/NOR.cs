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
    /// <summary>
    /// Klasa bramki NOR
    /// </summary>
    public class NOR : LogicGate
    {
        private static readonly ImageSource _imageSource = ((Image)Application.Current.FindResource("NORImage")).Source;

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

        public override ImageSource GateImageSource
        {
            get
            {
                return _imageSource;
            }
        }
    }
}
