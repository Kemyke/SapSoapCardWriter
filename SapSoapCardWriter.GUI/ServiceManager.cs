﻿using SapSoapCardWriter.BusinessLogic.SapService;
using SapSoapCardWriter.Common;
using SapSoapCardWriter.GUI.NakCardService;
using SapSoapCardWriter.GUI.SapNakAuthService;
using SapSoapCardWriter.GUI.SapNakCardService;
using SapSoapCardWriter.GUI.SapNakResponseService;
using SapSoapCardWriter.Logger.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Authentication;
using System.Text;
using System.Threading.Tasks;

namespace SapSoapCardWriter.GUI
{
    public class ServiceManager : IServiceManager
    {
        private readonly ILogger logger;
        private Z_CRM_NEBIH_CARD_AUTHClient authClient;
        private Z_CRM_NEBIH_CARD_FILE_GETClient cardClient;
        private Z_CRM_NEBIH_CARD_WRSUCCClient respClient;

        public ServiceManager(ILogger logger, ISapSoapCardWriterConfig config)
        {
            this.logger = logger;
            authClient = new Z_CRM_NEBIH_CARD_AUTHClient();
            cardClient = new Z_CRM_NEBIH_CARD_FILE_GETClient();
            respClient = new Z_CRM_NEBIH_CARD_WRSUCCClient();
        }

        public LoginData ValidateUser(string userName, string password)
        {
            var resp = authClient.Z_CRM_NEBIH_CARD_AUTH(new Z_CRM_NEBIH_CARD_AUTH_DATA { UNAME = userName, PASSWD = password });
            LoginData ld = new LoginData { IsSuccessful = string.IsNullOrEmpty(resp.ERROR), ErrorString = resp.ERROR };
            return ld;
        }

        public async Task<LoginData> ValidateUserAsync(string userName, string password)
        {
            var resp = await authClient.Z_CRM_NEBIH_CARD_AUTHAsync(new Z_CRM_NEBIH_CARD_AUTH_DATA { UNAME = userName, PASSWD = password });
            
            LoginData ld = new LoginData { IsSuccessful = string.IsNullOrEmpty(resp.Z_CRM_NEBIH_CARD_AUTHResponse.ERROR), ErrorString = resp.Z_CRM_NEBIH_CARD_AUTHResponse.ERROR };
            return ld;
        }

        public CardData GetCardData(UserData userData, string rfid)
        {
            var resp = cardClient.Z_CRM_NEBIH_CARD_FILE_GET(new Z_CRM_NEBIH_CARD_FILE_GET_DATA() { CARD_ID = rfid, UNAME = userData.LoginName, PASSWD = userData.Password });
            DateTime bd;
            if (!DateTime.TryParse(resp.INFO.BIRTHDATE, out bd))
            {
                bd = DateTime.MinValue;
                logger.Warning("Cannot parse birth date: {0}", resp.INFO.BIRTHDATE);
            }

            CardUIData uidata = new CardUIData
                                        {
                                            FullName = resp.INFO.NAME,
                                            BirthDate = DateTime.Parse(resp.INFO.BIRTHDATE),
                                            BirthPlace = resp.INFO.BIRTHPLACE,
                                            TaxId = resp.INFO.TAXNO,
                                            ChamberId = resp.INFO.KAMAZ,
                                            CardType = resp.INFO.CARD_TYPE,
                                            CardStatus = resp.INFO.CARD_STATUS,
                                            TaxNo = resp.INFO.TAXNO_ORG,
                                            LastWriteDate = resp.INFO.LAST_WRITE_DATE,
                                            LastWriteUser = resp.INFO.LAST_WRITE_USER
                                        };

            CardData cd = new CardData { AllEncryptedData = resp.CARD_NEBIH, PublicEncryptedData = resp.CARD_NAK, CardKey = resp.WRITE_KEY, CardUid = rfid, ErrorString = resp.ERROR, UIData = uidata };
            return cd;
        }

        public async Task<CardData> GetCardDataAsync(UserData userData, string rfid)
        {
            var resp1 = await cardClient.Z_CRM_NEBIH_CARD_FILE_GETAsync(new Z_CRM_NEBIH_CARD_FILE_GET_DATA() { CARD_ID = rfid, UNAME = userData.LoginName, PASSWD = userData.Password });
            var resp = resp1.Z_CRM_NEBIH_CARD_FILE_GETResponse;

            DateTime bd;
            if(!DateTime.TryParse(resp.INFO.BIRTHDATE, out bd))
            {
                bd = DateTime.MinValue;
                logger.Warning("Cannot parse birth date: {0}", resp.INFO.BIRTHDATE);
            }

            CardUIData uidata = new CardUIData
            {
                FullName = resp.INFO.NAME,
                BirthDate = DateTime.Parse(resp.INFO.BIRTHDATE),
                BirthPlace = resp.INFO.BIRTHPLACE,
                TaxId = resp.INFO.TAXNO,
                ChamberId = resp.INFO.KAMAZ,
                CardType = resp.INFO.CARD_TYPE,
                CardStatus = resp.INFO.CARD_STATUS,
                TaxNo = resp.INFO.TAXNO_ORG,
                LastWriteDate = resp.INFO.LAST_WRITE_DATE,
                LastWriteUser = resp.INFO.LAST_WRITE_USER
            };

            CardData cd = new CardData { AllEncryptedData = resp.CARD_NEBIH, PublicEncryptedData = resp.CARD_NAK, CardKey = resp.WRITE_KEY /*"FADDDEADFADDDEAD"*/, CardUid = rfid, ErrorString = resp.ERROR, UIData = uidata };
            return cd;
        }

        public void MarkWriteSuccessful(UserData userData, string rfid)
        {
            logger.Debug("MarkWriteSuccessful {0}", rfid);
            respClient.Z_CRM_NEBIH_CARD_WRSUCC(new Z_CRM_NEBIH_CARD_WRSUCC_DATA { CARD_ID = rfid, UNAME = userData.LoginName, PASSWD = userData.Password });
            logger.Debug("MarkWriteSuccessful done {0}", rfid);
        }

        public async Task MarkWriteSuccessfulAsync(UserData userData, string rfid)
        {
            await respClient.Z_CRM_NEBIH_CARD_WRSUCCAsync(new Z_CRM_NEBIH_CARD_WRSUCC_DATA { CARD_ID = rfid, UNAME = userData.LoginName, PASSWD = userData.Password });
        }
    }
}
