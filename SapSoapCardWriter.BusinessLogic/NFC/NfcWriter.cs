using SapSoapCardWriter.Logger.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SapSoapCardWriter.BusinessLogic.NFC
{
    public class NfcWriter : INfcWriter
    {
        const int KEY_DES_OR_3DES = 1;
        const int KEY_DES_OR_3DES_ISO = 2;
        const byte DF_ISO_WRAPPING_CARD = 1;
        const int KEY_AES_ISO = 3;
        const byte DF_APPLSETTING2_AES = 0x80;
        const UInt32 NFC_NDEF_DESFIRE_DF_ID = 0xEEEE10;
        const byte DF_APPLSETTING2_ISO_EF_IDS = 0x20;
        const ushort NFC_NDEF_7816_DF_ID = 0xE100;
        const byte NFC_NDEF_DESFIRE_CC_EF_ID = 0x03;
        const byte NFC_NDEF_DESFIRE_DATA_EF_ID = 0x04;
        const byte NDEF_FILE_CONTROL_TAG = 0x04;
        const byte NFC_NDEF_VERSION_2 = 0x20;
        const ushort NFC_NDEF_7816_CC_EF_ID = 0xE103;
        const ushort NFC_NDEF_7816_DATA_EF_ID = 0xE104;
        const UInt32 CC_SIZE = 32;

        private readonly ILogger logger;

        public NfcWriter(ILogger logger)
        {
            this.logger = logger;
        }

        public void StartMonitor()
        {
            List<string> readerNames = GetReaders();
            foreach (string readerName in readerNames)
            {
                try
                {
                    SmartCardReader reader = new SmartCardReader(readerName);
                    reader.StartMonitor(OnStatusChange);
                }
                catch 
                { 
                }
            }
        }

        private void OnStatusChange(uint ReaderState, CardBuffer CardAtr)
        {
            SapSoapCardWriter.BusinessLogic.NFC.ReaderState rs;

            var readers = GetReaders();
            bool isConnected = false;
            foreach(var reader in readers)
            {
                try
                {
                    isConnected = new SmartCardReader(reader).CardPresent;
                    if(isConnected)
                    {
                        break;
                    }
                }
                catch(Exception ex)
                {
                    logger.Error("Cannot connect to reader: {0}. Exception: {1}.", reader, ex);
                }
            }

            if (isConnected)
            {
                rs = NFC.ReaderState.CardPresent;
            }
            else
            {
                rs = NFC.ReaderState.CardNotPresent;
            }

            if(ReaderStateChanged != null)
            {
                ReaderStateChanged(this, rs);
            }
        }

        private SmartCardChannel Init(string readerName, string key)
        {
            string p_key = key;
            int p_key_type = 2;
            string p_set_key = null;
            int p_set_key_type = 0;

            byte[] blank_buffer = new byte[32];

            byte[] key_buffer = new byte[24];
            int key_length = 0;
            byte[] set_key_buffer = new byte[24];
            int set_key_length = 0;

            int rc;

            if (p_key == null)
            {
                logger.Debug("Key not specified, using default");

                key_buffer = new byte[24];
                for (int i = 0; i < key_buffer.Length; i++)
                {
                    key_buffer[i] = 0;
                }
                key_length = 8;
            }
            else
            {
                CardBuffer cardBuffer = new CardBuffer(p_key);
                key_buffer = new byte[24];
                Array.ConstrainedCopy(cardBuffer.GetBytes(), 0, key_buffer, 0, cardBuffer.GetBytes().Length);
                key_length = cardBuffer.GetBytes().Length;

            }

            if ((key_length != 8) && (key_length != 16) && (key_length != 24))
            {
                logger.Error("Key: invalid parameter");
                throw new InvalidOperationException("Key: invalid parameter");
            }

            if (key_length == 8)
            {

                Array.ConstrainedCopy(key_buffer, 0, key_buffer, 8, 8);
                Array.ConstrainedCopy(key_buffer, 0, key_buffer, 16, 8);
            }
            else if (key_length == 16)
            {
                Array.ConstrainedCopy(key_buffer, 0, key_buffer, 16, 8);
            }

            if (p_key_type == 0)
            {
                if (key_length == 8)
                {
                    logger.Debug("Type of key not specified, assuming DES");
                    p_key_type = KEY_DES_OR_3DES;
                }
                else if (key_length == 16)
                {
                    logger.Debug("Type of key not specified, assuming TripleDES");
                    p_key_type = KEY_DES_OR_3DES;
                }
                else if (key_length == 24)
                {
                    logger.Debug("Type of key not specified, assuming TripleDES with 3 keys");
                    p_key_type = KEY_DES_OR_3DES_ISO;
                }
                else
                {
                    logger.Error("You must specify the type of the key!");
                    throw new InvalidOperationException("You must specify the type of the key!");
                }
            }


            if (p_set_key != null)
            {
                CardBuffer set_key = new CardBuffer(p_set_key);
                set_key_buffer = new byte[24];
                Array.ConstrainedCopy(set_key.GetBytes(), 0, set_key_buffer, 0, set_key.GetBytes().Length);
                set_key_length = set_key.GetBytes().Length;


                if ((set_key_length != 8) && (set_key_length != 16) && (set_key_length != 24))
                {
                    logger.Error("New key: invalid parameter");
                    throw new InvalidOperationException("New key: invalid parameter");
                }

                if (set_key_length == 8)
                {
                    Array.ConstrainedCopy(set_key_buffer, 0, set_key_buffer, 8, 8);
                    Array.ConstrainedCopy(set_key_buffer, 0, set_key_buffer, 16, 8);
                }
                else if (set_key_length == 16)
                {
                    Array.ConstrainedCopy(set_key_buffer, 0, set_key_buffer, 16, 8);
                }

                if (p_set_key_type == 0)
                {
                    if (set_key_length == 8)
                    {
                        logger.Debug("Type of new key not specified, assuming DES");
                        p_key_type = KEY_DES_OR_3DES;
                    }
                    else if (set_key_length == 16)
                    {
                        logger.Debug("Type of new key not specified, assuming TripleDES");
                        p_key_type = KEY_DES_OR_3DES;
                    }
                    else if (set_key_length == 24)
                    {
                        logger.Debug("Type of new key not specified, assuming TripleDES with 3 keys");
                        p_key_type = KEY_DES_OR_3DES_ISO;
                    }
                    else
                    {
                        logger.Error("You must specify the type of the key!");
                        throw new InvalidOperationException("You must specify the type of the key!");
                    }
                }
            }

            logger.Debug("Looking for Desfire EV1 card on reader " + readerName);

            SmartCardChannel scard = Connect(readerName);

            byte[] version_info = new byte[30];

            //rc = SmartCardDesfire.GetVersion(scard.hCard, version_info);
            //if (rc != SmartCard.S_SUCCESS)
            //{
            //    logger.Error("Desfire 'get version' command failed.");
            //    throw new InvalidOperationException("Desfire 'get version' command failed.");
            //}

            //logger.Debug("Found a Desfire card");

            //logger.Debug("Hardware: Vendor=" + version_info[0]
            //                    + ", Type=" + version_info[1]
            //                    + ", SubType=" + version_info[2]
            //                    + ", v" + version_info[3] + "." + version_info[4]);

            //logger.Debug("Software: Vendor=" + version_info[7]
            //                    + ", Type=" + version_info[8]
            //                    + ", SubType=" + version_info[9]
            //                    + ", v" + version_info[10] + "." + version_info[11]);


            //if ((version_info[0] != 0x04) || (version_info[7] != 0x04))
            //{
            //    logger.Error("Manufacturer is not NXP");
            //    throw new InvalidOperationException("Manufacturer is not NXP");
            //}

            //if ((version_info[1] != 0x01) || (version_info[8] != 0x01))
            //{
            //    logger.Error("Type is not Desfire");
            //    throw new InvalidOperationException("Type is not Desfire");
            //}

            //if (version_info[10] < 1)
            //{
            //    logger.Error("Software version is below EV1");
            //    throw new InvalidOperationException("Software version is below EV1");
            //}

            logger.Debug("Authenticating...");

            switch (p_key_type)
            {
                case KEY_DES_OR_3DES: rc = SmartCardDesfire.Authenticate(scard.hCard, 0, key_buffer); break;
                case KEY_DES_OR_3DES_ISO: rc = SmartCardDesfire.AuthenticateIso24(scard.hCard, 0, key_buffer); break;
                case KEY_AES_ISO: rc = SmartCardDesfire.AuthenticateAes(scard.hCard, 0, key_buffer); break;
                default: rc = -1; break;
            }
            if (rc != SmartCard.S_SUCCESS)
            {
                string errorMsg = SmartCardDesfire.GetErrorMessage(rc);
                logger.Error("Authentication failed! Error messag: {0}", errorMsg);
                throw new InvalidOperationException(string.Format("Authentication failed! Error messag: {0}", errorMsg));
            }

            if (set_key_length > 0)
            {

                logger.Debug("Setting the new key...");

                if ((p_key_type == KEY_DES_OR_3DES) && (p_set_key_type == KEY_DES_OR_3DES))
                {
                    rc = SmartCardDesfire.ChangeKey(scard.hCard, 0, set_key_buffer, null);
                }
                else if ((p_key_type == KEY_DES_OR_3DES) && (p_set_key_type == KEY_DES_OR_3DES_ISO))
                {
                    rc = SmartCardDesfire.AuthenticateIso24(scard.hCard, 0, key_buffer);
                    if (rc == SmartCard.S_SUCCESS)
                    {
                        rc = SmartCardDesfire.ChangeKey24(scard.hCard, 0, set_key_buffer, null);
                    }
                }
                else if ((p_key_type == KEY_DES_OR_3DES) && (p_set_key_type == KEY_AES_ISO))
                {
                    rc = SmartCardDesfire.ChangeKeyAes(scard.hCard, DF_APPLSETTING2_AES | 0, 0, set_key_buffer, null);
                }
                else if ((p_key_type == KEY_DES_OR_3DES_ISO) && (p_set_key_type == KEY_DES_OR_3DES))
                {
                    rc = SmartCardDesfire.ChangeKeyAes(scard.hCard, DF_APPLSETTING2_AES | 0, 0, blank_buffer, null);
                    if (rc == SmartCard.S_SUCCESS)
                    {
                        rc = SmartCardDesfire.AuthenticateAes(scard.hCard, 0, blank_buffer);
                    }
                    if (rc == SmartCard.S_SUCCESS)
                    {
                        rc = SmartCardDesfire.ChangeKey(scard.hCard, 0, set_key_buffer, null);
                    }
                }
                else if ((p_key_type == KEY_DES_OR_3DES_ISO) && (p_set_key_type == KEY_DES_OR_3DES_ISO))
                {
                    rc = SmartCardDesfire.ChangeKey24(scard.hCard, 0, set_key_buffer, null);
                }
                else if ((p_key_type == KEY_DES_OR_3DES_ISO) && (p_set_key_type == KEY_AES_ISO))
                {
                    rc = SmartCardDesfire.ChangeKeyAes(scard.hCard, DF_APPLSETTING2_AES | 0, 0, set_key_buffer, null);
                }
                else if ((p_key_type == KEY_AES_ISO) && (p_set_key_type == KEY_DES_OR_3DES))
                {
                    rc = SmartCardDesfire.ChangeKey(scard.hCard, 0, set_key_buffer, null);
                }
                else if ((p_key_type == KEY_AES_ISO) && (p_set_key_type == KEY_DES_OR_3DES_ISO))
                {
                    rc = SmartCardDesfire.ChangeKey24(scard.hCard, 0, set_key_buffer, null);
                }
                else if ((p_key_type == KEY_AES_ISO) && (p_set_key_type == KEY_AES_ISO))
                {
                    rc = SmartCardDesfire.ChangeKeyAes(scard.hCard, DF_APPLSETTING2_AES | 0, 0, set_key_buffer, null);
                }

                if (rc != SmartCard.S_SUCCESS)
                {
                    string errorMsg = SmartCardDesfire.GetErrorMessage(rc);
                    logger.Error("Change key failed! Error message: {0}", errorMsg);
                    throw new InvalidOperationException(string.Format("Change key failed! Error message: {0}", errorMsg));
                }

                switch (p_set_key_type)
                {
                    case KEY_DES_OR_3DES: rc = SmartCardDesfire.Authenticate(scard.hCard, 0, set_key_buffer); break;
                    case KEY_DES_OR_3DES_ISO: rc = SmartCardDesfire.AuthenticateIso24(scard.hCard, 0, set_key_buffer); break;
                    case KEY_AES_ISO: rc = SmartCardDesfire.AuthenticateAes(scard.hCard, 0, set_key_buffer); break;
                    default: rc = -1; break;
                }

                if (rc != SmartCard.S_SUCCESS)
                {
                    string errorMsg = SmartCardDesfire.GetErrorMessage(rc);
                    logger.Error("Authentication with new key failed! Error message: {0}", errorMsg);
                    throw new InvalidOperationException(string.Format("Authentication with new key failed! Error message: {0}", errorMsg));
                }
            }

            if (p_set_key != null)
            {
                logger.Debug("Changing the key...");
            }

            return scard;
        }

        private SmartCardChannel Connect(string readerName)
        {
            SmartCardChannel scard = new SmartCardChannel(logger, readerName);

            if (scard == null)
            {
                logger.Error("Failed to connect to the target card.");
                throw new InvalidOperationException("Failed to connect to the target card.");
            }

            if (!scard.Connect())
            {
                logger.Error("Failed to connect to the target card.");
                throw new InvalidOperationException("Failed to connect to the target card.");
            }

            /* Open the Desfire DLL */
            int rc = SmartCardDesfire.AttachLibrary(scard.hCard);
            if (rc != SmartCard.S_SUCCESS)
            {
                logger.Error("Failed to instantiate the PC/SC Desfire DLL.");
                throw new InvalidOperationException("Failed to instantiate the PC/SC Desfire DLL.");
            }

            rc = SmartCardDesfire.IsoWrapping(scard.hCard, DF_ISO_WRAPPING_CARD);
            if (rc != SmartCard.S_SUCCESS)
            {
                logger.Error("Failed to select the ISO 7816 wrapping mode.");
                throw new InvalidOperationException("Failed to select the ISO 7816 wrapping mode.");
            }
            
            return scard;
        }

        private void Erase(SmartCardChannel scard, string key)
        {
            logger.Debug("Formating the card...");

            int rc = SmartCardDesfire.FormatPICC(scard.hCard);
            if (rc != SmartCard.S_SUCCESS)
            {
                string errorMsg = SmartCardDesfire.GetErrorMessage(rc);
                logger.Error("Format PICC failed! Error message: {0}", errorMsg);
                throw new InvalidOperationException(string.Format("Format PICC failed! Error message: {0}", errorMsg));
            }

            logger.Debug("Card erase done...");
        }

        public bool Erase(string key)
        {
            bool isSuccess = false;
            List<string> readerNames = GetReaders();
            foreach (string readerName in readerNames)
            {
                SmartCardChannel scard = null;
                try
                {
                    scard = Init(readerName, key);
                    Erase(scard, key);
                    logger.Info("Erase successful with reader: {0}.", readerName);
                    isSuccess = true;
                }
                catch (InvalidOperationException ex)
                {
                    logger.Warning("Cannot erase with reader: {0}. Exception: {1}.", readerName, ex.ToString());
                }
                finally
                {
                    if (scard != null)
                    {
                        SmartCardDesfire.DetachLibrary(scard.hCard);
                        scard.Disconnect();
                    }
                }
            }

            if (!isSuccess)
            {
                logger.Error("Cannot erase with any reader!");
            }

            return isSuccess;
        }

        private void Lock(SmartCardChannel scard)
        {
            NfcTag tag;
            string msg = null;
            bool isFormattable = false;

            if (!NfcTag.Recognize(logger, scard, out tag, out msg, out isFormattable))
            {
                throw new InvalidOperationException("Unrecognized or unsupported tag!");
            }

            if (!tag.Lock())
            {
                throw new InvalidOperationException("Unable to lock tag!");
            }
        }

        public bool Lock(string key)
        {
            bool isSuccess = false;
            List<string> readerNames = GetReaders();
            foreach (string readerName in readerNames)
            {
                SmartCardChannel scard = null;
                try
                {
                    SmartCardReader reader = new SmartCardReader(readerName);
                    scard = new SmartCardChannel(logger, reader);
                    bool connectSuccess = scard.Connect();
                    if (!connectSuccess)
                    {
                        logger.Error("Connect failed!");
                    }
                    else
                    {
                        logger.Info("Connect successfull!");
                        Lock(scard);
                    }

                    logger.Info("Lock successful with reader: {0}.", readerName);
                    isSuccess = true;
                }
                catch (InvalidOperationException ex)
                {
                    logger.Warning("Cannot erase with reader: {0}. Exception: {1}.", readerName, ex.ToString());
                }
                finally
                {
                    if (scard != null)
                    {
                        scard.Disconnect();
                    }
                }
            }

            if (!isSuccess)
            {
                logger.Error("Cannot lock with any reader!");
            }

            return isSuccess;
        }

        private List<string> GetReaders()
        {
            if (SmartCard.Readers == null || SmartCard.Readers.Length == 0)
            {
                throw new InvalidOperationException("WriteNfcTag! No reader found!");
            }

            List<string> ret = new List<string>();
            string reader_list = "";
            for (int i = 0; i < SmartCard.Readers.Length - 1; i++)
            {
                reader_list += "Reader " + i + "=" + SmartCard.Readers[i] + "  -  ";
            }
            reader_list += "Reader " + (SmartCard.Readers.Length - 1) + "=" + SmartCard.Readers[SmartCard.Readers.Length - 1];
            logger.Debug(reader_list);

            ret.AddRange(SmartCard.Readers);
            return ret;
        }

        private void WriteNfcTag(SmartCardChannel scard, List<string> dataList)
        {
            NfcTag tag;
            string msg = null;
            bool isFormattable = false;

            if (!NfcTag.Recognize(logger, scard, out tag, out msg, out isFormattable))
            {
                throw new InvalidOperationException("Unrecognized or unsupported tag!");
            }                        

            foreach (string data in dataList)
            {
                RtdText t = new RtdText(logger, data);
                tag.Content.Add(t);
            }                        

            if (!tag.Write())
            {
                throw new InvalidOperationException("Unable to write onto the tag");
            }            
        }

        public bool WriteNfcTag(List<string> dataList)
        {
            bool isSuccess = false;
            List<string> readerNames = GetReaders();
            foreach (string readerName in readerNames)
            {
                SmartCardChannel scard = null;
                try
                {
                    SmartCardReader reader = new SmartCardReader(readerName);
                    scard = new SmartCardChannel(logger, reader);
                    bool connectSuccess = scard.Connect();
                    if (!connectSuccess)
                    {
                        logger.Error("Connect failed!");
                    }
                    else
                    {
                        logger.Info("Connect successfull!");
                        WriteNfcTag(scard, dataList);
                        isSuccess = true;
                    }
                }
                catch (InvalidOperationException ex)
                {
                    logger.Warning("Cannot erase with reader: {0}. Exception: {1}.", readerName, ex.ToString());
                }
                finally
                {
                    if(scard != null)
                    {
                        scard.Disconnect();
                    }
                }
            }

            if (!isSuccess)
            {
                logger.Error("Cannot erase with any reader!");
            }

            return isSuccess;
        }

        private void Prepare(SmartCardChannel scard)
        {
            byte[] nfc_aid = new byte[7];
            byte[] NFC_NDEF_7816_AID = new byte[7] { 0xD2, 0x76, 0x00, 0x00, 0x85, 0x01, 0x01 };
            nfc_aid = NFC_NDEF_7816_AID;
            byte[] cc_buffer = new byte[15];

            logger.Debug("Creating the NFC NDEF application...");

            int rc = SmartCardDesfire.CreateIsoApplication(scard.hCard,
                                                    NFC_NDEF_DESFIRE_DF_ID,
                                                    0xFF,
                                                    DF_APPLSETTING2_ISO_EF_IDS | 0,
                                                    NFC_NDEF_7816_DF_ID,
                                                    nfc_aid,
                                                    (byte)nfc_aid.Length);
            if (rc != SmartCard.S_SUCCESS)
            {
                string errorMsg = SmartCardDesfire.GetErrorMessage(rc);
                logger.Error("Create application failed! Error message: {0}", errorMsg);
                throw new InvalidOperationException(string.Format("Create application failed! Error message: {0}", errorMsg));
            }

            logger.Debug("Entering the NFC NDEF directory...");

            rc = SmartCardDesfire.SelectApplication(scard.hCard, NFC_NDEF_DESFIRE_DF_ID);
            if (rc != SmartCard.S_SUCCESS)
            {
                string errorMsg = SmartCardDesfire.GetErrorMessage(rc);
                logger.Error("Select application failed! Error message: {0}", errorMsg);
                throw new InvalidOperationException(string.Format("Select application failed! Error message: {0}", errorMsg));
            }

            logger.Debug("Creating the NFC CC file...");

            rc = SmartCardDesfire.CreateIsoStdDataFile(scard.hCard,
                                                    NFC_NDEF_DESFIRE_CC_EF_ID,
                                                    NFC_NDEF_7816_CC_EF_ID,
                                                    0,
                                                    0xEEEE,
                                                    CC_SIZE);
            if (rc != SmartCard.S_SUCCESS)
            {
                string errorMsg = SmartCardDesfire.GetErrorMessage(rc);
                logger.Error("Create CC file failed! Error message: {0}", errorMsg);
                throw new InvalidOperationException(string.Format("Create CC file failed! Error message: {0}", errorMsg));
            }

            UInt32 ndef_size = 0;
            rc = SmartCardDesfire.GetFreeMemory(scard.hCard, ref ndef_size);
            if (rc != SmartCard.S_SUCCESS)
            {
                string errorMsg = SmartCardDesfire.GetErrorMessage(rc);
                logger.Error("Get free memory failed! Error message: {0}", errorMsg);
                throw new InvalidOperationException(string.Format("Get free memory failed! Error message: {0}", errorMsg));
            }

            int p_size = 0;
            if (p_size != 0)
            {
                if (ndef_size < p_size)
                {
                    string errorMsg = SmartCardDesfire.GetErrorMessage(rc);
                    logger.Error("NDEF size=" + p_size + ", bigger than available space on card (" + ndef_size + ")! Error message: {0}", errorMsg);
                    throw new InvalidOperationException(string.Format("NDEF size=" + p_size + ", bigger than available space on card (" + ndef_size + ")! Error message: {0}", errorMsg));
                }
                ndef_size = (UInt32)p_size;
            }

            logger.Debug("Creating the NFC NDEF file...");

            rc = SmartCardDesfire.CreateIsoStdDataFile(scard.hCard,
                                                        NFC_NDEF_DESFIRE_DATA_EF_ID,
                                                        NFC_NDEF_7816_DATA_EF_ID,
                                                        0,
                                                        0xEEEE,
                                                        ndef_size);
            if (rc != SmartCard.S_SUCCESS)
            {
                string errorMsg = SmartCardDesfire.GetErrorMessage(rc);
                logger.Error("Create NDEF file failed: " + NFC_NDEF_DESFIRE_DATA_EF_ID + "-" + NFC_NDEF_7816_DATA_EF_ID + "-" + ndef_size + " Error message: {0}", errorMsg);
                throw new InvalidOperationException(string.Format("Create NDEF file failed: " + NFC_NDEF_DESFIRE_DATA_EF_ID + "-" + NFC_NDEF_7816_DATA_EF_ID + "-" + ndef_size + " Error message: {0}", errorMsg));
            }

            logger.Debug("Populating the NFC CC file...");

            cc_buffer[0] = 0x00;
            cc_buffer[1] = 0x0F;
            cc_buffer[2] = NFC_NDEF_VERSION_2;
            cc_buffer[3] = 0x00;
            cc_buffer[4] = 0x3B;
            cc_buffer[5] = 0x00;
            cc_buffer[6] = 0x34;
            cc_buffer[7] = NDEF_FILE_CONTROL_TAG;
            cc_buffer[8] = 0x06;
            cc_buffer[9] = (byte)(NFC_NDEF_7816_DATA_EF_ID >> 8);
            cc_buffer[10] = (byte)(NFC_NDEF_7816_DATA_EF_ID & 0x00FF);
            cc_buffer[11] = (byte)(ndef_size >> 8);
            cc_buffer[12] = (byte)(ndef_size & 0x00FF);
            cc_buffer[13] = 0x00;
            cc_buffer[14] = 0x00;

            rc = SmartCardDesfire.WriteData2(scard.hCard,
                                         NFC_NDEF_DESFIRE_CC_EF_ID,
                                         0,
                                         (UInt32)cc_buffer.Length,
                                         cc_buffer);
            if (rc != SmartCard.S_SUCCESS)
            {
                string errorMsg = SmartCardDesfire.GetErrorMessage(rc);
                logger.Error("Populate CC file failed! Error message: {0}", errorMsg);
                throw new InvalidOperationException(string.Format("Populate CC file failed! Error message: {0}", errorMsg));
            }

            logger.Debug("Card prepare done...");
        }

        public bool Prepare(string key)
        {
            bool isSuccess = false;
            List<string> readerNames = GetReaders();
            foreach (string readerName in readerNames)
            {
                SmartCardChannel scard = null;
                try
                {
                    scard = Init(readerName, key);
                    Prepare(scard);
                    isSuccess = true;
                }
                catch (InvalidOperationException ex)
                {
                    logger.Warning("Cannot prepare with reader: {0}. Exception: {1}.", readerName, ex.ToString());
                }
                finally
                {
                    if (scard != null)
                    {
                        SmartCardDesfire.DetachLibrary(scard.hCard);
                        scard.Disconnect();
                    }
                }
            }

            if (!isSuccess)
            {
                logger.Error("Cannot prepare with any reader!");
            }
            return isSuccess;
        }


        public event EventHandler<ReaderState> ReaderStateChanged;


        public byte[] GetCardUID(String key)
        {
            List<string> readerNames = GetReaders();
            foreach (string readerName in readerNames)
            {
                SmartCardChannel scard = null;
                try
                {
                    scard = Init(readerName, key);
                    scard = Connect(readerName);                    
                    byte[] uid = new byte[7];
                    int rc = SmartCardDesfire.GetCardUID(scard.hCard, uid);

                    if (rc != SmartCard.S_SUCCESS)
                    {
                        string errorMsg = SmartCardDesfire.GetErrorMessage(rc);
                        logger.Error("Get RFID failed! Error message: {0}", errorMsg);
                        throw new InvalidOperationException(string.Format("Get RFID failed! Error message: {0}", errorMsg));
                    }
                    
                    return uid;
                }
                catch (InvalidOperationException ex)
                {
                    logger.Warning("Cannot prepare with reader: {0}. Exception: {1}.", readerName, ex.ToString());
                }
                finally
                {
                    if (scard != null)
                    {
                        SmartCardDesfire.DetachLibrary(scard.hCard);
                        scard.Disconnect();
                    }
                }
            }

            throw new InvalidOperationException("Cannot get RFID from any reader!");
        }

        public List<string> ReadNfcTags()
        {
            List<string> ret = new List<string>();
            List<string> readerNames = GetReaders();
            foreach (string readerName in readerNames)
            {
                SmartCardChannel scard = null;
                try
                {
                    scard = Connect(readerName);

                    NfcTag tag;
                    string msg = null;
                    bool isFormattable = false;

                    if (!NfcTag.Recognize(logger, scard, out tag, out msg, out isFormattable))
                    {
                        throw new InvalidOperationException("Unrecognized or unsupported tag!");
                    }

                    foreach (var data in tag.Content)
                    {
                        RtdText text = data as RtdText;
                        if (text != null)
                        {
                            ret.Add(text.Value);
                        }
                        else
                        {
                            throw new NotSupportedException(string.Format("Invlaid NFC Tag type: {0}.", data.GetType().Name));
                        }
                    }
                    return ret;
                }
                catch (InvalidOperationException ex)
                {
                    logger.Warning("Cannot prepare with reader: {0}. Exception: {1}.", readerName, ex.ToString());
                }
                finally
                {
                    if (scard != null)
                    {
                        SmartCardDesfire.DetachLibrary(scard.hCard);
                        scard.Disconnect();
                    }
                }
            }

            throw new InvalidOperationException("Cannot get data from any reader!");
        }
    }
}
