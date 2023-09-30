using Konscious.Security.Cryptography;
using System.Security.Cryptography;
using System.Text;

namespace Common.Extensions
{
    public static class StringExtensions
    {
        private const int SaltSize = 16;
        private const int HashSize = 32;
        public static string HashPassword(this string password)
        {
            byte[] salt = new byte[SaltSize];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(salt);
            }

            // Hash the password using Argon2id
            var argon2 = new Argon2id(Encoding.UTF8.GetBytes(password))
            {
                Salt = salt,
                DegreeOfParallelism = 1,
                Iterations = 4,
                MemorySize = 1024 * 1024
            };
            byte[] hash = argon2.GetBytes(HashSize);

            // Combine the salt and hash into a single string
            var saltBase64 = Convert.ToBase64String(salt);
            var hashBase64 = Convert.ToBase64String(hash);
            return $"{saltBase64}:{hashBase64}";
        }

        public static bool VerifyHash(this string hashedPassword, string password)
        {
            try
            {
                // Extract the salt and hash from the hashed password string
                var parts = hashedPassword.Split(':');
                var salt = Convert.FromBase64String(parts[0]);
                var expectedHash = Convert.FromBase64String(parts[1]);

                // Hash the password using the same salt and parameters
                var argon2 = new Argon2id(Encoding.UTF8.GetBytes(password))
                {
                    Salt = salt,
                    DegreeOfParallelism = 1,
                    Iterations = 4,
                    MemorySize = 1024 * 1024
                };
                byte[] hash = argon2.GetBytes(HashSize);

                // Compare the hashes
                return hash.SequenceEqual(expectedHash);
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}