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
    public partial class frmAccountCreation : Form
    {
        public frmAccountCreation()
        {
            InitializeComponent();

            ButtonStyle createButton = new ButtonStyle();
            createButton.Location = new Point(242, 483);
            createButton.Size = new Size(128, 44);
            createButton.Text = "Créer";
            createButton.Click += new EventHandler(this.CreateButtonClicked);
            this.Controls.Add(createButton);

            ButtonStyle cancelButton = new ButtonStyle();
            cancelButton.Location = new Point(50, 483);
            cancelButton.Size = new Size(128, 44);
            cancelButton.Text = "Annuler";
            cancelButton.Click += new EventHandler(this.CancelButtonClicked);
            this.Controls.Add(cancelButton);
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

        private void txtPasswordVerification_Enter(object sender, EventArgs e)
        {
            if (txtPasswordVerification.Text == "Vérification du mot de passe")
            {
                txtPasswordVerification.Text = "";
            }

            txtPasswordVerification.UseSystemPasswordChar = true;
        }

        private void txtPasswordVerification_Leave(object sender, EventArgs e)
        {
            if (txtPasswordVerification.Text == "")
            {
                txtPasswordVerification.UseSystemPasswordChar = false;
                txtPasswordVerification.Text = "Vérification du mot de passe";
            }
        }

        private void CancelButtonClicked(object sender, EventArgs e)
        {
            this.Close();

            frmLogin login = new frmLogin();
            login.Show();
        }

        private void CreateButtonClicked(object sender, EventArgs e)
        {
            this.Close();

            frmChat chat = new frmChat();
            chat.Show();
        }
    }
}
