using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows;
using System.Windows.Documents;

namespace LogicMaster.gui.dialog
{
    public class NewGameWindow : GateInfoWindow
    {
        public string Result { get; set; } = "";

        public NewGameWindow(string title, string[] options) : base()
        {
            AddTitleRow(title, 5);
            foreach (string option in options)
                AddOptionRow(option);
        }

        private void AddOptionRow(string option)
        {
            Grid optionRow = new Grid();

            optionRow.Height = 80;
            optionRow.HorizontalAlignment = HorizontalAlignment.Stretch;
            optionRow.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(1, GridUnitType.Star) });
            optionRow.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(8, GridUnitType.Star) });
            optionRow.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(1, GridUnitType.Star) });
            optionRow.RowDefinitions.Add(new RowDefinition() { Height = new GridLength(1, GridUnitType.Star) });
            optionRow.RowDefinitions.Add(new RowDefinition() { Height = new GridLength(4, GridUnitType.Star) });
            optionRow.RowDefinitions.Add(new RowDefinition() { Height = new GridLength(1, GridUnitType.Star) });

            Button optionButton = new Button()
            {
                Content = option,
                Style = (Style)Application.Current.FindResource("MainMenuButton")
            };

            optionButton.Click += OptionButton_Click;
            Grid.SetColumn(optionButton, 1);
            Grid.SetRow(optionButton, 1);

            optionRow.Children.Add(optionButton);

            contentStackPanel.Children.Add(optionRow);
        }

        private void OptionButton_Click(object sender, RoutedEventArgs e)
        {
            Button? optionButton = sender as Button;
            if (optionButton != null)
            {
                Result = optionButton.Content as string ?? "";
                DialogResult = true;
            }
        }
    }
}
