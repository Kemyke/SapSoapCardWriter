using SapSoapCardWriter.Logger.Logging;
using System;

namespace SapSoapCardWriter.BusinessLogic.NFC
{
	public class RtdMedia : Rtd
	{
        public RtdMedia(ILogger logger, string MimeType)
            : base(NDEF_HEADER_TNF_MEDIA_TYPE, MimeType, logger)
		{
			
		}

        public RtdMedia(ILogger logger, string MimeType, string TextContent)
            : base(NDEF_HEADER_TNF_MEDIA_TYPE, MimeType, logger)
		{
			payload = CardBuffer.BytesFromString(TextContent);
		}

        public RtdMedia(ILogger logger, string MimeType, byte[] RawContent)
            : base(NDEF_HEADER_TNF_MEDIA_TYPE, MimeType, logger)
		{
			payload = RawContent;
		}
		
		public string TextContent
		{
			get
			{
				if (payload == null)
					return "";
				return CardBuffer.StringFromBytes(payload);
			}
		}
		
		public byte[] RawContent
		{
			get
			{
				return payload;
			}
		}

	}
	
}
