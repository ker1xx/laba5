using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace laba5
{
    /// <summary>
    /// Логика взаимодействия для CashierPanel.xaml
    /// </summary>
    public partial class CashierPanel : Window
    {
        public CashierPanel()
        {
            InitializeComponent();
        }

        private void NewOrder_Click(object sender, RoutedEventArgs e)
        {
            Frame.Content = new Order(this);
        }

        private void PreviousOrders_Click(object sender, RoutedEventArgs e)
        {
            Frame.Content = new HistoryOfOrders();
        }
    }
}
