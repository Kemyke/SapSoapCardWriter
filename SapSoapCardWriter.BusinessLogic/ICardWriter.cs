using SapSoapCardWriter.BusinessLogic.NFC;
using SapSoapCardWriter.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SapSoapCardWriter.BusinessLogic
{
    public interface ICardWriter
    {
        ResultCode WriteCard(string key, List<string> dataList);
        Task<ResultCode> WriteCardAsync(string key, List<string> dataList);
        string GetRfid(string key);
        Task<List<string>> ReadNfcTags();
        event EventHandler<ReaderState> ReaderStateChanged;
        string GetSerialNumber();
        Task<string> GetSerialNumberAsync();
    }
}
