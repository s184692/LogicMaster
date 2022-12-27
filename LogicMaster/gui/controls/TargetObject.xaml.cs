using LogicMaster.gameplay.logic;
using System;
using System.Collections.Generic;
using System.ComponentModel;
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
    /// Interaction logic for TargetObject.xaml
    /// </summary>
    public partial class TargetObject : UserControl
    {
        private LogicTarget? logicTarget = null;

        public TargetObject()
        {
            InitializeComponent();
        }

        public TargetObject(LogicTarget target)
        {
            InitializeComponent();
            logicTarget = target;
            logicTarget.PropertyChanged += LogicTarget_PropertyChanged;
            targetLight.Fill = target.ActiveColor;
        }

        private void LogicTarget_PropertyChanged(object? sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "ActiveColor" && logicTarget != null)
            {
                targetLight.Fill = logicTarget.ActiveColor;
            }
        }
    }
}
