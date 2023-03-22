using laba5.DataSet2TableAdapters;
using System.Data;
using System;
using System.Windows.Controls;

namespace laba5
{
    /// <summary>
    /// Логика взаимодействия для markets.xaml
    /// </summary>
    public partial class markets : Page
    {
        marketTableAdapter market = new marketTableAdapter();
        AdminPanel AdminPanel;
        int AmountOfShops = 0;
        public markets(AdminPanel adminPanel)
        {
            InitializeComponent();
            Display.ItemsSource = market.GetData();
            Display.IsReadOnly = true;
            AdminPanel = adminPanel;
            var info = market.GetData();
            foreach (DataRow data in info.Rows)
            {
                AmountOfShops++;
            }
/*            adminPanel.Info1.Text = "Общее количество магазинов: " + AmountOfShops;
*/        }
    }
}
