using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SapSoapCardWriter.BusinessLogic.NFC;
using Moq;
using SapSoapCardWriter.Logger.Logging;
using SapSoapCardWriter.BusinessLogic;
using SapSoapCardWriter.UnitTests.Helper;
using System.Threading.Tasks;

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
        public void TestGetUid()
        {
            ILogger logger = new ConsoleLogger();
            NfcWriter writer = new NfcWriter(logger);

            byte[] rfid = writer.GetCardUID("FADDDEADFADDDEAD");

            Console.WriteLine("RFID length:");
            Console.WriteLine(rfid.Length);
            Console.WriteLine("RFID:");
            Console.WriteLine(BitConverter.ToString(rfid));
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

            List<string> testDataList = new List<string>();
            testDataList.Add("testdata1");
            testDataList.Add("testdata2");
            writer.WriteNfcTag(testDataList);
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

            List<string> testDataList = new List<string>();            
            testDataList.Add("1892567125");
            testDataList.Add("encpublicdata");
            testDataList.Add("encfulldata");
            writer.WriteCard("FADDDEADFADDDEAD", testDataList);
        }

        [TestMethod]
        public async Task TestRead()
        {
            ILogger logger = new ConsoleLogger();
            NfcCardWriter writer = new NfcCardWriter(logger);
            List<string> ret = await writer.ReadNfcTags();
            Console.WriteLine(string.Join(",", ret));
        }

        [TestMethod]
        public async Task TestReadWrite()
        {
            try
            {
                ILogger logger = new ConsoleLogger();
                NfcCardWriter writer = new NfcCardWriter(logger);
                List<string> ret = await writer.ReadNfcTags();
                Console.WriteLine(string.Join(",", ret));
                List<string> testDataList = new List<string>();
                testDataList.Add("guid");
                testDataList.Add("encpublicdata");
                testDataList.Add("encfulldata");
                writer.WriteCard("FADDDEADFADDDEAD", testDataList);
                Console.WriteLine("Write ok");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }
    }
}
