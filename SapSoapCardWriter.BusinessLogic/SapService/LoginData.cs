using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SapSoapCardWriter.BusinessLogic.SapService
{
    public class LoginData
    {
        public bool IsSuccessful
        {
            get;
            set;
        }

        public string ErrorString
        {
            get;
            set;
        }
    }
}
