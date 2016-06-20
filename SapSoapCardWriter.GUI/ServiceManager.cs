﻿using SapSoapCardWriter.BusinessLogic.SapService;
using SapSoapCardWriter.Common;
using SapSoapCardWriter.GUI.NakCardService;
using SapSoapCardWriter.GUI.SapNakAuthService;
using SapSoapCardWriter.GUI.SapNakCardService;
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

        public ServiceManager(ILogger logger, ISapSoapCardWriterConfig config)
        {
            this.logger = logger;
            authClient = new Z_CRM_NEBIH_CARD_AUTHClient();
            cardClient = new Z_CRM_NEBIH_CARD_FILE_GETClient();
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
            CardData cd = new CardData { AllEncryptedData = resp.CARD_NEBIH, PublicEncryptedData = resp.CARD_NAK, CardKey = resp.WRITE_KEY, CardUid = rfid, ErrorString = resp.ERROR, UIData = new CardUIData { FullName = resp.INFO.NAME, BirthDate = DateTime.Parse(resp.INFO.BIRTHDATE), BirthPlace = resp.INFO.BIRTHPLACE, TaxId = resp.INFO.TAXNO, ChamberId = resp.INFO.KAMAZ } };
            return cd;
        }

        public async Task<CardData> GetCardDataAsync(UserData userData, string rfid)
        {
            var resp = await cardClient.Z_CRM_NEBIH_CARD_FILE_GETAsync(new Z_CRM_NEBIH_CARD_FILE_GET_DATA() { CARD_ID = rfid, UNAME = userData.LoginName, PASSWD = userData.Password });
            DateTime bd;
            if(!DateTime.TryParse(resp.Z_CRM_NEBIH_CARD_FILE_GETResponse.INFO.BIRTHDATE, out bd))
            {
                bd = DateTime.MinValue;
                logger.Warning("Cannot parse birth date: {0}", resp.Z_CRM_NEBIH_CARD_FILE_GETResponse.INFO.BIRTHDATE);
            }
            CardData cd = new CardData { AllEncryptedData = resp.Z_CRM_NEBIH_CARD_FILE_GETResponse.CARD_NEBIH, PublicEncryptedData = resp.Z_CRM_NEBIH_CARD_FILE_GETResponse.CARD_NAK, CardKey = resp.Z_CRM_NEBIH_CARD_FILE_GETResponse.WRITE_KEY /*"FADDDEADFADDDEAD"*/, CardUid = rfid, ErrorString = resp.Z_CRM_NEBIH_CARD_FILE_GETResponse.ERROR, UIData = new CardUIData { FullName = resp.Z_CRM_NEBIH_CARD_FILE_GETResponse.INFO.NAME, BirthDate = bd, BirthPlace = resp.Z_CRM_NEBIH_CARD_FILE_GETResponse.INFO.BIRTHPLACE, TaxId = resp.Z_CRM_NEBIH_CARD_FILE_GETResponse.INFO.TAXNO, ChamberId = resp.Z_CRM_NEBIH_CARD_FILE_GETResponse.INFO.KAMAZ } };
            return cd;
        }
    }
}
