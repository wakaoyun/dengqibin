using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Time
{
    public partial class UserControl1 : UserControl
    {
        public UserControl1()
        {
            InitializeComponent();
        }
        private int i = 0;
        private int total;
        private string s;
        private string Count(int i)
        {
            if (i % 60 < 10)
                s = "0" + (i % 60).ToString();
            else
                s = (i % 60).ToString();
            return "0"+(i / 60).ToString() + ":" +s;
        }
       
        public string Side
        {
            set
            {
                label1.Text = value;
            }
        }
        public bool State
        {
            get
            {
                return timer1.Enabled;
            }
            set
            {
                timer1.Enabled = value;
            }
        }
        public int Total
        {
            get
            {
                return total;
            }
            set
            {
                total = value;
            }
        }
        public int I
        {
            get
            {
                return i;
            }
            set
            {
                i = value;
            }
        }

        private void timer1_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            label2.Text = Count(i);
            label3.Text = Count(total);
            i++;
        }

    }
}
