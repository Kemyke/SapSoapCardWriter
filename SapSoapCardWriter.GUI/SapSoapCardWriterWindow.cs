using SapSoapCardWriter.BusinessLogic;
using SapSoapCardWriter.BusinessLogic.NFC;
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
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Runtime.ExceptionServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SapSoapCardWriter.GUI
{
    public partial class SapSoapCardWriterWindow : Form
    {
        private const int MAXDATASIZE = 7500;

        private ICardWriter cardWriter = null;
        private ILogger logger = null;
        

        private UserData user = null;
        private CardData cardData = null;
        private IServiceManager serviceManager = null;

        private void InitDIContainer()
        {
            DIContainerFactory diFactory = new DIContainerFactory();
            IDIContainer di = diFactory.CreateAndLoadDIContainer();
            FormClosed += SapSoapCardWriterWindow_FormClosed;
            cardWriter = di.GetInstance<ICardWriter>();
            logger = di.GetInstance<ILogger>();
            var cm = di.GetInstance<IConfigurationManager<ISapSoapCardWriterConfig>>();
            cm.LoadConfiguation();
            di.RegisterInstance<ISapSoapCardWriterConfig>(cm.Config);
            serviceManager = di.GetInstance<IServiceManager>();

            AppDomain.CurrentDomain.UnhandledException += CurrentDomain_UnhandledException;
        }

        private void SapSoapCardWriterWindow_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (cardWriter != null)
            {
                cardWriter.Dispose();
            }
        }

        private void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            logger.Error(e.ExceptionObject.ToString());
        }

        public SapSoapCardWriterWindow()
        {
            InitializeComponent();
            InitDIContainer();

            cardWriter.ReaderStateChanged += cardWriter_ReaderStateChanged;
        }

        private bool IsDataSizeOk(CardData cd)
        {
            if(cd.AllEncryptedData.Length + cd.PublicEncryptedData.Length > MAXDATASIZE)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        private async Task StateChange(ReaderState newState)
        {
            try
            {
                if (user != null)
                {
                    cardData = null;
                    btnWriteCard.Enabled = false;
                    tbBirthPlace.Text = string.Empty;
                    tbBirthPlace.Enabled = false;
                    tbFullName.Text = string.Empty;
                    tbFullName.Enabled = false;

                    if (newState == ReaderState.CardPresent)
                    {
                        toolReaderStatus.Text = "RFID kiolvasás...";
                        string sn = await cardWriter.GetSerialNumberAsync();
                        toolReaderStatus.Text = "Kártya beolvasás...";
                        logger.Debug("Serial number: {0}", sn);
                        cardData = await serviceManager.GetCardDataAsync(user, sn); //"1892567125"
                        if (!string.IsNullOrEmpty(cardData.ErrorString))
                        {
                            btnWriteCard.Enabled = false;
                            tbBirthPlace.Text = string.Empty;
                            tbBirthPlace.Enabled = false;
                            tbFullName.Text = string.Empty;
                            tbFullName.Enabled = false;
                            tbBirthDate.Text = string.Empty;
                            tbBirthDate.Enabled = false;
                            tbTaxId.Text = string.Empty;
                            tbTaxId.Enabled = false;
                            tbChamberId.Text = string.Empty;
                            tbChamberId.Enabled = false;

                            toolReaderStatus.Text = "Adat lekérés közben hiba történt: " + cardData.ErrorString;
                        }
                        else
                        {
                            tbFullName.Text = cardData.UIData.FullName;
                            tbBirthPlace.Text = cardData.UIData.BirthPlace;
                            tbBirthDate.Text = cardData.UIData.BirthDate.ToShortDateString();
                            tbChamberId.Text = cardData.UIData.ChamberId;
                            tbTaxId.Text = cardData.UIData.TaxId;
                            tbBirthPlace.Enabled = true;
                            tbFullName.Enabled = true;
                            tbBirthDate.Enabled = true;
                            tbChamberId.Enabled = true;
                            tbTaxId.Enabled = true;
                            toolReaderStatus.Text = "Kártya beolvasva";
                            btnWriteCard.Enabled = true;
                        }
                    }
                    else
                    {
                        toolReaderStatus.Text = "Nincs kártya";
                    }
                }
                else
                {
                    logger.Warning("Bejelentkezés nélkül nem kérhetőek le a kártya adatai.");
                }
            }
            catch(Exception ex)
            {
                logger.Error(ex.ToString());
                MessageBox.Show("Olvasás sikertelen. Kérjük vegye le a kártyát és ismételje meg a műveletet!", "Olvasási művelet", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void cardWriter_ReaderStateChanged(object sender, BusinessLogic.NFC.ReaderState e)
        {
            this.Invoke(new Action(async () =>
                {
                    await StateChange(e);
                }));
        }

        protected override void OnShown(EventArgs e)
        {
            base.OnShown(e);
            LoginWindow lw = new LoginWindow(logger, serviceManager);
            lw.StartPosition = FormStartPosition.CenterParent;
            var result = lw.ShowDialog();
            if(result == DialogResult.OK)
            {
                user = lw.User;
                this.Text = string.Format("NAK kártyaíró (Felhasználó: {0})", user.LoginName);

                try
                {
                    string sn = cardWriter.GetSerialNumber();
                    StateChange(ReaderState.CardPresent);
                }
                catch(Exception ex)
                {

                }
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
                if (IsDataSizeOk(cardData))
                {
                    cardWriter.WriteCard(cardData.CardKey, new List<string> { cardData.PublicEncryptedData, cardData.AllEncryptedData });
                    List<string> data = cardWriter.ReadNfcTags().Result;
                    if(data.Count == 2)
                    {
                        if(data[0] == cardData.PublicEncryptedData && data[1] == cardData.AllEncryptedData)
                        {
                            toolReaderStatus.Text = "Kártya kész";
                            MessageBox.Show("Kártya írás kész!", "Írási művelet", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        else
                        {
                            toolReaderStatus.Text = "Kártya visszaolvasás sikertelen";
                            MessageBox.Show("A kártyára írt adat nem egyezik meg a szervertől kapottal! Vegye le a kártyát és ismételje meg a műveletet!", "Ellenőrzés művelet", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                    else
                    {
                        toolReaderStatus.Text = "Kártya visszaolvasás sikertelen";
                        MessageBox.Show("A kártya irás után, nem megfelelő számú adattag szerepel a kártyán! Vegye le a kártyát és ismételje meg a műveletet!", "Ellenőrzés művelet", MessageBoxButtons.OK, MessageBoxIcon.Error);

                    }
                }
                else
                {
                    btnWriteCard.Enabled = false;
                    toolReaderStatus.Text = "Kártya írás nem lehetséges";
                    MessageBox.Show("A szervertől lekért őstermelői adat mérete meghaladja a kártya kapacitását! Az írás nem lehetséges!", "Szerver művelet", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch(Exception ex)
            {
                logger.Error(ex.ToString());
                MessageBox.Show("Írás sikertelen. Kérjük vegye le a kártyát és ismételje meg a műveletet!", "Írási művelet", MessageBoxButtons.OK, MessageBoxIcon.Error);
                toolReaderStatus.Text = "Kártya írás sikertelen";
            }
        }
    }
}
