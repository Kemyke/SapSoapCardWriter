using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SapSoapCardWriter.GUI
{
    public class UserData
    {
        public UserData(string l, string p) { LoginName = l; Password = p; }
        public string LoginName { get; set; }
        public string Password { get; set; }
    }
}
