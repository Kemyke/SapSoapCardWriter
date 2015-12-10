using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Description;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Practices.Unity;
using SapSoapCardWriter.Common.DIContainer;

namespace SapSoapCardWriter.ServiceHost.Extensions
{
    public class UnityServiceHost : System.ServiceModel.ServiceHost
    {
        public IDIContainer Container { get; private set; }

        public UnityServiceHost(IDIContainer container, Type serviceType)
            : base(serviceType)
        {           
            this.Initialize(container);
        }

        public UnityServiceHost(IDIContainer container, object singletonInstance)
            : base(singletonInstance)
        {
            this.Initialize(container);
        }

        private void Initialize(IDIContainer container)
        {
            if (container == null)
            {
                throw new ArgumentNullException("container");
            }

            this.Container = container;

            ApplyServiceBehaviors(container);

            ApplyContractBehaviors(container);

            foreach (var contractDescription in ImplementedContracts.Values)
            {
                var contractBehavior =
                    new UnityContractBehavior(new UnityInstanceProvider(container, contractDescription.ContractType));

                contractDescription.Behaviors.Add(contractBehavior);
            }
        }

        private void ApplyContractBehaviors(IDIContainer container)
        {
            var registeredContractBehaviors = container.GetAllInstance<IContractBehavior>();

            foreach (var contractBehavior in registeredContractBehaviors)
            {
                foreach (var contractDescription in ImplementedContracts.Values)
                {
                    contractDescription.Behaviors.Add(contractBehavior);
                }
            }
        }

        private void ApplyServiceBehaviors(IDIContainer container)
        {
            var registeredServiceBehaviors = container.GetAllInstance<IServiceBehavior>();

            foreach (var serviceBehavior in registeredServiceBehaviors)
            {
                Description.Behaviors.Add(serviceBehavior);
            }
        }
    }
}
