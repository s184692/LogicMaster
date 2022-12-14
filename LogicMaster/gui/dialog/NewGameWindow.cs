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
using System.Media;

namespace LogicMaster.gui.dialog
{
    /// <summary>
    /// Klasa nowego okna dialogowego
    /// </summary>
    public class NewGameWindow : GateInfoWindow
    {
        private static readonly SoundPlayer clickSound = new SoundPlayer(@"resources/sounds/button_click.wav");

        public string Result { get; set; } = "";
        /// <summary>
        /// Tworzenie okna menu
        /// </summary>
        /// <param name="title">tytul gry</param>
        /// <param name="options">opcje gry</param>
        public NewGameWindow(string title, string[] options) : base()
        {
            AddTitleRow(title, 5);
            foreach (string option in options)
                AddOptionRow(option);
        }
        /// <summary>
        /// Funckja dodajaca pasek opcji
        /// </summary>
        /// <param name="option">opcja do wyboru</param>
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
            clickSound.Play();

            Button? optionButton = sender as Button;
            if (optionButton != null)
            {
                Result = optionButton.Content as string ?? "";
                DialogResult = true;
            }
        }
    }
}
