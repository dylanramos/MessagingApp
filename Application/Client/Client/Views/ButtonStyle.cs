using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Client.Views
{
    class ButtonStyle : Button
    {
        public ButtonStyle()
        {
            // Styles
            this.BackColor = Color.Tan;
            this.FlatStyle = FlatStyle.Flat;
            this.Font = new Font("Microsoft YaHei", 12F, FontStyle.Regular);
            this.TabStop = false;

            // Events
            this.MouseLeave += new EventHandler(this.MouseLeaved);
            this.MouseHover += new EventHandler(this.MouseHovered);
        }

        private void MouseHovered(object sender, EventArgs e)
        {
            this.Cursor = Cursors.Hand;
            this.BackColor = Color.Black;
            this.ForeColor = Color.Tan;
        }

        private void MouseLeaved(object sender, EventArgs e)
        {
            this.Cursor = Cursors.Default;
            this.BackColor = Color.Tan;
            this.ForeColor = Color.Black;
        }
    }
}
