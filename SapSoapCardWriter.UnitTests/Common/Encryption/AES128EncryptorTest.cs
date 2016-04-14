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
            string key = "12341234123412341234123412341234";
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
            string clearText = "SHcAJYM7jJVjawLnPoeWXIQhIIiJyZFXjDu5f9HRqwljEB1p5YoezQc3kUiVxrIBRuQnhJ8cu3ZAoztaSnMuvZ1BaZMTODZlVokWtO0bYxsAHQa6gROW1f36elEMJepu7ZcegfM9ai2cRGSS1CA1tnFi0qBKheDZqg+m4Ch/J37gXf5xW1dw6fRlPztgL41D07YQGvYm+2ETbW93FTRYRPUjnt2gf4yvddxAcpuRc9UOqe2Ck5DaDXzucfpjrLRr8c2yOa9zAXQv5cZZeX9n+2wip4yN50XGRVfZhSAVkoqbkLOd30dDq4gLmay9Zn0to6rcNgWAKGzT+IxgQHu9Sg5UmSV/dbX0o4g74DPHspvK0si0W94RQfg4VH+tR72g3P41nlbiervlJ1chzNZxvuR/R4HP/nuT13U3tS+0ZqdY9UqLtKmkrXvgKyjwSNB2E9ZHwAnOGwbzMn9EpnRa8ZZjrVRe9I4OywjxzlKpUT2WdRv3WlAvP2/VKGIVp02G8RHwIZRfdL5IX32tZBeSsiS2rJmDO1PaomYR1o2YOSl7ADw6lJAed8X142hADDUIuL6ugUf4XzJl1ZCHNmtYrQ3Z4qwGPTtNbmMBMiSRcEGp01vJvzUIXDRBi7y3EfMWddXon+3SR33Ipwce7PE8PGvqXIJhP/TSG1ndDxjhLqywqIaTqM+mBRmEg9U/oy6JpXO8leO9Tkm/ujayycPKgWTH5714iDkTumwgaf8t37BpbVwP9Rzn6jZPVp9b1+BoRZtpHGsyPFPWzhHYd3T2GsLESyrRKFLrzl8dWKqTx4LfS2dtEAqxUSMyL4UJF0yLl1OhxzE7bsIctPS8qjCH8fF1TxiyhdfXHzrogpvjIjpgRaX3QyyVvcZ7u402jkwvCJk8eyK8Jb+8bWV0WQglTIpP0NraeMkDThSQCvOfPS+X4AB0rsSJtyxGBLZN1aUwDMUeJqRB6gT89V9YKLM3hNex43/OhESBVNCrt8CtbbUnxm6FSp1H0Uh++vktF75z8kf86v/pZJ3XBddIeitLFn0dhSR8bZX7J7sHiUJ8iW0jvEHKy1n00abo/1ZvXsvKe3oSaLpbznukpoS+tiP8ZD1/XAYMH4HcbHEsc+2FpeigY+OAVdAAHaIAgou89GMCh+lZuTPtTexxt/PfBHCi1RDvTajNBYhtuCX9KJ2Vt07DLfjhLgCGXEIz/EVHKgqDT8FpNmiJlDcTYY7HcErNocC3nD4reXmcJeXR7tYTDyakNHvI19s5c/l5RG/5JDM3A9nXHBpm5yjz53bAF/KJooD7JvoqOja7Pig52DqB9kJzjrRCxobpAxymL+CLU64w";
            string key = "12341234123412341234123412341234";
            string encryptedPayload = enc.Encrypt(clearText, key);

            string clearTextAgain = enc.Decrypt(encryptedPayload, key);

            Assert.AreEqual(clearText, clearTextAgain);
        }

    }
}
