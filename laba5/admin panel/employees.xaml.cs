using laba5.DataSet2TableAdapters;
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
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace laba5
{
    /// <summary>
    /// Логика взаимодействия для employees.xaml
    /// </summary>
    public partial class employees : Page
    {
        employeeTableAdapter emp = new employeeTableAdapter();
        job_titleTableAdapter job = new job_titleTableAdapter();
        AdminPanel AdminPanel;
        int AmountOfEmployees = 0;
        int FullSalary = 0;
        public employees(AdminPanel adminPanel)
        {
            InitializeComponent();
            Display.ItemsSource = emp.names();
            Display.IsReadOnly = true;
            AdminPanel = adminPanel;
            var info = emp.GetData();
            foreach (DataRow data in info.Rows)
            {
                AmountOfEmployees++;
                FullSalary += Convert.ToInt32(data[5]);
            }
            Info1.Text = "Общее количество сотрудников: " + AmountOfEmployees.ToString();
            Info2.Text = "Общая сумма зарплаты за месяц: " + FullSalary.ToString();
            JobTitleInput.ItemsSource = job.GetData();
            JobTitleInput.DisplayMemberPath = "name";
            JobTitleInput.SelectedValuePath = "id";
        }

        private void Display_AutoGeneratingColumn(object sender, DataGridAutoGeneratingColumnEventArgs e)
        {
            PropertyDescriptor propertyDescriptor = (PropertyDescriptor)e.PropertyDescriptor;
            e.Column.Header = propertyDescriptor.DisplayName;
            if (propertyDescriptor.DisplayName == "job title id")
            {
                e.Cancel = true;
            }
        }

        private void CreateButton_Click(object sender, RoutedEventArgs e)
        {
            if (NameInput.Text == string.Empty || SurnameInput.Text == string.Empty || SalaryInput.Text == string.Empty || JobTitleInput.SelectedIndex == -1)
                ErrorMessage.Text = "Вы заполнили не все поля";
            else
            {
                try
                {
                    emp.InsertQuery((string)NameInput.Text, (string)SurnameInput.Text, (string)SalaryInput.Text, (int)JobTitleInput.SelectedIndex, Convert.ToDecimal(SalaryInput.Text));
                    Display.ItemsSource = emp.names();
                }
                catch (Exception ex)
                {
                    ErrorMessage.Text = "Вы ввели неверные значения";
                }
            }
        }

        private void UpdateButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Display_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var item = Display.SelectedItem as DataRowView;
            NameInput.Text = (string)item[1];
            SurnameInput.Text = (string)item[2];
            LastNameInput.Text = (string)item[3];
            SalaryInput.Text = Convert.ToString(item[5]);
            var a  = item[6];
/*         JobTitleInput.SelectedValue = a.ToString();*/
        }
    }
}
