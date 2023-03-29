using laba5.admin_panel;
using laba5.mamaTableAdapters;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlTypes;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace laba5
{
    /// <summary>
    /// Логика взаимодействия для cheks.xaml
    /// </summary>
    public partial class cheks : Page
    {
        check_infoTableAdapter check = new check_infoTableAdapter();
        storageTableAdapter stor = new storageTableAdapter();
        goodsTableAdapter goods = new goodsTableAdapter();
        int ChecksCount = 0;
        Decimal TotalProfit = 0;
        public cheks()
        {
            InitializeComponent();
            Display.ItemsSource = check.names();
            int previousid = 0;
            foreach (DataRow name in check.names())
            {
                if (previousid != (int)name[0])
                {
                    previousid++;
                    ChecksCount++;
                }
            }
            foreach (DataRow data in goods.GetData())
            {
                TotalProfit += Convert.ToDecimal(data[1]);
            }
            foreach (DataRow data in stor.GetData())
            {
                TotalProfit -= Convert.ToDecimal(data[4]);
            }
            Info1.Text = "Общее количество чеков: " + ChecksCount.ToString();
            Info2.Text = "Общее количество прибыли: " + TotalProfit.ToString();
        }

        private void Display_AutoGeneratingColumn(object sender, DataGridAutoGeneratingColumnEventArgs e)
        {
            PropertyDescriptor propertyDescriptor = (PropertyDescriptor)e.PropertyDescriptor;
            e.Column.Header = propertyDescriptor.DisplayName;
            if (propertyDescriptor.DisplayName == "employee id" || propertyDescriptor.DisplayName == "market id")
            {
                e.Cancel = true;
            }
        }

        private void InsertJson_Click(object sender, RoutedEventArgs e)
        {
            List<ChecksModel> forimport = Converter.DeserializeObject<List<ChecksModel>>();
            if (forimport != null)
            {
                foreach (var item in forimport)
                    check.InsertQuery(item.employee_id, item.market_id, item.total_money, item.date);
            }
        }
    }
}
