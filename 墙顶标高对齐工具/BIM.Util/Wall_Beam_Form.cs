using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BIM.Util
{
    public partial class Wall_Beam_Form : Form
    {

        public Wall_Beam_Form()
        {
            InitializeComponent();
        }

        public bool ArrangeMode_UP;
        public bool ArrangeMode_DOWN;

        public void button2_Click(object sender, EventArgs e)
        {
           ArrangeMode_UP = checkBox1.Checked;
           ArrangeMode_DOWN = checkBox2.Checked;
            Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
