using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SapSoapCardWriter.BusinessLogic.NFC
{
    /**c* SpringCardPCSC/CAPDU
	 *
	 * NAME
	 *   CAPDU
	 * 
	 * DESCRIPTION
	 *   The CAPDU object is used to format and send COMMAND APDUs (according to ISO 7816-4) to the smartcard
	 * 
	 * DERIVED FROM
	 *   CardBuffer
	 *
	 **/

    public class Capdu : CardBuffer
    {
        public Capdu()
        {

        }

        public Capdu(byte[] bytes)
        {
            _bytes = bytes;
        }

        public Capdu(byte CLA, byte INS, byte P1, byte P2)
        {
            _bytes = new byte[4];
            _bytes[0] = CLA;
            _bytes[1] = INS;
            _bytes[2] = P1;
            _bytes[3] = P2;
        }

        public Capdu(byte CLA, byte INS, byte P1, byte P2, byte P3)
        {
            _bytes = new byte[5];
            _bytes[0] = CLA;
            _bytes[1] = INS;
            _bytes[2] = P1;
            _bytes[3] = P2;
            _bytes[4] = P3;
        }

        public Capdu(byte CLA, byte INS, byte P1, byte P2, byte[] data)
        {
            int i;
            _bytes = new byte[5 + data.Length];
            _bytes[0] = CLA;
            _bytes[1] = INS;
            _bytes[2] = P1;
            _bytes[3] = P2;
            _bytes[4] = (byte)data.Length;
            for (i = 0; i < data.Length; i++)
                _bytes[5 + i] = data[i];
        }

        public Capdu(byte CLA, byte INS, byte P1, byte P2, string data)
        {
            int i;
            byte[] _data = (new CardBuffer(data)).GetBytes();
            _bytes = new byte[5 + _data.Length];
            _bytes[0] = CLA;
            _bytes[1] = INS;
            _bytes[2] = P1;
            _bytes[3] = P2;
            _bytes[4] = (byte)_data.Length;
            for (i = 0; i < _data.Length; i++)
                _bytes[5 + i] = _data[i];
        }

        public Capdu(byte CLA, byte INS, byte P1, byte P2, byte[] data, byte LE)
        {
            int i;
            _bytes = new byte[6 + data.Length];
            _bytes[0] = CLA;
            _bytes[1] = INS;
            _bytes[2] = P1;
            _bytes[3] = P2;
            _bytes[4] = (byte)data.Length;
            for (i = 0; i < data.Length; i++)
                _bytes[5 + i] = data[i];
            _bytes[5 + data.Length] = LE;
        }

        public Capdu(byte CLA, byte INS, byte P1, byte P2, string data, byte LE)
        {
            int i;
            byte[] _data = (new CardBuffer(data)).GetBytes();
            _bytes = new byte[6 + _data.Length];
            _bytes[0] = CLA;
            _bytes[1] = INS;
            _bytes[2] = P1;
            _bytes[3] = P2;
            _bytes[4] = (byte)_data.Length;
            for (i = 0; i < _data.Length; i++)
                _bytes[5 + i] = _data[i];
            _bytes[5 + _data.Length] = LE;
        }

        public Capdu(string str)
        {
            SetString(str);
        }

        public byte CLA
        {
            get
            {
                if (_bytes == null)
                    return 0xFF;
                return _bytes[0];
            }
            set
            {
                if (_bytes == null)
                    _bytes = new byte[4];
                _bytes[0] = value;
            }
        }

        public byte INS
        {
            get
            {
                if (_bytes == null)
                    return 0xFF;
                return _bytes[1];
            }
            set
            {
                if (_bytes == null)
                    _bytes = new byte[4];
                _bytes[1] = value;
            }
        }

        public byte P1
        {
            get
            {
                if (_bytes == null)
                    return 0xFF;
                return _bytes[2];
            }
            set
            {
                if (_bytes == null)
                    _bytes = new byte[4];
                _bytes[2] = value;
            }
        }

        public byte P2
        {
            get
            {
                if (_bytes == null)
                    return 0xFF;
                return _bytes[3];
            }
            set
            {
                if (_bytes == null)
                    _bytes = new byte[4];
                _bytes[3] = value;
            }
        }

        private bool hasLc()
        {
            if (!Valid())
                return false;
            if (_bytes.Length <= 5)
                return false;
            return true;
        }

        private bool hasLe()
        {
            if (!Valid())
                return false;
            if (_bytes.Length == 6 + _bytes[4])
                return false;
            return true;
        }

        public bool Valid()
        {
            if (_bytes == null)
                return false;
            if (_bytes.Length <= 4)
                return false;
            if (_bytes.Length == 5)
                return true;
            if (_bytes.Length == 5 + _bytes[4])
                return true;
            if (_bytes.Length == 6 + _bytes[4])
                return true;
            return false;
        }

        public byte Lc
        {
            get
            {
                if (!hasLc())
                    return 0x00;

                return _bytes[4];
            }
        }

        public byte Le
        {
            get
            {
                if (!hasLe())
                    return 0x00;
                return _bytes[_bytes.Length - 1];
            }

            set
            {
                if (_bytes == null)
                    _bytes = new byte[5];
                if (!hasLe())
                {
                    byte[] t = new byte[_bytes.Length + 1];
                    for (int i = 0; i < _bytes.Length; i++)
                        t[i] = _bytes[i];
                    _bytes = t;
                }
                _bytes[_bytes.Length - 1] = value;
            }
        }

        public CardBuffer data
        {
            get
            {
                if (!hasLc())
                    return null;

                byte[] t = new byte[Lc];
                for (int i = 0; i < t.Length; i++)
                    t[i] = _bytes[5 + i];

                return new CardBuffer(t);
            }

            set
            {
                int length;
                uint apdu_size;

                if (value == null)
                    length = 0;
                else
                    length = value.Length;

                if (length == 0)
                {

                }
                else
                    if (length < 256)
                {
                    if (hasLe())
                        apdu_size = (uint)(6 + length);
                    else
                        apdu_size = (uint)(5 + length);

                    byte[] t = new byte[apdu_size];

                    if (Valid())
                    {
                        for (int i = 0; i < 4; i++)
                            t[i] = _bytes[i];
                        if (hasLe())
                            t[t.Length - 1] = _bytes[_bytes.Length - 1];
                    }

                    for (int i = 0; i < length; i++)
                        t[5 + i] = value.GetByte(i);

                    t[4] = (byte)length;

                    _bytes = t;
                }
                else
                {
                    /* Oups ? */
                }
            }
        }
    }
}
