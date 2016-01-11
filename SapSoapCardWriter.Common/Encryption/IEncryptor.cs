using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SapSoapCardWriter.Common.Encryption
{
    public interface IEncryptor
    {
        string Encrypt(string clearText, string key);
        string Decrypt(string cipherText, string key);
    }
}
