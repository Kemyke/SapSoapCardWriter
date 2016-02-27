using SapSoapCardWriter.BusinessLogic;
using SapSoapCardWriter.BusinessLogic.NFC;
using SapSoapCardWriter.GUI.NakCardService;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SapSoapCardWriter.GUI
{
    //TODO: logging, async
    public partial class SapSoapCardWriterWindow : Form
    {
        private UserData user = null;
        private ICardWriter cardWriter = null;
        private ServiceManager sm = null;
        private CardData cardData = null;

        public SapSoapCardWriterWindow()
        {
            InitializeComponent();
            sm = new ServiceManager();
            //cardWriter = new NfcCardWriter(new Logger.Logging.Log4Net.Logger());
            cardWriter = new MockCardWriter(new Logger.Logging.Log4Net.Logger());
            cardWriter.ReaderStateChanged += cardWriter_ReaderStateChanged;
        }

        private void cardWriter_ReaderStateChanged(object sender, BusinessLogic.NFC.ReaderState e)
        {
            this.Invoke(new Action(() =>
                {
                    cardData = null;
                    btnWriteCard.Enabled = false;
                    tbAddress.Text = string.Empty;
                    tbAddress.Enabled = false;
                    tbFullName.Text = string.Empty;
                    tbFullName.Enabled = false;

                    if (e == ReaderState.CardPresent)
                    {
                        toolReaderStatus.Text = "RFID kiolvasás...";
                        string rfid = cardWriter.GetRfid();
                        toolReaderStatus.Text = "Kártya beolvasás...";
                        cardData = sm.GetCardData(user, rfid);
                        tbFullName.Text = cardData.UIData.FullName;
                        tbAddress.Text = cardData.UIData.Address;
                        tbAddress.Enabled = true;
                        tbFullName.Enabled = true;
                        toolReaderStatus.Text = "Kártya beolvasva";
                        btnWriteCard.Enabled = true;
                    }
                    else
                    {
                        toolReaderStatus.Text = "Nincs kártya";
                    }
                }));
        }

        protected override void OnShown(EventArgs e)
        {
            base.OnShown(e);
            LoginWindow lw = new LoginWindow();
            var result = lw.ShowDialog();
            if(result == DialogResult.OK)
            {
                user = lw.User;
            }
            else
            {
                this.Close();
            }
        }

        private void closeMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnWriteCard_Click(object sender, EventArgs e)
        {
            toolReaderStatus.Text = "Kártya írás folyamatban...";
            try
            {
                cardWriter.WriteCard(cardData.CardKey, new List<string> { cardData.PublicEncryptedData, cardData.AllEncryptedData });
                toolReaderStatus.Text = "Kártya kész";
            }
            catch(Exception ex)
            {
                toolReaderStatus.Text = "Kártya írás sikertelen";
            }
        }
    }
}
