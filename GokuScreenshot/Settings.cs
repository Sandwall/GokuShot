using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GokuScreenshot
{
    public partial class Settings : Form
    {
        public Settings()
        {
            InitializeComponent();
            textBox1.Text = Properties.Settings.Default.savedCaptureFolder;
            textBox2.Text = Properties.Settings.Default.savedCaptureName;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog fbd = new FolderBrowserDialog();
            fbd.RootFolder = Environment.SpecialFolder.Desktop;
            fbd.ShowNewFolderButton = true;
            fbd.ShowDialog();
            textBox1.Text = fbd.SelectedPath;
            Properties.Settings.Default.savedCaptureFolder = fbd.SelectedPath;
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            Properties.Settings.Default.savedCaptureFolder = textBox1.Text;
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            Properties.Settings.Default.savedCaptureName = textBox2.Text;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Properties.Settings.Default.Save();
            this.Close();
            this.Dispose();
        }
    }
}
