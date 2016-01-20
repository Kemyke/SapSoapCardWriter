using System;

namespace SapSoapCardWriter.BusinessLogic.NFC
{
	public class RtdMedia : Rtd
	{
		public RtdMedia(string MimeType) : base(NDEF_HEADER_TNF_MEDIA_TYPE, MimeType)
		{
			
		}
		
		public RtdMedia(string MimeType, string TextContent) : base(NDEF_HEADER_TNF_MEDIA_TYPE, MimeType)
		{
			_payload = CardBuffer.BytesFromString(TextContent);
		}

		public RtdMedia(string MimeType, byte[] RawContent) : base(NDEF_HEADER_TNF_MEDIA_TYPE, MimeType)
		{
			_payload = RawContent;
		}
		
		public string TextContent
		{
			get
			{
				if (_payload == null)
					return "";
				return CardBuffer.StringFromBytes(_payload);
			}
		}
		
		public byte[] RawContent
		{
			get
			{
				return _payload;
			}
		}

	}
	
}
