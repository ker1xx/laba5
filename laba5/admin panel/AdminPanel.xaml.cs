using laba5.mamaTableAdapters;
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
    /// Логика взаимодействия для AdminPanel.xaml
    /// </summary>
    public partial class AdminPanel : Window
    {
        MainWindow MainWindow;
        public AdminPanel(MainWindow mainWindow)
        {
            InitializeComponent();
            this.MainWindow = mainWindow;

        }

        private void CheksButton_Click(object sender, RoutedEventArgs e)
        {
            Frame.Content = new cheks();

        }

        private void EmployeesButton_Click(object sender, RoutedEventArgs e)
        {
            Frame.Content = new employees(this);

        }

        private void MarketsButton_Click(object sender, RoutedEventArgs e)
        {
            Frame.Content = new markets();
        }

        private void StorageButton_Click(object sender, RoutedEventArgs e)
        {
            Frame.Content = new storage(this);
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            MainWindow.Visibility = Visibility.Visible;
        }
    }
}
