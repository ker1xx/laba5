using laba5.glubglob;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace laba5
{
    /// <summary>
    /// Логика взаимодействия для App.xaml
    /// </summary>
    public partial class App : Application
    {
        public App()
        {
            laba5.Properties.Settings.Default.notebook = globalconstants.connection;
            laba5.Properties.Settings.Default.Save();
        }
    }
}
