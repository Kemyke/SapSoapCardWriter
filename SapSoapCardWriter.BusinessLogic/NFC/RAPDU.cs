using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SapSoapCardWriter.BusinessLogic.NFC
{
    /**c* SpringCardPCSC/RAPDU
	 *
	 * NAME
	 *   RAPDU
	 * 
	 * DESCRIPTION
	 *   The RAPDU object is used to receive and decode RESPONSE APDUs (according to ISO 7816-4) from the smartcard
	 * 
	 * DERIVED FROM
	 *   CardBuffer
	 *
	 **/

    public class RAPDU : CardBuffer
    {
        public bool isValid
        {
            get
            {
                return (_bytes.Length >= 2);
            }
        }

        public RAPDU(byte[] bytes, int length)
        {
            SetBytes(bytes, length);
        }

        public RAPDU(byte[] bytes)
        {
            SetBytes(bytes);
        }

        public RAPDU(byte[] bytes, byte SW1, byte SW2)
        {
            byte[] t;
            if (bytes == null)
            {
                t = new byte[2];
                t[0] = SW1;
                t[1] = SW2;
            }
            else
            {
                t = new byte[bytes.Length + 2];
                for (int i = 0; i < bytes.Length; i++)
                    t[i] = bytes[i];
                t[bytes.Length] = SW1;
                t[bytes.Length + 1] = SW2;
            }
            SetBytes(t);
        }

        public RAPDU(byte sw1, byte sw2)
        {
            byte[] t = new byte[2];
            t[0] = sw1;
            t[1] = sw2;
            SetBytes(t);
        }

        public RAPDU(ushort sw)
        {
            byte[] t = new byte[2];
            t[0] = (byte)(sw / 0x0100);
            t[1] = (byte)(sw % 0x0100);
            SetBytes(t);
        }

        public bool hasData
        {
            get
            {
                if ((_bytes == null) || (_bytes.Length < 2))
                    return false;

                return true;
            }
        }

        public CardBuffer data
        {
            get
            {
                if ((_bytes == null) || (_bytes.Length < 2))
                    return null;

                return new CardBuffer(_bytes, _bytes.Length - 2);
            }
        }

        public byte SW1
        {
            get
            {
                if ((_bytes == null) || (_bytes.Length < 2))
                    return 0xCC;
                return _bytes[_bytes.Length - 2];
            }
        }

        public byte SW2
        {
            get
            {
                if ((_bytes == null) || (_bytes.Length < 2))
                    return 0xCC;
                return _bytes[_bytes.Length - 1];
            }
        }

        public ushort SW
        {
            get
            {
                if ((_bytes == null) || (_bytes.Length < 2))
                    return 0xCCCC;

                ushort r;

                r = _bytes[_bytes.Length - 2];
                r *= 0x0100;
                r += _bytes[_bytes.Length - 1];

                return r;
            }
        }

        public string SWString
        {
            get
            {
                return SCARD.CardStatusWordsToString(SW1, SW2);
            }
        }
    }
}
