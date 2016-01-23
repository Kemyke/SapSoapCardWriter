using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SapSoapCardWriter.BusinessLogic.NFC;
using Moq;
using SapSoapCardWriter.Logger.Logging;

namespace SapSoapCardWriter.UnitTests.CardWriter
{
    [TestClass]
    public class NfcWriterTest
    {
        [TestMethod]
        public void TestErase()
        {
            Mock<ILogger> logger = new Mock<ILogger>();
            NfcWriter writer = new NfcWriter(logger.Object);

            writer.Erase();
        }

        [TestMethod]
        public void TestPrepare()
        {
            Mock<ILogger> logger = new Mock<ILogger>();
            NfcWriter writer = new NfcWriter(logger.Object);

            writer.Prepare();
        }

        [TestMethod]
        public void TestWrite()
        {
            Mock<ILogger> logger = new Mock<ILogger>();
            NfcWriter writer = new NfcWriter(logger.Object);

            writer.WriteNfcTag("testdata");
        }

        [TestMethod]
        public void TestLock()
        {
            Mock<ILogger> logger = new Mock<ILogger>();
            NfcWriter writer = new NfcWriter(logger.Object);

            writer.Lock();
        }
    }
}
