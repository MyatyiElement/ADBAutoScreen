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
            
            if (Properties.Settings.Default.singlePath != "") { textBox.Text = Properties.Settings.Default.singlePath; }
            else { textBox.Text = Directory.GetCurrentDirectory(); }

        }
        private void _pause(int value)
        {
            Stopwatch sw = new Stopwatch();
            sw.Start();
            while (sw.ElapsedMilliseconds < value)
                System.Windows.Forms.Application.DoEvents();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            List<string> listCommands = new List<string>();
            int numberScreen = Directory.GetFiles(textBox.Text, "*.png").Length+1;
            listCommands.Add("shell screencap -p /sdcard/screen" + numberScreen.ToString()+".png");
            listCommands.Add("pull /sdcard/screen" + numberScreen.ToString() + ".png "+textBox.Text);
            listCommands.Add("shell rm /sdcard/screen" + numberScreen.ToString() + ".png");
            foreach (string str in listCommands)
            {
                Process screenProcess = new Process();
                screenProcess.StartInfo.FileName = "adb.exe";
                screenProcess.StartInfo.Arguments = str;
                screenProcess.StartInfo.CreateNoWindow = true;
                screenProcess.StartInfo.UseShellExecute = false;
                screenProcess.StartInfo.RedirectStandardInput = true;
                screenProcess.StartInfo.RedirectStandardOutput = true;
                screenProcess.StartInfo.RedirectStandardError = true;
                screenProcess.Start();
                _pause(1500);
            }
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

        private void Button_Click_5(object sender, RoutedEventArgs e)
        {
            System.Windows.Forms.FolderBrowserDialog openFileDlg = new System.Windows.Forms.FolderBrowserDialog();
            openFileDlg.SelectedPath = textBox.Text;
            if (openFileDlg.ShowDialog().ToString() == "OK")
            {
                textBox.Text = openFileDlg.SelectedPath;
                Properties.Settings.Default.singlePath = textBox.Text;
                Properties.Settings.Default.Save();
            }
        }
    }
}
