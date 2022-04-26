using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace Restaurant.Core.Utils
{
    public static class AccountUtils
    {
        public static string Hash(string input, string alg)
        {
            HashAlgorithm algorithm = HashAlgorithm.Create(alg);

            byte[] inputBytes = Encoding.UTF8.GetBytes(input);
            byte[] outputBytes = algorithm.ComputeHash(inputBytes);

            return Convert.ToBase64String(outputBytes);
        }
    }
}