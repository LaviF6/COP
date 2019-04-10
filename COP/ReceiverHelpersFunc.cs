using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace COP
{
    public partial class Receiver : COPProtocol
    {
        private ulong bytes_to_number(List<byte> bytes)
        {
            return 0;
        }

        private void pixel_analysis(int x, int y)
        {
            Color pixel = _fastImage.GetPixel(x, y);
            if(pixel == _defines.Agreed_Mark[0] || pixel == _defines.Agreed_Mark[1])
            {
                _coded_screen_pixels[x,y].
            }
            _coded_screen_pixels[x, y].setPixel(_fastImage.GetPixel(x, y));
        }
    }
}
