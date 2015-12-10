using SapSoapCardWriter.Common;
using SapSoapCardWriter.Common.Configuration;
using SapSoapCardWriter.Common.DIContainer;
using SapSoapCardWriter.ServiceContracts;
using SapSoapCardWriter.ServiceHost.Extensions;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;

namespace SapSoapCardWriter.ServiceHost
{
    public partial class SapSoapCardWriterService : ServiceBase
    {
        public SapSoapCardWriterService()
        {
            InitializeComponent();
        }

        private UnityServiceHost serviceHost = null;

        protected override void OnStart(string[] args)
        {
            DIContainerFactory factory = new DIContainerFactory();
            IDIContainer di = factory.CreateAndLoadDIContainer();
            var configManager = di.GetInstance<IConfigurationManager<ISapSoapCardWriterConfig>>();
            configManager.LoadConfiguation();
            di.RegisterInstance<ISapSoapCardWriterConfig>(configManager.Config);

            serviceHost = new UnityServiceHost(di, di.GetInstance<ISapSoapCardWriter>());
            serviceHost.Open();
        }

        protected override void OnStop()
        {
            serviceHost.Close();
        }
    }
}
