using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Diagnostics;
using System.Media;

namespace MobileWizard
{
    public partial class AutoScreen : Form
    {
        public AutoScreen()
        {
            InitializeComponent();
        }

        public int numberCatalog = 0;
        public int numberScreen = 0;
        public bool isWorked = false;

        private bool isImgEqual(Bitmap img1, Bitmap img2)
        {
           // bool result = true;
            for (int i = (img1.Width/2); i<img1.Width; i++)
            {
                for (int j = 0; j<img1.Height; j++)
                {
                    if (img1.GetPixel(i, j) != img2.GetPixel(i, j))
                    {
                        return false;
                    }
                }
            }
            return true;
        }
        private void _pause(int value)
        {
            Stopwatch sw = new Stopwatch();
            sw.Start();
            while (sw.ElapsedMilliseconds < value)
                Application.DoEvents();
        }

        private void ButtonStartScreen_Click(object sender, EventArgs e)
        {
            try
            {
                if (buttonStartScreen.Text == "Начать")
                {
                    Process deviceProcess = new Process();
                    deviceProcess.StartInfo.FileName = "adb.exe";
                    deviceProcess.StartInfo.Arguments = "devices";
                    deviceProcess.StartInfo.CreateNoWindow = true;
                    deviceProcess.StartInfo.UseShellExecute = false;
                    deviceProcess.StartInfo.RedirectStandardInput = true;
                    deviceProcess.StartInfo.RedirectStandardOutput = true;
                    deviceProcess.StartInfo.RedirectStandardError = true;
                    deviceProcess.Start();
                    _pause(1000);
                    string answerDevice;
                    answerDevice = deviceProcess.StandardOutput.ReadToEnd();
                    //MessageBox.Show(answerDevice.Substring(answerDevice.Length - 10));
                    if (answerDevice.Substring(answerDevice.Length - 10).Trim() == "device")
                    {
                        buttonStartScreen.Text = "Прервать";
                        isWorked = false;
                        bool isSucsess = true;
                        List<string> listCommands = new List<string>();
                        string rootpath = saveTextBox.Text;
                        numberCatalog = Directory.GetDirectories(rootpath).Length + 1;
                        numberScreen = 1;
                        string path = rootpath + "/" + numberCatalog.ToString();
                        Directory.CreateDirectory(path);
                        while (!isWorked)
                        {
                            listCommands.Add(@"shell screencap -p /sdcard/screen" + numberScreen.ToString() + ".png");
                            listCommands.Add(@"pull /sdcard/screen" + numberScreen.ToString() + ".png " + rootpath + "/" + numberCatalog.ToString());
                            listCommands.Add(@"shell rm /sdcard/screen" + numberScreen.ToString() + ".png");
                            if (numberScreen > 2)
                            {
                                try
                                {
                                    if (checkBox1.Checked)
                                    {
                                        isWorked = isImgEqual((Bitmap)Bitmap.FromFile(path + "\\screen" + (numberScreen - 2).ToString() + ".png"),
                                            (Bitmap)Bitmap.FromFile(path + "\\screen" + (numberScreen - 1).ToString() + @".png"));
                                    }
                                }
                                catch
                                {
                                    MessageBox.Show(@"Возникла ошибка при создании скриншота. Приложение будет закрыто");
                                    isWorked = true;
                                    isSucsess = false;
                                };
                            };
                            if (radioButton1.Checked) //down
                            {
                                listCommands.Add(@"shell input swipe 300 1000 300 " + (1000 - int.Parse(textBox1.Text)).ToString());
                            };
                            if (radioButton2.Checked) //up
                            {
                                listCommands.Add(@"shell input swipe 300 300 300 " + (300 + int.Parse(textBox1.Text)).ToString());
                            }
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
                            listCommands.Clear();
                            numberScreen += 1;
                        }
                        if (isSucsess) { MessageBox.Show("Успешно!"); } else { MessageBox.Show("Неудача!"); };
                        isWorked = false;
                    }
                    else
                    {
                        MessageBox.Show("Не обнаружено подключенное в режиме \"Отладка по USB\" устройство! Пожалуйста, проверьте подключение.");
                    }
                }
                else
                {
                    isWorked = true;
                    buttonStartScreen.Text = "Начать";
                }
            }
            catch
            {

            }
        }
        private void SaveButton_Click(object sender, EventArgs e)
        {
            if (folderBrowserDialog1.ShowDialog() == DialogResult.OK)
            {
                saveTextBox.Text = folderBrowserDialog1.SelectedPath;
            }
        }
    }
}
