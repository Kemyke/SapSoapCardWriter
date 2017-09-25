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
    [ServiceContract]
    public interface INakCardService
    {
        [OperationContract]
        LoginData ValidateUser(string userName, string password);

        [OperationContract]
        CardData GetCardData(string userName, string password, string rfid);

        [OperationContract]
        void ReportSuccess(string userName, string password, string rfid);

        [OperationContract]
        IList<EventData> GetEvents(string userName, string password);

        [OperationContract]
        CardEventRegistrationData RegisterCardToEvent(string userName, string password, EventData eventData, string rfid);

    }
}
