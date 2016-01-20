using System;

namespace SapSoapCardWriter.BusinessLogic.NFC
{
	public class Rtd : Ndef
	{
		public Rtd(byte TNF, string Type) : base(TNF, Type)
		{
		}
		
		public Rtd(byte TNF, string Type, byte[] id) : base(TNF, Type, id, null)
		{
		}
		
		public Rtd(byte TNF, string Type, byte[] id, byte[] payload) : base(TNF, Type, id, payload)
		{
		}
		
		public Rtd(Ndef record) : base(record)
		{
		}
	}
}
