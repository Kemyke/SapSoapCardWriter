using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SapSoapCardWriter.BusinessLogic.SapService
{
    public class CardUIData
    {
        public string FullName
        {
            get;
            set;
        }

        public string BirthPlace
        {
            get;
            set;
        }

        public DateTime BirthDate
        {
            get;
            set;
        }

        public string ChamberId
        {
            get;
            set;
        }

        //Adóazonosító
        public string TaxId
        {
            get;
            set;
        }

        //Adószám
        public string TaxNo
        {
            get;
            set;
        }

        public string CardType
        {
            get;
            set;
        }

        public string CardStatus
        {
            get;
            set;
        }

        public string LastWriteDate
        {
            get;
            set;
        }

        public string LastWriteUser
        {
            get;
            set;
        }
    }
}
