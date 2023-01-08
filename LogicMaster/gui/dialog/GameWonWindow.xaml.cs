using System;
using System.Collections.Generic;
using System.Linq;
using System.Media;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace LogicMaster.gui.dialog
{
    /// <summary>
    /// Interaction logic for GameWonWindow.xaml
    /// </summary>
    public partial class GameWonWindow : Window
    {
        private static readonly SoundPlayer clickSound = new SoundPlayer(@"resources/sounds/button_click.wav");

        public GameWonWindow(int time)
        {
            InitializeComponent();
            timeLabel.Content = $"In {(time / 60) % 100} minutes {time % 60} seconds";
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            clickSound.Play();
            DialogResult = true;
        }
    }
}
