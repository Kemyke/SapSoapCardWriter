using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using System.Diagnostics;
using SapSoapCardWriter.Logger.Logging;

namespace SapSoapCardWriter.BusinessLogic.NFC
{
	public abstract class NfcTag
	{
		public const byte NFC_FORUM_MAGIC_NUMBER	= 0xE1;
		public const byte NFC_FORUM_VERSION_NUMBER	= 0x10;
		
		public const byte LOCK_CONTROL_TLV 		= 0x01;
		public const byte MEMORY_CONTROL_TLV 	= 0x02;
		public const byte NDEF_MESSAGE_TLV 		= 0x03;
		public const byte NDEF_FILE_CONTROL_TLV = 0x04;
		public const byte PROPRIETARY_TLV 		= 0xFD;
		public const byte TERMINATOR_TLV 		= 0xFE;
		public const byte NULL_TLV				= 0x00;
		
		protected SmartCardChannel channel = null;
		
		protected long capacity = 0;
		protected bool is_empty = false;
		protected bool formatted = false;
		protected bool formattable = false;
		protected bool locked = false;
		protected bool lockable = false;

        public NfcTag(ILogger logger, SmartCardChannel channel)
		{
            this.logger = logger;
			this.channel = channel;
		}
		
		public List<Ndef> Content = new List<Ndef>();
        protected readonly ILogger logger;

		protected abstract bool WriteContent(byte[] content);
		
		public static bool Recognize(ILogger logger, SmartCardChannel cardchannel, out NfcTag tag, out string msg, out bool Desfire_formatable)
		{
			bool res = false;
			msg = "";
			tag = null;
			Desfire_formatable = false;
			
			if (NfcTagType2.RecognizeAtr(logger, cardchannel))
			{
                logger.Debug("Based on the ATR, this card is likely to be a NFC type 2 Tag");

                if (NfcTagType2.Recognize(logger, cardchannel))
				{
                    logger.Debug("This card is actually a NFC type 2 Tag");
					tag = NfcTagType2.Create(logger, cardchannel);
					if (tag == null)
						msg = "An error has occured while reading the Tag's content";
					res = true;
				} 
                else
				{
                    logger.Debug("Based on its content, this card is not a NFC type 2 Tag, sorry");
					msg = "From the ATR it may be a NFC type 2 Tag, but the content is invalid";
				}
			} 
            else if (NfcTagType4.Recognize(logger, cardchannel))
			{
                logger.Debug("This card is a NFC type 4 Tag");
				tag = NfcTagType4.Create(logger, cardchannel);
				if (tag == null)
					msg = "An error has occured while reading the Tag's content";
				
				res = true;
				
			} 
            else if (NfcTagType4Desfire.Recognize(logger, cardchannel))
			{
				msg = "A DESFire EV1 card has been detected.\nIt may be formatted into a type 4 Tag.";
				Desfire_formatable = true;
				res = false;			
			} 
            else
			{
				msg = "Unrecognized or unsupported card";
				tag = null;
			}
			return res;
		}
		
		
		private byte[] SerializeContent()
		{
			if ((Content == null) || (Content.Count == 0))
			{
                logger.Debug("Nothing to serialize");
				return null;
			}
			
			CardBuffer result = new CardBuffer();
			
			for (int i=0; i<Content.Count; i++)
			{
				bool is_begin = (i == 0) ? true : false;
				bool is_end = (i == (Content.Count-1)) ? true : false;
				byte[] t = Content[i].GetBytes(is_begin, is_end);
				
				result.Append(t);
			}
			
			return result.GetBytes();
		}

		public bool IsEmpty()
		{
			return is_empty;
		}

		public bool IsFormatted()
		{
			return formatted;
		}

		public bool IsFormattable()
		{
			return (!locked && !formatted && formattable);
		}

		public bool IsLocked()
		{
			return locked;
		}

		public bool IsLockable()
		{
			return (!locked && lockable);
		}
		
		public long Capacity()
		{
			return capacity;
		}

		public long ContentSize()
		{
			byte[] bytes = SerializeContent();
			
			if ((bytes == null) || (bytes.Length == 0))
				return 0;
			
			return bytes.Length;
		}

		public abstract bool Format();

		public bool Write()
		{
			return Write(false);
		}

		public bool Write(bool skip_checks)
		{
			if (!IsFormatted() && !skip_checks)
			{
                logger.Debug("The Tag is not formatted");
				return false;
			}
			
			if (IsLocked() && !skip_checks)
			{
                logger.Debug("The Tag is not writable");
				return false;
			}

			byte[] bytes = SerializeContent();
			
			if ((bytes == null) || (bytes.Length == 0))
			{
                logger.Debug("Nothing to write on the Tag");
				return false;
			}
			
			if (bytes.Length > Capacity())
			{
                logger.Debug("The size of the content is bigger than the Tag's capacity");
				return false;
			}

            logger.Debug("Writing the Tag...");

			if (!WriteContent(bytes))
			{
                logger.Debug("Write failed!");
				return false;
			}

            logger.Debug("Write success!");
			return true;
		}
		
		public abstract bool Lock();

		protected abstract bool Read();
	}
	
}
