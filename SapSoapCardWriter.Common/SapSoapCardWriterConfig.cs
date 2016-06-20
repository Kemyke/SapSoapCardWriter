using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SapSoapCardWriter.Common
{
    public sealed class SapSoapCardWriterConfig : ConfigurationSection, ISapSoapCardWriterConfig
    {
        [ConfigurationProperty("EnvName", IsRequired = true)]
        public string EnvName
        {
            get { return (string)this["EnvName"]; }
            set { this["EnvName"] = value; }
        }

        [ConfigurationProperty("BackgroundColor", IsRequired = true)]
        public string BackgroundColor
        {
            get { return (string)this["BackgroundColor"]; }
            set { this["BackgroundColor"] = value; }
        }
    }
}
