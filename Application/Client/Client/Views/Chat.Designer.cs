namespace Client.Forms
{
    partial class frmChat
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.pnlContacts = new System.Windows.Forms.Panel();
            this.pnlChat = new System.Windows.Forms.Panel();
            this.SuspendLayout();
            // 
            // pnlContacts
            // 
            this.pnlContacts.Dock = System.Windows.Forms.DockStyle.Left;
            this.pnlContacts.Location = new System.Drawing.Point(0, 0);
            this.pnlContacts.Name = "pnlContacts";
            this.pnlContacts.Size = new System.Drawing.Size(200, 479);
            this.pnlContacts.TabIndex = 0;
            // 
            // pnlChat
            // 
            this.pnlChat.Location = new System.Drawing.Point(249, 57);
            this.pnlChat.Name = "pnlChat";
            this.pnlChat.Size = new System.Drawing.Size(442, 372);
            this.pnlChat.TabIndex = 1;
            // 
            // frmChat
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 21F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.IndianRed;
            this.ClientSize = new System.Drawing.Size(739, 479);
            this.Controls.Add(this.pnlChat);
            this.Controls.Add(this.pnlContacts);
            this.Font = new System.Drawing.Font("Microsoft YaHei", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Margin = new System.Windows.Forms.Padding(5, 5, 5, 5);
            this.Name = "frmChat";
            this.Text = "Chat";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnlContacts;
        private System.Windows.Forms.Panel pnlChat;
    }
}