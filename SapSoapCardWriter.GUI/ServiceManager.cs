using SapSoapCardWriter.GUI.NakCardService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Authentication;
using System.Text;
using System.Threading.Tasks;

namespace SapSoapCardWriter.GUI
{
    public class ServiceManager
    {
        private NakCardServiceClient client;

        public ServiceManager()
        {
            client = new NakCardServiceClient();
        }

        public void ValidateUser(string userName, string password)
        {
            LoginData ld = client.ValidateUser(userName, password);
            if(!ld.IsSuccessful)
            {
                throw new AuthenticationException(ld.ErrorString);
            }
        }

        public CardData GetCardData(UserData userData, string rfid)
        {
            CardData cd = client.GetCardData(userData.LoginName, userData.Password, rfid);
            return cd;
        }
    }
}
