using laba5.DataSet2TableAdapters;
using System;
using System.ComponentModel;
using System.Data;
using System.Windows.Controls;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TextBox;

namespace laba5
{
    /// <summary>
    /// Логика взаимодействия для storage.xaml
    /// </summary>
    public partial class storage : Page
    {
        storageTableAdapter storageData = new storageTableAdapter();
        AdminPanel AdminPanel;
        int AmountOfItems = 0;
        int FullFirstPrice = 0;
        public storage(AdminPanel adminPanel)
        {
            InitializeComponent();
            Display.ItemsSource = storageData.names();
            Display.IsReadOnly = true;
            AdminPanel = adminPanel;
            var info = storageData.GetData();
            foreach (DataRow data in info.Rows)
            {
                AmountOfItems += Convert.ToInt32(data[2]);
                FullFirstPrice += Convert.ToInt32(data[4]) * Convert.ToInt32(data[2]);
            }
/*            adminPanel.Info1.Text = "Общее количество товаров: " + AmountOfItems.ToString();
            adminPanel.Info2.Text = "Общая закупочная стоимость товаров: " + FullFirstPrice.ToString();*/
        }

        private void Display_AutoGeneratingColumn(object sender, DataGridAutoGeneratingColumnEventArgs e)
        {
            PropertyDescriptor propertyDescriptor = (PropertyDescriptor)e.PropertyDescriptor;
            e.Column.Header = propertyDescriptor.DisplayName;
            if (propertyDescriptor.DisplayName == "id_dealer" || propertyDescriptor.DisplayName == "id_model")
            {
                e.Cancel = true;
            }
        }
    }
}
