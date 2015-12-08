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
        //[ConfigurationProperty("Sample", IsRequired = true)]
        //public string Sample
        //{
        //    get { return (string)this["Sample"]; }
        //    set { this["Sample"] = value; }
        //}
    }
}
