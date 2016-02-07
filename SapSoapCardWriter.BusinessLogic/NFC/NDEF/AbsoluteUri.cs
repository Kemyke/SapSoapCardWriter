using SapSoapCardWriter.Logger.Logging;
using System;

namespace SapSoapCardWriter.BusinessLogic.NFC
{
	public class AbsoluteUri : Ndef
	{
		public AbsoluteUri(ILogger logger, byte[] Id, byte[] Payload) 
            : base(Ndef.NDEF_HEADER_TNF_ABSOLUTE_URI, "U", logger)
		{
			ID = Id;
			PAYLOAD = Payload;
		}

        public AbsoluteUri(ILogger logger, string Id, string uri)
            : base(Ndef.NDEF_HEADER_TNF_ABSOLUTE_URI, "U", logger)
		{
			ID = System.Text.Encoding.ASCII.GetBytes(Id);
			PAYLOAD = System.Text.Encoding.ASCII.GetBytes(uri);
		}

		
	}
	
}
