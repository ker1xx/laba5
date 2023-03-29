using laba5.admin_panel.Models;
using laba5.mamaTableAdapters;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
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
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TextBox;

namespace laba5
{
    /// <summary>
    /// Логика взаимодействия для GoodsPanel.xaml
    /// </summary>
    public partial class GoodsPanel : Page
    {
        goodsTableAdapter goods = new goodsTableAdapter();
        storageTableAdapter stor = new storageTableAdapter();
        string regex = @"\d*,\d{4}";
        public GoodsPanel()
        {
            InitializeComponent();
            Display.ItemsSource = goods.GetData();
            Display.IsReadOnly = true;
            ModelInput.ItemsSource = stor.names();
            ModelInput.DisplayMemberPath = "id_model";
            ModelInput.SelectedValuePath = "id";
        }
        private void CreateButton_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(SexInput.Text) || string.IsNullOrWhiteSpace(SizeInput.Text) || string.IsNullOrWhiteSpace(PriceInput.Text) || PriceInput.Text.Any(x => char.IsLetter(x)) ||  !Regex.IsMatch(PriceInput.Text, regex) || ModelInput.SelectedValue == null)
                ErrorMessage.Text = "Вы заполнили не все поля";
            else
            {
                try
                {
                    goods.InsertQuery(Convert.ToDecimal(PriceInput.Text),SexInput.Text.ToString(),Convert.ToInt32( ModelInput.SelectedValue), Convert.ToInt32(SizeInput.Text));
                    updated();
                }
                catch (Exception) { ErrorMessage.Text = "Вы ввели неверные значения"; }
            }
        }

        private void UpdateButton_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            var item = Display.SelectedItem as DataRowView;
            if (string.IsNullOrWhiteSpace(SexInput.Text) || string.IsNullOrWhiteSpace(SizeInput.Text) || string.IsNullOrWhiteSpace(PriceInput.Text) || PriceInput.Text.Any(x => char.IsLetter(x)) || SexInput.Text.Any(x => char.IsLetter(x)) || !Regex.IsMatch(PriceInput.Text, regex) || ModelInput.SelectedValue == null)
            {
                ErrorMessage.Text = "Вы заполнили не все поля";
            }
            else
            {
                try
                {
                    goods.UpdateQuery(Convert.ToDecimal(PriceInput.Text), SexInput.ToString(), Convert.ToInt32(ModelInput.SelectedValue), Convert.ToInt32(SizeInput.Text), Convert.ToInt32(item[0]));
                    updated();
                }
                catch (Exception) { ErrorMessage.Text = "Вы ввели неверные значения"; }
            }
        }

        private void DeleteButton_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            var item = Display.SelectedItem as DataRowView;
            if (Display.SelectedItem != null)
            {
                goods.DeleteQuery((int)item[0]);
                updated();
            }
            else
                ErrorMessage.Text = "Вы не выбрали элемент!";
        }
        private void updated()
        {
            Display.ItemsSource = goods.GetData();
            ErrorMessage.Text = string.Empty;
        }

        private void Display_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (Display.SelectedItem != null)
            {
                var item = Display.SelectedItem as DataRowView;
                PriceInput.Text = (string)item[1];
                SexInput.Text = (string)item[2];
                SizeInput.Text = item[3].ToString();
                ModelInput.SelectedValue = item[4].ToString();
                ErrorMessage.Text = string.Empty;
            }
        }
    }
}
