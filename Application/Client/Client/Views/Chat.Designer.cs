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
            this.pnlOnlineContacts = new System.Windows.Forms.Panel();
            this.lblOnline = new System.Windows.Forms.Label();
            this.pnlOfflineContacts = new System.Windows.Forms.Panel();
            this.lblOffline = new System.Windows.Forms.Label();
            this.cmdClose = new System.Windows.Forms.Button();
            this.rtbMessage = new System.Windows.Forms.RichTextBox();
            this.ptbSend = new System.Windows.Forms.PictureBox();
            this.pnlChat = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.panel3 = new System.Windows.Forms.Panel();
            this.pnlOnlineContacts.SuspendLayout();
            this.pnlOfflineContacts.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ptbSend)).BeginInit();
            this.SuspendLayout();
            // 
            // pnlOnlineContacts
            // 
            this.pnlOnlineContacts.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pnlOnlineContacts.AutoScroll = true;
            this.pnlOnlineContacts.Controls.Add(this.lblOnline);
            this.pnlOnlineContacts.Location = new System.Drawing.Point(0, 0);
            this.pnlOnlineContacts.Name = "pnlOnlineContacts";
            this.pnlOnlineContacts.Size = new System.Drawing.Size(176, 232);
            this.pnlOnlineContacts.TabIndex = 0;
            // 
            // lblOnline
            // 
            this.lblOnline.AutoSize = true;
            this.lblOnline.Font = new System.Drawing.Font("Microsoft YaHei", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblOnline.Location = new System.Drawing.Point(50, 9);
            this.lblOnline.Name = "lblOnline";
            this.lblOnline.Size = new System.Drawing.Size(74, 22);
            this.lblOnline.TabIndex = 0;
            this.lblOnline.Text = "En ligne";
            // 
            // pnlOfflineContacts
            // 
            this.pnlOfflineContacts.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pnlOfflineContacts.AutoScroll = true;
            this.pnlOfflineContacts.Controls.Add(this.lblOffline);
            this.pnlOfflineContacts.Location = new System.Drawing.Point(0, 238);
            this.pnlOfflineContacts.Name = "pnlOfflineContacts";
            this.pnlOfflineContacts.Size = new System.Drawing.Size(176, 241);
            this.pnlOfflineContacts.TabIndex = 1;
            // 
            // lblOffline
            // 
            this.lblOffline.AutoSize = true;
            this.lblOffline.Font = new System.Drawing.Font("Microsoft YaHei", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblOffline.Location = new System.Drawing.Point(41, 12);
            this.lblOffline.Name = "lblOffline";
            this.lblOffline.Size = new System.Drawing.Size(96, 22);
            this.lblOffline.TabIndex = 0;
            this.lblOffline.Text = "Hors-ligne";
            // 
            // cmdClose
            // 
            this.cmdClose.BackColor = System.Drawing.Color.Firebrick;
            this.cmdClose.FlatAppearance.BorderSize = 0;
            this.cmdClose.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cmdClose.Location = new System.Drawing.Point(704, -1);
            this.cmdClose.Name = "cmdClose";
            this.cmdClose.Size = new System.Drawing.Size(36, 32);
            this.cmdClose.TabIndex = 4;
            this.cmdClose.Text = "X";
            this.cmdClose.UseVisualStyleBackColor = false;
            this.cmdClose.Click += new System.EventHandler(this.cmdClose_Click);
            // 
            // rtbMessage
            // 
            this.rtbMessage.Location = new System.Drawing.Point(243, 405);
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
            this.ptbSend.Location = new System.Drawing.Point(623, 412);
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
            this.pnlChat.Location = new System.Drawing.Point(243, 38);
            this.pnlChat.Name = "pnlChat";
            this.pnlChat.Size = new System.Drawing.Size(431, 361);
            this.pnlChat.TabIndex = 6;
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.Black;
            this.panel2.Location = new System.Drawing.Point(236, 38);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(1, 361);
            this.panel2.TabIndex = 8;
            // 
            // panel3
            // 
            this.panel3.BackColor = System.Drawing.Color.Black;
            this.panel3.Location = new System.Drawing.Point(680, 38);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(1, 361);
            this.panel3.TabIndex = 9;
            // 
            // frmChat
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 21F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.IndianRed;
            this.ClientSize = new System.Drawing.Size(739, 479);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.pnlChat);
            this.Controls.Add(this.ptbSend);
            this.Controls.Add(this.rtbMessage);
            this.Controls.Add(this.cmdClose);
            this.Controls.Add(this.pnlOfflineContacts);
            this.Controls.Add(this.pnlOnlineContacts);
            this.Font = new System.Drawing.Font("Microsoft YaHei", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Margin = new System.Windows.Forms.Padding(5);
            this.Name = "frmChat";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Chat";
            this.pnlOnlineContacts.ResumeLayout(false);
            this.pnlOnlineContacts.PerformLayout();
            this.pnlOfflineContacts.ResumeLayout(false);
            this.pnlOfflineContacts.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ptbSend)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnlOnlineContacts;
        private System.Windows.Forms.Label lblOnline;
        private System.Windows.Forms.Panel pnlOfflineContacts;
        private System.Windows.Forms.Label lblOffline;
        private System.Windows.Forms.Button cmdClose;
        private System.Windows.Forms.RichTextBox rtbMessage;
        private System.Windows.Forms.PictureBox ptbSend;
        private System.Windows.Forms.Panel pnlChat;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Panel panel3;
    }
}