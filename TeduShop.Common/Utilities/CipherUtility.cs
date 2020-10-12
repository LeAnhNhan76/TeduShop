using System;
using System.Security.Cryptography;
using System.Text;

namespace TeduShop.Common.Utilities
{
    public static class CipherUtility
    {
        public static string EncryptToMD5(string sample)
        {
            string result = "";
            byte[] buffer = Encoding.UTF8.GetBytes(sample);
            MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider();
            buffer = md5.ComputeHash(buffer);
            for (int i = 0; i < buffer.Length; i++)
            {
                result += buffer[i].ToString("x2");
            }
            return result;
        }

        public static string GetHash(string input)
        {
            HashAlgorithm hashAlgorithm = new SHA256CryptoServiceProvider();
            byte[] byteValue = System.Text.Encoding.UTF8.GetBytes(input);
            byte[] byteHash = hashAlgorithm.ComputeHash(byteValue);
            return Convert.ToBase64String(byteHash);
        }
    }
}