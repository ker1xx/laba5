using laba5.mamaTableAdapters;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;

namespace laba5
{
    /// <summary>
    /// Логика взаимодействия для ConfirmOrder.xaml
    /// </summary>
    public partial class ConfirmOrder : Page
    {
        employeeTableAdapter emp = new employeeTableAdapter();
        check_infoTableAdapter checkinfo = new check_infoTableAdapter();
        marketTableAdapter markets = new marketTableAdapter();
        decimal FullPrice = 0;
        string regex = @"\d*,\d{4}";
        List<CartItems> cart;
        public ConfirmOrder(List<CartItems> cart, decimal FullPrice, decimal Profit)
        {
            InitializeComponent();
            Order.ItemsSource = cart;
            NameOfCashier_Input.ItemsSource = emp.GetCashiers();
            NameOfCashier_Input.DisplayMemberPath = "surname";
            NameOfCashier_Input.SelectedValuePath = "id";
            NameOfMarketInput.ItemsSource = markets.GetData();
            NameOfMarketInput.DisplayMemberPath = "name";
            NameOfMarketInput.SelectedValuePath = "id";
            ProfitText.Content = "Общая прибыль для компании: " + Profit;
            this.FullPrice = FullPrice;
            FullPrice_Input.Text = FullPrice.ToString();
            this.cart = cart;
        }

        private void CreateButton_Click(object sender, RoutedEventArgs e)
        {
            if (NameOfMarketInput.SelectedItem!=null && NameOfCashier_Input.SelectedItem != null && !string.IsNullOrWhiteSpace(FullPrice_Input.Text) && !FullPrice_Input.Text.Any(x => char.IsLetter(x)) && Regex.IsMatch(Given_Input.Text, regex) && Convert.ToDecimal(Given_Input.Text) >= FullPrice)
            {
                checkinfo.InsertQuery((int)NameOfCashier_Input.SelectedValue, (int)NameOfMarketInput.SelectedValue, FullPrice, DateTime.Now);
                string desktop = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
                FileStream stream = File.Create(desktop + "\\" + (checkinfo.GetData().Count + 1) + ".txt");
                stream.Close();
                string checkcontains = $"\t\t\tОбувной Центр\n\tКассовый чек № {checkinfo.GetData().Count + 1}\n";
                foreach (var a in cart)
                    checkcontains += $"\t{a.Name}\t количество: {a.Amount}\tцена за штуку: {a.Price} - \t {a.Price * a.Amount}\n";
                var cassir = emp.GetCashiers().Rows;
                checkcontains += $"Итого к оплате: {FullPrice}\nВнесено: {Given_Input.Text}\nСдача: {FullPrice-Convert.ToDecimal(Given_Input.Text)}\n";
                for (int i = 0; i < cassir.Count;i++)
                {
                    if (cassir[i][0].ToString() == NameOfCashier_Input.SelectedValue.ToString())
                        checkcontains += $"\tКассир: {cassir[i][1]} {cassir[i][2]} {cassir[i][3]}\n";
                }
                checkcontains += $"Дата: {DateTime.Today}";
                File.AppendAllText(desktop + "\\" + (checkinfo.GetData().Count + 1) + ".txt", checkcontains);
            }
            else
            {
                Window1 error = new Window1();
                error.ShowDialog();
            }
        }
    }
}
