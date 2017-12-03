using System;
using System.Security.Cryptography;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;

namespace Engine.Extensions
{
    public static class StringExtensions
    {
        public static string Hash(this string self, ref string base64Salt)
        {
            byte[] salt;

            if (base64Salt == null)
            {
                salt = new byte[128 / 8];
                using (var rng = RandomNumberGenerator.Create())
                {
                    rng.GetBytes(salt);
                }

                base64Salt = Convert.ToBase64String(salt);
            }
            else
            {
                salt = Convert.FromBase64String(base64Salt);
            }

            // derive a 256-bit subkey (use HMACSHA1 with 10,000 iterations)
            return Convert.ToBase64String(KeyDerivation.Pbkdf2(
                password: self,
                salt: salt,
                prf: KeyDerivationPrf.HMACSHA1,
                iterationCount: 10000,
                numBytesRequested: 256 / 8));
        }
    }
}