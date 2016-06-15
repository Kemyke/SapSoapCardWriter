using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SapSoapCardWriter.BusinessLogic.NFC
{
    [Serializable]
    public class NoSmartCardReaderFoundException : Exception
    {
        public NoSmartCardReaderFoundException()
        {
        }

        public NoSmartCardReaderFoundException(string message)
            : base(message)
        {
        }

        public NoSmartCardReaderFoundException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
}
