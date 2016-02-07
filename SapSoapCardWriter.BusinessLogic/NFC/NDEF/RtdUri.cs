using SapSoapCardWriter.Logger.Logging;
using System;
using System.Collections;
using System.Collections.Generic;

namespace SapSoapCardWriter.BusinessLogic.NFC
{
	public class RtdUri : Rtd
	{
		
		public RtdUri(ILogger logger) 
            : base(Ndef.NDEF_HEADER_TNF_NFC_RTD_WKN, "U", logger)
		{

		}

        public RtdUri(ILogger logger, string Uri)
            : base(Ndef.NDEF_HEADER_TNF_NFC_RTD_WKN, "U", logger)
		{
			Value = Uri;
		}

        public RtdUri(ILogger logger, byte[] Payload)
            : base(Ndef.NDEF_HEADER_TNF_NFC_RTD_WKN, "U", logger)
		{
			payload = Payload;
		}

        public RtdUri(ILogger logger, Ndef record)
            : base(record, logger)
		{

		}

        public string Value
		{
			get
			{
				List<UriShortcut> shortcuts = UriShortcuts();
				
				byte[] t = new byte[payload.Length - 1];
				
				for (int i=0; i<payload.Length-1; i++)
					t[i] = payload[i+1];
				
				string s = CardBuffer.StringFromBytes(t);
				
				for (int i=0; i<shortcuts.Count; i++)
				{
					if (shortcuts[i].Shortcut == payload[0])
					{
						s = shortcuts[i].StandFor + s;
						break;
					}
				}
				
				return s;
			}
			set
			{
				List<UriShortcut> shortcuts = UriShortcuts();
				
				byte shortcut = 0;
				
				string s = value;
				
				for (int i=0; i<shortcuts.Count; i++)
				{
					if (s.StartsWith(shortcuts[i].StandFor))
					{
						s = s.Substring(shortcuts[i].StandFor.Length);
						shortcut = shortcuts[i].Shortcut;
						break;
					}
				}
				
				byte[] t = CardBuffer.BytesFromString(s);
				
				payload = new byte[t.Length + 1];
				
				payload[0] = shortcut;
				
				for (int i=0; i<t.Length; i++)
					payload[i+1] = t[i];
				

			}
		}
		
		private class UriShortcut
		{
			public byte Shortcut;
			public string StandFor;
			public UriShortcut(byte b, string s)
			{
				Shortcut = b;
				StandFor = s;
			}
		}
		
		private static List<UriShortcut> UriShortcuts()
		{
			List<UriShortcut> r = new List<UriShortcut>();
			
			r.Add(new UriShortcut(0x01, "http://www."));
			r.Add(new UriShortcut(0x02, "https://www."));
			r.Add(new UriShortcut(0x03, "http://"));
			r.Add(new UriShortcut(0x04, "https://"));
			r.Add(new UriShortcut(0x05, "tel:"));
			r.Add(new UriShortcut(0x06, "mailto:"));
			r.Add(new UriShortcut(0x07, "ftp://anonymous:anonymous@"));
			r.Add(new UriShortcut(0x08, "ftp://ftp."));
			r.Add(new UriShortcut(0x09, "ftps://"));
			r.Add(new UriShortcut(0x0A, "sftp://"));
			r.Add(new UriShortcut(0x0B, "smb://"));
			r.Add(new UriShortcut(0x0C, "nfs://"));
			r.Add(new UriShortcut(0x0D, "ftp://"));
			r.Add(new UriShortcut(0x0E, "dav://"));
			r.Add(new UriShortcut(0x0F, "news:"));
			r.Add(new UriShortcut(0x10, "telnet://"));
			r.Add(new UriShortcut(0x11, "imap:"));
			r.Add(new UriShortcut(0x12, "rtsp://"));
			r.Add(new UriShortcut(0x13, "urn:"));
			r.Add(new UriShortcut(0x14, "pop:"));
			r.Add(new UriShortcut(0x15, "sip:"));
			r.Add(new UriShortcut(0x16, "sips:"));
			r.Add(new UriShortcut(0x17, "tftp:"));
			r.Add(new UriShortcut(0x18, "btspp://"));
			r.Add(new UriShortcut(0x19, "btl2cap://"));
			r.Add(new UriShortcut(0x1A, "btgoep://"));
			r.Add(new UriShortcut(0x1B, "tcpobex://"));
			r.Add(new UriShortcut(0x1C, "irdaobex://"));
			r.Add(new UriShortcut(0x1D, "file://"));
			r.Add(new UriShortcut(0x1E, "urn:epc:id:"));
			r.Add(new UriShortcut(0x1F, "urn:epc:tag:"));
			r.Add(new UriShortcut(0x20, "urn:epc:pat:"));
			r.Add(new UriShortcut(0x21, "urn:epc:raw:"));
			r.Add(new UriShortcut(0x22, "urn:epc:"));
			r.Add(new UriShortcut(0x23, "urn:nfc:"));
			return r;                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                        }
	}
	
}
