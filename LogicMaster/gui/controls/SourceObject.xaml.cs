using LogicMaster.gameplay.logic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace LogicMaster.gui.controls
{
    /// <summary>
    /// Interaction logic for SourceObject.xaml
    /// </summary>
    public partial class SourceObject : UserControl
    {
        public LogicSource? logicSource { get; set; }

        public SourceObject()
        {
            InitializeComponent();
            logicSource = null;
        }

        public SourceObject(LogicSource source)
        {
            InitializeComponent();
            logicSource = source;
            sourceLight.Fill = source.ActiveColor;
        }
    }
}
