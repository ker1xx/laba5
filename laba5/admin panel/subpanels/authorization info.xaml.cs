using laba5.mamaTableAdapters;
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
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TextBox;
using DataGrid = System.Windows.Controls.DataGrid;

namespace laba5
{
    /// <summary>
    /// Логика взаимодействия для authorization_info.xaml
    /// </summary>
    public partial class authorization_info : Page
    {
        authorizationTableAdapter auth = new authorizationTableAdapter();

        int AmountOfEmployees = 0;
        public authorization_info()
        {
            InitializeComponent();
            Display.ItemsSource = auth.GetData();
            Display.IsReadOnly = true;
            AdditionalInfo();
        }

        private void CreateButton_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(LoginInput.Text) || string.IsNullOrWhiteSpace(PasswordInput.Text) || string.IsNullOrWhiteSpace(IDInput.Text) || IDInput.Text.Any(x => char.IsDigit(x)))
                ErrorMessage.Text = "Вы заполнили не все поля";
            else
            {

                try
                {
                    auth.InsertQuery(Convert.ToInt32(IDInput.Text), LoginInput.Text, PasswordInput.Text);
                    updated();
                    AdditionalInfo();
                }
                catch(Exception ) { ErrorMessage.Text = "Вы ввели неверные значения"; }
            }
        }

        private void UpdateButton_Click(object sender, RoutedEventArgs e)
        {
            var a = auth.getlastid().Rows[0];
            if (Display.SelectedItem != null)
            {
                var item = Display.SelectedItem as DataRowView;
                if (string.IsNullOrWhiteSpace(LoginInput.Text) || string.IsNullOrWhiteSpace(PasswordInput.Text) || string.IsNullOrWhiteSpace(IDInput.Text) || IDInput.Text.Any(x => char.IsDigit(x)))
                    ErrorMessage.Text = "Вы заполнили не все поля";
                else
                {

                    try
                    {
                        auth.UpdateQuery(Convert.ToInt32(IDInput.Text), LoginInput.Text, PasswordInput.Text, (int)item[0]);
                        updated();
                        AdditionalInfo();
                    }
                    catch (Exception ) { ErrorMessage.Text = "Вы ввели неверные значения"; };
                }
            }
        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            var item = Display.SelectedItem as DataRowView;
            auth.DeleteQuery((int)item[0]);
        }
        private void Display_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (Display.SelectedItem != null)
            {
                var item = Display.SelectedItem as DataRowView;
                LoginInput.Text = (string)item[1];
                PasswordInput.Text = (string)item[2];
                ErrorMessage.Text = string.Empty;
            }
        }
        private void AdditionalInfo()
        {
            var info = auth.GetData();
            foreach (DataRow data in info.Rows)
                AmountOfEmployees++;
            Info1.Text = "Общее количество сотрудников: " + AmountOfEmployees.ToString();
        }
        private void updated()
        {

            Display.ItemsSource = auth.GetData();
            ErrorMessage.Text = string.Empty;
        }

    }
}
