using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SapSoapCardWriter.BusinessLogic.NFC
{
    public interface INfcWriter
    {
        bool Erase(string key);
        bool Prepare(string key);
        bool Lock(string key);
        bool WriteNfcTag(string data);
    }
}
