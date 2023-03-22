using laba5.DataSet2TableAdapters;
using System.Data;
using System;
using System.Windows.Controls;
using System.Linq;

namespace laba5
{
    /// <summary>
    /// Логика взаимодействия для markets.xaml
    /// </summary>
    public partial class markets : Page
    {
        marketTableAdapter market = new marketTableAdapter();
        int AmountOfShops = 0;
        public markets(AdminPanel adminPanel)
        {
            InitializeComponent();
            Display.ItemsSource = market.GetData();
            Display.IsReadOnly = true;
            AdditionalInfo();
        }

        private void CreateButton_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            if (NameInput.Text == string.Empty || AdressInput.Text == string.Empty || BeginInput.Text == string.Empty || EndInput.Text == string.Empty || BeginInput.Text.Any(x => char.IsLetter(x)) || EndInput.Text.Any(x => char.IsLetter(x)))
            {
                ErrorMessage.Text = "Вы заполнили не все поля";
            }
            else
            {
                try
                {
                    market.InsertQuery(AdressInput.Text, DateTime.Today + TimeSpan.Parse(BeginInput.Text), DateTime.Today + TimeSpan.Parse(EndInput.Text), NameInput.Text);
                    updated();
                }
                catch (Exception ex) { ErrorMessage.Text = "Вы ввели неверные значения"; }
                AdditionalInfo();
            }
        }

        private void UpdateButton_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            var item = Display.SelectedItem as DataRowView;
            if (NameInput.Text == string.Empty || AdressInput.Text == string.Empty || BeginInput.Text == string.Empty || EndInput.Text == string.Empty || BeginInput.Text.Any(x => char.IsLetter(x)) || EndInput.Text.Any(x => char.IsLetter(x)))
            {
                ErrorMessage.Text = "Вы заполнили не все поля";
            }
            else
            {
                try
                {
                    market.UpdateQuery(AdressInput.Text, DateTime.Today + TimeSpan.Parse(BeginInput.Text), DateTime.Today + TimeSpan.Parse(EndInput.Text), NameInput.Text, (int)item[0]);
                    updated();
                }
                catch (Exception ex) { ErrorMessage.Text = "Вы ввели неверные значения"; }
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
            }
            else
                ErrorMessage.Text = "Вы не выбрали элемент!";
        }
        private void AdditionalInfo()
        {
            var info = market.GetData();
            foreach (DataRow data in info.Rows)
            {
                AmountOfShops++;
            }
            Info1.Text = "Общее количество магазинов: " + AmountOfShops;
        }
        private void updated()
        {
            Display.ItemsSource = market.GetData();
            ErrorMessage.Text = string.Empty;
        }

        private void Display_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var item = Display.SelectedItem as DataRowView;
            NameInput.Text = (string)item[4];
            AdressInput.Text = (string)item[1];
            BeginInput.Text = ((DateTime)item[2]).ToString();
            EndInput.Text = ((DateTime)item[3]).ToString();
            ErrorMessage.Text = string.Empty;
        }
    }
}
