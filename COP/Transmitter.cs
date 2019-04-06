using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace COP
{
    public partial class Transmitter
    {
        ////////////////////////////////////
        ////// Statement of variables //////
        ////////////////////////////////////
        //App
        private Form1 _form;

        //Space on screen
        private PictureBox _board;

        //Array of pixels
        private Bitmap _image;

        //The file name and data
        private byte[][] _file_data;

        //Timer
        private Timer _timer;

        //Defines class
        private Defines _defines;

        //Index of the stage in the program
        private int _program_status;

        //Index of the action in the sub-stage
        private int _step_status;

        //Message index
        private byte _message_index;

        //Index of the file name in the file text
        private byte[] _file_indexs;
  
        //Displays the size of the current window
        private Size _current_window_size;
        ///////////////////////////////////////
        ///////////////////////////////////////

        public Transmitter(Form1 form)
        {
            _defines = new Defines();

            _form = form;
            _form.Location = new Point(0, 0);
            _form.FormBorderStyle = FormBorderStyle.None;

            _board = new PictureBox();
            _board.Location = new Point(0, 0);
            _form.Controls.Add(_board);

            _current_window_size = new Size(0, 0);

            _timer = new Timer();
            _timer.Interval = 100;
            _timer.Tick += timerTick;
        }

        public void start(string file_path)
        {
            _program_status = 0;
            _step_status = 0;
            _message_index = 0;

            _file_indexs = new byte[]{0, 0};

            read_file(file_path);

            _timer.Enabled = true;
        }

        private void timerTick(object sender, EventArgs e)
        {
            switch (_program_status)
            {
                case 0:
                    broadcast_signaling();
                    break;
                case 1:
                    broadcast_header();
                    break;
                case 2:
                    broadcast_message();
                    break;
                case 3:
                    _timer.Enabled = false;
                    break;
            }
        }

        private void broadcast_signaling()
        {
            TestAndReSizeTheSizes(_defines.Contact_Window_Size);

            if (_step_status == 6)
            {
                _program_status++;
            }
            else
            {
                Color c = _step_status % 2 == 0 ?
                    new_pixel(0, 0, 255) :
                    new_pixel(0, 255, 0);

                _image.SetPixel(0, 0, c);

                launch();
            }
            _step_status++;
        }

        private void broadcast_header()
        {
            TestAndReSizeTheSizes(_defines.Header_Window_Size);

            _image.SetPixel(0, 0, new_pixel(
                    get_index(),
                    (byte)_defines.Broadcast_Window_Size.Height,
                    (byte)_defines.Broadcast_Window_Size.Width));

            long file_name_length = _file_data[_defines.FILE_NAME].Length;
            byte[] name_lenght_arr = number_to_byte_array(file_name_length, 3);

            _image.SetPixel(1, 0, new_pixel(
                name_lenght_arr[0],
                name_lenght_arr[1],
                name_lenght_arr[2]));

            long data_length = _file_data[_defines.DATA].Length;
            byte[] data_arr = number_to_byte_array(data_length, 6);

            _image.SetPixel(0, 1, new_pixel(
                data_arr[0],
                data_arr[1],
                data_arr[2]));

            _image.SetPixel(1, 1, new_pixel(
                data_arr[3],
                data_arr[4]
                , data_arr[5]));

            launch();
            _program_status++;
        }

        private void broadcast_message()
        {
            /*
            if (_step_status == 0)
            {
                for (int x = 0; x < _current_window_size.Width; x++)
                {
                    for (int y = 0; y < _current_window_size.Height; y++)
                    {
                        _image.SetPixel(x, y, new_pixel(
                            _file_name[_name_index++],
                            _file_name[_name_index++],
                            _file_name[_name_index++]));
                    }
                }
            }
            else
            {

            }

            /*
            int imageAmount = _message.Length /
                (_defines.Broadcast_Window_Size.Width * _defines.Broadcast_Window_Size.Height);
            for (int imageIndex = 0; imageIndex < imageAmount; imageIndex++)
            {
                for(int x = 0; x < _defines.Broadcast_Window_Size.Width; x++)
                {
                    for (int y = 0; y < _defines.Broadcast_Window_Size.Height; y++)
                    {
                        _image.SetPixel(x, y, pixel(
                            nextMassageByte(),
                            nextMassageByte(),
                            nextMassageByte()));
                    }
                }
                launch();
            }
            */
        }
    }
}