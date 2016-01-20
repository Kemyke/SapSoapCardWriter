using System;
using System.Collections;
using System.Collections.Generic;

namespace SapSoapCardWriter.BusinessLogic.NFC
{
	public class NfcTlv
	{
		private const byte FLAG_L_3_BYTES = 0xFF;
		private byte _t;
		private byte[] _v;
		
		public NfcTlv()
		{
			_t = 0;
			_v = null;
		}
		
		public NfcTlv(byte t, byte[] v)
		{
			_t = t;
			_v = v;
		}
		
		private Ndef[] child = new Ndef[0];
		
		
		public byte T
		{
			get
			{
				return _t;
			}
		}

		public long L
		{
			get
			{
				if (_v == null)
					return 0;
				return _v.Length;
			}
		}
		
		public byte[] V
		{
			get
			{
				return _v;
			}
		}
		
		public byte[] Serialize()
		{
			byte[] r = null;
			
			if (L <= 254)
			{
				/* L is on 1 byte only */
				r = new byte[1 + 1 + L];
				r[0] = T;
				r[1] = (byte) L;
				for (long i=0; i<L; i++)
					r[2+i] = V[i];
				
			} else
				if (L < 65535)
			{
				/* L is on 3 bytes */
				r = new byte[1 + 3 + L];
				r[0] = T;
				r[1] = 0xFF;
				r[2] = (byte) (L / 0x0100);
				r[3] = (byte) (L % 0x0100);
				for (long i=0; i<L; i++)
					r[4+i] = V[i];
				
			}
			
			return r;
		}
		
		public static NfcTlv Unserialize(byte[] buffer, ref byte[] remaining_buffer)
		{
			byte t;
			long l, o = 0;
			byte[] v = null;
			
			remaining_buffer = null;
			
			if (buffer == null)
				return null;
			if (buffer.Length < 2)
				return null;
			
			t = buffer[0];
			
			if (buffer[1] == 0xFF)
			{
				if (buffer.Length < 4)
					return null;
				l = buffer[2] * 0x0100 + buffer[3];
				o = 4;
			} else
			{
				l = buffer[1];
				o = 2;
			}
			
			if ((o + l) > buffer.Length)
				return null;
			
			if (l > 0)
			{
				v = new byte[l];
				for (long i=0; i<l; i++)
					v[i] = buffer[o + i];
			}
			
			o += l;
			
			if (o < buffer.Length)
			{
				remaining_buffer = new byte[buffer.Length - o];
				for (long i=0; i<remaining_buffer.Length; i++)
					remaining_buffer[i] = buffer[o + i];
			}
			
			return new NfcTlv(t, v);
		}
		
		public void add_child(Ndef ndef)
		{
			Ndef[] tmp = new Ndef[child.Length + 1];
			Array.Copy(child, tmp, child.Length);
			tmp[tmp.Length -1 ] = ndef;
			child = new Ndef[tmp.Length];
			Array.Copy(tmp, child, child.Length);
		}

		public Ndef get_child(int  i)
		{
			if ((i <= this.count_children()) && (i>=0))
			{
				return child[i];
			} else
			{
				return null;
			}
		}
		
		public int count_children()
		{
			return child.Length;
		}

	}
	
}
