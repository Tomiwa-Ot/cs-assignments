using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace BankApplication
{
    /// <summary>
    /// Utility functions required by the application
    /// </summary>
    public class Util
    {
        /// <summary>
        /// Generates a bank account for a new customer
        /// </summary>
        /// <returns>The generated account number</returns>
        public static string GenerateAccountNumber()
        {
            string bankIdentifier = "061";
            StringBuilder stringBuilder = new StringBuilder(10);
            stringBuilder.Append(bankIdentifier);
            Random random = new Random();

            for (int i = 0; i < 7; i++)
            {
                int randomNumber = random.Next(0, 10);
                stringBuilder.Append(randomNumber);
            }
            return stringBuilder.ToString();
        }

        /// <summary>
        /// Generate a PBKDF2 hash
        /// </summary>
        /// <param name="text">The string to convert into a PBKDF2 hash</param>
        /// <returns>The generated PBKDF2 hash</returns>
        public static string HashString(string text)
        {
            int iterations = 10000;  // Number of iteraitons for PBKDF2
            int hashBytesSize = 20; // Hash size for PBKDF2 (in bytes)

            byte[] salt = new byte[] { 0xA, 0xF, 0x13, 0x1, 0xE3, 0x1, 0x32, 0x11 };

            // Generate hash using PBKDF2 algorithm
            var pbkdf2 = new Rfc2898DeriveBytes(text, salt, iterations, HashAlgorithmName.SHA512);
            byte[] hash = pbkdf2.GetBytes(hashBytesSize);

            // Convert hash and salt to base64-encoded string
            string hashString = Convert.ToBase64String(hash);
            string saltString = Convert.ToBase64String(salt);

            // Combine hash and salt strings with a separator
            return $"{hashString}:{saltString}";
        }

        /// <summary>
        /// Generates PBKDF2 hash of text and compares with hashedText to see if they match
        /// </summary>
        /// <param name="text">The string to convert into a PBKDF2 hash</param>
        /// <param name="hashedText">The hash to be compared with</param>
        /// <returns><c>true</c> if hashes match, else <c>false</c></returns>
        public static bool VerifyHash(string text, string hashedText)
        {
            // Split the hash and salt strings
            string[] parts = hashedText.Split(':');
            byte[] hash = Convert.FromBase64String(parts[0]);
            byte[] salt = Convert.FromBase64String(parts[1]);

            int iterations = 10000;  // Number of iteraitons for PBKDF2

            // Generate hash using the same parameters as the original hash
            var pbkdf2 = new Rfc2898DeriveBytes(text, salt, iterations, HashAlgorithmName.SHA512);
            byte[] newHash = pbkdf2.GetBytes(hash.Length);

            // Compare the two hashes byte by byte
            for (int i = 0; i < hash.Length; i++)
            {
                if (hash[i] != newHash[i])
                {
                    return false;
                }
            }
            return true;
        }
    }
}
