﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using FastBitmapLib;

using Pixel = System.Drawing.Color;

namespace COP
{
    public partial class Transmitter : COPProtocol
    {
        //Space on screen
        private PictureBox _board;

        //Index of the file name in the file text
        private ulong[] _file_indexs;

        byte _index;

        public Transmitter(Form1 form) : base(form)
        {
            _board = new PictureBox();
            _board.Location = new Point(0, 0);
            _form.Controls.Add(_board);

            _current_window_size = new Size(0, 0);
        }

        public void send(string file_path)
        {
            _program_status = 0;
            _step_status = 0;
            _index = 0;

            _file_indexs = new ulong[]{0, 0};

            read_file(file_path);

            _timer.Enabled = true;
        }

        protected override void timerTick(object sender, EventArgs e)
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
                    exitProg();
                    break;
            }
        }
        
        private void broadcast_signaling()
        {
            TestAndReSizeTheSizes(_defines.Contact_Window_Size);

            Pixel mark;
            if(_step_status % 2 == 0)
            {
                mark = new_pixel(_index++, _defines.Agreed_Mark[0,0], _defines.Agreed_Mark[0,1]);
            }
            else
            {
                mark = new_pixel(_index++, _defines.Agreed_Mark[1, 0], _defines.Agreed_Mark[1, 1]);
            }

            _image.SetPixel(0, 0, mark);

            launch();

            _step_status++;

            if (_step_status == 5)
            {
                _program_status++;
                _step_status = 0;
            }
        }

        private void broadcast_header()
        {
            TestAndReSizeTheSizes(_defines.Header_Window_Size);

            _fastImage.Lock();

            _fastImage.SetPixel(0, 0, new_pixel(
                    _index++,
                    (byte)_defines.Broadcast_Window_Size.Height,
                    (byte)_defines.Broadcast_Window_Size.Width));

            long file_name_length = _file_data[_defines.FILE_NAME].Length;
            byte[] name_lenght_arr = number_to_byte_array(file_name_length, 3);

            _fastImage.SetPixel(1, 0, new_pixel(
                name_lenght_arr[0],
                name_lenght_arr[1],
                name_lenght_arr[2]));

            long data_length = _file_data[_defines.DATA].Length;
            byte[] data_arr = number_to_byte_array(data_length, 6);

            _fastImage.SetPixel(0, 1, new_pixel(
                data_arr[0],
                data_arr[1],
                data_arr[2]));

            _fastImage.SetPixel(1, 1, new_pixel(
                data_arr[3],
                data_arr[4],
                data_arr[5]));

            _fastImage.Unlock();
            launch();
            _program_status++;
            _step_status = 0;
        }

        private void broadcast_message()
        {
            TestAndReSizeTheSizes(_defines.Broadcast_Window_Size);

            if (_step_status == 2)
            {
                _program_status++;
            }

            _fastImage.Lock();
            for (int y = 0; y < _current_window_size.Height; y++)
            {
                for (int x = 0; x < _current_window_size.Width; x++)
                {
                    byte r;
                    if (x == 0 && y == 0)
                        r = _index++;
                    else
                        r = next_byte(_step_status);

                    byte g = next_byte(_step_status);
                    byte b = next_byte(_step_status);

                    Pixel c = new_pixel(r, g, b);

                    _fastImage.SetPixel(x, y, c);
                         
                }
            }
            _fastImage.Unlock();

            launch();
        }
    }
}