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
    public class Receiver : COPProtocol
    {
        Point collecting_point;
        Graphics g;
        ulong[] _file_size;
        //Message index
        private ulong _message_index;
        private Size _window_size;
        private List<Pixel> _pixel_in_tracking;

        private class Pixel
        {
            public Pixel()
            {
                _chance_sign = 0;
            }
            private int _chance_sign { set; get; }
            private Point point { set; get; }
            private Color color { set; get; }
            public void reset()
            {
                _chance_sign = 0;
            }
            public void moreChance()
            {
                _chance_sign++;
            }
        }

        public Receiver(Form1 form) : base(form)
        {
            _image = new Bitmap(1,1);
            g = Graphics.FromImage(_image);
            _fastImage = new FastBitmap(_image);

            _current_window_size = _defines.Contact_Window_Size;
            _file_size = new ulong[2];
        }

        public void start()
        {
            _step_status = 0;
            _program_status = 0;
            _message_index = 0;
            _timer.Enabled = true;

            var screen_size = Screen.PrimaryScreen.WorkingArea;
            _window_size = new Size(screen_size.Width, screen_size.Height);
            _current_window_size = new Size(1, 1);
            _pixel_in_tracking = new List<Pixel>();
        }

        protected override void timerTick(object sender, EventArgs e)
        {
            g.CopyFromScreen(collecting_point, new Point(0,0), _current_window_size);
            _fastImage.Lock();

            switch (_program_status)
            {
                case 0:
                    findTransmitter();
                    break;
                case 1:
                    getConfiguration();
                    break;
                case 2:
                    getdata();
                    break;
                case 3:
                    exitProg();
                    break;
            }
            _fastImage.Unlock();
        }

        private void findTransmitter()
        {
            for(int x = 0; x < _window_size.Width; x++)
            {
                for(int y = 0; y < _window_size.Height; y++)
                {
                    Color c = _fastImage.GetPixel(x, y);
                    if ((c.G == _defines.Agreed_Mark[0,0] && c.B == _defines.Agreed_Mark[0,1]) ||
                        (c.G == _defines.Agreed_Mark[1, 0] && c.B == _defines.Agreed_Mark[1, 1]))
                    {
                        if()
                    }
                }
            }
        }

        private void getConfiguration()
        {
            _current_window_size = new Size(_fastImage.GetPixel(0, 0).G, _fastImage.GetPixel(0, 0).B);
            List<byte> bytes = new List<byte>();
            Color pixel = _fastImage.GetPixel(1, 0);
            bytes.Add(pixel.R);
            bytes.Add(pixel.G);
            bytes.Add(pixel.B);

            _file_size[0] = bytes_to_number(bytes);
            bytes.Clear();

            pixel = _fastImage.GetPixel(0, 1);
            bytes.Add(pixel.R);
            bytes.Add(pixel.G);
            bytes.Add(pixel.B);

            pixel = _fastImage.GetPixel(1, 1);
            bytes.Add(pixel.R);
            bytes.Add(pixel.G);
            bytes.Add(pixel.B);

            _file_size[1] = bytes_to_number(bytes); 
        }

        private ulong bytes_to_number(List<byte> bytes)
        {
            return 0;
        }

        private void getdata()
        {
            for (int y = 0; y < _current_window_size.Height; y++)
            {
                for (int x = 0; x < _current_window_size.Width; x++)
                {
                    int a = _fastImage.GetPixelInt(x,y);
                    for(int i = 0; i < 3; i++)
                    {
                        if(_message_index < _file_size[_step_status])
                        {
                            _file_data[_step_status][_message_index++] = (byte)(a >> (8 * i) & 0xff);
                        }
                    }
                }
            }
        }
    }
}
