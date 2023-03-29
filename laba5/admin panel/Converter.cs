using Microsoft.Win32;
using Newtonsoft.Json;
using System.IO;

namespace laba5.admin_panel
{
    public class Converter
    {
        public static T DeserializeObject<T>()
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Filter = "json files (*.json) |*.json";
            if (dialog.ShowDialog() == true)
            {
                string json = File.ReadAllText(dialog.FileName);
                T obj = JsonConvert.DeserializeObject<T>(json);
                return obj;
            }
            else
                return default(T);
        }
    }
}
