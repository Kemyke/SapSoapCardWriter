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
        private readonly NfcWriter writer;

        public NfcCardWriter(ILogger logger)
        {
            this.logger = logger;
            writer = new NfcWriter(logger);
        }

        public ResultCode WriteCard(string key, string data)
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
            isSuccess = writer.WriteNfcTag(data);
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
    }
}