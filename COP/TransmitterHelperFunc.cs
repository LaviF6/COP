using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using FastBitmapLib;

using Pixel = System.Drawing.Color;

namespace COP
{
    public partial class Transmitter
    {

        private bool TestAndReSizeTheSizes(Size newSize)
        {
            if (_current_window_size != newSize)
            {
                _form.Size = newSize;
                _board.Size = newSize;
                _image = new Bitmap(newSize.Width, newSize.Height);
                _fastImage = new FastBitmap(_image);
                _current_window_size = newSize;
                return true;
            }
            else
            {
                return false;
            }
        }

        private void read_file(string file_path)
        {
            _file_data = new byte[2][];
            String file_name = Path.GetFileName(file_path);
            _file_data[_defines.FILE_NAME] = Encoding.ASCII.GetBytes(file_name);
            _file_data[_defines.DATA] = File.ReadAllBytes(file_path);
        }

        private void launch()
        {
            _board.Image = _image;
        }

        private Pixel new_pixel(byte r, byte g, byte b)
        {
            return Pixel.FromArgb(r, g, b);
        }

        private byte next_byte(int from)
        {
            byte ret = _defines.defult;
            if(from < 2)
            {
                if (_file_indexs[from] < _file_data[from].Length)
                {
                    ret = _file_data[from][_file_indexs[from]];
                    _file_indexs[from] += 1;
                }
                else if (_file_indexs[from] == _file_data[from].Length)
                {
                    _step_status++;
                }
            }
            return ret;
        }

        private byte get_index()
        {
            return _message_index++;
        }

        private byte[] number_to_byte_array(long number, int arraySize)
        {
            byte[] arr = new byte[arraySize];
            for (int i = 0; i < arraySize; i++)
            {
                arr[i] = (byte)(number >> (8 * i) & 0xff);
            }
            return arr;
        }
         private void exitProg()
        {
            _timer.Enabled = false;
            Application.Exit();
        }
    }
}
