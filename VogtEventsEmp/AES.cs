using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;
using System.IO;

namespace VogtEventsEmp
{
    class AESC
    {
        #region EncryptByAESStandard
        /// <summary>
        /// Method that takes a string to be encrypted by AES
        /// </summary>
        /// <param name="str">String to pass in to be encrypted</param>
        /// <param name="Key">Key for the data</param>
        /// <param name="IV">Initialization Vector for the data</param>
        /// <returns></returns>
        public static byte[] EncryptByAES(string str, byte[] Key, byte[] IV)
        {
            // AES Object
            Aes aes = Aes.Create();

            MemoryStream ms = new MemoryStream();
            CryptoStream cs = new CryptoStream(ms, aes.CreateEncryptor(Key, IV), CryptoStreamMode.Write);

            Byte[] toEncrypt = new ASCIIEncoding().GetBytes(str);

            cs.Write(toEncrypt, 0, toEncrypt.Length);
            cs.FlushFinalBlock();

            byte[] encrypted = ms.ToArray();

            ms.Close();
            cs.Close();

            return encrypted;

        }
        #endregion

        #region DecryptByAES standard
        /// <summary>
        /// Method that decrypts AES
        /// </summary>
        /// <param name="Data">Byte Array to pass in the encrypted data</param>
        /// <param name="Key">Key for the data</param>
        /// <param name="IV">Initialization Vector for the data</param>
        /// <returns></returns>
        public static string DecryptByAES(byte[] Data, byte[] Key, byte[] IV)
        {
            // AES Object
            Aes aes = Aes.Create();

            MemoryStream ms = new MemoryStream(Data);
            CryptoStream cs = new CryptoStream(ms, aes.CreateDecryptor(Key, IV), CryptoStreamMode.Read);

            byte[] decrypted = new byte[Data.Length];

            cs.Read(decrypted, 0, decrypted.Length);

            return new ASCIIEncoding().GetString(decrypted);

        }
        #endregion
    }
}
