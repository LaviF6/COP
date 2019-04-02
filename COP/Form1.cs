using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace COP
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

            string[] args = Environment.GetCommandLineArgs();
            if(args.Length == 1)
            {
                //Receiver r = new Receiver(this);
            }
            else if(args.Length == 2)
            {
                Transmitter t = new Transmitter(this);
                t.start(args[1]);
            }
            else
            {
                MessageBox.Show(
                    "Drag a file To send information,\n" +
                    "Open to receive information.",
                    "Error",
                    MessageBoxButtons.OK);
            }
        }
    }
}
