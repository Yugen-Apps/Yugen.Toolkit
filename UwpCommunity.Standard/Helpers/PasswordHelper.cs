using System;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using System.Security.Cryptography;
using System.Text;

namespace UwpCommunity.Standard.Helpers
{
    public class PasswordHelper
    {
        private static readonly ASCIIEncoding Encoding = new ASCIIEncoding();
        public static byte[] Salt = Encoding.GetBytes("salt");

        public static string SaltString => Convert.ToBase64String(Salt);

        public static string HashPassword(string password)
        {
            if (string.IsNullOrEmpty(password))
                return "";

            // generate a 128-bit salt using a secure PRNG
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(Salt);
            }

            return HashPassword(password, Salt);
        }

        public static string HashPassword(string password, string salt) => 
            HashPassword(password, Convert.FromBase64String(salt));

        public static string HashPassword(string password, byte[] salt)
        {
            if (string.IsNullOrEmpty(password))
                return "";

            Salt = salt;

            // derive a 256-bit subkey (use HMACSHA1 with 10,000 iterations)
            string hashed = Convert.ToBase64String(KeyDerivation.Pbkdf2(
                password: password,
                salt: salt,
                prf: KeyDerivationPrf.HMACSHA1,
                iterationCount: 10000,
                numBytesRequested: 256 / 8));

            return hashed;
        }
    }
}