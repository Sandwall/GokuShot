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
    public partial class Goku : Form
    {
        public const int WM_NCLBUTTONDOWN = 0xA1;
        public const int HT_CAPTION = 0x2;

        [System.Runtime.InteropServices.DllImport("user32.dll")]
        public static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);
        [System.Runtime.InteropServices.DllImport("user32.dll")]
        public static extern bool ReleaseCapture();

        Point[] points;
        public Goku()
        {
            InitializeComponent();
            points = new Point[3] { new Point(0, 0), new Point(this.Size.Width, 0), new Point(0, this.Size.Height) };
            //Refresh();
        }

        private void Goku_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                ReleaseCapture();
                SendMessage(Handle, WM_NCLBUTTONDOWN, HT_CAPTION, 0);
            }
        }

        public void GokuBeginCapture()
        {
            pictureBox1.Dock = DockStyle.None;
            pictureBox1.Location = new Point(0, 32);
            pictureBox1.Size = new Size(this.Size.Width, this.Size.Height - 32);
        }

        public void GokuEndCapture()
        {
            pictureBox1.Dock = DockStyle.Fill;
        }

        public void RotateCW(bool rot)
        {
            switch(rot)
            {
                case true:
                    pictureBox1.Image.RotateFlip(RotateFlipType.Rotate90FlipNone);
                    break;
                case false:
                    pictureBox1.Image.RotateFlip(RotateFlipType.Rotate270FlipNone);
                    break;
            }
            pictureBox1.Refresh();
        }
    }
}
