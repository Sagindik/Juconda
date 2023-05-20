using Juconda.Domain.Models.Users;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using System.Security.Cryptography;
using System.Text;

namespace Juconda.Core.Common
{
    public class CustomPasswordHasher : IPasswordHasher<User>
	{
		private readonly int _iterCount;
		private readonly RandomNumberGenerator _rng;

		public CustomPasswordHasher(IOptions<PasswordHasherOptions> optionsAccessor = null)
		{
			var options = optionsAccessor?.Value ?? new PasswordHasherOptions();

			_iterCount = options.IterationCount;
			if (_iterCount < 1) throw new InvalidOperationException("Invalid Password Hasher Iteration Count");
			_rng = RandomNumberGenerator.Create();
		}

		public string HashPassword(User user, string password)
		{
			if (password == null)
				throw new ArgumentNullException(nameof(password));

			return Convert.ToBase64String(HashPasswordV3(password, _rng));
		}

		public PasswordVerificationResult VerifyHashedPassword(User user, string hashedPassword, string providedPassword)
		{
			if (hashedPassword == null)
			{
				throw new ArgumentNullException(nameof(hashedPassword));
			}
			if (providedPassword == null)
			{
				throw new ArgumentNullException(nameof(providedPassword));
			}

			#region Old logic for passwords

			var hash = MD5.Create().ComputeHash(Encoding.UTF8.GetBytes(providedPassword));

			var s = new StringBuilder();
			foreach (var b in hash)
				s.Append(b.ToString("X2").ToLower());

			if (hashedPassword.Equals(s.ToString()))
				return PasswordVerificationResult.SuccessRehashNeeded;

			#endregion

			#region New logic for passwords

			byte[] decodedHashedPassword = Convert.FromBase64String(hashedPassword);

			// read the format marker from the hashed password
			if (decodedHashedPassword.Length == 0)
				return PasswordVerificationResult.Failed;

			if (decodedHashedPassword[0] != 0x01)
				return PasswordVerificationResult.Failed; // unknown format marker

			if (VerifyHashedPasswordV3(decodedHashedPassword, providedPassword, out int embeddedIterCount))
			{
				// If this hasher was configured with a higher iteration count, change the entry now.
				return embeddedIterCount < _iterCount
					? PasswordVerificationResult.SuccessRehashNeeded
					: PasswordVerificationResult.Success;
			}

			#endregion

			return PasswordVerificationResult.Failed;
		}

		private byte[] HashPasswordV3(string password, RandomNumberGenerator rng)
		{
			KeyDerivationPrf prf = KeyDerivationPrf.HMACSHA256;
			const int saltSize = 128 / 8;
			const int numBytesRequested = 256 / 8;

			var salt = new byte[saltSize];
			rng.GetBytes(salt);
			var subkey = KeyDerivation.Pbkdf2(password, salt, prf, _iterCount, numBytesRequested);

			var outputBytes = new byte[13 + salt.Length + subkey.Length];
			outputBytes[0] = 0x01; // format marker
			WriteNetworkByteOrder(outputBytes, 1, (uint)prf);
			WriteNetworkByteOrder(outputBytes, 5, (uint)_iterCount);
			WriteNetworkByteOrder(outputBytes, 9, saltSize);
			Buffer.BlockCopy(salt, 0, outputBytes, 13, salt.Length);
			Buffer.BlockCopy(subkey, 0, outputBytes, 13 + saltSize, subkey.Length);
			return outputBytes;
		}
		private static bool VerifyHashedPasswordV3(byte[] hashedPassword, string password, out int iterCount)
		{
			iterCount = default;

			try
			{
				// Read header information
				KeyDerivationPrf prf = (KeyDerivationPrf)ReadNetworkByteOrder(hashedPassword, 1);
				iterCount = (int)ReadNetworkByteOrder(hashedPassword, 5);
				int saltLength = (int)ReadNetworkByteOrder(hashedPassword, 9);

				// Read the salt: must be >= 128 bits
				if (saltLength < 128 / 8)
				{
					return false;
				}
				byte[] salt = new byte[saltLength];
				Buffer.BlockCopy(hashedPassword, 13, salt, 0, salt.Length);

				// Read the subkey (the rest of the payload): must be >= 128 bits
				int subkeyLength = hashedPassword.Length - 13 - salt.Length;
				if (subkeyLength < 128 / 8)
				{
					return false;
				}
				byte[] expectedSubkey = new byte[subkeyLength];
				Buffer.BlockCopy(hashedPassword, 13 + salt.Length, expectedSubkey, 0, expectedSubkey.Length);

				// Hash the incoming password and verify it
				byte[] actualSubkey = KeyDerivation.Pbkdf2(password, salt, prf, iterCount, subkeyLength);
#if NETSTANDARD2_0 || NET461
                return ByteArraysEqual(actualSubkey, expectedSubkey);
#elif NETCOREAPP
				return CryptographicOperations.FixedTimeEquals(actualSubkey, expectedSubkey);
#else
#error Update target frameworks
#endif
			}
			catch
			{
				// This should never occur except in the case of a malformed payload, where
				// we might go off the end of the array. Regardless, a malformed payload
				// implies verification failed.
				return false;
			}
		}

		private static uint ReadNetworkByteOrder(byte[] buffer, int offset)
		{
			return (uint)buffer[offset + 0] << 24
				   | (uint)buffer[offset + 1] << 16
				   | (uint)buffer[offset + 2] << 8
				   | buffer[offset + 3];
		}
		private static void WriteNetworkByteOrder(byte[] buffer, int offset, uint value)
		{
			buffer[offset + 0] = (byte)(value >> 24);
			buffer[offset + 1] = (byte)(value >> 16);
			buffer[offset + 2] = (byte)(value >> 8);
			buffer[offset + 3] = (byte)(value >> 0);
		}

	}
}
