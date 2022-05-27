using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using Backend.SessionStart;
using Management_System.IU;

namespace Management_System
{
    public partial class Start_frame : Form
    {
        public Start_frame()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Hide();
            Welcome_frame welcome_frame = new Welcome_frame(new SessionStart(userName.Text));
            welcome_frame.ShowDialog();
            this.Close();
        }
    }
}
