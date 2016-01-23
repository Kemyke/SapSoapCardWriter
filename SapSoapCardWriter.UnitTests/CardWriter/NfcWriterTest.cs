using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SapSoapCardWriter.BusinessLogic.NFC;
using Moq;
using SapSoapCardWriter.Logger.Logging;
using SapSoapCardWriter.BusinessLogic;

namespace SapSoapCardWriter.UnitTests.CardWriter
{
    [TestClass]
    public class NfcWriterTest
    {
        [TestMethod]
        public void Foo()
        {
            SmartCardChannel scard = new SmartCardChannel("OMNIKEY CardMan 5x21-CL 0");
            bool present = scard.CardPresent;
            Console.WriteLine("present: {0}", present);
            var beforeConnect = scard.Connected;
            Console.WriteLine("beforeConnect: {0}", beforeConnect);
            bool connectMEthod = scard.Connect();
            Console.WriteLine("connectMEthod: {0}", connectMEthod);
            var afterConnect = scard.Connected;
            Console.WriteLine("afterConnect: {0}", afterConnect);

            var d  = scard.Disconnect();

        }

        [TestMethod]
        public void TestErase()
        {
            Mock<ILogger> logger = new Mock<ILogger>();
            logger.Setup(a => a.Debug(It.IsAny<string>(), It.IsAny<object[]>())).Callback<string, object[]>((m, p) => { Console.WriteLine(string.Format(m, p)); });
            logger.Setup(a => a.Info(It.IsAny<string>(), It.IsAny<object[]>())).Callback<string, object[]>((m, p) => { Console.WriteLine(string.Format(m, p)); });
            logger.Setup(a => a.Warning(It.IsAny<string>(), It.IsAny<object[]>())).Callback<string, object[]>((m, p) => { Console.WriteLine(string.Format(m, p)); });
            logger.Setup(a => a.Error(It.IsAny<string>(), It.IsAny<object[]>())).Callback<string, object[]>((m, p) => { Console.WriteLine(string.Format(m, p)); });
            NfcWriter writer = new NfcWriter(logger.Object);

            writer.Erase("FADDDEADFADDDEAD");
        }

        [TestMethod]
        public void TestPrepare()
        {
            Mock<ILogger> logger = new Mock<ILogger>();
            logger.Setup(a => a.Debug(It.IsAny<string>(), It.IsAny<object[]>())).Callback<string, object[]>((m, p) => { Console.WriteLine(string.Format(m, p)); });
            logger.Setup(a => a.Info(It.IsAny<string>(), It.IsAny<object[]>())).Callback<string, object[]>((m, p) => { Console.WriteLine(string.Format(m, p)); });
            logger.Setup(a => a.Warning(It.IsAny<string>(), It.IsAny<object[]>())).Callback<string, object[]>((m, p) => { Console.WriteLine(string.Format(m, p)); });
            logger.Setup(a => a.Error(It.IsAny<string>(), It.IsAny<object[]>())).Callback<string, object[]>((m, p) => { Console.WriteLine(string.Format(m, p)); });
            NfcWriter writer = new NfcWriter(logger.Object);

            writer.Prepare("FADDDEADFADDDEAD");
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

            writer.Lock("FADDDEADFADDDEAD");
        }


        [TestMethod]
        public void TestFullWrite()
        {
            Mock<ILogger> logger = new Mock<ILogger>();
            logger.Setup(a => a.Debug(It.IsAny<string>(), It.IsAny<object[]>())).Callback<string, object[]>((m, p) => { Console.WriteLine(string.Format(m, p)); });
            logger.Setup(a => a.Info(It.IsAny<string>(), It.IsAny<object[]>())).Callback<string, object[]>((m, p) => { Console.WriteLine(string.Format(m, p)); });
            logger.Setup(a => a.Warning(It.IsAny<string>(), It.IsAny<object[]>())).Callback<string, object[]>((m, p) => { Console.WriteLine(string.Format(m, p)); });
            logger.Setup(a => a.Error(It.IsAny<string>(), It.IsAny<object[]>())).Callback<string, object[]>((m, p) => { Console.WriteLine(string.Format(m, p)); });
            NfcCardWriter writer = new NfcCardWriter(logger.Object);

            writer.WriteCard("FADDDEADFADDDEAD", "testfulldataa2");
        }
    }
}
