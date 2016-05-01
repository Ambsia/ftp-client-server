using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace FTPClient.util
{
    public static class Cryptography
    {
        public static string Sha1(string unHashedString)
        {
            SHA1 sha1 = new SHA1CryptoServiceProvider();
            byte[] originalBytes = ASCIIEncoding.Default.GetBytes(unHashedString);

            byte[] encodedBytes = sha1.ComputeHash(originalBytes);

            return Regex.Replace(BitConverter.ToString(encodedBytes), "-", "").ToLower();
        }
    }
}
