using SAP_API.Options;
using System;
using System.Security.Cryptography;

namespace SAP_API.Utils
{
    public class Hasher: IHasher
    {

        private readonly EncriptionOptions _encryptionOptions;

        public Hasher(EncriptionOptions encryptionOptions)
        {
            _encryptionOptions = encryptionOptions; 
        }

        public string Hash(string stringToHash)
        {
            using (var algorithm = new Rfc2898DeriveBytes(
              stringToHash,
              _encryptionOptions.SaltSize,
              _encryptionOptions.Iterations,
              HashAlgorithmName.SHA512))
            {
                var key = Convert.ToBase64String(algorithm.GetBytes(_encryptionOptions.KeySize));
                var salt = Convert.ToBase64String(algorithm.Salt);
                var iterations = _encryptionOptions.Iterations;

                return $"{iterations}.{salt}.{key}";
            }
        }

        public (bool Verified, bool NeedsUpgrade) Check(string hash, string stringToHash)
        {
            var parts = hash.Split('.', 3);

            if (parts.Length != 3)
            {
                throw new FormatException("Unexpected hash format. " +
                  "Should be formatted as `{iterations}.{salt}.{hash}`");
            }

            var iterations = Convert.ToInt32(parts[0]);
            var salt = Convert.FromBase64String(parts[1]);
            var key = (ReadOnlySpan<byte>)Convert.FromBase64String(parts[2]);

            var needsUpgrade = iterations != _encryptionOptions.Iterations;

            using (var algorithm = new Rfc2898DeriveBytes(
              stringToHash,
              salt,
              iterations,
              HashAlgorithmName.SHA512))
            {
                ReadOnlySpan<byte> keyToCheck = algorithm.GetBytes(_encryptionOptions.KeySize);

                var verified = keyToCheck.SequenceEqual(key);

                return (verified, needsUpgrade);
            }
        }

    }
}
