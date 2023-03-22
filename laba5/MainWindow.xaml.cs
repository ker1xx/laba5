using laba5.DataSet2TableAdapters;
using System;
using System.Collections.Generic;
using System.Data;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;

namespace laba5
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        authorizationTableAdapter auth = new authorizationTableAdapter();
        employeeTableAdapter emp = new employeeTableAdapter();
        List<string> passwords = new List<string>();
        bool IsLogined;
        int role;
        public MainWindow()
        {
            InitializeComponent();

        }

        private void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            var info = auth.GetID();
            if (LoginText.Text == String.Empty || PasswordInput.Password == String.Empty)
                ErrorBlock.Text = "Одно из введенных полей пусто!";
            else
            {
                foreach (DataRow data in info.Rows)
                {
                    if ((string)data[1] == LoginInput.Text)
                    {
                        if ((string)data[2] == PasswordInput.Password)
                        {
                            IsLogined = true;
                            role = (int)data[3];
                        }
                    }
                }
                if (IsLogined != true)
                {
                    ErrorBlock.Text = "Вы ввели неверные данные";
                    LoginInput.Text = String.Empty;
                    PasswordInput.Password = String.Empty;
                }
                else
                {
                    if (role == 1)
                    {
                        AdminPanel adminPanel = new AdminPanel();
                        this.Visibility = Visibility.Hidden;
                        adminPanel.ShowDialog();
                    }
                    else if (role == 2)
                    {
                        CashierPanel cashierPanel = new CashierPanel();
                        this.Visibility = Visibility.Hidden;
                        cashierPanel.ShowDialog();
                    }
                    LoginInput.Text = String.Empty;
                    PasswordInput.Password = String.Empty;
                    ErrorBlock.Text = String.Empty;
                }
            }
        }
    }
}
