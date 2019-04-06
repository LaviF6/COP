using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using FastBitmapLib;

namespace Look_On_Corner
{
    public partial class Form1 : Form
    {
        int zoom;
        Size sizeToCopy;
        Bitmap start, end;
        PictureBox pb;
        FastBitmap endFB, startFB;
        Graphics g;
        bool isOK;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            string[] args = Environment.GetCommandLineArgs();
            if(args.Length == 2)
            {
                isOK = true;
                FormBorderStyle = FormBorderStyle.None;
                zoom = Int32.Parse(args[1]);
                sizeToCopy = new Size(10, 10);
                timer1.Interval = 16;

                start = new Bitmap(sizeToCopy.Height, sizeToCopy.Width);
                end = new Bitmap(sizeToCopy.Height * zoom, sizeToCopy.Width * zoom);

                startFB = new FastBitmap(start);
                endFB = new FastBitmap(end);

                Size = new Size(sizeToCopy.Height * zoom, sizeToCopy.Width * zoom);
                Location = new Point(700, 0);

                pb = new PictureBox();
                pb.Size = new Size(end.Size.Width, end.Size.Height);
                pb.Location = new Point(0, 0);
                Controls.Add(pb);

                g = Graphics.FromImage(start);
            }
            else
            {
                isOK = false;
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if(isOK)
            {
                g.CopyFromScreen(0, 0, 0, 0, sizeToCopy);

                endFB.Lock();
                startFB.Lock();
                for (int xStart = 0; xStart < sizeToCopy.Width; xStart++)
                {
                    for (int yStart = 0; yStart < sizeToCopy.Height; yStart++)
                    {
                        for (int xEnd = xStart * zoom; xEnd < (xStart + 1) * zoom; xEnd++)
                        {
                            for (int yEnd = yStart * zoom; yEnd < (yStart + 1) * zoom; yEnd++)
                            {
                                endFB.SetPixel(xEnd, yEnd, startFB.GetPixel(xStart, yStart));
                            }
                        }
                    }
                }

                endFB.Unlock();
                startFB.Unlock();
                TopMost = true;
                TopMost = false;

                pb.Image = end;
            }
        }
    }
}
