﻿using SapSoapCardWriter.BusinessLogic.SapService;
using SapSoapCardWriter.GUI.NakCardService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Authentication;
using System.Text;
using System.Threading.Tasks;

namespace SapSoapCardWriter.GUI
{
    public class MockServiceManager : IServiceManager
    {
        private NakCardServiceClient client;

        public MockServiceManager()
        {
            client = new NakCardServiceClient();
        }

        public LoginData ValidateUser(string userName, string password)
        {
            LoginData ld = client.ValidateUser(userName, password);
            return ld;
        }

        public Task<LoginData> ValidateUserAsync(string userName, string password)
        {
            Task<LoginData> ld = client.ValidateUserAsync(userName, password);
            return ld;
        }

        public CardData GetCardData(UserData userData, string rfid)
        {
            CardData cd = client.GetCardData(userData.LoginName, userData.Password, rfid);
            return cd;
        }

        public Task<CardData> GetCardDataAsync(UserData userData, string rfid)
        {
            Task<CardData> cd = client.GetCardDataAsync(userData.LoginName, userData.Password, rfid);
            return cd;
        }

        public void MarkWriteSuccessful(UserData userData, string rfid)
        {
            client.ReportSuccess(userData.LoginName, userData.Password, rfid);
        }

        public Task MarkWriteSuccessfulAsync(UserData userData, string rfid)
        {
            return client.ReportSuccessAsync(userData.LoginName, userData.Password, rfid);
        }

        public IList<EventData> GetEvents(UserData userData)
        {
            return client.GetEvents(userData.LoginName, userData.Password);
        }

        public async Task<IList<EventData>> GetEventsAsync(UserData userData)
        {
            var ret = await client.GetEventsAsync(userData.LoginName, userData.Password);
            return ret;
        }

        public CardEventRegistrationData RegisterCardToEvent(UserData userData, EventData eventData, string rfid)
        {
            return client.RegisterCardToEvent(userData.LoginName, userData.Password, eventData, rfid);
        }

        public Task<CardEventRegistrationData> RegisterCardToEventAsync(UserData userData, EventData eventData, string rfid)
        {
            return client.RegisterCardToEventAsync(userData.LoginName, userData.Password, eventData, rfid);
        }
    }
}
