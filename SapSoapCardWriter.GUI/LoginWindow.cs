using SapSoapCardWriter.BusinessLogic.SapService;
using SapSoapCardWriter.Common;
using SapSoapCardWriter.Common.Configuration;
using SapSoapCardWriter.Common.DIContainer;
using SapSoapCardWriter.GUI.NakCardService;
using SapSoapCardWriter.Logger.Logging;
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
        private IServiceManager serviceManager;
        private ILogger logger;
        public UserData User { get; private set; }

        public LoginWindow(ILogger logger, IServiceManager sm)
        {
            InitializeComponent();
            serviceManager = sm;
            this.logger = logger;
            User = null;
        }

        private async void btnLogin_Click(object sender, EventArgs e)
        {
            await DoLogin();
        }

        private async Task DoLogin()
        {
            try
            {
                LoginData ld = await serviceManager.ValidateUserAsync(tbUserName.Text, tbPassword.Text);
                if (ld.IsSuccessful)
                {
                    User = new UserData(tbUserName.Text, tbPassword.Text);
                    DialogResult = System.Windows.Forms.DialogResult.OK;
                }
                else
                {
                    MessageBox.Show(ld.ErrorString, "Bejelentkezési művelet", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch(Exception ex)
            {
                logger.Error(ex.ToString());
                MessageBox.Show("Bejelentkezés sikertelen!", "Bejelentkezési művelet", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
        }

        private async void tbPassword_KeyPress(object sender, KeyPressEventArgs e)
        {
            if(e.KeyChar == (char)Keys.Enter)
            {
                await DoLogin();
            }
        }

        private async void tbUserName_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                await DoLogin();
            }
        }
    }
}
