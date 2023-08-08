using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;

namespace TicketManagementSystem.Models
{

    public static class PasswordHelper
    {
        public static string GenerateSalt()
        {
            byte[] saltBytes = new byte[16];
            using (var rngCsp = new RNGCryptoServiceProvider())
            { rngCsp.GetNonZeroBytes(saltBytes); }
            return Convert.ToBase64String(saltBytes);
        }
        public static string GeneratePasswordHash(string password, string salt)
        {
            using (var sha256 = SHA256.Create())
            {
                if (password == null)
                    throw new ArgumentNullException(nameof(password), "Password cannot be null.");

                if (salt == null)
                    throw new ArgumentNullException(nameof(salt), "Salt cannot be null.");


                byte[] saltBytes = Convert.FromBase64String(salt);
                byte[] passwordBytes = Encoding.UTF8.GetBytes(password);
                byte[] combinedBytes = new byte[saltBytes.Length + passwordBytes.Length];
                Buffer.BlockCopy(saltBytes, 0, combinedBytes, 0, saltBytes.Length);
                Buffer.BlockCopy(passwordBytes, 0, combinedBytes, saltBytes.Length, passwordBytes.Length);
                byte[] hashBytes = sha256.ComputeHash(combinedBytes); return Convert.ToBase64String(hashBytes);


            }

        }
    }
}