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
	public static class CryptographyHelper
	{
		#region Symmetric Encryption

		public static string SymmetricEncrypt(SymmetricAlgorithm algorithm,
			string data, byte[] key, byte[] iv)
		{
			if (algorithm == null) throw new ArgumentNullException("algorithm");
			if (data == null) throw new ArgumentNullException("data");
			if (key == null) throw new ArgumentNullException("key");
			if (iv == null) throw new ArgumentNullException("iv");
			SymmetricEncryptor encryptor = 
				new SymmetricEncryptor(algorithm, data, key, iv);
			return encryptor.GetEncryptedString();
		}

		public static byte[] SymmetricEncrypt(SymmetricAlgorithm algorithm,
			byte[] data, byte[] key, byte[] iv)
		{
			if (algorithm == null) throw new ArgumentNullException("algorithm");
			if (data == null) throw new ArgumentNullException("data");
			if (key == null) throw new ArgumentNullException("key");
			if (iv == null) throw new ArgumentNullException("iv");
			SymmetricEncryptor encryptor =
				new SymmetricEncryptor(algorithm, data, key, iv);
			return encryptor.GetEncryptedBytes();
		}

		public static MemoryStream SymmetricEncrypt(SymmetricAlgorithm algorithm,
			MemoryStream data, byte[] key, byte[] iv)
		{
			if (algorithm == null) throw new ArgumentNullException("algorithm");
			if (data == null) throw new ArgumentNullException("data");
			if (key == null) throw new ArgumentNullException("key");
			if (iv == null) throw new ArgumentNullException("iv");
			SymmetricEncryptor encryptor =
				new SymmetricEncryptor(algorithm, data, key, iv);
			return encryptor.GetEncryptedStream();
		}

		#region Encrypt Abbreviated Calls

		public static byte[] SymmetricEncrypt(byte[] data, byte[] key, byte[] iv)
		{
			return SymmetricEncrypt(new RijndaelManaged(), data, key, iv);
		}

		public static string SymmetricEncrypt(string data, byte[] key, byte[] iv)
		{
			return SymmetricEncrypt(new RijndaelManaged(), data, key, iv);
		}

		public static MemoryStream SymmetricEncrypt(MemoryStream data, byte[] key,
			byte[] iv)
		{
			return SymmetricEncrypt(new RijndaelManaged(), data, key, iv);
		}

		#endregion Encrypt Abbreviated Calls

		#endregion Symmetric Encryption

		#region Symmetric Decryption

		public static string SymmetricDecrypt(SymmetricAlgorithm algorithm,
			string data, byte[] key, byte[] iv)
		{
			if (algorithm == null) throw new ArgumentNullException("algorithm");
			if (data == null) throw new ArgumentNullException("data");
			if (key == null) throw new ArgumentNullException("key");
			if (iv == null) throw new ArgumentNullException("iv");
			SymmetricDecryptor decryptor = 
				new SymmetricDecryptor(algorithm, data, key, iv);
			return decryptor.GetDecryptedString();
		}

		public static byte[] SymmetricDecrypt(SymmetricAlgorithm algorithm,
			byte[] data, byte[] key, byte[] iv)
		{
			if (algorithm == null) throw new ArgumentNullException("algorithm");
			if (data == null) throw new ArgumentNullException("data");
			if (key == null) throw new ArgumentNullException("key");
			if (iv == null) throw new ArgumentNullException("iv");
			SymmetricDecryptor decryptor =
				new SymmetricDecryptor(algorithm, data, key, iv);
			return decryptor.GetDecryptedBytes();
		}

		public static MemoryStream SymmetricDecrypt(SymmetricAlgorithm algorithm,
			MemoryStream data, byte[] key, byte[] iv)
		{
			if (algorithm == null) throw new ArgumentNullException("algorithm");
			if (data == null) throw new ArgumentNullException("data");
			if (key == null) throw new ArgumentNullException("key");
			if (iv == null) throw new ArgumentNullException("iv");
			SymmetricDecryptor decryptor =
				new SymmetricDecryptor(algorithm, data, key, iv);
			return decryptor.GetDecryptedStream();
		}

		#region Decrypt Abbreviated Calls

		public static byte[] SymmetricDecrypt(byte[] data, byte[] key, byte[] iv)
		{
			return SymmetricDecrypt(new RijndaelManaged(), data, key, iv);
		}

		public static string SymmetricDecrypt(string data, byte[] key, byte[] iv)
		{
			return SymmetricDecrypt(new RijndaelManaged(), data, key, iv);
		}

		public static MemoryStream SymmetricDecrypt(MemoryStream data, byte[] key,
			byte[] iv)
		{
			return SymmetricDecrypt(new RijndaelManaged(), data, key, iv);
		}

		#endregion Decrypt Abbreviated Calls

		#endregion Symmetric Decryption

		#region Asymmetric Encryption

		public static string AsymmetricEncrypt(
			RSACryptoServiceProvider rsa, string data)
		{
			if (rsa == null) throw new ArgumentNullException("rsa");
			if (data == null) throw new ArgumentNullException("data");
			RSAEncryptor encryptor = new RSAEncryptor(rsa, data);
			return encryptor.GetEncryptedString();
		}

		public static byte[] AsymmetricEncrypt(
			RSACryptoServiceProvider rsa, byte[] data)
		{
			if (rsa == null) throw new ArgumentNullException("rsa");
			if (data == null) throw new ArgumentNullException("data");
			RSAEncryptor encryptor = new RSAEncryptor(rsa, data);
			return encryptor.GetEncryptedBytes();
		}

		public static MemoryStream AsymmetricEncrypt(
			RSACryptoServiceProvider rsa, MemoryStream data)
		{
			if (rsa == null) throw new ArgumentNullException("rsa");
			if (data == null) throw new ArgumentNullException("data");
			RSAEncryptor encryptor = new RSAEncryptor(rsa, data);
			return encryptor.GetEncryptedStream();
		}

		#region AsymmetricEncrypt Alternate Calls

		public static string AsymmetricEncrypt(X509Certificate2 cert, string data)
		{
			return AsymmetricEncrypt((RSACryptoServiceProvider)cert.PublicKey.Key,
				data);
		}

		public static byte[] AsymmetricEncrypt(X509Certificate2 cert, byte[] data)
		{
			return AsymmetricEncrypt((RSACryptoServiceProvider)cert.PublicKey.Key,
				data);
		}

		public static MemoryStream AsymmetricEncrypt(X509Certificate2 cert,
			MemoryStream data)
		{
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
			RSADecryptor decryptor = new RSADecryptor(rsa, data);
			return decryptor.GetDecryptedString();
		}

		public static byte[] AsymmetricDecrypt(
			RSACryptoServiceProvider rsa, byte[] data)
		{
			if (rsa == null) throw new ArgumentNullException("rsa");
			if (data == null) throw new ArgumentNullException("data");
			RSADecryptor decryptor = new RSADecryptor(rsa, data);
			return decryptor.GetDecryptedBytes();
		}

		public static MemoryStream AsymmetricDecrypt(
			RSACryptoServiceProvider rsa, MemoryStream data)
		{
			if (rsa == null) throw new ArgumentNullException("rsa");
			if (data == null) throw new ArgumentNullException("data");
			RSADecryptor decryptor = new RSADecryptor(rsa, data);
			return decryptor.GetDecryptedStream();
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

		public static MemoryStream AsymmetricDecrypt(X509Certificate2 cert,
			MemoryStream data)
		{
			return AsymmetricDecrypt((RSACryptoServiceProvider)cert.PrivateKey,
				data);
		}

		#endregion AsymmetricDecrypt Alternate Calls

		#endregion Asymmetric Decryption
	}
}