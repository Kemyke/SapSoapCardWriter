using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;

namespace SapSoapCardWriter.BusinessLogic.NFC
{
	public class RtdSmartPosterAction : Rtd
	{
		public RtdSmartPosterAction(int Action) : base(Ndef.NDEF_HEADER_TNF_NFC_RTD_WKN, "act")
		{
			_payload = new byte[1];
			_payload[0] = (byte) Action;
		}
		
		public RtdSmartPosterAction(byte[] Payload) : base(Ndef.NDEF_HEADER_TNF_NFC_RTD_WKN, "act")
		{
			if ((Payload == null) || (Payload.Length == 0))
			{
				_payload = new byte[1];
			} else
			{
				_payload = Payload;
			}
		}

		public int Value
		{
			get{ return _payload[0]; }
			set{ _payload[0] = (byte) Value;}
		}
	}

	public class RtdSmartPosterTargetIcon : Rtd
	{
		public RtdSmartPosterTargetIcon(string IconMimeType, byte[] ImageBlob) : base(Ndef.NDEF_HEADER_TNF_MEDIA_TYPE, IconMimeType)
		{
			_payload = ImageBlob;
		}
	}

    public class RtdSmartPosterTargetSize : Rtd
	{

		public RtdSmartPosterTargetSize(int Size) : base(NDEF_HEADER_TNF_NFC_RTD_WKN, "s")
		{
			_payload = new byte[4];
			_payload[3] = (byte) (Size); Size /= 0x00000100;
			_payload[2] = (byte) (Size); Size /= 0x00000100;
			_payload[1] = (byte) (Size); Size /= 0x00000100;
			_payload[0] = (byte) (Size);
		}
		
		public RtdSmartPosterTargetSize(byte[] Payload) : base(NDEF_HEADER_TNF_NFC_RTD_WKN, "s")
		{
			if ((Payload == null) || (Payload.Length == 0))
			{
				_payload = new byte[4];
			} else
			{
				_payload = Payload;
			}
		}
		
		public int Value
		{
			get
			{
				int size = 0;
				for (int i = 0 ; i< (_payload.Length -1) ; i++)
				{
					size += _payload[i];
					size <<= 8;
				}
				size += _payload[_payload.Length -1] ;
				return size;
			}
			set
			{
				_payload = new byte[4];
				_payload[3] = (byte) (Value); Value /= 0x00000100;
				_payload[2] = (byte) (Value); Value /= 0x00000100;
				_payload[1] = (byte) (Value); Value /= 0x00000100;
				_payload[0] = (byte) (Value);

			}
		}
		
	}

	public class RtdSmartPosterTargetType : Rtd
	{
		public RtdSmartPosterTargetType(string MimeType) : base(Ndef.NDEF_HEADER_TNF_NFC_RTD_WKN, "t")
		{
			_payload = CardBuffer.BytesFromString(MimeType);
		}
		
		public RtdSmartPosterTargetType(byte[] Payload) : base(Ndef.NDEF_HEADER_TNF_NFC_RTD_WKN, "t")
		{
			if ((Payload == null) || (Payload.Length == 0))
			{
				
			} else
			{
				_payload = Payload;
			}
		}
		
		public string Value
		{
			get
			{
				if (_payload != null)
				{
					return CardBuffer.StringFromBytes(_payload);
				} else
				{
					return "";
				}
			}
			set{ _payload = CardBuffer.BytesFromString(Value);}
		}
		
	}

	public class RtdSmartPoster : Rtd
	{
		public RtdUri Uri = null;
		public List<RtdText> Title = new List<RtdText>();
		public RtdSmartPosterAction Action = null;
		public RtdSmartPosterTargetIcon TargetIcon = null;
		public RtdSmartPosterTargetType TargetType = null;
		public RtdSmartPosterTargetSize TargetSize = null;
		
		public RtdSmartPoster() : base(Ndef.NDEF_HEADER_TNF_NFC_RTD_WKN, "Sp")
		{
			
		}

		public RtdSmartPoster(byte[] payload) : base(Ndef.NDEF_HEADER_TNF_NFC_RTD_WKN, "Sp")
		{
			int offset = 0;
			Ndef ndef = null;
			bool terminated = true;
			
			while (Ndef.Parse(payload, ref offset, ref ndef, ref terminated))
			{
				if (ndef is RtdUri)
				{
					Trace.WriteLine("Got a new URI");
					Uri = (RtdUri) ndef;
				} else
					if (ndef is RtdText)
				{
					Trace.WriteLine("Got a new Text");
					Title.Add((RtdText) ndef);
				} else
					if (ndef is RtdSmartPosterAction)
				{
					Trace.WriteLine("Got a new SmartPoster Action");
					Action = (RtdSmartPosterAction) ndef;
				} else
					if (ndef is RtdSmartPosterTargetIcon)
				{
					Trace.WriteLine("Got a new SmartPoster Icon");
					TargetIcon = (RtdSmartPosterTargetIcon) ndef;
				} else
					if (ndef is RtdSmartPosterTargetType)
				{
					Trace.WriteLine("Got a new SmartPoster MIME type");
					TargetType = (RtdSmartPosterTargetType) ndef;
				} else
					if (ndef is RtdSmartPosterTargetSize)
				{
					Trace.WriteLine("Got a new SmartPoster Size");
					TargetSize = (RtdSmartPosterTargetSize) ndef;
				} else
				{
					Trace.WriteLine("Got an unknown child in the SmartPoster");
				}
				
				if (terminated)
					break;
			}
		}

		public override bool Encode(ref byte[] buffer)
		{
			_children.Clear();
			
			if (Uri != null)
				_children.Add(Uri);
			for (int i=0; i<Title.Count; i++)
				_children.Add(Title[i]);
			if (Action != null)
				_children.Add(Action);
			if (TargetIcon != null)
				_children.Add(TargetIcon);
			if (TargetType != null)
				_children.Add(TargetType);
			if (TargetSize != null)
				_children.Add(TargetSize);
			
			return base.Encode(ref buffer);
		}
	}
}
