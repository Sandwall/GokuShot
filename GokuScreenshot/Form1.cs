using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Windows.Forms;

namespace GokuScreenshot
{
    public partial class Form1 : Form
    {
        Goku goku;
        Bitmap recentBMP;

        public Form1()
        {
            InitializeComponent();
            if(Properties.Settings.Default.savedCaptureFolder == "defaultPathForMeToBeAbleToCheckInitializationSomethingSomething")
            {
                Properties.Settings.Default.savedCaptureFolder = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
                Properties.Settings.Default.Save();
            }

            this.TransparencyKey = Color.Magenta;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            goku = new Goku();
            goku.Show();
        }

        private void Form1_Resize(object sender, EventArgs e)
        {
            if (WindowState == FormWindowState.Minimized)
            {
                goku.WindowState = FormWindowState.Minimized;
            } else if (WindowState == FormWindowState.Normal)
            {
                goku.WindowState = FormWindowState.Normal;
            }
        }

        private void menuStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e) {
            switch (e.ClickedItem.Text)
            {
                case "Capture":
                    {
                        this.CaptureWindow();
                    }
                    break;
                case "Settings":
                    {
                        Settings s = new Settings();
                        s.ShowDialog();
                    } break;
                case "About":
                    {
                        About a = new About();
                        a.ShowDialog();
                    } break;
                case "Copy Recent Capture to Clipboard":
                    {
                        if(recentBMP != null)
                        {
                            Clipboard.SetImage(recentBMP);
                        }
                    } break;
                case "Reset Goku":
                    {
                        goku.ClientSize = new Size(200, 200);
                    } break;
            }
        }

        private void CaptureWindow()
        {
            Point gokuLoc = goku.Location;
            Point gokuLocChanged = goku.Location;
            Size gokuSize = goku.ClientSize;
            Size gokuSizeChanged = goku.ClientSize;

            gokuLocChanged.X += 8;  //The windows caption bar is 32 pixels tall
            gokuSizeChanged.Height += 32;

            goku.FormBorderStyle = FormBorderStyle.None;
            goku.Location = gokuLocChanged;
            goku.ClientSize = gokuSizeChanged;
            goku.GokuBeginCapture();
            goku.Refresh();
            //THIS DOESN'T WORK
            try
            {
                //These are just kind of hardcoded values, too lazy to figure out some other solution lol
                Rectangle rect = new Rectangle(this.Left + 13, this.Top + 59, this.Size.Width - 26, this.Size.Height - 135);
                Bitmap bmp = new Bitmap(rect.Width, rect.Height, PixelFormat.Format32bppArgb);
                Graphics g = Graphics.FromImage(bmp);
                g.CopyFromScreen(rect.Left, rect.Top, 0, 0, new Size(rect.Right, rect.Bottom), CopyPixelOperation.SourceCopy);
                recentBMP = bmp;
                string saveFilename = Properties.Settings.Default.savedCaptureFolder + "\\" + Properties.Settings.Default.savedCaptureName + ".png";
                bmp.Save(getNextFileName(saveFilename));
                MessageBox.Show("Screen Captured");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            goku.FormBorderStyle = FormBorderStyle.Sizable;
            goku.Location = gokuLoc;
            goku.ClientSize = gokuSize;
            goku.GokuEndCapture();
            goku.Refresh();
        }

        private string getNextFileName(string fileName)
        {
            string extension = Path.GetExtension(fileName);

            int i = 0;
            while (File.Exists(fileName))
            {
                if (i == 0)
                    fileName = fileName.Replace(extension, ++i + extension);
                else
                    fileName = fileName.Replace(i + extension, ++i + extension);
            }

            return fileName;
        }

        private void trackBar1_Scroll(object sender, EventArgs e)
        {
            goku.Opacity = trackBar1.Value / 50.0f;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            goku.RotateCW(true);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            goku.RotateCW(false);
        }
    }
}
