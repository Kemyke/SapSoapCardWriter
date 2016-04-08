using SapSoapCardWriter.BusinessLogic.SapService;
using System;
using System.Threading.Tasks;

namespace SapSoapCardWriter.GUI
{
    public interface IServiceManager
    {
        CardData GetCardData(UserData userData, string rfid);
        Task<CardData> GetCardDataAsync(UserData userData, string rfid);
        LoginData ValidateUser(string userName, string password);
        Task<LoginData> ValidateUserAsync(string userName, string password);
    }
}
