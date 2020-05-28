using System.Windows.Forms;

namespace Client.Forms
{
    public partial class frmMain : Form
    {
        public frmMain()
        {
            InitializeComponent();

            frmLogin login = new frmLogin();
            login.Show();
        }
    }
}
