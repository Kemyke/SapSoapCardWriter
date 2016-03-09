using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace MockSapService
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service1" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select Service1.svc or Service1.svc.cs at the Solution Explorer and start debugging.
    public class NakCardService : INakCardService
    {

        public LoginData ValidateUser(string userName, string password)
        {
            if(userName == "bad")
            {
                return new LoginData() { IsSuccessful = false, ErrorString = "Nem jó felhasználó" };
            }
            else
            {
                return new LoginData() { IsSuccessful = true, ErrorString = null };
            }
        }

        public CardData GetCardData(string userName, string password, string rfid)
        {
            if (userName == "bad")
            {
                return new CardData() { CardKey = null, AllEncryptedData = null, PublicEncryptedData = null, UIData = null, ErrorString = "Nincs bejelentkezve" };
            }
            else
            {
                if (rfid == "guid")
                {
                    return new CardData()
                    {
                        ErrorString = null,
                        CardUid = "guid",
                        CardKey = "FADDDEADFADDDEAD",
                        AllEncryptedData = "alldatafromsapmock",
                        PublicEncryptedData = "pubdatafromsapmock",
                        UIData = new CardUIData() { FullName = "Test User", Address = "1111 Test Address 60." }
                    };
                }
                else
                {
                    return new CardData()
                    {
                        ErrorString = "Rfid not found",
                        CardUid = rfid,
                        CardKey = string.Empty,
                        AllEncryptedData = string.Empty,
                        PublicEncryptedData = string.Empty,
                        UIData = null
                    };
                }
            }
        }
    }
}
