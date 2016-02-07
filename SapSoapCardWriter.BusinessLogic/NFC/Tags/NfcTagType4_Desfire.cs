using SapSoapCardWriter.Logger.Logging;
using System;
using System.Diagnostics;

namespace SapSoapCardWriter.BusinessLogic.NFC
{
	public class NfcTagType4Desfire : NfcTagType4
	{
		public static new bool Recognize(ILogger logger, SmartCardChannel channel)
		{
			bool write_protected = false;
			ushort max_le = 0;
			ushort max_lc = 0;
			ushort ndef_file_id = 0;
			long ndef_file_size = 0;
			
			if (!IsDesfireEV1(logger, channel))
			{
				logger.Error("The card is not a Desfire EV1");
				return false;
			}
			
			if (!NfcTagType4.Recognize(logger, channel, ref write_protected, ref max_le, ref max_lc, ref ndef_file_id, ref ndef_file_size))
			{
				/* Failed to recognize a Type 4 card, but anyway a Desfire EV1 may be formatted later on */
                logger.Debug("The card is a Desfire EV1, it may become a type 4 Tag");
			} 
            else
			{
                logger.Debug("The card is a Desfire EV1, already formatted as type 4 Tag");
			}
			return true;
		}

		public static bool IsDesfireEV1(ILogger logger, SmartCardChannel channel)
		{
			bool is_desfire_ev1 = false;

            Capdu capdu = new Capdu(0x90, 0x60, 0x00, 0x00, 0x00);
			
			logger.Debug("< " + capdu.AsString(" "));

			Rapdu rapdu = channel.Transmit(capdu);
            logger.Debug("> " + rapdu.AsString(" "));
			
			if (rapdu.SW != 0x91AF)
			{
                logger.Error("Desfire GetVersion function failed");
				return false;
			}
			
			if (rapdu.GetByte(3) > 0)
			{
                logger.Debug("This is a Desfire EV1");
				is_desfire_ev1 = true;
			} 
            else
			{
                logger.Debug("This is a Desfire EV0");
			}

            capdu = new Capdu(0x90, 0xAF, 0x00, 0x00, 0x00);

            logger.Debug("< " + capdu.AsString(" "));

			rapdu = channel.Transmit(capdu);
            logger.Debug("> " + rapdu.AsString(" "));
			
			if (rapdu.SW != 0x91AF)
			{
                logger.Error("Desfire GetVersion(2) function failed");
				return false;
			}

            capdu = new Capdu(0x90, 0xAF, 0x00, 0x00, 0x00);

            logger.Debug("< " + capdu.AsString(" "));

			rapdu = channel.Transmit(capdu);
            logger.Debug("> " + rapdu.AsString(" "));
			
			if (rapdu.SW != 0x9100)
			{
                logger.Error("Desfire GetVersion(3) function failed");
				return false;
			}

			return is_desfire_ev1;
		}
		
		public NfcTagType4Desfire(ILogger logger, SmartCardChannel Channel) : base(logger, Channel)
		{
			formattable = true;
		}

		public static NfcTagType4Desfire Read(ILogger logger, SmartCardChannel channel)
		{
			NfcTagType4Desfire t = new NfcTagType4Desfire(logger, channel);
			
			t.Read();
			
			return t;
		}
	}
}