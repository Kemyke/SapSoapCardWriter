using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SapSoapCardWriter.BusinessLogic.NFC
{
    public interface INfcWriter
    {
        void Erase();
        void Prepare();
        void Lock();
        void WriteNfcTag(string data);
    }
}
