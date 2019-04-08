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
    public partial class Transmitter
    {
        public class Defines
        {
            public int FILE_NAME = 0;
            public int DATA = 1;

            public int timer_interval = 16;

            public byte Contact_sign = 255;
            public byte Zero = 0;
            public byte defult = 255;

            public int TimeTime_Between_Frames = 1;

            public Size Contact_Window_Size = new Size(1, 1);
            public Size Header_Window_Size = new Size(2, 2);
            public Size Broadcast_Window_Size = new Size(10, 10);
        }
    }
}
