using System;
using System.Security.Cryptography;
using System.Text;

namespace OneB
{
    public class Utils
    {
        public static string GetSHA256Hash(string input, int numChars = 8)
        {
            using (SHA256 sha256 = SHA256.Create())
            {
                byte[] hashBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(input));
                StringBuilder builder = new StringBuilder();
                for (int i = 0; i < hashBytes.Length; i++)
                {
                    builder.Append(hashBytes[i].ToString("x2"));
                }
                return builder.ToString().Substring(0, numChars);
            }
        }
    }
}