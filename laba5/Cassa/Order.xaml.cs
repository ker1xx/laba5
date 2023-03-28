using laba5.mamaTableAdapters;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Reflection;
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
    /// Логика взаимодействия для Order.xaml
    /// </summary>
    public partial class Order : Page
    {
        goodsTableAdapter goods = new goodsTableAdapter();
        List<CartItems> cart = new List<CartItems>();
        CashierPanel cartPanel;
        orderTableAdapter order = new orderTableAdapter();
        decimal FullPrice = 0;
        int Amount = 0;
        decimal Profit = 0;
        public Order(CashierPanel cartPanel)
        {
            InitializeComponent();
            Display.ItemsSource = goods.names();
            ListedItems.ItemsSource = cart;
            this.cartPanel = cartPanel;

        }

        private void Display_AutoGeneratingColumn(object sender, DataGridAutoGeneratingColumnEventArgs e)
        {
            PropertyDescriptor propertyDescriptor = (PropertyDescriptor)e.PropertyDescriptor;
            e.Column.Header = propertyDescriptor.DisplayName;
            if (propertyDescriptor.DisplayName == "name id" || propertyDescriptor.DisplayName == "id" || propertyDescriptor.DisplayName == "first_price")
            {
                e.Cancel = true;
            }
        }
        private void ListedItems_AutoGeneratingColumn(object sender, DataGridAutoGeneratingColumnEventArgs e)
        {
            PropertyDescriptor propertyDescriptor = (PropertyDescriptor)e.PropertyDescriptor;
            e.Column.Header = propertyDescriptor.DisplayName;
            if (propertyDescriptor.DisplayName == "Id")
            {
                e.Cancel = true;
            }
        }

        private void Add_Click(object sender, RoutedEventArgs e)
        {
            if (Display.SelectedItem != null)
            {
                var item = Display.SelectedItem as DataRowView;
                if (cart.Any(x => x.Id == (int)item.Row[0]) && cart.First(x => x.Id == (int)item.Row[0]).Amount < (int)item.Row[7])
                    cart.First(x => x.Id == (int)item.Row[0]).Amount += 1;
                else if (!cart.Exists(x => x.Id == (int)item.Row[0]))
                    cart.Add(new CartItems((int)item.Row[0], (string)item.Row[5], (string)item.Row[6], (string)item.Row[2], (int)item.Row[4], 1, (decimal)item.Row[1]));
                Profit += (decimal)item.Row[1]-(decimal)item[8];
                cart.First(x => x.Id == (int)item.Row[0]).Profit -= ((decimal)item.Row[1] - (decimal)item[8]);
                FullPrice += (decimal)item.Row[1];
                Amount++;
                Price.Text = "Общая стоимость заказа: " + FullPrice;
                AmountItems.Text = "Количество товаров в заказе: " + Amount;
                ListedItems.ItemsSource = null;
                ListedItems.ItemsSource = cart;
            }
        }

        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            if (Display.SelectedItem != null)
            {
                var item = Display.SelectedItem as DataRowView;
                if (cart.Any(x => x.Id == (int)item.Row[0]))
                {
                    if (cart.First(x => x.Id == (int)item.Row[0]).Amount > 1)
                        cart.First(x => x.Id == (int)item.Row[0]).Amount -= 1;
                    else if (cart.First(x => x.Id == (int)item.Row[0]).Amount == 1)
                        cart.Remove(cart.First(x => x.Id == (int)item.Row[0]));
                    Profit -= (decimal)item.Row[1] - (decimal)item[8];
                    cart.First(x => x.Id == (int)item.Row[0]).Profit -= ((decimal)item.Row[1] - (decimal)item[8]);
                    FullPrice -= (decimal)item.Row[1];
                    Amount--;
                    Price.Text = "Общая стоимость заказа: " + FullPrice;
                    AmountItems.Text = "Количество товаров в заказе: " + Amount;
                }
                ListedItems.ItemsSource = null;
                ListedItems.ItemsSource = cart;
            }

        }

        private void Save_Click(object sender, RoutedEventArgs e)
        {
            if (ListedItems.Items.Count != 0)
            {
                int i = 0;
                foreach (var item1 in cart)
                {
                    order.InsertQuery(order.GetData().Count + 1, item1.Id, item1.Amount*(item1.Price - item1.Profit));

                }
                cartPanel.Frame.Content = new ConfirmOrder(cart, FullPrice, Profit);
            }
        }

    }
}


