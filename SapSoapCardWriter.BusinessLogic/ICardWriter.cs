using SapSoapCardWriter.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SapSoapCardWriter.BusinessLogic
{
    public interface ICardWriter
    {
        ResultCode WriteCard(User user);
    }
}
