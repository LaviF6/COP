using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace COP
{
    public class Transmitter
    {

        byte _message_index;
        byte[] _message;
        Bitmap _image;
        PictureBox _board;
        Form1 _form;

        Defines _defines;

        public Transmitter(Form1 form)
        {
            _defines = new Defines();

            _form = form;
            _form.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;

            _board = new PictureBox();
            _board.Location = new Point(0, 0);

            resize(_defines.Contact_Window_Size); 
        }

        public class Defines
        {
            public byte Contact_sign = 255;
            public byte Zero = 0;
            

            public int TimeTime_Between_Frames = 1;

            public Size Broadcast_Window_Size = new Size(10, 10);
            public Size Contact_Window_Size = new Size(1, 1);
            public Size Header_Window_Size = new Size(2, 1);
        }

        public void start(string path)
        {
            _message_index = 0;

            read_file(path);
            broadcast_signaling();
            resize(_defines.Header_Window_Size);
            broadcast_header();
            resize(_defines.Broadcast_Window_Size);
            broadcast_message();
        }

        private void resize(Size newSize)
        {
            _form.Size = newSize;
            _board.Size = newSize;
            if(_image != null)
                _image.Dispose();
            _image = new Bitmap(newSize.Height, newSize.Width);
        }

        private void read_file(string path)
        {
            _message = File.ReadAllBytes(path);
        }

        private void launch()
        {
            _board.Image = _image;
            Thread.Sleep(_defines.TimeTime_Between_Frames);
        }

        private Color pixel(byte r, byte g, byte b)
        {
            return Color.FromArgb(r, g, b);
        }

        private byte index()
        {
            return _message_index++;
        }

        private void broadcast_signaling()
        {
            for(byte i = 0; i < 6; i++)
            {
                _image.SetPixel(
                    0,
                    0,
                    pixel(
                        index(),
                        _defines.Zero,
                        _defines.Contact_sign));

                launch();
            }
        }

        private void broadcast_header()
        {
            _image.SetPixel(
                0,
                0,
                pixel(
                    index(),
                    (byte)_defines.Broadcast_Window_Size.Height,
                    (byte)_defines.Broadcast_Window_Size.Width));

            //int totalBytes = _message.Length;
            //byte a = (byte)(totalBytes % 1000);
            //byte b = 

            /*
            _image.SetPixel(
                1,
                0,
                pixel(
                    ,
                    ,
                    ;
            launch();
            launch();
            launch();
            */
        }

        private void broadcast_message()
        {

        }
    }
}
