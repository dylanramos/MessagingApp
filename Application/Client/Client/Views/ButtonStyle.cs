using System;
using System.Drawing;
using System.Windows.Forms;

namespace Client.Views
{
    class ButtonStyle : Button
    {
        public ButtonStyle()
        {
            // Properties
            this.BackColor = Color.Tan;
            this.FlatStyle = FlatStyle.Flat;
            this.Font = new Font("Microsoft YaHei", 12F, FontStyle.Regular);
            this.TabStop = true;

            // Events
            this.MouseLeave += new EventHandler(this.MouseLeaved);
            this.MouseHover += new EventHandler(this.MouseHovered);
        }

        /// <summary>
        /// When we hover the button with the mouse
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MouseHovered(object sender, EventArgs e)
        {
            this.Cursor = Cursors.Hand;
            this.BackColor = Color.Black;
            this.ForeColor = Color.Tan;
        }

        /// <summary>
        /// When we leave the button with the mouse
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MouseLeaved(object sender, EventArgs e)
        {
            this.Cursor = Cursors.Default;
            this.BackColor = Color.Tan;
            this.ForeColor = Color.Black;
        }
    }
}
