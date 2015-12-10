using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using SapSoapCardWriter.Common;

namespace SapSoapCardWriter.ServiceContracts
{
    [DataContract]
    public class Response
    {
        //TODO: remove
        /// <summary>
        /// DO NOT USE! THIS CONSTRUCTOR IS JUST FOR SERIALIZATION
        /// </summary>
        public Response() { }

        public Response(ResultCode resultCode)
        {
            ResultCode = resultCode;
        }

        [DataMember]
        public ResultCode ResultCode { get; set; }
        [DataMember]
        public string ErrorMessage { get; set; }
        [DataMember]
        public object ErrorData { get; set; }
    }

    [DataContract]
    public class Response<T> : Response
    {
        //TODO: remove
        /// <summary>
        /// DO NOT USE! THIS CONSTRUCTOR IS JUST FOR SERIALIZATION
        /// </summary>
        public Response() { }

        public Response(ResultCode resultCode, T result)
            : base(resultCode)
        {
            Result = result;
        }

        [DataMember]
        public T Result { get; set; }
    }
}
