using SapSoapCardWriter.Logger.Logging;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;

namespace SapSoapCardWriter.BusinessLogic.NFC
{
	public class RtdSmartPosterAction : Rtd
	{
		public RtdSmartPosterAction(ILogger logger, int Action) 
            : base(Ndef.NDEF_HEADER_TNF_NFC_RTD_WKN, "act", logger)
		{
			payload = new byte[1];
			payload[0] = (byte) Action;
		}

        public RtdSmartPosterAction(ILogger logger, byte[] Payload)
            : base(Ndef.NDEF_HEADER_TNF_NFC_RTD_WKN, "act", logger)
		{
			if ((Payload == null) || (Payload.Length == 0))
			{
				payload = new byte[1];
			} else
			{
				payload = Payload;
			}
		}

		public int Value
		{
			get{ return payload[0]; }
			set{ payload[0] = (byte) Value;}
		}
	}

	public class RtdSmartPosterTargetIcon : Rtd
	{
        public RtdSmartPosterTargetIcon(ILogger logger, string IconMimeType, byte[] ImageBlob) 
            : base(Ndef.NDEF_HEADER_TNF_MEDIA_TYPE, IconMimeType, logger)
		{
			payload = ImageBlob;
		}
	}

    public class RtdSmartPosterTargetSize : Rtd
	{

        public RtdSmartPosterTargetSize(ILogger logger, int Size) 
            : base(NDEF_HEADER_TNF_NFC_RTD_WKN, "s", logger)
		{
			payload = new byte[4];
			payload[3] = (byte) (Size); Size /= 0x00000100;
			payload[2] = (byte) (Size); Size /= 0x00000100;
			payload[1] = (byte) (Size); Size /= 0x00000100;
			payload[0] = (byte) (Size);
		}

        public RtdSmartPosterTargetSize(ILogger logger, byte[] Payload)
            : base(NDEF_HEADER_TNF_NFC_RTD_WKN, "s", logger)
		{
			if ((Payload == null) || (Payload.Length == 0))
			{
				payload = new byte[4];
			} else
			{
				payload = Payload;
			}
		}
		
		public int Value
		{
			get
			{
				int size = 0;
				for (int i = 0 ; i< (payload.Length -1) ; i++)
				{
					size += payload[i];
					size <<= 8;
				}
				size += payload[payload.Length -1] ;
				return size;
			}
			set
			{
				payload = new byte[4];
				payload[3] = (byte) (Value); Value /= 0x00000100;
				payload[2] = (byte) (Value); Value /= 0x00000100;
				payload[1] = (byte) (Value); Value /= 0x00000100;
				payload[0] = (byte) (Value);

			}
		}
		
	}

	public class RtdSmartPosterTargetType : Rtd
	{
		public RtdSmartPosterTargetType(ILogger logger, string MimeType) 
            : base(Ndef.NDEF_HEADER_TNF_NFC_RTD_WKN, "t", logger)
		{
			payload = CardBuffer.BytesFromString(MimeType);
		}
		
		public RtdSmartPosterTargetType(ILogger logger, byte[] Payload) 
            : base(Ndef.NDEF_HEADER_TNF_NFC_RTD_WKN, "t", logger)
		{
			if ((Payload == null) || (Payload.Length == 0))
			{
				
			} else
			{
				payload = Payload;
			}
		}
		
		public string Value
		{
			get
			{
				if (payload != null)
				{
					return CardBuffer.StringFromBytes(payload);
				} else
				{
					return "";
				}
			}
			set{ payload = CardBuffer.BytesFromString(Value);}
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
		
		public RtdSmartPoster(ILogger logger) 
            : base(Ndef.NDEF_HEADER_TNF_NFC_RTD_WKN, "Sp", logger)
		{
			
		}

		public RtdSmartPoster(ILogger logger, byte[] payload) 
            : base(Ndef.NDEF_HEADER_TNF_NFC_RTD_WKN, "Sp", logger)
		{
			int offset = 0;
			Ndef ndef = null;
			bool terminated = true;
			
			while (Ndef.Parse(logger, payload, ref offset, ref ndef, ref terminated))
			{
				if (ndef is RtdUri)
				{
					logger.Debug("Got a new URI");
					Uri = (RtdUri) ndef;
				} else
					if (ndef is RtdText)
				{
                    logger.Debug("Got a new Text");
					Title.Add((RtdText) ndef);
				} else
					if (ndef is RtdSmartPosterAction)
				{
                    logger.Debug("Got a new SmartPoster Action");
					Action = (RtdSmartPosterAction) ndef;
				} else
					if (ndef is RtdSmartPosterTargetIcon)
				{
                    logger.Debug("Got a new SmartPoster Icon");
					TargetIcon = (RtdSmartPosterTargetIcon) ndef;
				} else
					if (ndef is RtdSmartPosterTargetType)
				{
                    logger.Debug("Got a new SmartPoster MIME type");
					TargetType = (RtdSmartPosterTargetType) ndef;
				} else
					if (ndef is RtdSmartPosterTargetSize)
				{
                    logger.Debug("Got a new SmartPoster Size");
					TargetSize = (RtdSmartPosterTargetSize) ndef;
				} else
				{
                    logger.Debug("Got an unknown child in the SmartPoster");
				}
				
				if (terminated)
					break;
			}
		}

		public override bool Encode(ref byte[] buffer)
		{
			children.Clear();
			
			if (Uri != null)
				children.Add(Uri);
			for (int i=0; i<Title.Count; i++)
				children.Add(Title[i]);
			if (Action != null)
				children.Add(Action);
			if (TargetIcon != null)
				children.Add(TargetIcon);
			if (TargetType != null)
				children.Add(TargetType);
			if (TargetSize != null)
				children.Add(TargetSize);
			
			return base.Encode(ref buffer);
		}
	}
}
