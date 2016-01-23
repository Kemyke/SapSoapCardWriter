using SapSoapCardWriter.Common;
using SapSoapCardWriter.Logger.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SapSoapCardWriter.BusinessLogic
{
    public class MockCardWriter : ICardWriter
    {
        private readonly ILogger logger;
        public MockCardWriter(ILogger logger)
        {
            this.logger = logger;
        }

        public ResultCode WriteCard(string key, string data)
        {
            logger.Debug("Key: {0}. Data: {1}", key, data);
            return ResultCode.OK;
        }
    }
}
