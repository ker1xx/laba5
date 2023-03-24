using laba5.mamaTableAdapters;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
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
    /// Логика взаимодействия для Order.xaml
    /// </summary>
    public partial class Order : Page
    {
        storageTableAdapter storage = new storageTableAdapter();
        List<storageTableAdapter> cart = new List<storageTableAdapter>();
        public Order()
        {
            InitializeComponent();
            Display.ItemsSource = storage.names();
            ListedItems.ItemsSource = cart;
        }

        private void Display_AutoGeneratingColumn(object sender, DataGridAutoGeneratingColumnEventArgs e)
        {
            PropertyDescriptor propertyDescriptor = (PropertyDescriptor)e.PropertyDescriptor;
            e.Column.Header = propertyDescriptor.DisplayName;
            if (propertyDescriptor.DisplayName == "id_dealer" || propertyDescriptor.DisplayName == "id_model" || propertyDescriptor.DisplayName == "id")
            {
                e.Cancel = true;
            }
        }

        private void ListedItems_AutoGeneratingColumn(object sender, DataGridAutoGeneratingColumnEventArgs e)
        {
            PropertyDescriptor propertyDescriptor = (PropertyDescriptor)e.PropertyDescriptor;
            e.Column.Header = propertyDescriptor.DisplayName;
            if (propertyDescriptor.DisplayName == "id_dealer" || propertyDescriptor.DisplayName == "id_model" || propertyDescriptor.DisplayName == "id")
            {
                e.Cancel = true;
            }
        }

        private void Add_Click(object sender, RoutedEventArgs e)
        {
            if (ListedItems.Items.Count != 0 && Display.SelectedItem != null)
            {
                var item = Display.SelectedItem as DataGridView;
                foreach (var item1 in ListedItems.Items)
                {
                    if ((item1 as DataRowView)[0] == item.Rows[0])
                    {
                        (item1 as DataRowView)[2] = (int)(item1 as DataRowView)[2] + 1;
                        break;
                    }
                    else
                    {
                        ListedItems.Items.Add(Display.SelectedItem as DataGridView);
                        (item1 as DataRowView)[2] = 1;

                    }
                }

            }
            else
            {
                cart.Add((storageTableAdapter)Display.SelectedItem);
                ListedItems.ItemsSource = cart;

            }
        }

        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            if (ListedItems.Items.Count != 0 && Display.SelectedItem != null)
            {
                var item = Display.SelectedItem as DataGridView;
                foreach (var item1 in ListedItems.Items)
                {
                    if ((item1 as DataRowView)[0] == item.Rows[0])
                    {
                        if ((int)(item1 as DataRowView)[2] != 1)
                            (item1 as DataRowView)[2] = (int)(item1 as DataRowView)[2] - 1;
                        else
                            ListedItems.Items.Remove((int)(item1 as DataRowView)[2]);
                    }
                    else
                    {
                        ListedItems.Items.Add(Display.SelectedItem as DataGridView);
                        (item1 as DataRowView)[2] = 1;

                    }
                }

            }
        }

        private void Save_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
