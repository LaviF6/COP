using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using FastBitmapLib;


namespace COP
{
    public abstract class COPProtocol
    {
        protected COPProtocol(Form1 form)
        {
            _defines = new Defines();

            _form = form;
            _form.FormBorderStyle = FormBorderStyle.None;

            _form.Location = new Point(0, 0);
            _form.Size = new Size(0, 0);
            _form.BackColor = Color.Black;

            _timer = new Timer();
            _timer.Interval = _defines.timer_interval;
            _timer.Tick += timerTick;
        }

        protected abstract void timerTick(object sender, EventArgs e);

        //App
        protected Form1 _form;

        //Array of pixels
        protected Bitmap _image;
        protected FastBitmap _fastImage;

        //The file name and data
        protected byte[][] _file_data;

        //Timer
        protected Timer _timer;

        //Defines class
        protected Defines _defines;

        //Index of the stage in the program
        protected int _program_status;

        //Index of the action in the sub-stage
        protected int _step_status;

        //Displays the size of the current window
        protected Size _current_window_size;

        protected class Defines
        {
            public int FILE_NAME = 0;
            public int DATA = 1;

            public int timer_interval = 1000;

            public byte Contact_sign = 255;
            public byte Zero = 0;
            public byte defult = 0;

            public int TimeTime_Between_Frames = 1;

            public Size Contact_Window_Size = new Size(1, 1);
            public Size Header_Window_Size = new Size(2, 2);
            public Size Broadcast_Window_Size = new Size(10, 10);

            public CodedPixel[] Agreed_Mark = new CodedPixel[2] { new CodedPixel(0, 255, 0), new CodedPixel(0, 255, 0) };
        }

        protected void exitProg()
        {
            _timer.Enabled = false;
            Application.Exit();
        }
    }
}
