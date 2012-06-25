using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;

namespace DotNetUtilityLibrary.Cryptography
{
	public class RSAEncryptor : IDisposable
	{
		#region Properties

		private MemoryStream mRawDataStream;
		private RSACryptoServiceProvider mRsa;

		private byte[] _mRawDataByteArray;
		private byte[] mRawDataByteArray
		{
			get
			{
				if (_mRawDataByteArray == null)
				{
					_mRawDataByteArray = mRawDataStream.ToArray();
				}
				return _mRawDataByteArray;
			}
		}

		#endregion Properties

		#region Constructors

		private RSAEncryptor(RSACryptoServiceProvider rsaProvider)
		{
			mRsa = rsaProvider;
		}

		public RSAEncryptor(RSACryptoServiceProvider rsaProvider, MemoryStream rawData)
			: this(rsaProvider)
		{
			mRawDataStream = rawData;
		}

		public RSAEncryptor(RSACryptoServiceProvider rsaProvider, byte[] rawData)
			: this(rsaProvider)
		{
			mRawDataStream = new MemoryStream(rawData);
			_mRawDataByteArray = rawData;
		}

		public RSAEncryptor(RSACryptoServiceProvider rsaProvider, string rawData)
			: this(rsaProvider, ConvertHelper.StringToBytes(rawData)) { }

		public RSAEncryptor(X509Certificate2 certificate, MemoryStream rawData)
			: this((RSACryptoServiceProvider)certificate.PublicKey.Key, rawData)
		{ }

		public RSAEncryptor(X509Certificate2 certificate, byte[] rawData)
			: this((RSACryptoServiceProvider)certificate.PublicKey.Key, rawData)
		{ }

		public RSAEncryptor(X509Certificate2 certificate, string rawData)
			: this((RSACryptoServiceProvider)certificate.PublicKey.Key, rawData)
		{ }

		#endregion Constructors

		#region Exposed Methods

		public string GetEncryptedString()
		{
			return ConvertHelper.BytesToString(GetEncryptedBytes());
		}

		public byte[] GetEncryptedBytes()
		{
			return GetEncryptedStream().ToArray();
		}

		public MemoryStream GetEncryptedStream()
		{		
			return new MemoryStream(mRsa.Encrypt(mRawDataByteArray, false));
		}

		#endregion Exposed Methods

		#region IDisposable

		void IDisposable.Dispose()
		{
			if (mRawDataStream != null)
				mRawDataStream.Dispose();
			if (mRsa != null)
				mRsa.Dispose();
		}

		#endregion IDisposable
	}
}
