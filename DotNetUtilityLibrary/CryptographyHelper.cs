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

		public static string SymmetricEncrypt<T>(string data, byte[] key,
			byte[] iv) where T : SymmetricAlgorithm
		{
			if (data == null) throw new ArgumentNullException("data");
			if (key == null) throw new ArgumentNullException("key");
			if (iv == null) throw new ArgumentNullException("iv");
			SymmetricAlgorithm algorithm = Activator.CreateInstance<T>();
			SymmetricEncryptor encryptor = 
				new SymmetricEncryptor(algorithm, data, key, iv);
			return encryptor.GetEncryptedString();
		}

		public static byte[] SymmetricEncrypt<T>(byte[] data, byte[] key,
			byte[] iv) where T : SymmetricAlgorithm
		{
			if (data == null) throw new ArgumentNullException("data");
			if (key == null) throw new ArgumentNullException("key");
			if (iv == null) throw new ArgumentNullException("iv");
			SymmetricAlgorithm algorithm = Activator.CreateInstance<T>();
			SymmetricEncryptor encryptor =
				new SymmetricEncryptor(algorithm, data, key, iv);
			return encryptor.GetEncryptedBytes();
		}

		#region Encrypt Abbreviated Calls

		public static byte[] SymmetricEncrypt(byte[] data, byte[] key, byte[] iv)
		{
			return SymmetricEncrypt<RijndaelManaged>(data, key, iv);
		}

		public static string SymmetricEncrypt(string data, byte[] key, byte[] iv)
		{
			return SymmetricEncrypt<RijndaelManaged>(data, key, iv);
		}

		#endregion Encrypt Abbreviated Calls

		#endregion Symmetric Encryption

		#region Symmetric Decryption

		public static string SymmetricDecrypt<T>( string data, byte[] key,
			byte[] iv) where T : SymmetricAlgorithm
		{
			if (data == null) throw new ArgumentNullException("data");
			if (key == null) throw new ArgumentNullException("key");
			if (iv == null) throw new ArgumentNullException("iv");
			SymmetricAlgorithm algorithm = Activator.CreateInstance<T>();
			SymmetricDecryptor decryptor = 
				new SymmetricDecryptor(algorithm, data, key, iv);
			return decryptor.GetDecryptedString();
		}

		public static byte[] SymmetricDecrypt<T>(byte[] data, byte[] key,
			byte[] iv) where T : SymmetricAlgorithm
		{
			if (data == null) throw new ArgumentNullException("data");
			if (key == null) throw new ArgumentNullException("key");
			if (iv == null) throw new ArgumentNullException("iv");
			SymmetricAlgorithm algorithm = Activator.CreateInstance<T>();
			SymmetricDecryptor decryptor =
				new SymmetricDecryptor(algorithm, data, key, iv);
			return decryptor.GetDecryptedBytes();
		}

		#region Decrypt Abbreviated Calls

		public static byte[] SymmetricDecrypt(byte[] data, byte[] key, byte[] iv)
		{
			return SymmetricDecrypt<RijndaelManaged>(data, key, iv);
		}

		public static string SymmetricDecrypt(string data, byte[] key, byte[] iv)
		{
			return SymmetricDecrypt<RijndaelManaged>(data, key, iv);
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
	}
}