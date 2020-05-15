namespace Client
{
    partial class frmLogin
    {
        /// <summary>
        /// Variable nécessaire au concepteur.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Nettoyage des ressources utilisées.
        /// </summary>
        /// <param name="disposing">true si les ressources managées doivent être supprimées ; sinon, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Code généré par le Concepteur Windows Form

        /// <summary>
        /// Méthode requise pour la prise en charge du concepteur - ne modifiez pas
        /// le contenu de cette méthode avec l'éditeur de code.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmLogin));
            this.lblTitle = new System.Windows.Forms.Label();
            this.ptbTitle = new System.Windows.Forms.PictureBox();
            this.ptbUserName = new System.Windows.Forms.PictureBox();
            this.txtUsername = new System.Windows.Forms.TextBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.txtPassword = new System.Windows.Forms.TextBox();
            this.ptbPassword = new System.Windows.Forms.PictureBox();
            this.lblAccountCreation = new System.Windows.Forms.Label();
            this.lblTitle2 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.ptbTitle)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ptbUserName)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ptbPassword)).BeginInit();
            this.SuspendLayout();
            // 
            // lblTitle
            // 
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new System.Drawing.Font("Microsoft YaHei", 15F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTitle.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.lblTitle.Location = new System.Drawing.Point(128, 32);
            this.lblTitle.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(163, 27);
            this.lblTitle.TabIndex = 1;
            this.lblTitle.Text = "MessagingApp";
            // 
            // ptbTitle
            // 
            this.ptbTitle.Image = ((System.Drawing.Image)(resources.GetObject("ptbTitle.Image")));
            this.ptbTitle.Location = new System.Drawing.Point(165, 65);
            this.ptbTitle.Margin = new System.Windows.Forms.Padding(6);
            this.ptbTitle.Name = "ptbTitle";
            this.ptbTitle.Size = new System.Drawing.Size(90, 90);
            this.ptbTitle.TabIndex = 0;
            this.ptbTitle.TabStop = false;
            // 
            // ptbUserName
            // 
            this.ptbUserName.Image = ((System.Drawing.Image)(resources.GetObject("ptbUserName.Image")));
            this.ptbUserName.Location = new System.Drawing.Point(50, 229);
            this.ptbUserName.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.ptbUserName.Name = "ptbUserName";
            this.ptbUserName.Size = new System.Drawing.Size(38, 39);
            this.ptbUserName.TabIndex = 2;
            this.ptbUserName.TabStop = false;
            // 
            // txtUsername
            // 
            this.txtUsername.BackColor = System.Drawing.Color.IndianRed;
            this.txtUsername.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtUsername.Font = new System.Drawing.Font("Microsoft YaHei", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtUsername.Location = new System.Drawing.Point(92, 242);
            this.txtUsername.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.txtUsername.Name = "txtUsername";
            this.txtUsername.Size = new System.Drawing.Size(278, 22);
            this.txtUsername.TabIndex = 0;
            this.txtUsername.TabStop = false;
            this.txtUsername.Text = "Nom d\'utilisateur";
            this.txtUsername.Enter += new System.EventHandler(this.txtUsername_Enter);
            this.txtUsername.Leave += new System.EventHandler(this.txtUsername_Leave);
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.Black;
            this.panel1.Location = new System.Drawing.Point(50, 270);
            this.panel1.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(320, 1);
            this.panel1.TabIndex = 4;
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.Black;
            this.panel2.Location = new System.Drawing.Point(50, 348);
            this.panel2.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(320, 1);
            this.panel2.TabIndex = 7;
            // 
            // txtPassword
            // 
            this.txtPassword.BackColor = System.Drawing.Color.IndianRed;
            this.txtPassword.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtPassword.Font = new System.Drawing.Font("Microsoft YaHei", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtPassword.Location = new System.Drawing.Point(92, 320);
            this.txtPassword.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.txtPassword.Name = "txtPassword";
            this.txtPassword.Size = new System.Drawing.Size(278, 22);
            this.txtPassword.TabIndex = 1;
            this.txtPassword.TabStop = false;
            this.txtPassword.Text = "Mot de passe";
            this.txtPassword.Enter += new System.EventHandler(this.txtPassword_Enter);
            this.txtPassword.Leave += new System.EventHandler(this.txtPassword_Leave);
            // 
            // ptbPassword
            // 
            this.ptbPassword.Image = ((System.Drawing.Image)(resources.GetObject("ptbPassword.Image")));
            this.ptbPassword.Location = new System.Drawing.Point(50, 307);
            this.ptbPassword.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.ptbPassword.Name = "ptbPassword";
            this.ptbPassword.Size = new System.Drawing.Size(38, 39);
            this.ptbPassword.TabIndex = 5;
            this.ptbPassword.TabStop = false;
            // 
            // lblAccountCreation
            // 
            this.lblAccountCreation.AutoSize = true;
            this.lblAccountCreation.Font = new System.Drawing.Font("Microsoft YaHei", 12F, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblAccountCreation.ForeColor = System.Drawing.Color.Tan;
            this.lblAccountCreation.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.lblAccountCreation.Location = new System.Drawing.Point(46, 403);
            this.lblAccountCreation.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblAccountCreation.Name = "lblAccountCreation";
            this.lblAccountCreation.Size = new System.Drawing.Size(139, 21);
            this.lblAccountCreation.TabIndex = 0;
            this.lblAccountCreation.Text = "Créer un compte";
            this.lblAccountCreation.Click += new System.EventHandler(this.lblAccountCreation_Click);
            this.lblAccountCreation.MouseLeave += new System.EventHandler(this.lblAccountCreation_MouseLeave);
            this.lblAccountCreation.MouseHover += new System.EventHandler(this.lblAccountCreation_MouseHover);
            // 
            // lblTitle2
            // 
            this.lblTitle2.AutoSize = true;
            this.lblTitle2.Font = new System.Drawing.Font("Microsoft YaHei", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTitle2.ForeColor = System.Drawing.Color.Tan;
            this.lblTitle2.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.lblTitle2.Location = new System.Drawing.Point(46, 187);
            this.lblTitle2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblTitle2.Name = "lblTitle2";
            this.lblTitle2.Size = new System.Drawing.Size(177, 22);
            this.lblTitle2.TabIndex = 8;
            this.lblTitle2.Text = "Conexion au compte";
            // 
            // frmLogin
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 27F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.IndianRed;
            this.ClientSize = new System.Drawing.Size(420, 540);
            this.Controls.Add(this.lblTitle2);
            this.Controls.Add(this.lblAccountCreation);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.txtPassword);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.ptbPassword);
            this.Controls.Add(this.txtUsername);
            this.Controls.Add(this.ptbUserName);
            this.Controls.Add(this.lblTitle);
            this.Controls.Add(this.ptbTitle);
            this.Font = new System.Drawing.Font("Microsoft YaHei", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Margin = new System.Windows.Forms.Padding(6);
            this.Name = "frmLogin";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)(this.ptbTitle)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ptbUserName)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ptbPassword)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox ptbTitle;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.PictureBox ptbUserName;
        private System.Windows.Forms.TextBox txtUsername;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.TextBox txtPassword;
        private System.Windows.Forms.PictureBox ptbPassword;
        private System.Windows.Forms.Label lblAccountCreation;
        private System.Windows.Forms.Label lblTitle2;
    }
}

