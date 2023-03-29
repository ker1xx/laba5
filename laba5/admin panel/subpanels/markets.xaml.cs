using laba5.mamaTableAdapters;
using System.Data;
using System;
using System.Windows.Controls;
using System.Linq;
using System.Text.RegularExpressions;
using laba5.admin_panel.Models;
using System.Collections.Generic;
using laba5.admin_panel;

namespace laba5
{
    /// <summary>
    /// Логика взаимодействия для markets.xaml
    /// </summary>
    public partial class markets : Page
    {
        marketTableAdapter market = new marketTableAdapter();
        string regex = @"\d{2}:\d{2}:\d{2}";
        public markets()
        {
            InitializeComponent();
            Display.ItemsSource = market.GetData();
            Display.IsReadOnly = true;
            AdditionalInfo();
        }

        private void CreateButton_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(NameInput.Text) || string.IsNullOrWhiteSpace(AdressInput.Text) ||string.IsNullOrWhiteSpace(BeginInput.Text) || string.IsNullOrWhiteSpace(EndInput.Text) || BeginInput.Text.Any(x => char.IsLetter(x)) || EndInput.Text.Any(x => char.IsLetter(x)) || !Regex.IsMatch(BeginInput.Text,regex) || !Regex.IsMatch(EndInput.Text, regex))
                ErrorMessage.Text = "Вы заполнили не все поля";
            else
            {
                try
                {
                    market.InsertQuery(AdressInput.Text, DateTime.Today + TimeSpan.Parse(BeginInput.Text), DateTime.Today + TimeSpan.Parse(EndInput.Text), NameInput.Text);
                    updated();
                    AdditionalInfo();
                }
                catch (Exception ) { ErrorMessage.Text = "Вы ввели неверные значения"; }
                AdditionalInfo();
            }
        }

        private void UpdateButton_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            var item = Display.SelectedItem as DataRowView;
            if (string.IsNullOrWhiteSpace(NameInput.Text) || string.IsNullOrWhiteSpace(AdressInput.Text) || string.IsNullOrWhiteSpace(BeginInput.Text) || string.IsNullOrWhiteSpace(EndInput.Text) || BeginInput.Text.Any(x => char.IsLetter(x)) || EndInput.Text.Any(x => char.IsLetter(x)) || !Regex.IsMatch(BeginInput.Text, regex) || !Regex.IsMatch(EndInput.Text, regex))
            {
                ErrorMessage.Text = "Вы заполнили не все поля";
            }
            else
            {
                try
                {
                    market.UpdateQuery(AdressInput.Text, DateTime.Today + TimeSpan.Parse(BeginInput.Text), DateTime.Today + TimeSpan.Parse(EndInput.Text), NameInput.Text, (int)item[0]);
                    updated();
                    AdditionalInfo();
                }
                catch (Exception ) { ErrorMessage.Text = "Вы ввели неверные значения"; }
                AdditionalInfo();
            }
        }

        private void DeleteButton_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            var item = Display.SelectedItem as DataRowView;
            if (Display.SelectedItem != null)
            {
                market.DeleteQuery((int)item[0]);
                updated();
                AdditionalInfo();
            }
            else
                ErrorMessage.Text = "Вы не выбрали элемент!";
        }
        private void AdditionalInfo()
        {
            Info1.Text = "Общее количество магазинов: " + Display.Items.Count.ToString();
        }
        private void updated()
        {
            Display.ItemsSource = market.GetData();
            ErrorMessage.Text = string.Empty;
        }

        private void Display_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (Display.SelectedItem != null)
            { 
            var item = Display.SelectedItem as DataRowView;
            NameInput.Text = (string)item[2];
            AdressInput.Text = (string)item[1];
            BeginInput.Text = ((TimeSpan)item[3]).ToString();
            EndInput.Text = ((TimeSpan)item[4]).ToString();
            ErrorMessage.Text = string.Empty;
            }
        }

        private void InsertJson_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            List<MarketsModel> forimport = Converter.DeserializeObject<List<MarketsModel>>();
            foreach (var item in forimport)
                market.InsertQuery(item.Adress, item.Begin_Work_Time, item.End_Work_Time, item.name);
        }
    }
}
