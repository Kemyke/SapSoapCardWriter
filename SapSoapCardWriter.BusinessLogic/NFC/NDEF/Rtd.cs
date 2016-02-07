using SapSoapCardWriter.Logger.Logging;
using System;

namespace SapSoapCardWriter.BusinessLogic.NFC
{
	public class Rtd : Ndef
	{
        public Rtd(byte TNF, string Type, ILogger logger)
            : base(TNF, Type, logger)
		{
		}

        public Rtd(byte TNF, string Type, byte[] id, ILogger logger)
            : base(TNF, Type, id, null, logger)
		{
		}

        public Rtd(byte TNF, string Type, byte[] id, byte[] payload, ILogger logger)
            : base(TNF, Type, id, payload, logger)
		{
		}

        public Rtd(Ndef record, ILogger logger)
            : base(record, logger)
		{
		}
	}
}
