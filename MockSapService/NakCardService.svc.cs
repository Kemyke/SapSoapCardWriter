using SapSoapCardWriter.BusinessLogic.SapService;
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
                if (rfid == "1892567125")
                {
                    return new CardData()
                    {
                        ErrorString = null,
                        CardUid = "1892567125",
                        CardKey = "FADDDEADFADDDEAD",
                        AllEncryptedData = "alldatafromsapmock",
                        PublicEncryptedData = "pubdatafromsapmock",
                        UIData = new CardUIData() { FullName = "Test User", BirthPlace = "Töttös", CardType = "Kamarai", BirthDate = DateTime.Now, CardStatus = "Aktív", ChamberId = "123", TaxId = "456", TaxNo = "789", LastWriteDate = DateTime.Now.ToShortDateString(), LastWriteUser = "Kaszás Erzsi"  }
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


        public void ReportSuccess(string userName, string password, string rfid)
        {
            
        }

        public IList<EventData> GetEvents(string userName, string password)
        {
            List<EventData> ret = new List<EventData>();
            ret.Add(new EventData { ID = Guid.NewGuid(), Name = "Event1", Location = "Budapest" });
            ret.Add(new EventData { ID = Guid.NewGuid(), Name = "Event2", Location = "Mohács" });
            return ret;
        }

        public CardEventRegistrationData RegisterCardToEvent(string userName, string password, EventData eventData, string rfid)
        {
            return new CardEventRegistrationData();
        }
    }
}
