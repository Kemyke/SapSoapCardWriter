using SapSoapCardWriter.BusinessLogic.NFC;
using SapSoapCardWriter.Common;
using SapSoapCardWriter.Logger.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SapSoapCardWriter.BusinessLogic
{
    public class NfcCardWriter : ICardWriter
    {
        private readonly ILogger logger;
        private readonly INfcWriter writer;

        public bool HasSmartCardReader { get; private set; }

        public NfcCardWriter(ILogger logger)
        {
            this.logger = logger;
            writer = new NfcWriter(logger);
            try
            {
                writer.StartMonitor();
                writer.ReaderStateChanged += writer_ReaderStateChanged;
                HasSmartCardReader = true;
            }
            catch (NoSmartCardReaderFoundException ex)
            {
                logger.Error(ex.ToString());
                HasSmartCardReader = false;
            }
        }

        private void writer_ReaderStateChanged(object sender, ReaderState e)
        {
            if (ReaderStateChanged != null)
            {
                ReaderStateChanged(this, e);
            }
        }

        public ResultCode WriteCard(string key, List<string> dataList)
        {
            bool isSuccess;
            logger.Debug("Start erase");
            isSuccess = writer.Erase(key);
            if(!isSuccess)
            {
                throw new InvalidOperationException("Erase failed");
            }

            logger.Debug("Start prepare");
            isSuccess = writer.Prepare(key);
            if (!isSuccess)
            {
                throw new InvalidOperationException("Prepare failed");
            }

            logger.Debug("Start write");
            isSuccess = writer.WriteNfcTag(dataList);
            if (!isSuccess)
            {
                throw new InvalidOperationException("Write failed");
            }


            logger.Debug("Start lock");
            isSuccess = writer.Lock(key);
            if (!isSuccess)
            {
                throw new InvalidOperationException("Lock failed");
            }

            return ResultCode.OK;
        }


        public string GetRfid(string key)
        {            
            byte[] uid = writer.GetCardUID(key);
            string rfid = Encoding.Default.GetString(uid);
            return rfid;
        }


        public event EventHandler<ReaderState> ReaderStateChanged;


        public Task<ResultCode> WriteCardAsync(string key, List<string> dataList)
        {
            return Task.FromResult(WriteCard(key, dataList));
        }

        public Task<string> GetRfidAsync(string key)
        {
            return Task.FromResult(GetRfid(key));
        }

        public Task<List<string>> ReadNfcTags()
        {
            List<string> ret = writer.ReadNfcTags();
            return Task.FromResult(ret);
        }

        public string GetSerialNumber()
        {
            return writer.GetSerialNumber();
        }

        public Task<string> GetSerialNumberAsync()
        {
            return Task.FromResult(writer.GetSerialNumber());
        }

        public void Dispose()
        {
            writer.StopMonitor();
        }
    }
}