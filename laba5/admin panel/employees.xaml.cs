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
        int AmountOfEmployees = 0;
        int FullSalary = 0;
        AdminPanel AdminPanel;
        public employees(AdminPanel AdminPanel)
        {
            InitializeComponent();
            this.AdminPanel = AdminPanel;
            Display.ItemsSource = emp.names();
            Display.IsReadOnly = true;
            AdditionalInfo();
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
                if (LastNameInput.Text == string.Empty)
                {
                    try
                    {
                        emp.InsertQuery((string)NameInput.Text, (string)SurnameInput.Text, null, (int)JobTitleInput.SelectedIndex, Convert.ToDecimal(SalaryInput.Text));
                        updated();
                        AdditionalInfo();
                    }
                    catch (Exception )
                    {
                        ErrorMessage.Text = "Вы ввели неверные значения";
                    }
                }
                else
                {
                    try
                    {
                        emp.InsertQuery((string)NameInput.Text, (string)SurnameInput.Text, (string)LastNameInput.Text, (int)JobTitleInput.SelectedIndex, Convert.ToDecimal(SalaryInput.Text));
                        updated();
                        AdditionalInfo();
                    }
                    catch (Exception )
                    {
                        ErrorMessage.Text = "Вы ввели неверные значения";
                    }
                }
                Display.ItemsSource = emp.names();
            }
        }

        private void UpdateButton_Click(object sender, RoutedEventArgs e)
        {
            var item = Display.SelectedItem as DataRowView;
            if (NameInput.Text == string.Empty || SurnameInput.Text == string.Empty || SalaryInput.Text == string.Empty || JobTitleInput.SelectedIndex == -1 || SalaryInput.Text.Any(x => char.IsLetter(x)))
                ErrorMessage.Text = "Вы заполнили не все поля";
            else
            {

                if (LastNameInput.Text == string.Empty)
                {
                    try
                    {
                        emp.UpdateQuery((string)NameInput.Text, (string)SurnameInput.Text, null, (int)JobTitleInput.SelectedIndex, Convert.ToDecimal(SalaryInput.Text), (int)item[0]);
                        updated();
                        AdditionalInfo();
                    }
                    catch (Exception )
                    {
                        ErrorMessage.Text = "Вы ввели неверные значения";
                    }
                }
                else
                {
                    try
                    {
                        emp.UpdateQuery((string)NameInput.Text, (string)SurnameInput.Text, (string)LastNameInput.Text, (int)JobTitleInput.SelectedValue, Convert.ToDecimal(SalaryInput.Text), (int)item[0]);
                        updated();
                        AdditionalInfo();
                    }
                    catch (Exception )
                    {
                        ErrorMessage.Text = "Вы ввели неверные значения";
                    }
                    Display.ItemsSource = emp.names();
                }
            }
        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            if (Display.SelectedItem != null)
            {
                var item = Display.SelectedItem as DataRowView;
                emp.DeleteQuery((int)item[0]);
                updated();
                AdditionalInfo();
            }
            else
                ErrorMessage.Text = "Вы не выбрали элемент!";
        }

        private void Display_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (Display.SelectedItem != null)
            {
                var item = Display.SelectedItem as DataRowView;
                NameInput.Text = (string)item[1];
                SurnameInput.Text = (string)item[2];
                LastNameInput.Text = (string)item[3];
                SalaryInput.Text = Convert.ToString(item[5]);
                JobTitleInput.SelectedValue = Convert.ToString((int)item[4]);
                ErrorMessage.Text = string.Empty;
            }
        }
        private void AdditionalInfo()
        {
            var info = emp.GetData();
            foreach (DataRow data in info.Rows)
            {
                AmountOfEmployees++;
                FullSalary += Convert.ToInt32(data[5]);
            }
            Info1.Text = "Общее количество сотрудников: " + AmountOfEmployees.ToString();
            Info2.Text = "Общая сумма зарплаты за месяц: " + FullSalary.ToString();
        }
        private void updated()
        {
            Display.ItemsSource = emp.names();
            ErrorMessage.Text = string.Empty;
        }

        private void AutharizationButton_Click(object sender, RoutedEventArgs e)
        {
            AdminPanel.Frame.Content = new authorization_info();
        }
    }
}
