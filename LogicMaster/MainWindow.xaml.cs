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

namespace LogicMaster
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public double windowTargetAspectRatio;
        public double windowInitialMinWidth;
        public double mainGridHeightRatio;
        public MainWindow()
        {
            InitializeComponent();
            windowTargetAspectRatio = 9.0 / 16.0;
            windowInitialMinWidth = MinWidth;
            mainGridHeightRatio = mainGrid.RowDefinitions[1].Height.Value / Height;
        }

        protected override void OnRenderSizeChanged(SizeChangedInfo sizeInfo)
        {
            double targetWidth = sizeInfo.NewSize.Height * windowTargetAspectRatio;
            targetWidth = targetWidth > windowInitialMinWidth ? targetWidth : windowInitialMinWidth;
            MinWidth = targetWidth + 16; //fix zeby nie dalo sie zmniejszyc okna za bardzo
            Width = targetWidth > sizeInfo.NewSize.Width ? targetWidth : sizeInfo.NewSize.Width;
            mainGrid.Width = targetWidth;
            mainGrid.RowDefinitions[1].Height = new GridLength(sizeInfo.NewSize.Height * mainGridHeightRatio);
        }
    }
}
