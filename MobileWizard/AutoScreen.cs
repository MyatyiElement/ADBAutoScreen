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
            bool result = true;
            for (int i = (img1.Width/2); i<img1.Width; i++)
            {
                for (int j = 0; j<img1.Height; j++)
                {
                    if (img1.GetPixel(i, j) != img2.GetPixel(i, j))
                    {
                        result = false;
                    }
                }
            }
            return result;
        }
       /* private void playSimpleSound()
        *{
        *    SoundPlayer simpleSound = new SoundPlayer(@"C:\adb\screenshots\1.wav");
        *    simpleSound.Play();
        *}
       */
        private void _pause(int value)
        {
            Stopwatch sw = new Stopwatch();
            sw.Start();
            while (sw.ElapsedMilliseconds < value)
                Application.DoEvents();
        }

        private void ButtonStartScreen_Click(object sender, EventArgs e)
        {
            if (buttonStartScreen.Text == "Начать")
            {
                buttonStartScreen.Text = "Прервать";
                isWorked = false;
                bool isSucsess = true;
                List<string> listCommands = new List<string>();
                numberCatalog = Directory.GetDirectories(@"C:\adb\screenshots\auto\").Length + 1;
                numberScreen = 1;
                string path = @"C:\adb\screenshots\auto\" + numberCatalog.ToString();
                Directory.CreateDirectory(path);
                listCommands.Add("cd c:/adb");
                while (!isWorked)
                {
                    listCommands.Add("adb shell screencap -p /sdcard/screen" + numberScreen.ToString() + ".png");
                    listCommands.Add("/T 0.4");
                    listCommands.Add("adb pull /sdcard/screen" + numberScreen.ToString() + ".png " + "c:/adb/Screenshots/auto/" + numberCatalog.ToString());
                    listCommands.Add("/T 0.4");
                    listCommands.Add("adb shell rm /sdcard/screen" + numberScreen.ToString() + ".png");
                    listCommands.Add("/T 0.4");
                    if (numberScreen > 2)
                    {
                        try
                        {
                            if (checkBox1.Checked)
                            {
                                isWorked = isImgEqual((Bitmap)Bitmap.FromFile(path + "\\screen" + (numberScreen - 2).ToString() + ".png"), (Bitmap)Bitmap.FromFile(path + "\\screen" + (numberScreen - 1).ToString() + @".png"));
                            }
                        }

                        catch
                        {
                            MessageBox.Show("Возникла ошибка при создании скриншота. Приложение будет закрыто");
                            isWorked = true;
                            isSucsess = false;
                        };
                    };
                    if (radioButton1.Checked)
                    {
                        listCommands.Add("adb shell input swipe 300 1000 300 " + (1000 - int.Parse(textBox1.Text)).ToString());
                        listCommands.Add("/T 0.4");
                    };
                    if (radioButton2.Checked)
                    {
                        listCommands.Add("adb shell input swipe 300 300 300 " + (300 + int.Parse(textBox1.Text)).ToString());
                        listCommands.Add("/T 0.4");
                    }
                    File.WriteAllLines("temp.bat", listCommands);
                    Process screenProcess = new Process();
                    screenProcess.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
                    screenProcess.StartInfo.FileName = "temp.bat";
                    screenProcess.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
                    screenProcess.StartInfo.CreateNoWindow = true;
                    screenProcess.Start();
                    listCommands.Clear();
                    if (numberScreen == 1) { _pause(5000); } else { _pause(4000); };
                    numberScreen += 1;
                }
                //playSimpleSound();
                if (isSucsess) { MessageBox.Show("Успешно!"); } else { MessageBox.Show("Неудача!"); };
                isWorked = false;
                File.Delete("temp.bat");
            }
            else
            {
                isWorked = true;
                buttonStartScreen.Text = "Начать";

            }
        }
    }
}
