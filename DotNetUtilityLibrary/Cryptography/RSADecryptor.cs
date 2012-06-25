using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;

namespace DotNetUtilityLibrary.Cryptography
{
	public class RSADecryptor : IDisposable
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

		private RSADecryptor(RSACryptoServiceProvider rsaProvider)
		{
			mRsa = rsaProvider;
		}

		public RSADecryptor(RSACryptoServiceProvider rsaProvider, MemoryStream rawData)
			: this(rsaProvider)
		{
			mRawDataStream = rawData;
		}

		public RSADecryptor(RSACryptoServiceProvider rsaProvider, byte[] rawData)
			: this(rsaProvider)
		{
			mRawDataStream = new MemoryStream(rawData);
			_mRawDataByteArray = rawData;
		}

		public RSADecryptor(RSACryptoServiceProvider rsaProvider, string rawData)
			: this(rsaProvider, ConvertHelper.StringToBytes(rawData)) { }

		public RSADecryptor(X509Certificate2 certificate, MemoryStream rawData)
			: this((RSACryptoServiceProvider)certificate.PrivateKey, rawData)
		{ }

		public RSADecryptor(X509Certificate2 certificate, byte[] rawData)
			: this((RSACryptoServiceProvider)certificate.PrivateKey, rawData)
		{ }

		public RSADecryptor(X509Certificate2 certificate, string rawData)
			: this((RSACryptoServiceProvider)certificate.PrivateKey, rawData)
		{ }

		#endregion Constructors

		#region Exposed Methods

		public string GetDecryptedString()
		{
			return ConvertHelper.BytesToString(GetDecryptedBytes());
		}

		public byte[] GetDecryptedBytes()
		{
			return GetDecryptedStream().ToArray();
		}

		public MemoryStream GetDecryptedStream()
		{
			return new MemoryStream(mRsa.Decrypt(mRawDataByteArray, false));
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
