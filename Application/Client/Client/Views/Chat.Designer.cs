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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmChat));
            this.pnlContacts = new System.Windows.Forms.Panel();
            this.rtbMessage = new System.Windows.Forms.RichTextBox();
            this.ptbSend = new System.Windows.Forms.PictureBox();
            this.pnlChat = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.panel3 = new System.Windows.Forms.Panel();
            this.ptbReloadChat = new System.Windows.Forms.PictureBox();
            this.ptbReloadContacts = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.ptbSend)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ptbReloadChat)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ptbReloadContacts)).BeginInit();
            this.SuspendLayout();
            // 
            // pnlContacts
            // 
            this.pnlContacts.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.pnlContacts.AutoScroll = true;
            this.pnlContacts.Location = new System.Drawing.Point(42, 19);
            this.pnlContacts.Name = "pnlContacts";
            this.pnlContacts.Size = new System.Drawing.Size(267, 443);
            this.pnlContacts.TabIndex = 0;
            // 
            // rtbMessage
            // 
            this.rtbMessage.Location = new System.Drawing.Point(388, 405);
            this.rtbMessage.MaxLength = 250;
            this.rtbMessage.Name = "rtbMessage";
            this.rtbMessage.Size = new System.Drawing.Size(374, 62);
            this.rtbMessage.TabIndex = 1;
            this.rtbMessage.Text = "";
            this.rtbMessage.Visible = false;
            // 
            // ptbSend
            // 
            this.ptbSend.BackColor = System.Drawing.Color.IndianRed;
            this.ptbSend.Image = ((System.Drawing.Image)(resources.GetObject("ptbSend.Image")));
            this.ptbSend.Location = new System.Drawing.Point(768, 412);
            this.ptbSend.Name = "ptbSend";
            this.ptbSend.Size = new System.Drawing.Size(51, 50);
            this.ptbSend.TabIndex = 5;
            this.ptbSend.TabStop = false;
            this.ptbSend.Visible = false;
            this.ptbSend.Click += new System.EventHandler(this.ptbSend_Click);
            this.ptbSend.MouseLeave += new System.EventHandler(this.ptbSend_MouseLeave);
            this.ptbSend.MouseHover += new System.EventHandler(this.ptbSend_MouseHover);
            // 
            // pnlChat
            // 
            this.pnlChat.AutoScroll = true;
            this.pnlChat.Location = new System.Drawing.Point(388, 19);
            this.pnlChat.Name = "pnlChat";
            this.pnlChat.Size = new System.Drawing.Size(470, 361);
            this.pnlChat.TabIndex = 6;
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.Black;
            this.panel2.Location = new System.Drawing.Point(381, 19);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(1, 361);
            this.panel2.TabIndex = 8;
            // 
            // panel3
            // 
            this.panel3.BackColor = System.Drawing.Color.Black;
            this.panel3.Location = new System.Drawing.Point(825, 19);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(1, 361);
            this.panel3.TabIndex = 9;
            // 
            // ptbReloadChat
            // 
            this.ptbReloadChat.Image = ((System.Drawing.Image)(resources.GetObject("ptbReloadChat.Image")));
            this.ptbReloadChat.Location = new System.Drawing.Point(864, 12);
            this.ptbReloadChat.Name = "ptbReloadChat";
            this.ptbReloadChat.Size = new System.Drawing.Size(52, 48);
            this.ptbReloadChat.TabIndex = 0;
            this.ptbReloadChat.TabStop = false;
            this.ptbReloadChat.Visible = false;
            this.ptbReloadChat.Click += new System.EventHandler(this.ptbReloadChat_Click);
            this.ptbReloadChat.MouseLeave += new System.EventHandler(this.ptbReload_MouseLeave);
            this.ptbReloadChat.MouseHover += new System.EventHandler(this.ptbReload_MouseHover);
            // 
            // ptbReloadContacts
            // 
            this.ptbReloadContacts.Image = ((System.Drawing.Image)(resources.GetObject("ptbReloadContacts.Image")));
            this.ptbReloadContacts.Location = new System.Drawing.Point(315, 12);
            this.ptbReloadContacts.Name = "ptbReloadContacts";
            this.ptbReloadContacts.Size = new System.Drawing.Size(52, 48);
            this.ptbReloadContacts.TabIndex = 10;
            this.ptbReloadContacts.TabStop = false;
            this.ptbReloadContacts.Visible = false;
            this.ptbReloadContacts.Click += new System.EventHandler(this.ptbReloadContacts_Click);
            this.ptbReloadContacts.MouseLeave += new System.EventHandler(this.ptbReload_MouseLeave);
            this.ptbReloadContacts.MouseHover += new System.EventHandler(this.ptbReload_MouseHover);
            // 
            // frmChat
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 21F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.IndianRed;
            this.ClientSize = new System.Drawing.Size(926, 479);
            this.Controls.Add(this.ptbReloadContacts);
            this.Controls.Add(this.ptbReloadChat);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.pnlChat);
            this.Controls.Add(this.ptbSend);
            this.Controls.Add(this.rtbMessage);
            this.Controls.Add(this.pnlContacts);
            this.Font = new System.Drawing.Font("Microsoft YaHei", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(5);
            this.MaximizeBox = false;
            this.Name = "frmChat";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "MessagingApp - Chat";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmChat_FormClosing);
            ((System.ComponentModel.ISupportInitialize)(this.ptbSend)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ptbReloadChat)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ptbReloadContacts)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnlContacts;
        private System.Windows.Forms.RichTextBox rtbMessage;
        private System.Windows.Forms.PictureBox ptbSend;
        private System.Windows.Forms.Panel pnlChat;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.PictureBox ptbReloadChat;
        private System.Windows.Forms.PictureBox ptbReloadContacts;
    }
}