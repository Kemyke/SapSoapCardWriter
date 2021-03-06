﻿using SapSoapCardWriter.Logger.Logging;
using System;

namespace SapSoapCardWriter.BusinessLogic.NFC
{
	public class RtdText : Rtd
	{
		private string _lang = "";
		private string _text = "";
		
		public RtdText(ILogger logger, string Text)
            : base(Ndef.NDEF_HEADER_TNF_NFC_RTD_WKN, "T", logger)
		{
			_lang = "en";
			_text = Text;
			EncodePayload();
		}

        public RtdText(ILogger logger, string Text, string Lang)
            : base(Ndef.NDEF_HEADER_TNF_NFC_RTD_WKN, "T", logger)
		{
			_lang = Lang;
			_text = Text;
			EncodePayload();
		}

        public RtdText(ILogger logger, byte[] Payload)
            : base(Ndef.NDEF_HEADER_TNF_NFC_RTD_WKN, "T", logger)
		{
			_lang = "";
			_text = "";
			
			int offset = 1;
			
			if ((Payload[0] & 0x3F) != 0)
			{
				for (int i=0; i<(Payload[0] & 0x3F); i++)
					_lang = _lang + (char) Payload[offset++];
			}

			while (offset < Payload.Length)
				_text = _text + (char) Payload[offset++];
			
			EncodePayload();
		}
		
		private void EncodePayload()
		{
			payload = new byte[1 + _lang.Length + _text.Length];
			
			payload[0] = 0;
			
			payload[0] |= (byte) (_lang.Length & 0x3F);
			
			int offset = 1;
			
			for (int i=0; i<_lang.Length; i++)
				payload[offset++] = (byte) _lang[i];
			for (int i=0; i<_text.Length; i++)
				payload[offset++] = (byte) _text[i];
		}

		/**v* SpringCardNFC/RtdText.Value
		 *
		 * SYNOPSIS
		 *   public string Value
		 * 
		 * DESCRIPTION
		 *  Gets and sets the text of the object
		 *
		 **/
		public string Value
		{
			get
			{
				return _text;
			}
			set
			{
				_text = value;
				EncodePayload();
			}
		}

		/**v* SpringCardNFC/RtdText.Lang
		 *
		 * SYNOPSIS
		 *   public string TextContent
		 * 
		 * DESCRIPTION
		 *  Gets and sets the lang of the object
		 *
		 **/
		public string Lang
		{
			get
			{
				return _lang;
			}
			set
			{
				_lang = value;
				EncodePayload();
			}
		}
	}
}
