﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SapSoapCardWriter.Common
{
    public interface ISapSoapCardWriterConfig
    {
        string ServiceUserAcc { get; }
        string ServiceUserPwd { get; }
    }
}
