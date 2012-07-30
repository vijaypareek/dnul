using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Security.Cryptography;
using System.IO;

using DotNetUtilityLibrary.Cryptography;
using System.Security.Cryptography.X509Certificates;

namespace DotNetUtilityLibrary
{
	/// <summary>
	/// Wraps symmetric and asymmetric encryption calls, handling the bookkeeping
	/// and type conversions.
	/// </summary>
	public static class CryptographyHelper
	{
		#region Symmetric Encryption

		/// <summary>
		/// Returns an string representation of the input string using the
		/// provided key, iv, and specified symmetric encryption algorithm.
		/// </summary>
		public static string SymmetricEncrypt<T>(string data, byte[] key,
			byte[] iv) where T : SymmetricAlgorithm
		{
			ValidateParameters(data, key, iv);
			SymmetricAlgorithm algorithm = Activator.CreateInstance<T>();
			SymmetricEncryptor encryptor = 
				new SymmetricEncryptor(algorithm, data, key, iv);
			return encryptor.GetEncryptedString();
		}

		/// <summary>
		/// Returns an byte array representation of the byte array using the
		/// provided key, iv, and specified symmetric encryption algorithm.
		/// </summary>
		public static byte[] SymmetricEncrypt<T>(byte[] data, byte[] key,
			byte[] iv) where T : SymmetricAlgorithm
		{
			ValidateParameters(data, key, iv);
			SymmetricAlgorithm algorithm = Activator.CreateInstance<T>();
			SymmetricEncryptor encryptor =
				new SymmetricEncryptor(algorithm, data, key, iv);
			return encryptor.GetEncryptedBytes();
		}

		#region Encrypt Abbreviated Calls

		public static string SymmetricEncrypt(SymmetricAlgorithm algorithm,
			string data)
		{
			ValidateParameters(algorithm, data);
			SymmetricEncryptor encryptor =
				new SymmetricEncryptor(algorithm, data);
			return encryptor.GetEncryptedString();
		}

		public static byte[] SymmetricEncrypt(SymmetricAlgorithm algorithm,
			byte[] data)
		{
			ValidateParameters(algorithm, data);
			SymmetricEncryptor encryptor =
				new SymmetricEncryptor(algorithm, data);
			return encryptor.GetEncryptedBytes();
		}

		public static byte[] SymmetricEncrypt(byte[] data, byte[] key, byte[] iv)
		{
			ValidateParameters(data, key, iv);
			return SymmetricEncrypt<AesCryptoServiceProvider>(data, key, iv);
		}

		public static string SymmetricEncrypt(string data, byte[] key, byte[] iv)
		{
			ValidateParameters(data, key, iv);
			return SymmetricEncrypt<AesCryptoServiceProvider>(data, key, iv);
		}

		#endregion Encrypt Abbreviated Calls

		#endregion Symmetric Encryption

		#region Symmetric Decryption

		/// <summary>
		/// Returns the original string given the encrypted form and provided key
		/// and iv, and specified symmetric encrpytion algorithm.
		/// </summary>
		public static string SymmetricDecrypt<T>(string data, byte[] key,
			byte[] iv) where T : SymmetricAlgorithm
		{
			ValidateParameters(data, key, iv);
			SymmetricAlgorithm algorithm = Activator.CreateInstance<T>();
			SymmetricDecryptor decryptor = 
				new SymmetricDecryptor(algorithm, data, key, iv);
			return decryptor.GetDecryptedString();
		}

		/// <summary>
		/// Returns the original byte array given the encrypted form and provided
		/// key and iv, and specified symmetric encrpytion algorithm.
		/// </summary>
		public static byte[] SymmetricDecrypt<T>(byte[] data, byte[] key,
			byte[] iv) where T : SymmetricAlgorithm
		{
			ValidateParameters(data, key, iv);
			SymmetricAlgorithm algorithm = Activator.CreateInstance<T>();
			SymmetricDecryptor decryptor =
				new SymmetricDecryptor(algorithm, data, key, iv);
			return decryptor.GetDecryptedBytes();
		}

		#region Decrypt Abbreviated Calls

		public static string SymmetricDecrypt(SymmetricAlgorithm algorithm,
			string data)
		{
			ValidateParameters(algorithm, data);
			SymmetricDecryptor decryptor =
				new SymmetricDecryptor(algorithm, data);
			return decryptor.GetDecryptedString();
		}

		public static byte[] SymmetricDecrypt(SymmetricAlgorithm algorithm,
			byte[] data)
		{
			ValidateParameters(algorithm, data);
			SymmetricDecryptor decryptor =
				new SymmetricDecryptor(algorithm, data);
			return decryptor.GetDecryptedBytes();
		}

		public static byte[] SymmetricDecrypt(byte[] data, byte[] key, byte[] iv)
		{
			ValidateParameters(data, key, iv);
			return SymmetricDecrypt<RijndaelManaged>(data, key, iv);
		}

		public static string SymmetricDecrypt(string data, byte[] key, byte[] iv)
		{
			ValidateParameters(data, key, iv);
			return SymmetricDecrypt<RijndaelManaged>(data, key, iv);
		}
		
		#endregion Decrypt Abbreviated Calls

		#endregion Symmetric Decryption

		#region Asymmetric Encryption

		/// <summary>
		/// Returns an encrypted string given an RSACryptoServiceProvider and a
		/// plaintext string.
		/// </summary>
		public static string AsymmetricEncrypt(RSACryptoServiceProvider rsa,
			string data)
		{
			ValidateParameters(rsa, data);
			AsymmetricEncryptor encryptor = new AsymmetricEncryptor(rsa, data);
			return encryptor.GetEncryptedString();
		}

		/// <summary>
		/// Returns an encrypted byte array given an RSACryptoServiceProvider and
		/// an unencrypted byte array.
		/// </summary>
		public static byte[] AsymmetricEncrypt(RSACryptoServiceProvider rsa,
			byte[] data)
		{
			ValidateParameters(rsa, data);
			AsymmetricEncryptor encryptor = new AsymmetricEncryptor(rsa, data);
			return encryptor.GetEncryptedBytes();
		}

		#region AsymmetricEncrypt Alternate Calls

		public static string AsymmetricEncrypt(X509Certificate2 cert, string data)
		{
			ValidateParameters(cert, data);
			return AsymmetricEncrypt((RSACryptoServiceProvider)cert.PublicKey.Key,
				data);
		}

		public static byte[] AsymmetricEncrypt(X509Certificate2 cert, byte[] data)
		{
			ValidateParameters(cert, data);
			return AsymmetricEncrypt((RSACryptoServiceProvider)cert.PublicKey.Key,
				data);
		}

		#endregion AsymmetricEncrypt Alternate Calls

		#endregion Asymmetric Encryption

		#region Asymmetric Decryption

		public static string AsymmetricDecrypt(
			RSACryptoServiceProvider rsa, string data)
		{
			if (rsa == null) throw new ArgumentNullException("rsa");
			if (data == null) throw new ArgumentNullException("data");
			AsymmetricDecryptor decryptor = new AsymmetricDecryptor(rsa, data);
			return decryptor.GetDecryptedString();
		}

		public static byte[] AsymmetricDecrypt(
			RSACryptoServiceProvider rsa, byte[] data)
		{
			if (rsa == null) throw new ArgumentNullException("rsa");
			if (data == null) throw new ArgumentNullException("data");
			AsymmetricDecryptor decryptor = new AsymmetricDecryptor(rsa, data);
			return decryptor.GetDecryptedBytes();
		}

		#region AsymmetricDecrypt Alternate Calls

		public static string AsymmetricDecrypt(X509Certificate2 cert, string data)
		{
			return AsymmetricDecrypt((RSACryptoServiceProvider)cert.PrivateKey,
				data);
		}

		public static byte[] AsymmetricDecrypt(X509Certificate2 cert, byte[] data)
		{
			return AsymmetricDecrypt((RSACryptoServiceProvider)cert.PrivateKey,
				data);
		}

		#endregion AsymmetricDecrypt Alternate Calls

		#endregion Asymmetric Decryption

		#region Private Methods

		private static void ValidateParameters(string data, byte[] key, byte[] iv)
		{
			if (data == null) throw new ArgumentNullException("data");
			if (key == null) throw new ArgumentNullException("key");
			if (iv == null) throw new ArgumentNullException("iv");
		}
		private static void ValidateParameters(byte[] data, byte[] key, byte[] iv)
		{
			if (data == null) throw new ArgumentNullException("data");
			if (key == null) throw new ArgumentNullException("key");
			if (iv == null) throw new ArgumentNullException("iv");
		}

		private static void ValidateParameters(SymmetricAlgorithm algorithm,
			string data)
		{
			if (algorithm == null) throw new ArgumentNullException("algorithm");
			if (data == null) throw new ArgumentNullException("data");
		}

		private static void ValidateParameters(SymmetricAlgorithm algorithm,
			byte[] data)
		{
			if (algorithm == null) throw new ArgumentNullException("algorithm");
			if (data == null) throw new ArgumentNullException("data");
		}

		private static void ValidateParameters(RSACryptoServiceProvider rsa,
			string data)
		{
			if (rsa == null) throw new ArgumentNullException("rsa");
			if (data == null) throw new ArgumentNullException("data");
		}

		private static void ValidateParameters(RSACryptoServiceProvider rsa,
			byte[] data)
		{
			if (rsa == null) throw new ArgumentNullException("rsa");
			if (data == null) throw new ArgumentNullException("data");
		}

		private static void ValidateParameters(X509Certificate2 cert, string data)
		{
			if (cert == null) throw new ArgumentNullException("cert");
			if (data == null) throw new ArgumentNullException("data");
		}

		private static void ValidateParameters(X509Certificate2 cert, byte[] data)
		{
			if (cert == null) throw new ArgumentNullException("cert");
			if (data == null) throw new ArgumentNullException("data");
		}

		#endregion Private Methods
	}
}