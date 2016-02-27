using SapSoapCardWriter.GUI.NakCardService;
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

        private async void btnLogin_Click(object sender, EventArgs e)
        {
            LoginData ld = await serviceManager.ValidateUserAsync(tbUserName.Text, tbPassword.Text);
            if (ld.IsSuccessful)
            {
                User = new UserData(tbUserName.Text, tbPassword.Text);
                DialogResult = System.Windows.Forms.DialogResult.OK;
            }
            else
            {
                MessageBox.Show(ld.ErrorString, "Bejelentkezés sikertelen!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
        }
    }
}
