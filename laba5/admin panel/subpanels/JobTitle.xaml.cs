using laba5.admin_panel.Models;
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
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace laba5.admin_panel.subpanels
{
    /// <summary>
    /// Логика взаимодействия для JobTitle.xaml
    /// </summary>
    public partial class JobTitle : Page
    {
        job_titleTableAdapter job = new job_titleTableAdapter();
        public JobTitle()
        {
            InitializeComponent();
            Display.ItemsSource = job.GetData();
            Display.IsReadOnly = true;
        }
        private void Display_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (Display.SelectedItem != null)
            {
                var item = Display.SelectedItem as DataRowView;
                NameInput.Text = (string)item[1];
                ErrorMessage.Text = string.Empty;
            }
        }

        private void CreateButton_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(NameInput.Text) || NameInput.Text.Any(x => char.IsControl(x)))
                ErrorMessage.Text = "Вы заполнили не все поля";
            else
            {
                try
                {
                    job.InsertQuery(NameInput.Text);
                    updated();
                }
                catch (Exception) { ErrorMessage.Text = "Вы ввели неверные значения"; }
            }
        }

        private void UpdateButton_Click(object sender, RoutedEventArgs e)
        {
            if (Display.SelectedItem != null)
            {
                if (string.IsNullOrWhiteSpace(NameInput.Text) || NameInput.Text.Any(x => char.IsControl(x)))
                    ErrorMessage.Text = "Вы заполнили не все поля";
                else
                {
                    var item = Display.SelectedItem as DataRowView;
                    try
                    {
                        job.UpdateQuery(NameInput.Text, (int)item[0]);
                        updated();
                    }
                    catch (Exception) { ErrorMessage.Text = "Вы ввели неверные значения"; }
                }
            }
        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            var item = Display.SelectedItem as DataRowView;
            if (Display.SelectedItem != null)
            {
                job.DeleteQuery((int)item[0]);
                updated();
            }
            else
                ErrorMessage.Text = "Вы не выбрали элемент!";
        }
        private void updated()
        {
            Display.ItemsSource = null;
            Display.ItemsSource = job.GetData();
            ErrorMessage.Text = String.Empty;
        }

        private void InsertJson_Click(object sender, RoutedEventArgs e)
        {
            List<JobTitleModel> forimport = Converter.DeserializeObject<List<JobTitleModel>>();
            if (forimport != null)
            {
                foreach (var item in forimport)
                    job.InsertQuery(item.Name);
            }
        }
    }
}
