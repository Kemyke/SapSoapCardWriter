using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SapSoapCardWriter.Common.Encryption;

namespace SapSoapCardWriter.UnitTests
{
    [TestClass]
    public class AES128EncryptorTest
    {
        [TestMethod]
        public void AES128EncryptorTest1()
        {
            AES128Encryptor enc = new AES128Encryptor();
            string clearText = "Test text";
            string key = "qwerty";
            string chiperText = enc.Encrypt(clearText, key);

            string clearTextAgain = enc.Decrypt(chiperText, key);
            Assert.AreEqual(clearText, clearTextAgain);
        }
    }
}
