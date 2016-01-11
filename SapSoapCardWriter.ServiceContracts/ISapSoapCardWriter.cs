using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace SapSoapCardWriter.ServiceContracts
{
    [ServiceContract]
    public interface ISapSoapCardWriter
    {
        [OperationContract]
        Response WriteCard(string data);

        [OperationContract]
        Response<string> Encrypt(string payload, string key);

        [OperationContract]
        Response<string> Decrypt(string chiper, string key);
    }
}
