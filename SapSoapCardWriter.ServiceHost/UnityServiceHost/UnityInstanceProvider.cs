using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.ServiceModel.Dispatcher;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Practices.Unity;
using SapSoapCardWriter.Common.DIContainer;

namespace SapSoapCardWriter.ServiceHost.Extensions
{
    public class UnityInstanceProvider : IInstanceProvider
    {
        private readonly IDIContainer container;
        private readonly Type contractType;

        public UnityInstanceProvider(IDIContainer container, Type contractType)
        {
            if (container == null)
            {
                throw new ArgumentNullException("container");
            }

            if (contractType == null)
            {
                throw new ArgumentNullException("contractType");
            }

            this.container = container;
            this.contractType = contractType;
        }

        public object GetInstance(InstanceContext instanceContext, Message message)
        {
            return container.GetInstance(contractType);
        }

        public object GetInstance(InstanceContext instanceContext)
        {
            return GetInstance(instanceContext, null);
        }

        public void ReleaseInstance(InstanceContext instanceContext, object instance)
        {
            instanceContext.Extensions.Find<UnityInstanceContextExtension>().DisposeOfChildContainer();
        }
    }
}
