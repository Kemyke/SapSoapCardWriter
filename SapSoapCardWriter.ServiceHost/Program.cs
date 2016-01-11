using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;

namespace SapSoapCardWriter.ServiceHost
{
    public static class Program
    {
        public static void Main()
        {
            if (Environment.UserInteractive)
            {
                SapSoapCardWriterService service1 = new SapSoapCardWriterService();
                service1.TestStartupAndStop();
            }
            else
            {
                ServiceBase[] ServicesToRun;
                ServicesToRun = new ServiceBase[] 
                                { 
                                    new SapSoapCardWriterService() 
                                };
                ServiceBase.Run(ServicesToRun);

            }
        }
    }
}
