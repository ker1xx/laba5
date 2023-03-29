using laba5.admin_panel;
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
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TextBox;

namespace laba5
{
    /// <summary>
    /// Логика взаимодействия для Models.xaml
    /// </summary>
    public partial class Models : Page
    {
        modelTableAdapter model = new modelTableAdapter();
        public Models()
        {
            InitializeComponent();
            Display.ItemsSource = model.GetData();
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
            if (string.IsNullOrWhiteSpace(NameInput.Text))
                ErrorMessage.Text = "Вы заполнили не все поля";
            else
            {
                try
                {
                    model.InsertQuery(NameInput.Text);
                    updated();
                    AdditionalInfo();
                }
                catch (Exception) { ErrorMessage.Text = "Вы ввели неверные значения"; }
            }
        }

        private void UpdateButton_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(NameInput.Text))
                ErrorMessage.Text = "Вы заполнили не все поля";
            else
            {
                var item = Display.SelectedItem as DataRowView;
                try
                {
                    model.UpdateQuery(NameInput.Text, (int)item[0]);
                    updated();
                    AdditionalInfo();
                }
                catch (Exception) { ErrorMessage.Text = "Вы ввели неверные значения"; }
            }
        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            var item = Display.SelectedItem as DataRowView;
            if (Display.SelectedItem != null)
            {
                model.DeleteQuery((int)item[0]);
                updated();
                AdditionalInfo();
            }
            else
                ErrorMessage.Text = "Вы не выбрали элемент!";
        }
        private void updated()
        {
            Display.ItemsSource = model.GetData();
            ErrorMessage.Text = String.Empty;
        }
        private void AdditionalInfo()
        {
            Info1.Text = "Общее количество поставщиков: " + Display.Items.Count.ToString();
        }

        private void InsertJson_Click(object sender, RoutedEventArgs e)
        {
            List<ModelsModel> forimport = Converter.DeserializeObject<List<ModelsModel>>();
            if (forimport != null)
            {
                foreach (var item in forimport)
                    model.InsertQuery(item.Name);
            }
        }
    }
}
