using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Chess
{
    public partial class Form3 : Form
    {
        public Form3()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (Black.Checked == true)
            {
                Form1.Side = true;
                Form1.C = 0;
            }
            else if(Red.Checked==true)
            {
                Form1.Side = false;
                Form1.C = 1;
            }
            if (PtoM.Checked == true)
                Form1.emulant = true;
            else if (PtoP.Checked == true)
                Form1.emulant = false;
            Close();
        }
    }
}
