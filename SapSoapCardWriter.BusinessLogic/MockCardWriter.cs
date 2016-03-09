using SapSoapCardWriter.Common;
using SapSoapCardWriter.Logger.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

namespace SapSoapCardWriter.BusinessLogic
{
    public class MockCardWriter : ICardWriter
    {
        private readonly ILogger logger;
        public MockCardWriter(ILogger logger)
        {
            this.logger = logger;
            Timer timer = new Timer(20000);
            timer.AutoReset = false;
            timer.Elapsed += timer_Elapsed;
            timer.Start();
        }

        private void timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            if(ReaderStateChanged != null)
            {
                ReaderStateChanged(this, NFC.ReaderState.CardPresent);
            }
        }

        public ResultCode WriteCard(string key, List<string> dataList)
        {
            logger.Debug("Key: {0}. Data: {1}", key, dataList);
            return ResultCode.OK;
        }


        public string GetRfid()
        {
            return "rfid";
        }

        public event EventHandler<NFC.ReaderState> ReaderStateChanged;


        public Task<ResultCode> WriteCardAsync(string key, List<string> dataList)
        {
            return Task.FromResult(WriteCard(key, dataList));
        }

        public Task<string> GetRfidAsync()
        {
            return Task.FromResult(GetRfid());
        }

        public string GetRfid(string key)
        {
            throw new NotImplementedException();
        }

        public Task<List<string>> ReadNfcTags()
        {
            return Task.FromResult(new List<string>() { "guid", "encpublicdata", "encfulldata" });
        }
    }
}
