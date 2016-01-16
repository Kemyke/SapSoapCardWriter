using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SapSoapCardWriter.BusinessLogic.NFC
{
    public class CardBuffer
    {
        protected byte[] _bytes = null;

        private bool isb(char c)
        {
            bool r = false;

            if ((c >= '0') && (c <= '9'))
            {
                r = true;
            }
            else if ((c >= 'A') && (c <= 'F'))
            {
                r = true;
            }
            else if ((c >= 'a') && (c <= 'f'))
            {
                r = true;
            }

            return r;
        }

        private byte htob(char c)
        {
            int r = 0;

            if ((c >= '0') && (c <= '9'))
            {
                r = c - '0';
            }
            else if ((c >= 'A') && (c <= 'F'))
            {
                r = c - 'A';
                r += 10;
            }
            else if ((c >= 'a') && (c <= 'f'))
            {
                r = c - 'a';
                r += 10;
            }

            return (byte)r;
        }

        public CardBuffer()
        {

        }


        public CardBuffer(byte b)
        {
            _bytes = new byte[1];
            _bytes[0] = b;
        }

        public CardBuffer(ushort w)
        {
            _bytes = new byte[2];
            _bytes[0] = (byte)(w / 0x0100);
            _bytes[1] = (byte)(w % 0x0100);
        }

        public CardBuffer(byte[] bytes)
        {
            _bytes = bytes;
        }

        public CardBuffer(byte[] bytes, long length)
        {
            SetBytes(bytes, length);
        }

        public CardBuffer(byte[] bytes, long offset, long length)
        {
            SetBytes(bytes, offset, length);
        }

        public CardBuffer(string str)
        {
            SetString(str);
        }

        public byte this[long offset]
        {
            get
            {
                return GetByte(offset);
            }
        }

        public byte GetByte(long offset)
        {
            if (_bytes == null)
                return 0;

            if (offset >= _bytes.Length)
                offset = 0;

            return _bytes[offset];
        }

        public byte[] GetBytes()
        {
            return _bytes;
        }

        public byte[] GetBytes(long length)
        {
            if (_bytes == null)
                return null;

            if (length < 0)
                length = _bytes.Length - length;

            if (length > _bytes.Length)
                length = _bytes.Length;

            byte[] r = new byte[length];
            for (long i = 0; i < length; i++)
                r[i] = _bytes[i];

            return r;
        }

        public byte[] GetBytes(long offset, long length)
        {
            if (_bytes == null)
                return null;

            if (offset < 0)
                offset = _bytes.Length - offset;

            if (length < 0)
                length = _bytes.Length - length;

            if (offset >= _bytes.Length)
                return null;

            if (length > (_bytes.Length - offset))
                length = _bytes.Length - offset;

            byte[] r = new byte[length];
            for (long i = 0; i < length; i++)
                r[i] = _bytes[offset + i];

            return r;
        }

        public char[] GetChars(long offset, long length)
        {
            byte[] b = GetBytes(offset, length);

            if (b == null) return null;

            char[] c = new char[b.Length];
            for (long i = 0; i < b.Length; i++)
                c[i] = (char)b[i];

            return c;
        }

        public void SetBytes(byte[] bytes)
        {
            _bytes = bytes;
        }

        public void SetBytes(byte[] bytes, long length)
        {
            _bytes = new byte[length];

            long i;

            for (i = 0; i < length; i++)
                _bytes[i] = bytes[i];
        }

        public void SetBytes(byte[] bytes, long offset, long length)
        {
            _bytes = new byte[length];

            long i;

            for (i = 0; i < length; i++)
                _bytes[i] = bytes[offset + i];
        }

        public void Append(byte[] bytes)
        {
            if ((bytes == null) || (bytes.Length == 0))
                return;

            byte[] old_bytes = GetBytes();

            if ((old_bytes == null) || (old_bytes.Length == 0))
            {
                SetBytes(bytes);
                return;
            }

            _bytes = new byte[old_bytes.Length + bytes.Length];

            for (long i = 0; i < old_bytes.Length; i++)
                _bytes[i] = old_bytes[i];
            for (long i = 0; i < bytes.Length; i++)
                _bytes[old_bytes.Length + i] = bytes[i];

        }

        public void SetString(string str)
        {
            string s = "";
            int i, l;

            l = str.Length;
            for (i = 0; i < l; i++)
            {
                char c = str[i];

                if (isb(c))
                    s = s + c;
            }

            l = s.Length;
            _bytes = new Byte[l / 2];

            for (i = 0; i < l; i += 2)
            {
                _bytes[i / 2] = htob(s[i]);
                _bytes[i / 2] *= 0x10;
                _bytes[i / 2] += htob(s[i + 1]);
            }
        }

        public virtual string AsString(string separator)
        {
            string s = "";
            long i;

            if (_bytes != null)
            {
                for (i = 0; i < _bytes.Length; i++)
                {
                    if (i > 0)
                        s = s + separator;
                    s = s + String.Format("{0:X02}", _bytes[i]);
                }
            }

            return s;
        }

        public virtual string AsString()
        {
            return AsString("");
        }

        protected byte[] Bytes
        {
            get
            {
                return _bytes;
            }
            set
            {
                _bytes = value;
            }
        }

        public int Length
        {
            get
            {
                if (_bytes == null)
                    return 0;
                return _bytes.Length;
            }
        }

        public static byte[] BytesFromString(string s)
        {
            byte[] r = new byte[s.Length];
            for (int i = 0; i < r.Length; i++)
            {
                char c = s[i];
                r[i] = (byte)(c & 0x7F);
            }
            return r;
        }

        public static string StringFromBytes(byte[] a)
        {
            string r = "";
            if (a != null)
            {
                for (int i = 0; i < a.Length; i++)
                {
                    char c = (char)a[i];
                    r += c;
                }
            }
            return r;
        }
    }
}
