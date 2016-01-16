﻿using SapSoapCardWriter.Logger.Logging;
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

        private readonly ILogger logger;

        public NfcWriter(ILogger logger)
        {
            this.logger = logger;
        }

        public void Erase()
        {
            string p_key = null;
            int p_key_type = 0;
            string p_set_key = null;
            int p_set_key_type = 0;
            string p_reader = null;

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
                CardBuffer key = new CardBuffer(p_key);
                key_buffer = new byte[24];
                Array.ConstrainedCopy(key.GetBytes(), 0, key_buffer, 0, key.GetBytes().Length);
                key_length = key.GetBytes().Length;

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
                    else
                    if (set_key_length == 16)
                    {
                        logger.Debug("Type of new key not specified, assuming TripleDES");
                        p_key_type = KEY_DES_OR_3DES;
                    }
                    else
                    if (set_key_length == 24)
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

            if (p_reader == null)
            {
                logger.Error("No Reader selected!");
                throw new InvalidOperationException("No Reader selected!");
            }


            string ReaderName;

            /* Verify the reader */
            try
            {
                ReaderName = SCARD.Readers[Int32.Parse(p_reader)];
            }
            catch (Exception ex)
            {
                logger.Error("Reader not found! Exception: {0}.", ex.ToString());
                throw new InvalidOperationException("Reader not found!");
            }
            if (p_reader == null)
            {
                logger.Error("Reader not found!");
                throw new InvalidOperationException("Reader not found!");
            }


            logger.Debug("Looking for Desfire EV1 card on reader " + ReaderName);

            /* Connect to the card */
            SmartCardChannel scard = new SmartCardChannel(ReaderName);

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
            rc = SmartCardDesfire.AttachLibrary(scard.hCard);
            if (rc != SCARD.S_SUCCESS)
            {
                logger.Error("Failed to instantiate the PC/SC Desfire DLL.");
                throw new InvalidOperationException("Failed to instantiate the PC/SC Desfire DLL.");
            }

            rc = SmartCardDesfire.IsoWrapping(scard.hCard, DF_ISO_WRAPPING_CARD);
            if (rc != SCARD.S_SUCCESS)
            {
                logger.Error("Failed to select the ISO 7816 wrapping mode.");
                throw new InvalidOperationException("Failed to select the ISO 7816 wrapping mode.");
            }

            byte[] version_info = new byte[30];

            rc = SmartCardDesfire.GetVersion(scard.hCard, version_info);
            if (rc != SCARD.S_SUCCESS)
            {
                logger.Error("Desfire 'get version' command failed.");
                throw new InvalidOperationException("Desfire 'get version' command failed.");
            }

            logger.Debug("Found a Desfire card\n");

            logger.Debug("Hardware: Vendor=" + version_info[0]
                                + ", Type=" + version_info[1]
                                + ", SubType=" + version_info[2]
                                + ", v" + version_info[3] + "." + version_info[4]);

            logger.Debug("Software: Vendor=" + version_info[7]
                                + ", Type=" + version_info[8]
                                + ", SubType=" + version_info[9]
                                + ", v" + version_info[10] + "." + version_info[11]);


            if ((version_info[0] != 0x04) || (version_info[7] != 0x04))
            {
                logger.Error("Manufacturer is not NXP");
                throw new InvalidOperationException("Manufacturer is not NXP");
            }

            if ((version_info[1] != 0x01) || (version_info[8] != 0x01))
            {
                logger.Error("Type is not Desfire");
                throw new InvalidOperationException("Type is not Desfire");
            }

            if (version_info[10] < 1)
            {
                logger.Error("Software version is below EV1");
                throw new InvalidOperationException("Software version is below EV1");
            }

            logger.Debug("Authenticating...");

            switch (p_key_type)
            {
                case KEY_DES_OR_3DES: rc = SmartCardDesfire.Authenticate(scard.hCard, 0, key_buffer); break;
                case KEY_DES_OR_3DES_ISO: rc = SmartCardDesfire.AuthenticateIso24(scard.hCard, 0, key_buffer); break;
                case KEY_AES_ISO: rc = SmartCardDesfire.AuthenticateAes(scard.hCard, 0, key_buffer); break;
                default: rc = -1; break;
            }
            if (rc != SCARD.S_SUCCESS)
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
                    if (rc == SCARD.S_SUCCESS)
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
                    if (rc == SCARD.S_SUCCESS)
                    {
                        rc = SmartCardDesfire.AuthenticateAes(scard.hCard, 0, blank_buffer);
                    }
                    if (rc == SCARD.S_SUCCESS)
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

                if (rc != SCARD.S_SUCCESS)
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

                if (rc != SCARD.S_SUCCESS)
                {
                    string errorMsg = SmartCardDesfire.GetErrorMessage(rc);
                    logger.Error("Authentication with new key failed! Error message: {0}", errorMsg);
                    throw new InvalidOperationException(string.Format("Authentication with new key failed! Error message: {0}", errorMsg));
                }

            }
            logger.Debug("Formating the card...");

            rc = SmartCardDesfire.FormatPICC(scard.hCard);
            if (rc != SCARD.S_SUCCESS)
            {
                string errorMsg = SmartCardDesfire.GetErrorMessage(rc);
                logger.Error("Format PICC failed! Error message: {0}", errorMsg);
                throw new InvalidOperationException(string.Format("Format PICC failed! Error message: {0}", errorMsg));
            }

            if (p_set_key != null)
            {
                logger.Debug("Changing the key...");
            }

            logger.Debug("Card erase done...");

            SmartCardDesfire.DetachLibrary(scard.hCard);
            scard.Disconnect();
        }

        public void Lock()
        {
            throw new NotImplementedException();
        }

        public void WriteNfcTag(string data)
        {
            throw new NotImplementedException();
        }
    }
}