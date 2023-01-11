using LogicMaster.gameplay.logic;
using LogicMaster.gui.dialog;
using LogicMaster.gui.pages;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Media;
using System.Numerics;
using System.Text;
using System.Text.RegularExpressions;
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
    /// Interaction logic for GateInventoryBox.xaml
    /// </summary>
    public partial class GateInventoryBox : UserControl, INotifyPropertyChanged, IGateDragAndDrop
    {
        private static readonly SoundPlayer clickSound = new SoundPlayer(@"resources/sounds/button_click.wav");

        private bool[]? TruthTable { get; set; } = null;

        private int InputCount
        {
            get
            {
                return BitOperations.Log2((uint)TruthTable.Length);
            }
        }

        private bool[][] Inputs
        {
            get
            {
                bool[][] inputs = new bool[TruthTable.Length][];

                for (int i = 0; i < TruthTable.Length; i++)
                {
                    inputs[i] = new bool[InputCount];
                    for (int j = 0; j < InputCount; j++)
                    {
                        inputs[i][InputCount - j - 1] = ((i >> j) & 1) > 0;
                    }
                }

                return inputs;
            }
        }

        private string Title { get; set; } = "";

        private Type gateType { get; set; }
        /// <summary>
        /// Konstruktor okna wyposazenia gracza
        /// </summary>
        /// <param name="gate">rodzaj bramki</param>
        /// <param name="column">numer kolumny</param>
        /// <param name="row">numer wiersza</param>
        public GateInventoryBox(LogicGate gate, int column, int row)
        {
            InitializeComponent();
            Grid.SetColumn(this, column);
            Grid.SetRow(this, row);

            gateType = gate.GetType();
            Title = gate.Name;
            TruthTable = gate.TruthTable;
            titleLabel.Content = Title;
            gateContainer.Children.Add(new GateObject(this, gate, true));
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        private void infoButton_Click(object sender, RoutedEventArgs e)
        {
            clickSound.Play();

            if (TruthTable != null)
            {
                MainWindow? mainWindow = App.Current.MainWindow as MainWindow;
                if (mainWindow != null)
                {
                    GateInfoWindow window = new GateInfoWindow(Title, InputCount, Inputs, TruthTable);
                    window.Owner = mainWindow;
                    mainWindow.contentOverlay.Opacity = 0.5;
                    window.ShowDialog();
                    mainWindow.contentOverlay.Opacity = 0.0;
                }
            }
        }

        void IGateDragAndDrop.HandleDragLeave(GateObject source)
        {
            gateContainer.Children.Remove(source);
        }

        void IGateDragAndDrop.HandleDragCancel(GateObject source)
        {
            gateContainer.Children.Add(source);
        }

        void IGateDragAndDrop.HandleDragSuccess(GateObject source)
        {
            Type sourceGateType = source.logicGate.GetType();
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(sourceGateType.Name));
        }
        /// <summary>
        /// Funkcja zmieniajaca licznik ilosci elementow w slocie
        /// </summary>
        /// <param name="amount">zmienna okreslajaca pozostałą ilość bramek</param>
        public void SetAmountLabel(int amount)
        {
            amountLabel.Content = $"{amount}x";
            if (amount <= 0)
                gateContainer.Children.Clear();
            else if (gateContainer.Children.Count == 0)
                    gateContainer.Children.Add(new GateObject(this, Activator.CreateInstance(gateType) as LogicGate, true));
        }
    }
}
