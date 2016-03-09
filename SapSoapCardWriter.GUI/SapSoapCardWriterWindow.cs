﻿using SapSoapCardWriter.BusinessLogic;
using SapSoapCardWriter.BusinessLogic.NFC;
using SapSoapCardWriter.Common.DIContainer;
using SapSoapCardWriter.GUI.NakCardService;
using SapSoapCardWriter.Logger.Logging;
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
    public partial class SapSoapCardWriterWindow : Form
    {
        private ICardWriter cardWriter = null;
        private ILogger logger = null;

        private UserData user = null;
        private ServiceManager sm = null;
        private CardData cardData = null;

        private void InitDIContainer()
        {
            DIContainerFactory diFactory = new DIContainerFactory();
            IDIContainer di = diFactory.CreateAndLoadDIContainer();
            cardWriter = di.GetInstance<ICardWriter>();
            logger = di.GetInstance<ILogger>();
        }

        public SapSoapCardWriterWindow()
        {
            InitializeComponent();
            InitDIContainer();

            sm = new ServiceManager();
            cardWriter.ReaderStateChanged += cardWriter_ReaderStateChanged;
        }

        private async Task StateChange(ReaderState newState)
        {
            if (user != null)
            {
                cardData = null;
                btnWriteCard.Enabled = false;
                tbAddress.Text = string.Empty;
                tbAddress.Enabled = false;
                tbFullName.Text = string.Empty;
                tbFullName.Enabled = false;

                if (newState == ReaderState.CardPresent)
                {
                    toolReaderStatus.Text = "RFID kiolvasás...";
                    List<string> data = await cardWriter.ReadNfcTags();
                    string rfid = data[0];
                    toolReaderStatus.Text = "Kártya beolvasás...";
                    cardData = await sm.GetCardDataAsync(user, rfid);
                    if (!string.IsNullOrEmpty(cardData.ErrorString))
                    {
                        btnWriteCard.Enabled = false;
                        tbAddress.Text = string.Empty;
                        tbAddress.Enabled = false;
                        tbFullName.Text = string.Empty;
                        tbFullName.Enabled = false;
                        toolReaderStatus.Text = "Hibás kártya";
                    }
                    else
                    {
                        tbFullName.Text = cardData.UIData.FullName;
                        tbAddress.Text = cardData.UIData.Address;
                        tbAddress.Enabled = true;
                        tbFullName.Enabled = true;
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
            LoginWindow lw = new LoginWindow();
            lw.StartPosition = FormStartPosition.CenterParent;
            var result = lw.ShowDialog();
            if(result == DialogResult.OK)
            {
                user = lw.User;
                this.Text = string.Format("NAK kártyaíró (Felhasználó: {0})", user.LoginName);
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
                logger.Error(ex.ToString());
                toolReaderStatus.Text = "Kártya írás sikertelen";
            }
        }
    }
}
