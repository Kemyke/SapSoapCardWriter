using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security.Authentication;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SapSoapCardWriter.GUI
{
    public partial class LoginWindow : Form
    {
        private ServiceManager serviceManager;
        public UserData User { get; private set; }

        public LoginWindow()
        {
            InitializeComponent();
            serviceManager = new ServiceManager();
            User = null;
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            try
            {
                serviceManager.ValidateUser(tbUserName.Text, tbPassword.Text);
                User = new UserData(tbUserName.Text, tbPassword.Text);
                DialogResult = System.Windows.Forms.DialogResult.OK;
            }
            catch (AuthenticationException ex)
            {
                MessageBox.Show(ex.Message, "Bejelentkezés sikertelen!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
        }
    }
}
