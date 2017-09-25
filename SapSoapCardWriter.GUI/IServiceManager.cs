using SapSoapCardWriter.BusinessLogic.SapService;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SapSoapCardWriter.GUI
{
    public interface IServiceManager
    {
        CardData GetCardData(UserData userData, string rfid);
        Task<CardData> GetCardDataAsync(UserData userData, string rfid);
        LoginData ValidateUser(string userName, string password);
        Task<LoginData> ValidateUserAsync(string userName, string password);
        void MarkWriteSuccessful(UserData userData, string rfid);
        Task MarkWriteSuccessfulAsync(UserData userData, string rfid);
        IList<EventData> GetEvents(UserData userData);
        Task<IList<EventData>> GetEventsAsync(UserData userData);
        CardEventRegistrationData RegisterCardToEvent(UserData userData, EventData eventData, string rfid);
        Task<CardEventRegistrationData> RegisterCardToEventAsync(UserData userData, EventData eventData, string rfid);
    }
}
