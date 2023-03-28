using laba5.admin_panel;
using laba5.admin_panel.Models;
using laba5.mamaTableAdapters;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Odbc;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace laba5
{
    /// <summary>
    /// Логика взаимодействия для Dealers.xaml
    /// </summary>
    public partial class Dealers : Page
    {
        dealerTableAdapter dealers = new dealerTableAdapter();
        public Dealers()
        {
            InitializeComponent();
            Display.ItemsSource = dealers.GetData();
            Display.IsReadOnly = true;
        }

        private void Display_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (Display.SelectedItem != null)
            {
                var item = Display.SelectedItem as DataRowView;
                NameInput.Text = (string)item[1];
                CountryInput.Text = (string)item[2];
                ErrorMessage.Text = string.Empty;
            }
        }

        private void CreateButton_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(NameInput.Text) || string.IsNullOrWhiteSpace(CountryInput.Text))
                ErrorMessage.Text = "Вы заполнили не все поля";
            else
            {
                try
                {
                    dealers.InsertQuery(NameInput.Text, CountryInput.Text);
                    updated();
                    AdditionalInfo();
                }
                catch (Exception) { ErrorMessage.Text = "Вы ввели неверные значения"; }
            }
        }

        private void UpdateButton_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(NameInput.Text) || string.IsNullOrWhiteSpace(CountryInput.Text))
                ErrorMessage.Text = "Вы заполнили не все поля";
            else
            {
                var item = Display.SelectedItem as DataRowView;
                try
                {
                    dealers.UpdateQuery(NameInput.Text, CountryInput.Text, (int)item[0]);
                    updated();
                    AdditionalInfo();
                }
                catch (Exception) { ErrorMessage.Text = "Вы ввели неверные значения"; }
            }
        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            var item = Display.SelectedItem as DataRowView;
            if (Display.SelectedItem != null)
            {
                dealers.DeleteQuery((int)item[0]);
                updated();
                AdditionalInfo();
            }
            else
                ErrorMessage.Text = "Вы не выбрали элемент!";
        }
        private void updated()
        {
            Display.ItemsSource = dealers.GetData();
            ErrorMessage.Text = String.Empty;
        }
        private void AdditionalInfo()
        {
            Info1.Text = "Общее количество поставщиков: " + Display.Items.Count.ToString();
        }

        private void InsertJson_Click(object sender, RoutedEventArgs e)
        {
            List<DealersModel> forimport = Converter.DeserializeObject<List<DealersModel>>();
            foreach (var item in forimport)
                dealers.InsertQuery(item.Name, item.Country);
        }
    }
}
