using SapSoapCardWriter.Logger.Logging;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;

namespace SapSoapCardWriter.BusinessLogic.NFC
{
	public class NfcTagType2 : NfcTag
	{
		private const string ATR_MIFARE_UL   = "3B8F8001804F0CA0000003060300030000000068";
		private const string ATR_MIFARE_UL_C = "3B8F8001804F0CA00000030603003A0000000051";
		
		private const byte OFFSET_USER_DATA = 16;
		private const byte READ_4_PAGES = 16;
		
		private List<NfcTlv> _tlvs = new List<NfcTlv>();
		
		private byte[] _raw_data = null;


		public NfcTagType2(ILogger logger, SmartCardChannel Channel) 
            : base(logger, Channel)
		{
			/* A type 2 Tag can always be locked */
			lockable = true;
		}
		
		protected override bool WriteContent(byte[] ndef_content)
		{
            logger.Debug("Writing the NFC Forum type 2 Tag");
			
			if (ndef_content == null)
				return false;
			
			/* Get the new NDEF TLV to store into the Tag */
			NfcTlv ndef_tlv = new NfcTlv(NDEF_MESSAGE_TLV, ndef_content);
			
			/* Remove the Terminator TLV (if some) */
			while ((_tlvs.Count > 0) && (_tlvs[_tlvs.Count-1].T == TERMINATOR_TLV))
				_tlvs.RemoveAt(_tlvs.Count-1);
			
			/* Where shall I put the NDEF TLV in the Tag ? */
			if (_tlvs.Count == 0)
			{
				_tlvs.Add(ndef_tlv);
			} else
			{
				for (int i=0; i<_tlvs.Count; i++)
				{
					if (_tlvs[i].T == NDEF_MESSAGE_TLV)
					{
						/* Replace this one */
						_tlvs[i] = ndef_tlv;
						ndef_tlv = null;
						break;
					}
				}
				
				if (ndef_tlv != null)
				{
					/* No NDEF in the Tag beforehand? Let's add it the the end */
					_tlvs.Add(ndef_tlv);
				}
			}
			
			CardBuffer actual_content = new CardBuffer();
			
			for (int i=0; i<_tlvs.Count; i++)
			{
				actual_content.Append(_tlvs[i].Serialize());
			}
			
			if (actual_content.Length > Capacity())
			{
                logger.Error("The size of the content (with its TLVs) is bigger than the tag's capacity");
				return false;
			}
			
			if ((actual_content.Length + 2) < Capacity())
			{
				/* Add a Terminator at the end */
                logger.Debug("We add a TERMINATOR TLV at the end of the Tag");
				actual_content.Append((new NfcTlv(TERMINATOR_TLV, null)).Serialize());
			}
			
			/* And now write */
			ushort page = 4;
			for (long i=0; i<actual_content.Length; i+=4)
			{
				byte[] buffer = new byte[4];
				
				for (long j=0; j<4; j++)
				{
					if ((i+j) < actual_content.Length)
						buffer[j] = actual_content.GetByte(i+j);
				}
				
				if (!WriteBinary(logger, channel, page, buffer))
					return false;
				page++;
			}

			return true;
		}

		public override bool Format()
		{
            logger.Debug("Formatting the NFC Forum type 2 Tag");
			
			byte[] cc_block = new byte[4];
			byte[] user_data = new byte[4];
			
			long capacity;

			capacity  = Capacity();
			capacity /= 8;
			if (capacity > 255)
				capacity = 255;
			
			cc_block[0] = NFC_FORUM_MAGIC_NUMBER;
			cc_block[1] = NFC_FORUM_VERSION_NUMBER;
			cc_block[2] = (byte) (capacity);
			cc_block[3] = 0x00;

			if (!WriteBinary(logger, channel, 3, cc_block))
			{
                logger.Error("Can't write the CC bytes");
				return false;
			}
			
			
			user_data[0] = 0x00; // Erase first bytes
			user_data[1] = 0x00; // in order to avoid finding false
			user_data[2] = 0x00; // TLVs
			user_data[3] = 0x00;

			if (!WriteBinary(logger, channel, 4, user_data))
			{
                logger.Error("Can't write the 1st page of user data");
				return false;
			}
			
			
			/* The Tag is now formatted */
			formatted = true;
			/* So it's not formattable anymore */
			formattable = false;
			/* We consider it is empty */
			is_empty = true;

			return true;
		}
		
		public override bool Lock()
		{
            logger.Debug("Locking the NFC Forum type 2 Tag");
			
			byte[] cc_block = ReadBinary(logger, channel, 3, 4);
			if ((cc_block == null) || (cc_block.Length != 4))
				return false;
			
			/* No write access at all */
			cc_block[3] = 0x0F;

            if (!WriteBinary(logger, channel, 3, cc_block))
				return false;
			
			/* Write the LOCKs */
			byte[] lock_block = new byte[4];
			lock_block[0] = 0xFF;
			lock_block[1] = 0xFF;
			lock_block[2] = 0xFF;
			lock_block[3] = 0xFF;

            if (!WriteBinary(logger, channel, 2, lock_block))
				return false;
			
			/* OK! */
			locked = true;
			lockable = false;

			return true;
		}
		
		protected static byte[] ReadBinary(ILogger logger, SmartCardChannel channel, ushort address, byte length)
		{
			Capdu capdu = new Capdu(0xFF, 0xB0, (byte) (address / 0x0100), (byte) (address % 0x0100), length);

            logger.Debug("< " + capdu.AsString(" "));
			
			Rapdu rapdu = null;
			
			for (int retry = 0; retry < 4; retry++)
			{
				rapdu = channel.Transmit(capdu);
				
				if (rapdu == null)
					break;
				if ((rapdu.SW != 0x6F01) && (rapdu.SW != 0x6F02) && (rapdu.SW != 0x6F0B))
					break;
				
				Thread.Sleep(10);
			}
			
			if (rapdu == null)
			{
                logger.Error("Error '" + channel.LastErrorAsString + "' while reading the card");
				return null;
			}

            logger.Debug("> " + rapdu.AsString(" "));

			if (rapdu.SW != 0x9000)
			{
                logger.Error("Bad status word " + rapdu.SWString + " while reading the card");
				return null;
			}
			
			if (!rapdu.hasData)
			{
                logger.Error("Empty response");
				return null;
			}
			
			return rapdu.data.GetBytes();
		}

        protected static bool WriteBinary(ILogger logger, SmartCardChannel channel, ushort address, byte[] data)
		{
			if (data == null)
				return false;
			
			if (data.Length != 4)
			{
				logger.Error("Type 2 Tag: Write Binary accepts only 4B");
				return false;
			}
			
			Capdu capdu = new Capdu(0xFF, 0xD6, (byte) (address / 0x0100), (byte) (address % 0x0100), data);

            logger.Debug("< " + capdu.AsString(" "));
			
			
			Rapdu rapdu = null;
			
			for (int retry = 0; retry < 4; retry++)
			{
				rapdu = channel.Transmit(capdu);
				
				if (rapdu == null)
					break;
				if ((rapdu.SW != 0x6F01) && (rapdu.SW != 0x6F02) && (rapdu.SW != 0x6F0B))
					break;
				
				Thread.Sleep(15);
			}

			if (rapdu == null)
			{
                logger.Error("Error '" + channel.LastErrorAsString + "' while writing the card");
				return false;
			}

            logger.Debug("> " + rapdu.AsString(" "));
			
			if (rapdu.SW != 0x9000)
			{
                logger.Error("Bad status word " + rapdu.SWString + " while writing the card");
				return false;
			}
			
			
			return true;
		}
		
		protected override bool Read()
		{
            logger.Debug("Reading the NFC Forum type 2 Tag");
			
			ushort page = 0;
			
			if (!Recognize(logger, channel, ref formatted, ref formattable, ref locked))
			{
				return false;
			}
			
			CardBuffer buffer = new CardBuffer();
			
			for (page = 0; page < 256; page+=4)
			{
				byte[] data = ReadBinary(logger, channel, page, READ_4_PAGES);
				
				if (data == null)
					break;
				
				if (page > 0)
				{
					bool same_as_header = true;
					for (int i=0; i<OFFSET_USER_DATA; i++)
					{
						if (data[i] != buffer.GetByte(i))
						{
							same_as_header = false;
							break;
						}
					}
					if (same_as_header)
						break;
				}
				
				buffer.Append(data);
			}

            logger.Debug("Read " + buffer.Length + "B of data from the Tag");
			
			_raw_data = buffer.GetBytes();
			
			capacity = _raw_data.Length;
			if (capacity <= OFFSET_USER_DATA)
			{
				capacity = 0;
				return false;
			}
			
			if (!formatted)
			{
				/* Guess the capacity from the read area */
				if ((capacity > 64) && !formatted)
				{
					/* Drop the 16 last bytes if they are not empty (locks on Mifare UltraLight C) */
					bool locks_found = false;
					for (long i=capacity-16; i<capacity; i++)
					{
						if (_raw_data[i] != 0)
						{
							locks_found = true;
							break;
						}
					}
					if (locks_found)
					{
                        logger.Debug("Locks found at the end");
						capacity -= 16;
					}
				}
				capacity -= OFFSET_USER_DATA;
                logger.Debug("The Tag is not formatted, capacity=" + capacity + "B");
				
			} else
			{
				/* Read the capacity in the CC */
				capacity = 8 * _raw_data[14];
                logger.Debug("The Tag is formatted, capacity read from the CC=" + capacity + "B");
			}

			/* Is the tag empty ? */
			is_empty = true;
			for (long i=0; i<capacity; i++)
			{
				if (_raw_data[OFFSET_USER_DATA+i] != 0)
				{
					is_empty = false;
					break;
				}
			}
			
			if (is_empty)
			{
                logger.Debug("The Tag is empty");
				return true;
			}

			byte[] ndef_data = null;
			
			if (!ParseUserData(buffer.GetBytes(OFFSET_USER_DATA, -1), ref ndef_data))
			{
                logger.Error("The parsing of the Tag failed");
				return false;
			}
			
			if (ndef_data == null)
			{
                logger.Error("The Tag doesn't contain a NDEF");
				is_empty = true;
				return true;
			}
			
			is_empty = false;
			
			Ndef[] t = Ndef.Parse(logger, ndef_data);
			if (t == null)
			{
                logger.Error("The NDEF is invalid or unsupported");
				return false;
			}

            logger.Debug(t.Length + " NDEF record(s) found in the Tag");
			
			/* This NDEF is the new content of the tag */
			Content.Clear();
			for (int i=0; i<t.Length; i++)
				Content.Add(t[i]);
			
			return true;
		}
		
		private bool ParseUserData(byte[] user_data, ref byte[] ndef_data)
		{
			byte[] buffer = user_data;
			byte[] remaining_buffer = null;
			
			_tlvs.Clear();
			
			while (buffer != null)
			{
				NfcTlv tlv = NfcTlv.Unserialize(buffer, ref remaining_buffer);
				buffer = remaining_buffer;
				
				if (tlv == null)
				{
                    logger.Error("An invalid content has been found (not a T,L,V)");
					break;
				}
				
				switch (tlv.T)
				{
					case NDEF_MESSAGE_TLV :
                        logger.Debug("Found a NDEF TLV");
                        if (ndef_data != null)
                        {
                            logger.Debug("The Tag has already a NDEF, ignoring this one");
                        }
                        else
                        {
                            ndef_data = tlv.V;
                        }
						break;
					case LOCK_CONTROL_TLV :
                        logger.Debug("Found a LOCK CONTROL TLV");
						break;
					case MEMORY_CONTROL_TLV :
                        logger.Debug("Found a MEMORY CONTROL TLV");
						break;
					case PROPRIETARY_TLV :
                        logger.Debug("Found a PROPRIETARY TLV");
						break;
					case TERMINATOR_TLV :
                        logger.Debug("Found a TERMINATOR TLV");
						/* After a terminator... we terminate */
						buffer = null;
						break;
					case NULL_TLV :
						/* Terminate here */
						buffer = null;
						break;
					default:
                        logger.Error("Found an unsupported TLV (T=" + tlv.T + ")");
						return false;
				}
				
				if (tlv.T != NULL_TLV)
					_tlvs.Add(tlv);
				
			}

			return true;
		}


		public static bool RecognizeAtr(ILogger logger, CardBuffer atr)
		{
			string s = atr.AsString("");
			if (s.Equals(ATR_MIFARE_UL))
			{
                logger.Debug("ATR: Mifare UltraLight");
				return true;
			}
			if (s.Equals(ATR_MIFARE_UL_C))
			{
                logger.Debug("ATR: Mifare UltraLight C");
				return true;
			}
			
			return false;
		}
		
		public static bool RecognizeAtr(ILogger logger, SmartCardChannel channel)
		{
			CardBuffer atr = channel.CardAtr;
			
			return RecognizeAtr(logger, atr);
		}

		public static NfcTagType2 Create(ILogger logger, SmartCardChannel channel)
		{
			NfcTagType2 t = new NfcTagType2(logger, channel);
			
			if (!t.Read()) return null;
			
			return t;
		}

        public static bool Recognize(ILogger logger, SmartCardChannel channel)
		{
			bool formatted = false;
			bool formattable = false;
			bool write_protected = false;
			return Recognize(logger, channel, ref formatted, ref formattable, ref write_protected);
		}

		public static bool Recognize(ILogger logger, SmartCardChannel channel, ref bool formatted, ref bool formattable, ref bool write_protected)
		{
			byte[] header = ReadBinary(logger, channel, 0, READ_4_PAGES);
			
			if (header == null)
			{
				logger.Debug("Failed to read pages 0-3");
				return false;
			}
			
			if ((header[12] == 0) && (header[13] == 0) && (header[14] == 0) && (header[15] == 0))
			{
				/* The OTP bits are blank, assume the card is an unformatted type 2 Tag */
                logger.Debug("OTP are blank");
				formatted = false;
				formattable = true;
				write_protected = false;
				return true;
			}
			
			if (header[12] != NFC_FORUM_MAGIC_NUMBER)
			{
				/* The OTP bits contain something else */
                logger.Debug("OTP are not blank");
				formatted = false;
				formattable = false;
				write_protected = true;
				return false;
			}
			
			/* The OTP bits contain the NFC NDEF MAGIC NUMBER, so this is a formatted type 2 Tag */
            logger.Debug("OTP = NFC Forum magic number");
			formatted = true;
			formattable = false;
			write_protected = true;
			if ((header[13] & 0xF0) != (NFC_FORUM_VERSION_NUMBER & 0xF0))
			{
                logger.Debug("Version mismatch in OTP");
				return false;
			}
            if ((header[15] & 0xF0) == 0)
            {
                logger.Debug("Free read access");
            }
            else
            {
                logger.Error("No read access");
                return false;
            }
            if ((header[15] & 0x0F) == 0)
            {
                logger.Debug("Free write access");
                write_protected = false;
            }
            else
            {
                logger.Debug("No write access");
            }
			return true;
		}

	}
}
