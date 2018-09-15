using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;

namespace VogtEventsEmp
{
    class SHA512Crypto
    {
        
        #region SHA512
        /// <summary>
        /// A method that takes a string and returns a hash
        /// </summary>
        /// <param name="password">A string to pass in</param>
        /// <returns>A hashed string by SHA256</returns>
        public static string Hash(byte[] password)
        {
            // Variables
            string hashedPassword = string.Empty;
            SHA512CryptoServiceProvider sha512 = new SHA512CryptoServiceProvider();

            // SHA512
            hashedPassword = Convert.ToBase64String(sha512.ComputeHash(password));

            return hashedPassword;

        }
        #endregion

    }
}
