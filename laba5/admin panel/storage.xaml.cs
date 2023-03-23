﻿using laba5.DataSet2TableAdapters;
using System;
using System.ComponentModel;
using System.Data;
using System.Linq;
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
        int FullFirstPrice = 0;

        public storage( AdminPanel AdminPanel)
        {
            InitializeComponent();
            Display.ItemsSource = storageData.names();
            Display.IsReadOnly = true;
            var info = storageData.GetData();
            AdditionalInfo();
            DealerInput.ItemsSource = model.GetData();
            DealerInput.DisplayMemberPath = "name";
            DealerInput.SelectedValuePath = "id";
            ModelInput.ItemsSource = dealer.GetData();
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
            if (AmountInput.Text == String.Empty || FirstPriceInput.Text == String.Empty ||
                AmountInput.Text.Any(x => char.IsDigit(x)) || FirstPriceInput.Text.Any(x => char.IsDigit(x))
                || ModelInput.SelectedItem == null || DealerInput.SelectedItem == null)
                ErrorMessage.Text = "Вы заполнили не все поля";
            else
            {
                try
                {
                    storageData.InsertQuery((int)ModelInput.SelectedIndex, Convert.ToInt32(AmountInput.Text), (int)DealerInput.SelectedIndex, Convert.ToInt32(FirstPriceInput.Text));
                    updated();
                    AdditionalInfo();
                }
                catch (Exception ) { ErrorMessage.Text = "Вы ввели неверные значения"; }
            }   
        }

        private void UpdateButton_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            if (AmountInput.Text == String.Empty || FirstPriceInput.Text == String.Empty ||
    AmountInput.Text.Any(x => char.IsDigit(x)) || FirstPriceInput.Text.Any(x => char.IsDigit(x))
    || ModelInput.SelectedItem == null || DealerInput.SelectedItem == null)
                ErrorMessage.Text = "Вы заполнили не все поля";
            else
            {
                var item = Display.SelectedItem as DataRowView;
                try
                {
                    storageData.UpdateQuery((int)ModelInput.SelectedIndex, Convert.ToInt32(AmountInput.Text), (int)DealerInput.SelectedIndex, Convert.ToInt32(FirstPriceInput.Text),(int)item[0]);
                    updated();
                    AdditionalInfo();
                }
                catch (Exception ) { ErrorMessage.Text = "Вы ввели неверные значения"; }
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
            AdminPanel.Frame.Content = new Dealers();
        }

        private void DealerButton_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            AdminPanel.Frame.Content = new Models();
        }
    }
}
