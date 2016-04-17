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
            string clearText = 
@"<soapenv:Envelope xmlns:soapenv=""http://schemas.xmlsoap.org/soap/envelope/"" xmlns:tem=""http://tempuri.org/"">
   <soapenv:Header/>
   <soapenv:Body>
      <tem:Encrypt>
         <!--Optional:-->
         <tem:payload>PD94bWwgdmVyc2lvbj0iMS4wIiBlbmNvZGluZz0idXRmLTgiPz48REFUQT48T1NURVJNRUxPSV9JR0FaT0xWQU5ZX1NaQU0+MjE5Mzc0NjEyOTwvT1NURVJNRUxPSV9JR0FaT0xWQU5ZX1NaQU0+PEtBUlRZQV9TWkFNPjE4OTI1NjcxMjU8L0tBUlRZQV9TWkFNPjxGT19PU1RFUk1FTE8+PE9TVEVSTUVMT19BWk9OT1NJVE8+MTIzMTIzNDI1NDwvT1NURVJNRUxPX0FaT05PU0lUTz48RkVMSVJfQVpPTk9TSVRPPjE0OTM1MjM5NTQ8L0ZFTElSX0FaT05PU0lUTz48Q1NBTEFEVEFHSV9NSU5PU0VHPmNzYWzDoWR0YWc8L0NTQUxBRFRBR0lfTUlOT1NFRz48U1pVTEVURVNJX0lETz4yMDE1LTEyLTAxPC9TWlVMRVRFU0lfSURPPjxTWlVMRVRFU0lfSEVMWT5TesO8bGV0ZXR0PC9TWlVMRVRFU0lfSEVMWT48QURPX1NaQU0+SFUzPC9BRE9fU1pBTT48Q1NBTEFESV9IQVRBUk9aQVRfU1pBTUE+MTI8L0NTQUxBRElfSEFUQVJPWkFUX1NaQU1BPjxMRVZFTEVaRVNJX0NJTT48SVJBTllJVE9TWkFNPjEwOTQ8L0lSQU5ZSVRPU1pBTT48VEVMRVBVTEVTPkJ1ZGFwZXN0PC9URUxFUFVMRVM+PENJTT5Cb2tyw6l0YTwvQ0lNPjwvTEVWRUxFWkVTSV9DSU0+PC9GT19PU1RFUk1FTE8+PENTQUxBRFRBR09LPjxpdGVtPjxDU0FMQURUQUdJX01JTk9TRUc+QW55YTwvQ1NBTEFEVEFHSV9NSU5PU0VHPjxTWlVMRVRFU0lfSURPPjIwMTEtMTItMDE8L1NaVUxFVEVTSV9JRE8+PFNaVUxFVEVTSV9IRUxZPkjDs2RtZXrFkXbDoXPDoXJoZWx5PC9TWlVMRVRFU0lfSEVMWT48Q1NBTEFESV9IQVRBUk9aQVRfU1pBTUE+MTIzPC9DU0FMQURJX0hBVEFST1pBVF9TWkFNQT48L2l0ZW0+PC9DU0FMQURUQUdPSz48S0lBTExJVEFTX0RBVFVNQT4yMDE1LTEyLTIxPC9LSUFMTElUQVNfREFUVU1BPjxIQVRBTFlPU1NBR19LRVpERVRFPjIwMTYtMDEtMDE8L0hBVEFMWU9TU0FHX0tFWkRFVEU+PEhBVEFMWU9TU0FHX1ZFR0U+MDAwMC0wMC0wMDwvSEFUQUxZT1NTQUdfVkVHRT48RVJWRU5ZRVNTRUdfS0VaREVURT4yMDE1LTEyLTAxPC9FUlZFTllFU1NFR19LRVpERVRFPjxFUlZFTllFU1NFR19WRUdFPjIwMTUtMTItMzE8L0VSVkVOWUVTU0VHX1ZFR0U+PEFET1pBU0lfTU9ET0s+PGl0ZW0+PElEPjEyPC9JRD48RVY+MjAxNTwvRVY+PEFET1pBU0lfTU9EPkFkw7NuZW0gw6Fmw6FzPC9BRE9aQVNJX01PRD48L2l0ZW0+PC9BRE9aQVNJX01PRE9LPjxPVlRKX0tPRE9LPjxpdGVtPjxJRD4wPC9JRD48T1ZUSl9LT0Q+MDExMjAxPC9PVlRKX0tPRD48RVJWRU5ZRVNTRUdfS0VaREVURT4yMDE1LTEyLTAxPC9FUlZFTllFU1NFR19LRVpERVRFPjxFUlZFTllFU1NFR19WRUdFPjIwMTUtMTItMjM8L0VSVkVOWUVTU0VHX1ZFR0U+PC9pdGVtPjxpdGVtPjxJRD4wPC9JRD48T1ZUSl9LT0Q+MDExMTAxPC9PVlRKX0tPRD48RVJWRU5ZRVNTRUdfS0VaREVURT4yMDE1LTEyLTIxPC9FUlZFTllFU1NFR19LRVpERVRFPjxFUlZFTllFU1NFR19WRUdFPjIwMTktMTItMzE8L0VSVkVOWUVTU0VHX1ZFR0U+PC9pdGVtPjwvT1ZUSl9LT0RPSz48TllJTEFUS09aQVRfQklaVE9TSVRPVFQ+TzwvTllJTEFUS09aQVRfQklaVE9TSVRPVFQ+PE5ZSUxBVEtPWkFUX1NBSkFUX0dBWkRBU0FHPkk8L05ZSUxBVEtPWkFUX1NBSkFUX0dBWkRBU0FHPjwvREFUQT4=</tem:payload>
         <!--Optional:-->
         <tem:key>aaaassdfghjkloiuztrtwfayagvbnmj3</tem:key>
      </tem:Encrypt>
   </soapenv:Body>
</soapenv:Envelope>";
            string key = "aaaassdfghjkloiuztrtwfayagvbnmj3";
            string encryptedPayload = enc.Encrypt(clearText, key);

            string clearTextAgain = enc.Decrypt(encryptedPayload, key);

            Assert.AreEqual(clearText, clearTextAgain);
        }

        [TestMethod]
        public void AES128EncryptorTest2()
        {
            AES128Encryptor enc = new AES128Encryptor();
            string clearText = "SHcAJYM7jJVjawLnPoeWXIQhIIiJyZFXjDu5f9HRqwljEB1p5YoezQc3kUiVxrIBRuQnhJ8cu3ZAoztaSnMuvZ1BaZMTODZlVokWtO0bYxsAHQa6gROW1f36elEMJepu7ZcegfM9ai2cRGSS1CA1tnFi0qBKheDZqg+m4Ch/J37gXf5xW1dw6fRlPztgL41D07YQGvYm+2ETbW93FTRYRPUjnt2gf4yvddxAcpuRc9UOqe2Ck5DaDXzucfpjrLRr8c2yOa9zAXQv5cZZeX9n+2wip4yN50XGRVfZhSAVkoqbkLOd30dDq4gLmay9Zn0to6rcNgWAKGzT+IxgQHu9Sg5UmSV/dbX0o4g74DPHspvK0si0W94RQfg4VH+tR72g3P41nlbiervlJ1chzNZxvuR/R4HP/nuT13U3tS+0ZqdY9UqLtKmkrXvgKyjwSNB2E9ZHwAnOGwbzMn9EpnRa8ZZjrVRe9I4OywjxzlKpUT2WdRv3WlAvP2/VKGIVp02G8RHwIZRfdL5IX32tZBeSsiS2rJmDO1PaomYR1o2YOSl7ADw6lJAed8X142hADDUIuL6ugUf4XzJl1ZCHNmtYrQ3Z4qwGPTtNbmMBMiSRcEGp01vJvzUIXDRBi7y3EfMWddXon+3SR33Ipwce7PE8PGvqXIJhP/TSG1ndDxjhLqywqIaTqM+mBRmEg9U/oy6JpXO8leO9Tkm/ujayycPKgWTH5714iDkTumwgaf8t37BpbVwP9Rzn6jZPVp9b1+BoRZtpHGsyPFPWzhHYd3T2GsLESyrRKFLrzl8dWKqTx4LfS2dtEAqxUSMyL4UJF0yLl1OhxzE7bsIctPS8qjCH8fF1TxiyhdfXHzrogpvjIjpgRaX3QyyVvcZ7u402jkwvCJk8eyK8Jb+8bWV0WQglTIpP0NraeMkDThSQCvOfPS+X4AB0rsSJtyxGBLZN1aUwDMUeJqRB6gT89V9YKLM3hNex43/OhESBVNCrt8CtbbUnxm6FSp1H0Uh++vktF75z8kf86v/pZJ3XBddIeitLFn0dhSR8bZX7J7sHiUJ8iW0jvEHKy1n00abo/1ZvXsvKe3oSaLpbznukpoS+tiP8ZD1/XAYMH4HcbHEsc+2FpeigY+OAVdAAHaIAgou89GMCh+lZuTPtTexxt/PfBHCi1RDvTajNBYhtuCX9KJ2Vt07DLfjhLgCGXEIz/EVHKgqDT8FpNmiJlDcTYY7HcErNocC3nD4reXmcJeXR7tYTDyakNHvI19s5c/l5RG/5JDM3A9nXHBpm5yjz53bAF/KJooD7JvoqOja7Pig52DqB9kJzjrRCxobpAxymL+CLU64w";
            string key = "12341234123412341234123412341234";
            string encryptedPayload = enc.Encrypt(clearText, key);

            string clearTextAgain = enc.Decrypt(encryptedPayload, key);

            Assert.AreEqual(clearText, clearTextAgain);
        }

        [TestMethod]
        public void AES128EncryptorTest3()
        {
            AES128Encryptor enc = new AES128Encryptor();
            string clearText = "aaaa  bbbb";
            string key = "aaaassdfghjkloiuztrtwfayagvbnmj3";
            string encryptedPayload = enc.Encrypt(clearText, key);

            string clearTextAgain = enc.Decrypt(encryptedPayload, key);

            Assert.AreEqual(clearText, clearTextAgain);
        }

    }
}
