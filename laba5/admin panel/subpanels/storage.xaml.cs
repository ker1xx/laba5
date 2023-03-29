using laba5.admin_panel;
using laba5.admin_panel.Models;
using laba5.mamaTableAdapters;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows.Controls;
using System.Windows.Media.Animation;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TextBox;

namespace laba5
{
    /// <summary>
    /// Логика взаимодействия для storage.xaml
    /// </summary>
    public partial class storage : Page
    {
        storageTableAdapter storageData = new storageTableAdapter();
        modelTableAdapter model = new modelTableAdapter();
        dealerTableAdapter dealer = new dealerTableAdapter();
        AdminPanel AdminPanel;
        int AmountOfItems = 0;
        decimal FullFirstPrice = 0;
        string regex = @"\d*,\d{4}";

        public storage(AdminPanel AdminPanel)
        {
            InitializeComponent();
            Display.ItemsSource = storageData.names();
            Display.IsReadOnly = true;
            var info = storageData.GetData();
            AdditionalInfo();
            DealerInput.ItemsSource = dealer.GetData();
            DealerInput.DisplayMemberPath = "name";
            DealerInput.SelectedValuePath = "id";
            ModelInput.ItemsSource = model.GetData();
            ModelInput.DisplayMemberPath = "name";
            ModelInput.SelectedValuePath = "id";
            this.AdminPanel = AdminPanel;
        }
        private void Display_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (Display.SelectedItem != null)
            {
                var item = (Display.SelectedItem as DataRowView).Row;
                AmountInput.Text = ((int)item[2]).ToString();
                FirstPriceInput.Text = (item[4]).ToString();
                ModelInput.SelectedValue = Convert.ToString((int)item[1]);
                DealerInput.SelectedValue = Convert.ToString((int)item[3]);
                ErrorMessage.Text = string.Empty;
            }
        }

        private void CreateButton_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(AmountInput.Text) || string.IsNullOrWhiteSpace(FirstPriceInput.Text) ||
                !AmountInput.Text.Any(x => char.IsDigit(x)) || FirstPriceInput.Text.Any(x => char.IsLetter(x))
                || ModelInput.SelectedItem == null || DealerInput.SelectedItem == null ||
                Convert.ToInt32(AmountInput.Text) <= 0 || Convert.ToDecimal(FirstPriceInput.Text) <= 0 || !Regex.IsMatch(FirstPriceInput.Text, regex))
                ErrorMessage.Text = "Вы заполнили не все поля";
            else
            {
                try
                {
                    storageData.InsertQuery(Convert.ToInt32(ModelInput.SelectedValue), Convert.ToInt32(AmountInput.Text), Convert.ToInt32(DealerInput.SelectedValue), Convert.ToDecimal(FirstPriceInput.Text));
                    updated();
                    AdditionalInfo();
                }
                catch (Exception) { ErrorMessage.Text = "Вы ввели неверные значения"; }
            }
        }

        private void UpdateButton_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(AmountInput.Text) || string.IsNullOrWhiteSpace(FirstPriceInput.Text) ||
    AmountInput.Text.Any(x => char.IsDigit(x)) || FirstPriceInput.Text.Any(x => char.IsDigit(x))
    || ModelInput.SelectedItem == null || DealerInput.SelectedItem == null ||
                Convert.ToInt32(AmountInput.Text) <= 0 || Convert.ToDecimal(FirstPriceInput.Text) <= 0 || !Regex.IsMatch(FirstPriceInput.Text, regex))
                ErrorMessage.Text = "Вы заполнили не все поля";
            else
            {
                var item = Display.SelectedItem as DataRowView;
                try
                {
                    storageData.UpdateQuery((int)ModelInput.SelectedIndex, Convert.ToInt32(AmountInput.Text), (int)DealerInput.SelectedIndex, Convert.ToInt32(FirstPriceInput.Text), (int)item[0]);
                    updated();
                    AdditionalInfo();
                }
                catch (Exception) { ErrorMessage.Text = "Вы ввели неверные значения"; }
            }
        }

        private void DeleteButton_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            var item = Display.SelectedItem as DataRowView;
            if (Display.SelectedItem != null)
            {
                storageData.DeleteQuery((int)item[0]);
                updated();
                AdditionalInfo();
            }
            else
                ErrorMessage.Text = "Вы не выбрали элемент!";
        }

        private void Display_AutoGeneratingColumn_1(object sender, DataGridAutoGeneratingColumnEventArgs e)
        {
            PropertyDescriptor propertyDescriptor = (PropertyDescriptor)e.PropertyDescriptor;
            e.Column.Header = propertyDescriptor.DisplayName;
            if (propertyDescriptor.DisplayName == "id_dealer" || propertyDescriptor.DisplayName == "id_model")
            {
                e.Cancel = true;
            }
        }
        private void AdditionalInfo()
        {
            var info = storageData.GetData();
            foreach (DataRow data in info.Rows)
            {
                AmountOfItems += Convert.ToInt32(data[2]);
                FullFirstPrice += Convert.ToInt32(data[4]) * Convert.ToInt32(data[2]);
            }
            Info1.Text = "Общее количество товаров: " + AmountOfItems.ToString();
            Info2.Text = "Общая закупочная стоимость товаров: " + FullFirstPrice.ToString();

        }
        private void updated()
        {
            Display.ItemsSource = storageData.GetData();
            ErrorMessage.Text = string.Empty;
        }

        private void ModelButton_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            AdminPanel.Frame.Content = new Models();
        }

        private void DealerButton_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            AdminPanel.Frame.Content = new Dealers();
        }

        private void InsertJson_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            List<StorageModel> forimport = Converter.DeserializeObject<List<StorageModel>>();
            foreach (var item in forimport)
                storageData.InsertQuery(item.ModelId, item.Amount,item.IdDealer, item.first_price);
        }

        private void GoodsButton_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            AdminPanel.Frame.Content = new GoodsPanel();
        }
    }
}
