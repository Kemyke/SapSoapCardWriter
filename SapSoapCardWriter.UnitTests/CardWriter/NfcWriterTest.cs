using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SapSoapCardWriter.BusinessLogic.NFC;
using Moq;
using SapSoapCardWriter.Logger.Logging;
using SapSoapCardWriter.BusinessLogic;
using SapSoapCardWriter.UnitTests.Helper;

namespace SapSoapCardWriter.UnitTests.CardWriter
{
    [TestClass]
    public class NfcWriterTest
    {
        [TestMethod]
        public void Foo()
        {
            ILogger logger = new ConsoleLogger();
                
            SmartCardChannel scard = new SmartCardChannel(logger, "OMNIKEY CardMan 5x21-CL 0");
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
            ILogger logger = new ConsoleLogger();
            NfcWriter writer = new NfcWriter(logger);

            writer.Erase("FADDDEADFADDDEAD");
        }

        [TestMethod]
        public void TestPrepare()
        {
            ILogger logger = new ConsoleLogger(); 
            NfcWriter writer = new NfcWriter(logger);

            writer.Prepare("FADDDEADFADDDEAD");
        }

        [TestMethod]
        public void TestWrite()
        {
            ILogger logger = new ConsoleLogger(); 
            NfcWriter writer = new NfcWriter(logger);

            writer.WriteNfcTag("testdata");
        }

        [TestMethod]
        public void TestLock()
        {
            ILogger logger = new ConsoleLogger(); 
            NfcWriter writer = new NfcWriter(logger);

            writer.Lock("FADDDEADFADDDEAD");
        }


        [TestMethod]
        public void TestFullWrite()
        {
            ILogger logger = new ConsoleLogger(); 
            NfcCardWriter writer = new NfcCardWriter(logger);

            writer.WriteCard("FADDDEADFADDDEAD", "testfulldataa2");
        }
    }
}
