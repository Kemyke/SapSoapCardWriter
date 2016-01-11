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

        public ResultCode WriteCard(string data)
        {
            logger.Debug("Data: {0}", data);
            return ResultCode.OK;
        }
    }
}
