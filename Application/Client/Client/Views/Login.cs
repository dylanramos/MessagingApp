using Client.Forms;
using Client.Views;
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

            ButtonStyle loginButton = new ButtonStyle();
            loginButton.Location = new Point(242, 453);
            loginButton.Size = new Size(128, 44);
            loginButton.Text = "Se connecter";
            loginButton.Click += new EventHandler(this.LoginButtonClicked);
            this.Controls.Add(loginButton);
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

        private void txtUsername_Enter(object sender, EventArgs e)
        {
            if (txtUsername.Text == "Nom d'utilisateur")
                txtUsername.Text = "";
        }

        private void txtUsername_Leave(object sender, EventArgs e)
        {
            if (txtUsername.Text == "")
                txtUsername.Text = "Nom d'utilisateur";
        }

        private void txtPassword_Enter(object sender, EventArgs e)
        {
            if (txtPassword.Text == "Mot de passe")
            {
                txtPassword.Text = "";
            }

            txtPassword.UseSystemPasswordChar = true;
        }

        private void txtPassword_Leave(object sender, EventArgs e)
        {
            if (txtPassword.Text == "")
            {
                txtPassword.UseSystemPasswordChar = false;
                txtPassword.Text = "Mot de passe";
            }
        }

        private void lblAccountCreation_Click(object sender, EventArgs e)
        {
            this.Close();
            frmAccountCreation accountCreation = new frmAccountCreation();
            accountCreation.Show();
        }

        private void LoginButtonClicked(object sender, EventArgs e)
        {
            this.Close();

            frmChat chat = new frmChat();
            chat.Show();
        }
    }
}
