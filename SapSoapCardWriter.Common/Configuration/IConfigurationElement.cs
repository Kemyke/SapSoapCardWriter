﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;

namespace SapSoapCardWriter.Common.Configuration
{
    public interface IConfigurationElement
    {
        object GetElementKey { get; }
    }
}