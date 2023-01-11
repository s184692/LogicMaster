using System;
using System.Collections.Generic;
using System.Linq;
using System.Media;
using System.Reflection;
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
    /// Interaction logic for GateInfoWindow.xaml
    /// </summary>
    public partial class GateInfoWindow : Window
    {
        private static readonly SoundPlayer clickSound = new SoundPlayer(@"resources/sounds/button_click.wav");
        
        public GateInfoWindow()
        {
            InitializeComponent();
        }
        /// <summary>
        /// Konstruktor okna z informacjami o bramce
        /// </summary>
        /// <param name="title">nazwa</param>
        /// <param name="inputCount">ilosc wejsc</param>
        /// <param name="inputs">stan wejscia</param>
        /// <param name="truthTable">tablica prawdy</param>
        public GateInfoWindow(string title, int inputCount, bool[][] inputs, bool[] truthTable)
        {
            InitializeComponent();
            AddTitleRow(title, inputCount);
            AddTableHeader(inputCount);
            for (int i = 0; i < truthTable.Length; i++)
                AddTableRow(inputs[i], truthTable[i]);
        }
        /// <summary>
        /// Funkcja dodajaca wiersz z tytulem
        /// </summary>
        /// <param name="title">nazwa</param>
        /// <param name="inputCount">ilosc wejsc</param>
        protected void AddTitleRow(string title, int inputCount)
        {
            Grid titleRow = new Grid();

            titleRow.Height = 50;
            titleRow.HorizontalAlignment = HorizontalAlignment.Stretch;
            titleRow.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(inputCount, GridUnitType.Star) });
            titleRow.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(1, GridUnitType.Star) });
            titleRow.RowDefinitions.Add(new RowDefinition() { Height = new GridLength(1, GridUnitType.Star) });

            Label titleLabel = new Label() 
            { 
                Content = title,
                Style = (Style)Application.Current.FindResource("StandardLabel"),
                FontSize = 36,
                FontWeight = FontWeights.Bold
            };
            titleLabel.VerticalAlignment = VerticalAlignment.Center;
            titleLabel.HorizontalAlignment = HorizontalAlignment.Center;
            Grid.SetColumn(titleLabel, 0);
            Grid.SetRow(titleLabel, 0);

            Button confirmButton = new Button()
            {
                Content = new Image() { Source = ((Image)Application.Current.FindResource("ExitButtonImage")).Source },
                Style = (Style)Application.Current.FindResource("StandardButton")
            };

            confirmButton.Click += ConfirmButton_Click;
            Grid.SetColumn(confirmButton, 1);
            Grid.SetRow(confirmButton, 0);

            titleRow.Children.Add(titleLabel);
            titleRow.Children.Add(confirmButton);

            Border border = new Border()
            {
                BorderBrush = (SolidColorBrush)Application.Current.FindResource("Layer1"),
                BorderThickness = new Thickness(0, 0, 0, 4),
                Child = titleRow,
                CornerRadius = new CornerRadius(8, 8, 0, 0),
                Background = (SolidColorBrush)Application.Current.FindResource("Layer2")
            };

            contentStackPanel.Children.Add(border);
        }
        /// <summary>
        /// Funkcja dodajaca naglowek IN i OUT do tabelki
        /// </summary>
        /// <param name="inputCount">ilosc wejsc</param>
        private void AddTableHeader(int inputCount)
        {
            Grid headerRow = new Grid();

            headerRow.Height = 50;
            headerRow.HorizontalAlignment = HorizontalAlignment.Stretch;
            headerRow.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(inputCount, GridUnitType.Star) });
            headerRow.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(1, GridUnitType.Star) });
            headerRow.RowDefinitions.Add(new RowDefinition() { Height = new GridLength(1, GridUnitType.Star) });

            Label inputLabel = new Label()
            {
                Content = "IN",
                Style = (Style)Application.Current.FindResource("StandardLabel"),
                FontSize = 36
            };
            inputLabel.VerticalAlignment = VerticalAlignment.Center;
            inputLabel.HorizontalAlignment = HorizontalAlignment.Center;
            Grid.SetColumn(inputLabel, 0);
            Grid.SetRow(inputLabel, 0);

            Label outputLabel = new Label()
            { 
                Content = "OUT",
                Style = (Style)Application.Current.FindResource("StandardLabel"),
                FontSize = 36
            };
            outputLabel.VerticalAlignment = VerticalAlignment.Center;
            outputLabel.HorizontalAlignment = HorizontalAlignment.Center;

            Border outputBorder = new Border()
            {
                BorderBrush = (SolidColorBrush)Application.Current.FindResource("Layer1"),
                BorderThickness = new Thickness(4, 0, 0, 0),
                Child = outputLabel
            };
            Grid.SetColumn(outputBorder, 1);
            Grid.SetRow(outputBorder, 0);

            headerRow.Children.Add(inputLabel);
            headerRow.Children.Add(outputBorder);

            Border border = new Border()
            {
                BorderBrush = (SolidColorBrush)Application.Current.FindResource("Layer1"),
                BorderThickness = new Thickness(0, 0, 0, 4),
                Child = headerRow
            };

            contentStackPanel.Children.Add(border);
        }
        /// <summary>
        /// Funkcja dodajaca wiersze do tabeli
        /// </summary>
        /// <param name="inputs">stan wejscia</param>
        /// <param name="truth">stan wyjscia</param>
        private void AddTableRow(bool[] inputs, bool truth)
        {
            Grid tableRow = new Grid();

            tableRow.Height = 50;
            tableRow.HorizontalAlignment = HorizontalAlignment.Stretch;
            foreach (bool _ in inputs)
                tableRow.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(1, GridUnitType.Star) });
            tableRow.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(1, GridUnitType.Star) });
            tableRow.RowDefinitions.Add(new RowDefinition() { Height = new GridLength(1, GridUnitType.Star) });

            for (int i = 0; i < inputs.Length; i++)
            {
                Label inputLabel = new Label()
                { 
                    Content = inputs[i] ? "1" : "0" ,
                    Style = (Style)Application.Current.FindResource("StandardLabel"),
                    FontSize = 36
                };
                inputLabel.VerticalAlignment = VerticalAlignment.Center;
                inputLabel.HorizontalAlignment = HorizontalAlignment.Center;
                Grid.SetColumn(inputLabel, i);
                Grid.SetRow(inputLabel, 0);
                tableRow.Children.Add(inputLabel);
            }

            Label outputLabel = new Label()
            { 
                Content = truth ? "1" : "0",
                Style = (Style)Application.Current.FindResource("StandardLabel"),
                FontSize = 36
            };
            outputLabel.VerticalAlignment = VerticalAlignment.Center;
            outputLabel.HorizontalAlignment = HorizontalAlignment.Center;

            Border outputBorder = new Border()
            {
                BorderBrush = (SolidColorBrush)Application.Current.FindResource("Layer1"),
                BorderThickness = new Thickness(4, 0, 0, 0),
                Child = outputLabel
            };
            Grid.SetColumn(outputBorder, inputs.Length);
            Grid.SetRow(outputBorder, 0);

            tableRow.Children.Add(outputBorder);

            contentStackPanel.Children.Add(tableRow);
        }

        private void ConfirmButton_Click(object sender, RoutedEventArgs e)
        {
            clickSound.Play();
            DialogResult = true;
        }
    }
}
