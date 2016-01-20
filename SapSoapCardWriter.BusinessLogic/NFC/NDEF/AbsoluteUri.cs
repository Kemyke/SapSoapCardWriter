using System;

namespace SapSoapCardWriter.BusinessLogic.NFC
{
	public class AbsoluteUri : Ndef
	{
		public AbsoluteUri(byte[] Id, byte[] Payload) : base(Ndef.NDEF_HEADER_TNF_ABSOLUTE_URI, "U")
		{
			ID = Id;
			PAYLOAD = Payload;
		}
		
		public AbsoluteUri(string Id, string uri) :  base(Ndef.NDEF_HEADER_TNF_ABSOLUTE_URI, "U")
		{
			ID = System.Text.Encoding.ASCII.GetBytes(Id);
			PAYLOAD = System.Text.Encoding.ASCII.GetBytes(uri);
		}

		
	}
	
}
