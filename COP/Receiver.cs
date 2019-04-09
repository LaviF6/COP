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
        ulong _message_index;

        public Receiver(Form1 form) : base(form)
        {
            _image = new Bitmap(0, 0);
            g = Graphics.FromImage(_image);
            _current_window_size = _defines.Contact_Window_Size;
            _file_size = new ulong[2];
        }

        public void start()
        {
            _step_status = 0;
            _program_status = 0;
            _message_index = 0;
            _timer.Enabled = true;
            collecting_point = new Point(0, 0);
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
            collecting_point = new Point(0,0);
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
