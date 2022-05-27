using Backend.SessionStart;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Management_System.IU
{
    public partial class Welcome_frame : Form
    {
        SessionStart sessionStart;
        public Welcome_frame(object sessionStart)
        {
            this.sessionStart = (SessionStart)sessionStart;
            InitializeComponent();
        }

        private void Welcome_frame_Load(object sender, EventArgs e)
        {
            userNameLabel.Text += sessionStart.UserName;
            sessionIdLabel.Text += sessionStart.SessionID;
            logTimeLabel.Text += sessionStart.LogTime;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Hide();
            Main_frame main_frame = new Main_frame();
            main_frame.ShowDialog();
            this.Close();
        }
    }
}
