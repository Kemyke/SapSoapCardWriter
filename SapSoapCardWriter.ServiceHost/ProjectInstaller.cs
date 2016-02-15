using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration.Install;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace SapSoapCardWriter.ServiceHost
{
    [RunInstaller(true)]
    public partial class ProjectInstaller : System.Configuration.Install.Installer
    {
        public ProjectInstaller()
        {
            InitializeComponent();
        }

        protected override void OnAfterInstall(IDictionary savedState)
        {
            base.OnAfterInstall(savedState);

            var cmd = "netsh http add urlacl url=http://+:10132/ user=\"NETWORK SERVICE\"";
            var processInfo = new ProcessStartInfo
            {
                Verb = "runas",
                FileName = "cmd",
                Arguments = "/c " + cmd
            };
            Process.Start(processInfo);

            cmd = "netsh http add urlacl url=https://+:10134/ user=\"NETWORK SERVICE\"";
            processInfo = new ProcessStartInfo
            {
                Verb = "runas",
                FileName = "cmd",
                Arguments = "/c " + cmd
            };
            Process.Start(processInfo);
        }

        protected override void OnAfterUninstall(IDictionary savedState)
        {
            base.OnAfterUninstall(savedState);

            var cmd = "netsh http delete urlacl url=http://+:10132/";
            var processInfo = new ProcessStartInfo
            {
                Verb = "runas",
                FileName = "cmd",
                Arguments = "/c " + cmd
            };
            Process.Start(processInfo);

            cmd = "netsh http delete urlacl url=https://+:10134/";
            
            processInfo = new ProcessStartInfo
            {
                Verb = "runas",
                FileName = "cmd",
                Arguments = "/c " + cmd
            };
            Process.Start(processInfo);
        }
    }
}
