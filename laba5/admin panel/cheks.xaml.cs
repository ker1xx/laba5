using laba5.DataSet2TableAdapters;
using System;
using System.Collections.Generic;
using System.Data;
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
        int ChecksCount = 0;
        public cheks()
        {
            InitializeComponent();
            Display.ItemsSource = check.names();
            int previoudid = 0;
            foreach (DataRow name in check.names())
            {
                if (previoudid != (int)name[0])
                {
                    previoudid++;
                    ChecksCount++;
                }

            }
            Info1.Text = "Общее количество чеков: " + ChecksCount.ToString();
            Info2.Text
        }
    }
}
