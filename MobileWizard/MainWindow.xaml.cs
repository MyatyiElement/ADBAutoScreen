using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
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

namespace MobileWizard
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            List<string> listCommands = new List<string>();
            listCommands.Add("cd c:/adb/screenshots");
             listCommands.Add("/T 0.4");
            listCommands.Add("Set sum=0");
             listCommands.Add("/T 0.4");
            listCommands.Add("For %%A in (*.*) Do Set /a sum=sum+1");
             listCommands.Add("/T 0.4");
            listCommands.Add("cd c:/adb");
             listCommands.Add("/T 0.4");
            listCommands.Add("adb shell screencap -p /sdcard/screen%sum%.png");
             listCommands.Add("/T 0.4");
            listCommands.Add("adb pull /sdcard/screen%sum%.png c:/adb/Screenshots");
             listCommands.Add("/T 0.4");
            listCommands.Add("adb shell rm /sdcard/screen%sum%.png");
             listCommands.Add("/T 0.4");
            listCommands.Add("set sum=0");
             listCommands.Add("/T 0.4");
            File.WriteAllLines("temp.bat", listCommands);
            Process screenProcess = new Process();
                screenProcess.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
                screenProcess.StartInfo.FileName = "temp.bat";
                screenProcess.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
                screenProcess.StartInfo.CreateNoWindow = true;
            screenProcess.Start();
            listCommands.Clear();
            //File.Delete("temp.bat");
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            Backup ba = new Backup();
            ba.Owner = this;
            ba.ShowDialog();
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            AutoScreen auts = new AutoScreen();
            auts.ShowDialog();
        }

        public static implicit operator Form(MainWindow v)
        {
            throw new NotImplementedException();
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            Environment.Exit(0);
        }

        private void Button_Click_4(object sender, RoutedEventArgs e)
        {
            AutoScreen auts = new AutoScreen();
            auts.ShowDialog();
        }
    }
}
