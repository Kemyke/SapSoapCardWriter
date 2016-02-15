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
            string encryptedPayload = enc.Encrypt(clearText, key);

            string clearTextAgain = enc.Decrypt(encryptedPayload, key);

            Assert.AreEqual(clearText, clearTextAgain);
        }
    }
}
