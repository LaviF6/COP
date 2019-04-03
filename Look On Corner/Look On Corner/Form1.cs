using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Look_On_Corner
{
    public partial class Form1 : Form
    {
        int zoom;
        Size sizeToCopy;
        Bitmap start, end;
        Graphics g;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            
            zoom = 10;
            sizeToCopy = new Size(10, 10);
            timer1.Interval = 100;

            start = new Bitmap(sizeToCopy.Height, sizeToCopy.Width);
            end = new Bitmap(sizeToCopy.Height * zoom, sizeToCopy.Width * zoom);
            this.Size = new Size(sizeToCopy.Height * zoom, sizeToCopy.Width * zoom);
            this.Location = new Point(700, 0);
            g = Graphics.FromImage(start);
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            g.CopyFromScreen(0, 0, 0, 0, sizeToCopy);

            for(int xStart = 0; xStart < sizeToCopy.Width; xStart++)
            {
                for (int yStart = 0; yStart < sizeToCopy.Height; yStart++)
                {
                    for (int xEnd = 0; xEnd < sizeToCopy.Width; xEnd++)
                    {
                        for (int yEnd = 0; yEnd < sizeToCopy.Height; yEnd++)
                        {
                            end.SetPixel((xStart * zoom) + xEnd, (yStart * zoom) + yEnd, start.GetPixel(xStart, yStart));
                        }
                    }
                }
            }

            TopMost = true;
            TopMost = false;

            pictureBox1.Image = end;
        }
    }
}
