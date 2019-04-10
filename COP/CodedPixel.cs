using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace COP
{
    public class CodedPixel
    {
        public byte index, a, b;

        private bool _isEmpty;
        private int _compatibility_level;

        public CodedPixel()
        {
            _compatibility_level = 0;
            _isEmpty = true;
        }

        public CodedPixel(byte r, byte g, byte b)
        {
            _compatibility_level = 0;
            _isEmpty = true;
        }

        public void setPixel(Color c)
        {
            _isEmpty = false;
            index = c.R;
            a = c.G;
            b = c.B;
        }

        public static bool operator ==(Color c, CodedPixel p)
        {
            return p.a == c.G && p.b == c.B;
        }

        public static bool operator !=(Color c, CodedPixel p)
        {
            return p.a != c.G || p.b != c.B;
        }

        public static bool operator ==(Color c, CodedPixel p)
        {
            return p.a == c.G && p.b == c.B;
        }

        public static bool operator !=(Color c, CodedPixel p)
        {
            return p.a != c.G || p.b != c.B;
        }

        public void levelUP()
        {
            _compatibility_level++;
        }

        public void resetLevel()
        {
            _isEmpty = true;
            _compatibility_level = 0;
        }

        public int getCompatibilityLevel()
        {
            return _compatibility_level;
        }
    }
}
