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

namespace MobileWizard
{
    /// <summary>
    /// Логика взаимодействия для Backup.xaml
    /// </summary>
    public partial class Backup : Window
    {
        public bool isApk = false;
        public bool isSystem = false;
        public Backup()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            string command = "adb backup -obb -shared -all";
            if (isApk)
            {
                command += "-apk";
            };
            if (isSystem)
            {
                command += "-system";
            };
            System.Diagnostics.Process.Start("cmd.exe");
            MessageBox.Show(command);
        }

        private void CheckBox_Checked(object sender, RoutedEventArgs e)
        {
            isApk = true;
        }

        private void CheckBox_Checked_1(object sender, RoutedEventArgs e)
        {
            isSystem = true;
        }

        private void CheckBox_Unchecked(object sender, RoutedEventArgs e)
        {
            isApk = false;
        }

        private void CheckBox_Unchecked_1(object sender, RoutedEventArgs e)
        {
            isSystem = false;
        }
    }
}
