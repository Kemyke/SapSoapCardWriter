using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SapSoapCardWriter.BusinessLogic.NFC
{
    public enum ReaderState
    {
        CardNotPresent,
        CardPresent,
    }

    public interface INfcWriter
    {
        bool Erase(string key);
        bool Prepare(string key);
        bool Lock(string key);
        bool WriteNfcTag(List<string> dataList);

        event EventHandler<ReaderState> ReaderStateChanged;
        byte[] GetCardUID();
    }
}
