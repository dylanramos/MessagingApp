using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Client
{
    public partial class frmLogin : Form
    {
        public frmLogin()
        {
            InitializeComponent();
        }

        private void lblAccountCreation_MouseHover(object sender, EventArgs e)
        {
            this.Cursor = Cursors.Hand;
            lblAccountCreation.ForeColor = Color.Black; 
        }

        private void lblAccountCreation_MouseLeave(object sender, EventArgs e)
        {
            this.Cursor = Cursors.Default;
            lblAccountCreation.ForeColor = Color.Tan;
        }

        private void button1_MouseHover(object sender, EventArgs e)
        {
            this.Cursor = Cursors.Hand;
            cmdLogin.BackColor = Color.Black;
            cmdLogin.ForeColor = Color.Tan;
        }

        private void cmdLogin_MouseLeave(object sender, EventArgs e)
        {
            this.Cursor = Cursors.Default;
            cmdLogin.BackColor = Color.Tan;
            cmdLogin.ForeColor = Color.Black;
        }
    }
}
