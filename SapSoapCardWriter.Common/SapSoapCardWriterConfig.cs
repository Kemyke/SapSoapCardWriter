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
        [ConfigurationProperty("ServiceUserAcc", IsRequired = true)]
        public string ServiceUserAcc
        {
            get { return (string)this["ServiceUserAcc"]; }
            set { this["ServiceUserAcc"] = value; }
        }

        [ConfigurationProperty("ServiceUserPwd", IsRequired = true)]
        public string ServiceUserPwd
        {
            get { return (string)this["ServiceUserPwd"]; }
            set { this["ServiceUserPwd"] = value; }
        }
    }
}
