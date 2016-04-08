using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SapSoapCardWriter.BusinessLogic.SapService
{
    public class CardData
    {
        public string CardKey
        {
            get;
            set;
        }

        public string CardUid
        {
            get;
            set;
        }

        public string ErrorString
        {
            get;
            set;
        }

        public CardUIData UIData
        {
            get;
            set;
        }

        public string PublicEncryptedData
        {
            get;
            set;
        }

        public string AllEncryptedData
        {
            get;
            set;
        }
    }
}
