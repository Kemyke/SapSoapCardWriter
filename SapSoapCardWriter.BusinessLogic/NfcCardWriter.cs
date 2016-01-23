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

        public ResultCode WriteCard(string data)
        {
            writer.Erase();
            writer.Prepare();
            writer.WriteNfcTag(data);
            writer.Lock();

            return ResultCode.OK;
        }
    }
}