using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;
using System.IO;

namespace SapSoapCardWriter.Common.Encryption
{
    public class AES128Encryptor : IEncryptor
    {
        public byte[] EncryptData(byte[] bytearraytoencrypt, string key, string iv)
        {
            AesCryptoServiceProvider dataencrypt = new AesCryptoServiceProvider();
            //Block size : Gets or sets the block size, in bits, of the cryptographic operation.  
            dataencrypt.BlockSize = 128;
            //KeySize: Gets or sets the size, in bits, of the secret key  
            dataencrypt.KeySize = 128;
            //Key: Gets or sets the symmetric key that is used for encryption and decryption.  
            dataencrypt.Key = System.Text.Encoding.UTF8.GetBytes(key);
            //IV : Gets or sets the initialization vector (IV) for the symmetric algorithm  
            dataencrypt.IV = System.Text.Encoding.UTF8.GetBytes(iv);
            //Padding: Gets or sets the padding mode used in the symmetric algorithm  
            dataencrypt.Padding = PaddingMode.PKCS7;
            //Mode: Gets or sets the mode for operation of the symmetric algorithm  
            dataencrypt.Mode = CipherMode.CBC;
            //Creates a symmetric AES encryptor object using the current key and initialization vector (IV).  
            ICryptoTransform crypto1 = dataencrypt.CreateEncryptor(dataencrypt.Key, dataencrypt.IV);
            //TransformFinalBlock is a special function for transforming the last block or a partial block in the stream.   
            //It returns a new array that contains the remaining transformed bytes. A new array is returned, because the amount of   
            //information returned at the end might be larger than a single block when padding is added.  
            byte[] encrypteddata = crypto1.TransformFinalBlock(bytearraytoencrypt, 0, bytearraytoencrypt.Length);
            crypto1.Dispose();
            //return the encrypted data  
            return encrypteddata;
        }

        private byte[] DecryptData(byte[] bytearraytodecrypt, string key, string iv)
        {

            AesCryptoServiceProvider keydecrypt = new AesCryptoServiceProvider();
            keydecrypt.BlockSize = 128;
            keydecrypt.KeySize = 128;
            keydecrypt.Key = System.Text.Encoding.UTF8.GetBytes(key);
            keydecrypt.IV = System.Text.Encoding.UTF8.GetBytes(iv);
            keydecrypt.Padding = PaddingMode.PKCS7;
            keydecrypt.Mode = CipherMode.CBC;
            ICryptoTransform crypto1 = keydecrypt.CreateDecryptor(keydecrypt.Key, keydecrypt.IV);

            byte[] returnbytearray = crypto1.TransformFinalBlock(bytearraytodecrypt, 0, bytearraytodecrypt.Length);
            crypto1.Dispose();
            return returnbytearray;
        }

        private byte[] GetBytes(string str)
        {
            byte[] bytes = new byte[str.Length * sizeof(char)];
            System.Buffer.BlockCopy(str.ToCharArray(), 0, bytes, 0, bytes.Length);
            return bytes;
        }

        private string GetString(byte[] bytes)
        {
            char[] chars = new char[bytes.Length / sizeof(char)];
            System.Buffer.BlockCopy(bytes, 0, chars, 0, bytes.Length);
            return new string(chars);
        }

        public string Decrypt(string cipherText, string key)
        {
            byte[] ba = GetBytes(cipherText);
            byte[] a = DecryptData(ba, key, key.Substring(0, 16));
            string r = GetString(a);
            return r;
        }

        public string Encrypt(string clearText, string key)
        {
            byte[] ba = GetBytes(clearText);
            byte[] a = EncryptData(ba, key, key.Substring(0, 16));
            string r = GetString(a);
            return r;
        }
    }
}
