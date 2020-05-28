namespace Server
{
    partial class frmServer
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmServer));
            this.cmdStart = new System.Windows.Forms.Button();
            this.cmdStop = new System.Windows.Forms.Button();
            this.lstlogs = new System.Windows.Forms.ListBox();
            this.lsvConnectedUsers = new System.Windows.Forms.ListView();
            this.UserColumn = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.IpColumn = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.SuspendLayout();
            // 
            // cmdStart
            // 
            this.cmdStart.BackColor = System.Drawing.Color.Olive;
            this.cmdStart.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cmdStart.Location = new System.Drawing.Point(989, 340);
            this.cmdStart.Name = "cmdStart";
            this.cmdStart.Size = new System.Drawing.Size(182, 41);
            this.cmdStart.TabIndex = 0;
            this.cmdStart.Text = "Démarrer le serveur";
            this.cmdStart.UseVisualStyleBackColor = false;
            this.cmdStart.Click += new System.EventHandler(this.cmdStart_Click);
            // 
            // cmdStop
            // 
            this.cmdStop.BackColor = System.Drawing.Color.Firebrick;
            this.cmdStop.Enabled = false;
            this.cmdStop.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cmdStop.Location = new System.Drawing.Point(801, 340);
            this.cmdStop.Name = "cmdStop";
            this.cmdStop.Size = new System.Drawing.Size(182, 41);
            this.cmdStop.TabIndex = 1;
            this.cmdStop.Text = "Arrêter le serveur";
            this.cmdStop.UseVisualStyleBackColor = false;
            this.cmdStop.Click += new System.EventHandler(this.cmdStop_Click);
            // 
            // lstlogs
            // 
            this.lstlogs.FormattingEnabled = true;
            this.lstlogs.ItemHeight = 21;
            this.lstlogs.Location = new System.Drawing.Point(428, 40);
            this.lstlogs.Name = "lstlogs";
            this.lstlogs.Size = new System.Drawing.Size(743, 277);
            this.lstlogs.TabIndex = 2;
            // 
            // lsvConnectedUsers
            // 
            this.lsvConnectedUsers.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.UserColumn,
            this.IpColumn});
            this.lsvConnectedUsers.HideSelection = false;
            this.lsvConnectedUsers.Location = new System.Drawing.Point(45, 40);
            this.lsvConnectedUsers.Name = "lsvConnectedUsers";
            this.lsvConnectedUsers.Size = new System.Drawing.Size(339, 277);
            this.lsvConnectedUsers.TabIndex = 4;
            this.lsvConnectedUsers.UseCompatibleStateImageBehavior = false;
            this.lsvConnectedUsers.View = System.Windows.Forms.View.Details;
            // 
            // UserColumn
            // 
            this.UserColumn.Text = "Utilisateur";
            this.UserColumn.Width = 141;
            // 
            // IpColumn
            // 
            this.IpColumn.Text = "Adresse IP";
            this.IpColumn.Width = 137;
            // 
            // frmServer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 21F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.IndianRed;
            this.ClientSize = new System.Drawing.Size(1210, 404);
            this.Controls.Add(this.lsvConnectedUsers);
            this.Controls.Add(this.lstlogs);
            this.Controls.Add(this.cmdStop);
            this.Controls.Add(this.cmdStart);
            this.Font = new System.Drawing.Font("Microsoft YaHei", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(5);
            this.MaximizeBox = false;
            this.Name = "frmServer";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "MessagingApp - Server";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button cmdStart;
        private System.Windows.Forms.Button cmdStop;
        private System.Windows.Forms.ListBox lstlogs;
        private System.Windows.Forms.ListView lsvConnectedUsers;
        private System.Windows.Forms.ColumnHeader UserColumn;
        private System.Windows.Forms.ColumnHeader IpColumn;
    }
}

