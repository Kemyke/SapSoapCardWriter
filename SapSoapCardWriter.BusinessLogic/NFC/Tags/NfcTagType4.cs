using SapSoapCardWriter.Logger.Logging;
using System;
using System.Diagnostics;

namespace SapSoapCardWriter.BusinessLogic.NFC
{
	public class NfcTagType4 : NfcTag
	{
		private const string NDEF_APPLICATION_ID = "D2760000850101";
		private const ushort NDEF_CC_FILE_ID = 0xE103;
		
		private bool application_selected = false;
		private ushort ndef_file_id = 0;
		private ushort file_selected = 0;
		private ushort max_le = 0;
		private ushort max_lc = 0;

		public override bool Format()
		{
			return false;
		}
		
		public override bool Lock()
		{
            SelectRootApplication(logger, channel);

            if (!SelectNfcApplication(logger, channel))
				return false;
			if (!SelectFile(NDEF_CC_FILE_ID))
				return false;

			byte[] cc_write_control = ReadBinary(14, 1);
			if (cc_write_control == null)
				return false;
			
			cc_write_control[0] = 0xFF;
			return WriteBinary(14, cc_write_control);
		}
		
		protected bool SelectNfcApplication()
		{
			if (!application_selected)
			{
                if (!SelectNfcApplication(logger, channel))
				{
                    SelectRootApplication(logger, channel);
                    if (!SelectNfcApplication(logger, channel))
						return false;
				}
				application_selected = true;
				file_selected = 0;
			}
			return true;
		}

		protected bool SelectFile(ushort file_id)
		{
			if (file_selected != file_id)
			{
                if (!SelectFile(logger, channel, file_id))
					return false;
				file_selected = file_id;
			}
			return true;
		}
		
		protected byte[] ReadBinary(ushort offset, ushort length)
		{
            byte[] r = ReadBinary(logger, channel, offset, length);
			if (r == null)
			{
				application_selected = false;
				file_selected = 0;
			}
			return r;
		}

		protected bool WriteBinary(ushort offset, byte[] buffer)
		{
			bool r = WriteBinary(logger, channel, offset, buffer);
			if (!r)
			{
				application_selected = false;
				file_selected = 0;
			}
			return r;
		}

		protected override bool WriteContent(byte[] content)
		{
			long offset_in_content = 0;
			ushort offset_in_file = 2;
			byte[] buffer;
			
			/* Write the content */
			while (offset_in_content < content.Length)
			{
				if ((content.Length - offset_in_content) > max_lc)
				{
					buffer = new byte[max_lc];
				} else
					if ((content.Length - offset_in_content) > 254)
				{
					buffer = new byte[254];
				} else
				{
					buffer = new byte[content.Length - offset_in_content];
				}
				
				for (int i=0; i<buffer.Length; i++)
					buffer[i] = content[offset_in_content++];
				
				if (!WriteBinary(offset_in_file, buffer))
				{
					logger.Error("Failed to write the NDEF file at offset " + offset_in_file);
					return false;
				}
				
				offset_in_file += (ushort) buffer.Length;
			}
			
			/* Write the length as header */
			buffer = new byte[2];
			
			buffer[0] = (byte) (content.Length / 0x0100);
			buffer[1] = (byte) (content.Length % 0x0100);

			if (!WriteBinary(0, buffer))
			{
                logger.Error("Failed to write the header in the NDEF file");
				return false;
			}

			return true;
		}

		public NfcTagType4(ILogger logger, SmartCardChannel Channel)
            : base(logger, Channel)
        {

        }
		
		protected override bool Read()
		{
			long ndef_file_size = 0;
			byte[] buffer;
			
			if (!Recognize(logger, channel, ref locked, ref max_le, ref max_lc, ref ndef_file_id, ref ndef_file_size))
				return false;
			
			if (ndef_file_size > 2)
				capacity = ndef_file_size - 2;
			else
				capacity = 0;
			
			formatted = true;
			
			application_selected = true;
			file_selected = NDEF_CC_FILE_ID;
			
			if (!SelectFile(ndef_file_id))
			{
                logger.Error("Failed to select the NDEF file");
				return false;
			}
			
			buffer = ReadBinary(0, 2);
			if (buffer == null)
			{
                logger.Error("Failed to read from the NDEF file");
				return false;
			}
			
			if ((buffer[0] == 0) && (buffer[1] == 0))
			{
                logger.Debug("Tag is empty");
				is_empty = true;
				return true;
			}
			
			is_empty = false;
			
			long ndef_announced_size = (long) (buffer[0] * 0x0100 + buffer[1]);
			
			if ((ndef_announced_size > (ndef_file_size - 2)) || (ndef_announced_size > 0xFFFF))
			{
                logger.Error("The NDEF file contains an invalid length");
				return false;
			}
			
			byte[] content = new byte[ndef_announced_size];
			
			ushort offset_in_file = 2;
			long offset_in_content = 0;
			
			while (offset_in_content < content.Length)
			{
				if (max_le > 254)
					buffer = ReadBinary(offset_in_file, 254);
				else
					buffer = ReadBinary(offset_in_file, max_le);
				
				if ((buffer == null) || (buffer.Length == 0))
				{
					buffer = ReadBinary(offset_in_file, 0);
					if ((buffer == null) || (buffer.Length == 0))
					{
                        logger.Error("Failed to read the NDEF file at offset " + offset_in_file);
						return false;
					}
				}
				
				
				
				for (int i=0; i<buffer.Length; i++)
				{
					if (offset_in_content >= content.Length)
						break;
					content[offset_in_content++] = buffer[i];
				}
				
				offset_in_file += (ushort) buffer.Length;

				if ((offset_in_content >= content.Length) || (offset_in_file >= ndef_file_size))
					break;
			}
			
			Ndef[] ndefs = Ndef.Parse(logger, content);
			
			if (ndefs == null)
			{
                logger.Error("The NDEF is invalid or unsupported");
				return true;
			}

            logger.Error(ndefs.Length + " NDEF record(s) found in the tag");
			
			/* This NDEF is the new content of the tag */
			Content.Clear();
			for (int i=0; i<ndefs.Length; i++)
				Content.Add(ndefs[i]);
			
			return true;
		}
		
		private static bool SelectFile(ILogger logger, SmartCardChannel channel, ushort file_id)
		{
            Capdu capdu = new Capdu(0x00, 0xA4, 0x00, 0x0C, (new CardBuffer(file_id)).GetBytes());

            logger.Debug("< " + capdu.AsString(" "));
			
			Rapdu rapdu = channel.Transmit(capdu);
			
			if (rapdu == null)
			{
                logger.Error("SelectFile " + String.Format("{0:X4}", file_id) + " error " + channel.LastError + " (" + channel.LastErrorAsString + ")");
				return false;
			}

            logger.Debug("> " + rapdu.AsString(" "));
			
			if (rapdu.SW != 0x9000)
			{
                logger.Error("SelectFile " + String.Format("{0:X4}", file_id) + " failed " + rapdu.SWString + " (" + SmartCard.CardStatusWordsToString(rapdu.SW) + ")");
				return false;
			}
			
			return true;
		}
		
		private static bool SelectRootApplication(ILogger logger, SmartCardChannel channel)
		{
            Capdu capdu = new Capdu(0x00, 0xA4, 0x00, 0x00, "3F00");

            logger.Debug("< " + capdu.AsString(" "));
			
			Rapdu rapdu = channel.Transmit(capdu);
			
			if (rapdu == null)
			{
                logger.Error("SelectRootApplication error " + channel.LastError + " (" + channel.LastErrorAsString + ")");
				return false;
			}

            logger.Debug("> " + rapdu.AsString(" "));

			if (rapdu.SW != 0x9000)
			{
                logger.Error("SelectRootApplication failed " + rapdu.SWString + " (" + SmartCard.CardStatusWordsToString(rapdu.SW) + ")");
				return false;
			}

			return true;
		}
		
		private static bool SelectNfcApplication(ILogger logger, SmartCardChannel channel)
		{
            Capdu capdu = new Capdu(0x00, 0xA4, 0x04, 0x00, (new CardBuffer(NDEF_APPLICATION_ID)).GetBytes(), 0x00);

            logger.Debug("< " + capdu.AsString(" "));

            Rapdu rapdu = channel.Transmit(capdu);
			
			if (rapdu == null)
			{
                logger.Error("SelectNfcApplication error " + channel.LastError + " (" + channel.LastErrorAsString + ")");
				return false;
			}

            logger.Debug("> " + rapdu.AsString(" "));

			if (rapdu.SW != 0x9000)
			{
                logger.Error("SelectNfcApplication failed " + rapdu.SWString + " (" + SmartCard.CardStatusWordsToString(rapdu.SW) + ")");
				return false;
			}

			return true;
		}

		public static NfcTagType4 Create(ILogger logger, SmartCardChannel channel)
		{
            NfcTagType4 t = new NfcTagType4(logger, channel);
			
			if (!t.Read()) return null;
			
			return t;
		}

		public static bool Recognize(ILogger logger, SmartCardChannel channel)
		{
			bool write_protected = false;
			ushort max_le = 0;
			ushort max_lc = 0;
			ushort ndef_file_id = 0;
			long ndef_file_size = 0;
			return Recognize(logger, channel, ref write_protected, ref max_le, ref max_lc, ref ndef_file_id, ref ndef_file_size);
		}

        public static bool Recognize(ILogger logger, SmartCardChannel channel, ref bool write_protected, ref ushort max_le, ref ushort max_lc, ref ushort ndef_file_id, ref long ndef_file_size)
		{
			if (!SelectNfcApplication(logger, channel))
			{
				SelectRootApplication(logger, channel);
				if (!SelectNfcApplication(logger, channel))
					return false;
			}
			if (!SelectFile(logger, channel, NDEF_CC_FILE_ID))
				return false;
			
			byte[] cc_file_content = ReadBinary(logger, channel, 0, 15);
			
			if ((cc_file_content == null) || (cc_file_content.Length < 15))
			{
                logger.Error("Failed to read the CC file");
				return false;
			}
			
			long l = cc_file_content[0] * 0x0100 + cc_file_content[1];
			if (l < 15)
			{
                logger.Error("Bad length in the CC file");
				return false;
			}
			
			if ((cc_file_content[2] & 0xF0) != 0x20)
			{
                logger.Error("Bad version in the CC file");
				return false;
			}
			
			max_le = (ushort) (cc_file_content[3] * 0x0100 + cc_file_content[4]);
			max_lc = (ushort) (cc_file_content[5] * 0x0100 + cc_file_content[6]);
			
			if (cc_file_content[7] != NDEF_FILE_CONTROL_TLV)
			{
                logger.Error("Bad TLV's Tag in the CC file");
				return false;
			}
			
			if (cc_file_content[8] < 6)
			{
                logger.Error("Bad TLV's Length in the CC file");
				return false;
			}
			
			ndef_file_id = (ushort) (cc_file_content[9] * 0x0100 + cc_file_content[10]);
			ndef_file_size = (long) (cc_file_content[11] * 0x0100 + cc_file_content[12]);
			
			if (cc_file_content[13] != 0x00)
			{
                logger.Error("No read access");
				return false;
			}
			
			if (cc_file_content[14] != 0x00)
			{
                logger.Error("No write access");
				write_protected = true;
			} else
				write_protected = false;
			
			return true;
		}
		
		protected static byte[] ReadBinary(ILogger logger, SmartCardChannel channel, ushort offset, ushort length)
		{
            Capdu capdu = new Capdu(0x00, 0xB0, (byte)(offset / 0x0100), (byte)(offset % 0x0100), (byte)length);

            logger.Debug("< " + capdu.AsString(" "));

			Rapdu rapdu = channel.Transmit(capdu);

			if (rapdu == null)
			{
                logger.Error("ReadBinary " + String.Format("{0:X4}", offset) + "," + String.Format("{0:X2}", (byte)length) + " error " + channel.LastError + " (" + channel.LastErrorAsString + ")");
				return null;
			}

            logger.Debug("> " + rapdu.AsString(" "));
			
			if (rapdu.SW != 0x9000)
			{
                logger.Error("ReadBinary " + String.Format("{0:X4}", offset) + "," + String.Format("{0:X2}", (byte)length) + " failed " + rapdu.SWString + " (" + SmartCard.CardStatusWordsToString(rapdu.SW) + ")");
				return null;
			}

            if (rapdu.hasData)
            {
                return rapdu.data.GetBytes();
            }

			return null;
		}

		protected static bool WriteBinary(ILogger logger, SmartCardChannel channel, ushort offset, byte[] buffer)
		{
            Capdu capdu = new Capdu(0x00, 0xD6, (byte)(offset / 0x0100), (byte)(offset % 0x0100), buffer);

            logger.Debug("< " + capdu.AsString(" "));

            Rapdu rapdu = channel.Transmit(capdu);
			
			if (rapdu == null)
			{
                logger.Error("WriteBinary " + String.Format("{0:X4}", offset) + "," + String.Format("{0:X2}", (byte)buffer.Length) + " error " + channel.LastError + " (" + channel.LastErrorAsString + ")");
				return false;
			}

            logger.Debug("> " + rapdu.AsString(" "));

			if (rapdu.SW != 0x9000)
			{
                logger.Error("WriteBinary " + String.Format("{0:X4}", offset) + "," + String.Format("{0:X2}", (byte)buffer.Length) + " failed " + rapdu.SWString + " (" + SmartCard.CardStatusWordsToString(rapdu.SW) + ")");
				return false;
			}

			return true;
		}

	}
}
