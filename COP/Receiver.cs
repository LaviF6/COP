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
    public class Receiver
    {
        public Receiver()
        {
            _defines = new Defines();

            _form = form;
            _form.FormBorderStyle = FormBorderStyle.None;

            _form.Location = new Point(0, 0);
            _form.Size = new Size(0, 0);
            _form.BackColor = Color.Black;

            _board = new PictureBox();
            _board.Location = new Point(0, 0);
            _form.Controls.Add(_board);

            _current_window_size = new Size(0, 0);

            _timer = new Timer();
            _timer.Interval = _defines.timer_interval;
            _timer.Tick += timerTick;
        }

        public bool start()
        {
            return true;
        }
    }
}
