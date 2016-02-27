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
    }


    [DataContract]
    public class LoginData
    {
        [DataMember]
        public bool IsSuccessful
        {
            get;
            set;
        }

        [DataMember]
        public string ErrorString
        {
            get;
            set;
        }
    }

    [DataContract]
    public class CardData
    {
        [DataMember]
        public string CardKey
        {
            get;
            set;
        }

        [DataMember]
        public string ErrorString
        {
            get;
            set;
        }

        [DataMember]
        public CardUIData UIData
        {
            get;
            set;
        }

        [DataMember]
        public string PublicEncryptedData
        {
            get;
            set;
        }

        [DataMember]
        public string AllEncryptedData
        {
            get;
            set;
        }
    }

    [DataContract]
    public class CardUIData
    {
        [DataMember]
        public string FullName
        {
            get;
            set;
        }

        [DataMember]
        public string Address
        {
            get;
            set;
        }
    }

}
