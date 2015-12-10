using SapSoapCardWriter.ServiceContracts.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace SapSoapCardWriter.ServiceContracts
{
    [ServiceContract]
    public interface IStepMapService
    {
        [OperationContract]
        Response WriteCard(User user);
    }
}
