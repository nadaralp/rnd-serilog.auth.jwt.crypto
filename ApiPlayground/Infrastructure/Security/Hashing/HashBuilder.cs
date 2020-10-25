using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace ApiPlayground.Infrastructure.Security.Hashing
{
    public class HashBuilder
    {
        /// <summary>
        /// Hashing works in the following way:
        /// </summary>
        public string Sha256(string message)
        {
            var sha256Algorithm = new HMACSHA256();
            return PerformHashing(sha256Algorithm, message);
        }


        public string Sha256(string message, string key)
        {

            byte[] keyBuffer = Encoding.ASCII.GetBytes(key);
            using (var sha256Algorithm = new HMACSHA256(keyBuffer))
            {
                return PerformHashing(sha256Algorithm, message);
            }
        }


        private string PerformHashing(HashAlgorithm algorithm, string message)
        {
            byte[] messageBuffer = Encoding.ASCII.GetBytes(message);
            byte[] hashedMessage = algorithm.ComputeHash(messageBuffer);

            var hexString = BitConverter.ToString(hashedMessage).Replace("-", "");
            return hexString.ToLower();
        }
    }

    public static class ByteExtensionHelper
    {
        public static string AsBase64String(this byte[] buffer)
        {
            return Convert.ToBase64String(buffer);
        }

        public static string AsHexString(this byte[] buffer)
        {
            return BitConverter.ToString(buffer).Replace("-", "");
        }
    }
}
