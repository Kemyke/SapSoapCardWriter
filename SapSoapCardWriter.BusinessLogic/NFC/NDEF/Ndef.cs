using SapSoapCardWriter.Logger.Logging;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;

namespace SapSoapCardWriter.BusinessLogic.NFC
{
	public class Ndef
	{
		public const byte NDEF_HEADER_MESSAGE_BEGIN     = 0x80;
		public const byte NDEF_HEADER_MESSAGE_END       = 0x40;
		public const byte NDEF_HEADER_CHUNK_FLAG        = 0x20;
		public const byte NDEF_HEADER_SHORT_RECORD      = 0x10;
		public const byte NDEF_HEADER_ID_LENGTH_PRESENT = 0x08;
		public const byte NDEF_HEADER_TNF_MASK          = 0x07;
		public const byte NDEF_HEADER_TNF_EMPTY         = 0x00;
		public const byte NDEF_HEADER_TNF_NFC_RTD_WKN   = 0x01;
		public const byte NDEF_HEADER_TNF_MEDIA_TYPE    = 0x02;
		public const byte NDEF_HEADER_TNF_ABSOLUTE_URI  = 0x03;
		public const byte NDEF_HEADER_TNF_NFC_RTD_EXT   = 0x04;
		public const byte NDEF_HEADER_TNF_UNKNOWN       = 0x05;
		public const byte NDEF_HEADER_TNF_UNCHANGED     = 0x06;
		public const byte NDEF_HEADER_TNF_RESERVED      = 0x07;
		
		protected byte[] payload = null;
		protected List<Ndef> children = new List<Ndef>();

		private byte header = 0;
		private byte[] type = null;
		private byte[] id = null;

        protected readonly ILogger logger;

        private Ndef(ILogger logger)
        {
            this.logger = logger;
        }

		public Ndef(Ndef ndef, ILogger logger)
            : this(logger)
		{
			TNF = ndef.TNF;
			TYPE = ndef.TYPE;
			PAYLOAD = ndef.PAYLOAD;
		}

        public Ndef(byte _TNF, string _TYPE, ILogger logger)
            : this(logger)
		{
			_TNF &= 0x07;
			header &= 0xF8;
			header |= _TNF;
			type = CardBuffer.BytesFromString(_TYPE);
		}

        public Ndef(byte _TNF, string _TYPE, byte[] _PAYLOAD, ILogger logger)
            : this(logger)
		{
			_TNF &= 0x07;
			header &= 0xF8;
			header |= _TNF;
			type = CardBuffer.BytesFromString(_TYPE);
			payload = _PAYLOAD;
		}

        public Ndef(byte _TNF, string _TYPE, byte[] ID, byte[] _PAYLOAD, ILogger logger)
            : this(logger)
		{
			_TNF &= 0x07;
			header &= 0xF8;
			header |= _TNF;
			type = CardBuffer.BytesFromString(_TYPE);
			id = ID;
			payload = _PAYLOAD;
		}
		
		public Ndef(byte _TNF, byte[] _TYPE, byte[] ID, byte[] _PAYLOAD, ILogger logger)
            : this(logger)
		{
			_TNF &= 0x07;
			header &= 0xF8;
			header |= _TNF;
			type = _TYPE;
			id = ID;
			payload = _PAYLOAD;
		}
		
		public void SetMessageBegin(bool mb)
		{
			if (mb)
				header |= NDEF_HEADER_MESSAGE_BEGIN;
			else
				header = (byte) (header & ~NDEF_HEADER_MESSAGE_BEGIN);
		}

		public void SetMessageEnd(bool me)
		{
			if (me)
				header |= NDEF_HEADER_MESSAGE_END;
			else
				header = (byte) (header & ~NDEF_HEADER_MESSAGE_END);
		}

		public byte TNF
		{
			get
			{
				return (byte) (header & 0x07);
			}
			set
			{
				value &= 0x07;
				header &= 0xF8;
				header |= value;
			}
		}

		public string TYPE
		{
			get
			{
				return CardBuffer.StringFromBytes(type);
			}
			set
			{
				type = CardBuffer.BytesFromString(value);
			}
		}
		
		public byte[] ID
		{
			get
			{
				return id;
			}
			set
			{
				id = value;
			}
		}

		public byte[] PAYLOAD
		{
			get
			{
				return payload;
			}
			set
			{
				payload = value;
			}
		}

		public int Size(ref bool is_short_record)
		{
			int l = 2; /* 1 byte for the header and 1 for type length			*/
			
			is_short_record = (payload.Length < 256) ? true : false;

			if (is_short_record)
			{
				l += 1;
			} else
			{
				l += 4;
			}

			if (id != null)
			{
				l += 1; 		/* ID_Length byte	*/
				l += id.Length;
			}
			
			l += type.Length;
			l += payload.Length;
			
			return l;
		}
		
		public byte[] GetBytes(bool isBegin, bool isEnd)
		{
			byte[] b = null;
			
			SetMessageBegin(isBegin);
			SetMessageEnd(isEnd);
			
			if (Encode(ref b))
				return b;
			
			return null;
		}

		public byte[] GetBytes()
		{
			return GetBytes(true, true);
		}

		public virtual bool Encode(ref byte[] buffer)
		{

			int offset;
			
			/* Serializes the children (if any), which will become the payload of the NDEF	*/
			if (children.Count != 0)
			{
				logger.Debug("Encoding children...");
				
				int payload_size = 0;
				bool child_is_short = false;
				
				for (int i=0; i<children.Count; i++)
					payload_size += children[i].Size(ref child_is_short);

				payload = new byte[payload_size];
				
				offset = 0;
				for (int i=0; i<children.Count; i++)
				{
					byte[] child_buffer = null;
					
					children[i].SetMessageBegin((i == 0) ? true : false);
					children[i].SetMessageEnd((i == children.Count - 1) ? true : false);
					
					if (!children[i].Encode(ref child_buffer))
						return false;
					
					for (int j=0; j<child_buffer.Length; j++)
						payload[offset++] = child_buffer[j];
				}
			}
			
			/* Serializes the NDEF	*/
            logger.Debug("Encoding NDEF");
			
			bool is_short_record = false;
			int record_size = Size(ref is_short_record);
			
			if (is_short_record)
				header |= NDEF_HEADER_SHORT_RECORD;
			else
				header = (byte) (header & ~NDEF_HEADER_SHORT_RECORD);
			
			if (id != null)
				header |= NDEF_HEADER_ID_LENGTH_PRESENT;
			else
				header = (byte) (header & ~NDEF_HEADER_ID_LENGTH_PRESENT);

			buffer = new byte[record_size];
			offset = 0;

            logger.Debug(String.Format("- Header : {0:X2}", header));
			buffer[offset++] = header;

            logger.Debug(String.Format("- Type length : {0}", type.Length));
			buffer[offset++] = (byte) type.Length;

            logger.Debug(String.Format("- Payload length : {0}", payload.Length));
			if (is_short_record)
			{
				buffer[offset++] = (byte) payload.Length;
			} else
			{
				int l = payload.Length;
				buffer[offset + 3] = (byte) (l % 0x00000100); l /= 0x00000100;
				buffer[offset + 2] = (byte) (l % 0x00000100); l /= 0x00000100;
				buffer[offset + 1] = (byte) (l % 0x00000100); l /= 0x00000100;
				buffer[offset + 0] = (byte) (l % 0x00000100);
				offset += 4;
			}
			
			if (id != null)
			{
                logger.Debug(String.Format("- ID length : {0}", id.Length));
				buffer[offset++] = (byte) id.Length;
			}

            logger.Debug("- Type : " + (new CardBuffer(type)).AsString(" "));
			for (int i=0; i<type.Length; i++)
				buffer[offset++] = type[i];
			
			if (id != null)
			{
                logger.Debug("- ID : " + (new CardBuffer(id)).AsString(" "));
				for (int i=0; i<id.Length; i++)
					buffer[offset++] = id[i];
			}

            logger.Debug("- Payload : " + (new CardBuffer(payload)).AsString(" "));
			for (int i=0; i<payload.Length; i++)
				buffer[offset++] = payload[i];


			return true;
		}

		public delegate void NdefFoundCallback(Ndef ndef);

		public static bool Parse(ILogger logger, byte[] buffer, NdefFoundCallback callback)
		{
			int offset = 0;
			Ndef ndef = null;
			bool terminated = true;
			
			while (Ndef.Parse(logger, buffer, ref offset, ref ndef, ref terminated))
			{
				if (callback != null)
					callback(ndef);
				
				if (terminated)
					return true;
			}

            logger.Debug("Parsing failed at offset " + offset);
			return false;
		}

		public static bool Parse(ILogger logger, byte[] buffer, ref int offset, ref Ndef ndef, ref bool terminated)
		{
			if (offset > buffer.Length)
				return false;
			
			terminated = false;
			ndef = null;
			
			/*  Header */
			if (offset+1 > buffer.Length)
			{
                logger.Debug("NDEF truncated after 'Header' byte");
				return false;
			}
			byte header = buffer[offset++];
			
			if (header == 0)
			{
                logger.Debug("Empty byte?");
				return false;
			}
			
			/* Type length		*/
			if (offset+1 > buffer.Length)
			{
                logger.Debug("NDEF truncated after 'Type Length' byte");
				return false;
			}
			int type_length = buffer[offset++];
			
			/* Payload length	*/
			int payload_length = 0;
			if ((header & NDEF_HEADER_SHORT_RECORD) != 0)
			{
				if (offset+1 > buffer.Length)
				{
                    logger.Debug("NDEF truncated after 'Payload Length' byte");
					return false;
				}
				payload_length = buffer[offset++];
			} else
			{
				if (offset+4 > buffer.Length)
				{
                    logger.Debug("NDEF truncated after 'Payload Length' dword");
					return false;
				}
				payload_length  = buffer[offset++]; payload_length *= 0x00000100;
				payload_length += buffer[offset++]; payload_length *= 0x00000100;
				payload_length += buffer[offset++]; payload_length *= 0x00000100;
				payload_length += buffer[offset++];
			}

			/* 	ID Length			*/
			int id_length = 0;
			if ((header & NDEF_HEADER_ID_LENGTH_PRESENT) != 0)
			{
				if (offset+1 > buffer.Length)
				{
                    logger.Debug("NDEF truncated after 'ID Length' byte");
					return false;
				}
				id_length = buffer[offset++];
			}
			
			/* Type */
			byte[] type = null;
			if (type_length > 0)
			{
				if (offset+type_length > buffer.Length)
				{
                    logger.Debug("NDEF truncated after 'Type' bytes");
					return false;
				}
				type = new byte[type_length];
				for (int i=0; i<type_length; i++)
					type[i] = buffer[offset++];
			}
			
			/* ID */
			byte[] id = null;
			if (id_length > 0)
			{
				if (offset+id_length > buffer.Length)
				{
                    logger.Debug("NDEF truncated after 'ID' bytes");
					return false;
				}
				id = new byte[id_length];
				for (int i=0; i<id_length; i++)
					id[i] = buffer[offset++];
			}
			
			/* Payload */
			byte[] payload = null;
			if (payload_length > 0)
			{
				if (offset+payload_length > buffer.Length)
				{
                    logger.Debug("NDEF truncated afterTra 'Payload' bytes");
					return false;
				}
				payload = new byte[payload_length];
				for (int i=0; i<payload_length; i++)
					payload[i] = buffer[offset++];
			}
			
			/* OK */
			string type_s = CardBuffer.StringFromBytes(type);
			
			switch (header & NDEF_HEADER_TNF_MASK)
			{
				case NDEF_HEADER_TNF_EMPTY :
					break;
					
				case NDEF_HEADER_TNF_NFC_RTD_WKN :
					if (type_s.Equals("Sp"))
					{
                        logger.Debug("Found a SmartPoster");
						ndef = new RtdSmartPoster(logger, payload);
					} else
						if (type_s.Equals("U"))
					{
                        logger.Debug("Found an URI");
						ndef = new RtdUri(logger, payload);
					} else
						if (type_s.Equals("T"))
					{
                        logger.Debug("Found a Text");
                        ndef = new RtdText(logger, payload);
					} else
						if (type_s.Equals("act"))
					{
                        logger.Debug("Found an Action");
                        ndef = new RtdSmartPosterAction(logger, payload);
						
					} else
						if (type_s.Equals("s"))
					{
                        logger.Debug("Found a Size");
                        ndef = new RtdSmartPosterTargetSize(logger, payload);
						
					} else
						if (type_s.Equals("t"))
					{
                        logger.Debug("Found a MIME-Type");
                        ndef = new RtdSmartPosterTargetType(logger, payload);
					} else
						if (type_s.Equals("Hs"))
					{
                        logger.Debug("Found a Handover Selector");
                        ndef = new RtdHandoverSelector(logger, payload, ref buffer, ref offset);
					} else
						if (type_s.Equals("ac"))
					{
                        logger.Debug("Found a Alternative Carrier");
                        ndef = new RtdAlternativeCarrier(logger, payload);
					} else
					{
                        logger.Debug("Found an unknown RTD : " + type_s);
					}
					break;
					
				case NDEF_HEADER_TNF_MEDIA_TYPE :
					if (type_s.ToLower().Equals("text/x-vcard"))
					{
                        logger.Debug("Found a vCard");
                        ndef = new RtdVCard(logger, payload);
					} else
					{
                        logger.Debug("Found a MIME Media : " + type_s);
                        ndef = new RtdMedia(logger, type_s, payload);
					}
					break;
					
				case NDEF_HEADER_TNF_ABSOLUTE_URI :
					if (type_s.Equals("U"))
					{
                        logger.Debug("Found an absolute URI");
                        ndef = new AbsoluteUri(logger, id, payload);
					}
					break;
					
				case NDEF_HEADER_TNF_NFC_RTD_EXT :
                    logger.Debug("Found TNF urn:nfc:ext");
					break;
				case NDEF_HEADER_TNF_UNKNOWN :
                    logger.Debug("Found TNF unknown");
					break;
				case NDEF_HEADER_TNF_UNCHANGED :
                    logger.Debug("Found TNF unchanged");
					break;
				case NDEF_HEADER_TNF_RESERVED :
                    logger.Debug("Found TNF reserved");
					break;
					
				default :
					return false; // Invalid
			}
			
			if (ndef == null)
			{
				ndef = new Ndef(header, type, id, payload, logger);
			}
			
			if (offset >= buffer.Length)
			{
                logger.Debug("Done!");
				terminated = true;
			}

			return true;
		}
		
		public static Ndef[] Parse(ILogger logger, byte[] buffer)
		{
			int offset = 0;
			List<Ndef> ndefs = new List<Ndef>();
			Ndef ndef = null;
			bool terminated = true;
			
			while (Ndef.Parse(logger, buffer, ref offset, ref ndef, ref terminated))
			{
				ndefs.Add(ndef);
				
				if (terminated)
					break;
			}
			
			if (ndefs.Count == 0)
				return null;
			
			return ndefs.ToArray();
		}
		
	}
	
}
