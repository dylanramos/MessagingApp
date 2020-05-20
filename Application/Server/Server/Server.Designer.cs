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
            this.lsvClients = new System.Windows.Forms.ListView();
            this.IpAddressColumn = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.IdColumn = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.SuspendLayout();
            // 
            // cmdStart
            // 
            this.cmdStart.BackColor = System.Drawing.Color.Olive;
            this.cmdStart.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cmdStart.Location = new System.Drawing.Point(476, 568);
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
            this.cmdStop.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cmdStop.Location = new System.Drawing.Point(288, 568);
            this.cmdStop.Name = "cmdStop";
            this.cmdStop.Size = new System.Drawing.Size(182, 41);
            this.cmdStop.TabIndex = 1;
            this.cmdStop.Text = "Arrêter le serveur";
            this.cmdStop.UseVisualStyleBackColor = false;
            // 
            // lstlogs
            // 
            this.lstlogs.FormattingEnabled = true;
            this.lstlogs.ItemHeight = 21;
            this.lstlogs.Location = new System.Drawing.Point(12, 292);
            this.lstlogs.Name = "lstlogs";
            this.lstlogs.Size = new System.Drawing.Size(646, 235);
            this.lstlogs.TabIndex = 2;
            // 
            // cmdClose
            // 
            this.cmdClose.BackColor = System.Drawing.Color.Firebrick;
            this.cmdClose.FlatAppearance.BorderSize = 0;
            this.cmdClose.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cmdClose.Location = new System.Drawing.Point(660, 0);
            this.cmdClose.Name = "cmdClose";
            this.cmdClose.Size = new System.Drawing.Size(36, 32);
            this.cmdClose.TabIndex = 3;
            this.cmdClose.Text = "X";
            this.cmdClose.UseVisualStyleBackColor = false;
            this.cmdClose.Click += new System.EventHandler(this.cmdClose_Click);
            // 
            // lsvClients
            // 
            this.lsvClients.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.IpAddressColumn,
            this.IdColumn});
            this.lsvClients.HideSelection = false;
            this.lsvClients.Location = new System.Drawing.Point(12, 38);
            this.lsvClients.Name = "lsvClients";
            this.lsvClients.Size = new System.Drawing.Size(646, 232);
            this.lsvClients.TabIndex = 4;
            this.lsvClients.UseCompatibleStateImageBehavior = false;
            this.lsvClients.View = System.Windows.Forms.View.Details;
            // 
            // IpAddressColumn
            // 
            this.IpAddressColumn.Text = "Adresse IP";
            this.IpAddressColumn.Width = 120;
            // 
            // IdColumn
            // 
            this.IdColumn.Text = "ID du client";
            this.IdColumn.Width = 120;
            // 
            // frmServer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 21F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.IndianRed;
            this.ClientSize = new System.Drawing.Size(696, 621);
            this.Controls.Add(this.lsvClients);
            this.Controls.Add(this.cmdClose);
            this.Controls.Add(this.lstlogs);
            this.Controls.Add(this.cmdStop);
            this.Controls.Add(this.cmdStart);
            this.Font = new System.Drawing.Font("Microsoft YaHei", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Margin = new System.Windows.Forms.Padding(5);
            this.Name = "frmServer";
            this.Text = "Form1";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button cmdStart;
        private System.Windows.Forms.Button cmdStop;
        private System.Windows.Forms.ListBox lstlogs;
        private System.Windows.Forms.Button cmdClose;
        private System.Windows.Forms.ListView lsvClients;
        private System.Windows.Forms.ColumnHeader IpAddressColumn;
        private System.Windows.Forms.ColumnHeader IdColumn;
    }
}

