using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.CodeDom.Compiler;

namespace WindowsWeatherChecker
{
    public partial class WeatherChecker : Form
    {
        string APIKEY = "";
        string City = "";
        double temp = 0;
        string conditions = "";
        public WeatherChecker()
        {
            InitializeComponent();
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            label1.Text = "City/Country: ";
            button1.Text = "Check";
            label2.Text = "Temp: ";
            label3.Text = "Conditions: ";
            label4.Hide();
            label5.Hide();

            readConfig();




        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(City))
               runChecker(APIKEY);
              else
            {
                MessageBox.Show("Please enter a city!");
            }
        }

        public void runChecker(string APIKEY)
        {
            
            
                ArrayList info = new ArrayList();
                Process checker = new Process();
                checker.StartInfo.FileName = "java";
                checker.StartInfo.Arguments = "-jar WeatherApp.jar " + APIKEY + " " + City;
                checker.StartInfo.UseShellExecute = false;
                checker.StartInfo.RedirectStandardOutput = true;
                checker.StartInfo.CreateNoWindow = true;
                checker.Start();

                string CurrentLine;
                while ((CurrentLine = checker.StandardOutput.ReadLine()) != null)
                {
                    info.Add(CurrentLine);
                }
                checker.WaitForExit();
                temp = double.Parse(info[0].ToString(), System.Globalization.CultureInfo.InvariantCulture);
                conditions = (string)info[1];
                label4.Text = temp.ToString() + "°C";
                label5.Text = conditions;
                label4.Show();
                label5.Show();
            
          

        }
        public void readConfig()
        {
            try
            {
                String filePath = "apikey.txt";
                String fileContent = File.ReadAllText(filePath);
                APIKEY = fileContent;
            }
            catch (Exception ex)
            {
                Application.Exit();
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            City = textBox1.Text;
        }
    }
}
