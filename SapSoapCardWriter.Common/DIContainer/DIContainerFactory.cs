using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SapSoapCardWriter.Common.DIContainer
{
    public class DIContainerFactory
    {
        public IDIContainer CreateDIContainer()
        {
            var ret = new DIContainerUnity();
            return ret;
        }

        public IDIContainer CreateAndLoadDIContainer()
        {
            var ret = CreateDIContainer();
            ret.LoadConfiguration();
            return ret;
        }
    }
}
