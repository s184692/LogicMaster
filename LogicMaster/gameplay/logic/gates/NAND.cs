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
    /// Klasa bramki NAND
    /// </summary>
    public class NAND : LogicGate
    {
        private static readonly ImageSource _imageSource = ((Image)Application.Current.FindResource("NANDImage")).Source;

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
                return new bool[] { true, true, true, false };
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
