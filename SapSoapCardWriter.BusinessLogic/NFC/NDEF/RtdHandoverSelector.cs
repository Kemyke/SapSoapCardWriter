using System;
using System.Diagnostics;
using System.Collections;
using System.Collections.Generic;
using SapSoapCardWriter.Logger.Logging;

namespace SapSoapCardWriter.BusinessLogic.NFC
{
	public class RtdHandoverSelector : Rtd
	{
		private byte version = 0x00;
		private RtdAlternativeCarrier[] alternative_carriers;
		private AbsoluteUri[] related_absolute_uris;

		public RtdHandoverSelector(ILogger logger) 
            : base(Ndef.NDEF_HEADER_TNF_NFC_RTD_WKN, "Hs", logger)
		{
			
		}
		
		public RtdHandoverSelector(ILogger logger, byte[] payload, ref byte[] buffer, ref int next_ndef_starting_point) 
            : base(Ndef.NDEF_HEADER_TNF_NFC_RTD_WKN, "Hs", logger)
		{
			
			/* Take care of Payload	*/
			int offset = 1;
			Ndef ndef = null;
			bool terminated = true;
			
			if ((payload != null) && (payload.Length >0 ))
				version = payload[0];
			
			if (version < 0x10)
			{
				logger.Error("Incompatible version: " + String.Format("{0:x02}", version));
				offset = payload.Length - 1; /* so that it won't be parsed	*/
			}
			
			List<RtdAlternativeCarrier> altenative_carriers_list = new List<RtdAlternativeCarrier>();
			
			while (Ndef.Parse(logger, payload, ref offset, ref ndef, ref terminated))
			{
				if (ndef is RtdAlternativeCarrier)
				{
					logger.Debug("Got a new Alternative Carrier");
					altenative_carriers_list.Add((RtdAlternativeCarrier) ndef);
				}
				
				if (terminated)
					break;
			}
			alternative_carriers = altenative_carriers_list.ToArray();
			
			
			/* Take care of following NDEFs	*/
			terminated = true;
			List<AbsoluteUri> related_absolute_uris_list = new List<AbsoluteUri>();
			while (Ndef.Parse(logger, buffer, ref next_ndef_starting_point, ref ndef, ref terminated))
			{
				if (ndef is AbsoluteUri)
				{
					logger.Debug("Got a new Absolute Uri");
					related_absolute_uris_list.Add((AbsoluteUri) ndef);
				}
				
				if (terminated)
					break;
			}
			related_absolute_uris = related_absolute_uris_list.ToArray();
			
		}


        public RtdHandoverSelector(ILogger logger, RtdAlternativeCarrier[] alternative_carrier, AbsoluteUri[] absolute_uri) 
            : base(Ndef.NDEF_HEADER_TNF_NFC_RTD_WKN, "Hs", logger)
		{
			version = 0x12;
			
			alternative_carriers = new RtdAlternativeCarrier[alternative_carrier.Length];
			alternative_carriers = alternative_carrier;
			
			related_absolute_uris = new AbsoluteUri[absolute_uri.Length];
			related_absolute_uris = absolute_uri;
		}

        public RtdHandoverSelector(ILogger logger, RtdAlternativeCarrier alternative_carrier, AbsoluteUri absolute_uri)
            : base(Ndef.NDEF_HEADER_TNF_NFC_RTD_WKN, "Hs", logger)
		{
			version = 0x12;
			
			alternative_carriers = new RtdAlternativeCarrier[1];
			alternative_carriers[0] = alternative_carrier;
			
			related_absolute_uris = new AbsoluteUri[1];
			related_absolute_uris[0] = absolute_uri;
		}
		
		/**v* SpringCardNFC/RtdHandoverSelector.AlternativeCarriers
		 *
		 * SYNOPSIS
		 *   public RtdAlternativeCarrier[] AlternativeCarriers
		 * 
		 * DESCRIPTION
		 *  Gets the Alternative Carriers of this Handover Selector record
		 *
		 *
		 **/
		public RtdAlternativeCarrier[] AlternativeCarriers
		{
			get
			{
				return alternative_carriers;
			}
		}
		
		/**v* SpringCardNFC/RtdHandoverSelector.RelatedAbsoluteUris
		 *
		 * SYNOPSIS
		 *   public AbsoluteUri[] RelatedAbsoluteUris
		 * 
		 * DESCRIPTION
		 *  Gets the related absolute URIs of this Handover Selector record.
		 * 	These absolute URIs are those which are referenced by all the Carrier
		 * 	Data References and Auxiliary Data References of all the Alternative Carrier Records
		 * 	of his Handover Selector.
		 *
		 *
		 **/
		public AbsoluteUri[] RelatedAbsoluteUris
		{
			get
			{
				return related_absolute_uris;
			}
		}
		
		/**m* SpringCardNFC/RtdHandoverSelector.Encode
		 *
		 * SYNOPSIS
		 *   public virtual bool Encode(ref byte[] buffer)
		 * 
		 * DESCRIPTION
		 *   Serializes the Handover Selector Record and also concatenates
		 *   the serialized Absolute Uris, referenced by the Alternative
		 * 	 Carrier Records
		 * 	 Returns true if it succeeds, false otherwise
		 * 
		 **/
		public override bool Encode(ref byte[] buffer)
		{
			/* First : encode the alternative carriers	*/
			byte[] buffer_HsNDEF = null;
			for (int i=0; i<alternative_carriers.Length ; i++)
			{
				if (i==0)
					alternative_carriers[i].SetMessageBegin(true);
				
				if (i==alternative_carriers.Length - 1)
					alternative_carriers[i].SetMessageEnd(true);
				
				alternative_carriers[i].Encode(ref buffer_HsNDEF);
			}
			
			/* Second : add the version in front of it : this becomes the payload of the Hs NDEF	*/
			byte[] payload = new byte[buffer_HsNDEF.Length + 1];
			payload[0] = version;
			Array.ConstrainedCopy(buffer_HsNDEF, 0, payload, 1, buffer_HsNDEF.Length);
			PAYLOAD = payload;
			
			/* Third : encode the Hs NDEF	*/
			base.SetMessageEnd(false);
			if (!base.Encode(ref buffer_HsNDEF))
				return false;
			
			/* Fourth : Encode the _related_absolute_uris	*/
			byte[] buffer_related_ndef = null;
			List<byte> buffer_related_ndefs_list_bytes = new List<byte>();
			for (int i=0; i<related_absolute_uris.Length ; i++)
			{
				
				byte[] buffer_tmp = null;
				if (i== (related_absolute_uris.Length-1))
					related_absolute_uris[i].SetMessageEnd(true);
				
				if (!related_absolute_uris[i].Encode(ref buffer_tmp))
					return false;
				
				if (buffer_tmp != null)
					for (int j = 0; j<buffer_tmp.Length; j++)
						buffer_related_ndefs_list_bytes.Add(buffer_tmp[j]);
				
			}
			
			buffer_related_ndef = buffer_related_ndefs_list_bytes.ToArray();
			
			
			/* Fourth : concat the two buffers (Hs NDEF + related absolute URI NDEFs)	*/
			buffer = new byte[buffer_HsNDEF.Length + buffer_related_ndef.Length];
			Array.ConstrainedCopy(buffer_HsNDEF, 0, buffer, 0, buffer_HsNDEF.Length);
			Array.ConstrainedCopy(buffer_related_ndef, 0, buffer, buffer_HsNDEF.Length, buffer_related_ndef.Length);
			
			return true;
		}
		
	}
	
}
