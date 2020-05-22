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
            this.cmdStart = new System.Windows.Forms.Button();
            this.cmdStop = new System.Windows.Forms.Button();
            this.lstlogs = new System.Windows.Forms.ListBox();
            this.cmdClose = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // cmdStart
            // 
            this.cmdStart.BackColor = System.Drawing.Color.Olive;
            this.cmdStart.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cmdStart.Location = new System.Drawing.Point(593, 568);
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
            this.cmdStop.Location = new System.Drawing.Point(405, 568);
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
            this.lstlogs.Location = new System.Drawing.Point(32, 40);
            this.lstlogs.Name = "lstlogs";
            this.lstlogs.Size = new System.Drawing.Size(743, 508);
            this.lstlogs.TabIndex = 2;
            // 
            // cmdClose
            // 
            this.cmdClose.BackColor = System.Drawing.Color.Firebrick;
            this.cmdClose.FlatAppearance.BorderSize = 0;
            this.cmdClose.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cmdClose.Location = new System.Drawing.Point(779, 0);
            this.cmdClose.Name = "cmdClose";
            this.cmdClose.Size = new System.Drawing.Size(36, 32);
            this.cmdClose.TabIndex = 3;
            this.cmdClose.Text = "X";
            this.cmdClose.UseVisualStyleBackColor = false;
            this.cmdClose.Click += new System.EventHandler(this.cmdClose_Click);
            // 
            // frmServer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 21F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.IndianRed;
            this.ClientSize = new System.Drawing.Size(812, 621);
            this.Controls.Add(this.cmdClose);
            this.Controls.Add(this.lstlogs);
            this.Controls.Add(this.cmdStop);
            this.Controls.Add(this.cmdStart);
            this.Font = new System.Drawing.Font("Microsoft YaHei", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Margin = new System.Windows.Forms.Padding(5);
            this.Name = "frmServer";
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "frmServer";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button cmdStart;
        private System.Windows.Forms.Button cmdStop;
        private System.Windows.Forms.ListBox lstlogs;
        private System.Windows.Forms.Button cmdClose;
    }
}

