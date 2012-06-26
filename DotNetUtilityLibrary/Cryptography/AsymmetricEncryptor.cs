using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;

namespace DotNetUtilityLibrary.Cryptography
{
	public class AsymmetricEncryptor : IDisposable
	{
		#region Properties

		private MemoryStream mRawDataStream;
		private RSACryptoServiceProvider mProvider;

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

		private AsymmetricEncryptor(RSACryptoServiceProvider provider)
		{
			mProvider = provider;
		}

		public AsymmetricEncryptor(RSACryptoServiceProvider provider, MemoryStream rawData)
			: this(provider)
		{
			mRawDataStream = rawData;
		}

		public AsymmetricEncryptor(RSACryptoServiceProvider provider, byte[] rawData)
			: this(provider)
		{
			mRawDataStream = new MemoryStream(rawData);
			_mRawDataByteArray = rawData;
		}

		public AsymmetricEncryptor(RSACryptoServiceProvider provider, string rawData)
			: this(provider, ConvertHelper.StringToBytes(rawData)) { }

		public AsymmetricEncryptor(X509Certificate2 certificate, MemoryStream rawData)
			: this((RSACryptoServiceProvider)certificate.PublicKey.Key, rawData)
		{ }

		public AsymmetricEncryptor(X509Certificate2 certificate, byte[] rawData)
			: this((RSACryptoServiceProvider)certificate.PublicKey.Key, rawData)
		{ }

		public AsymmetricEncryptor(X509Certificate2 certificate, string rawData)
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

		#endregion Exposed Methods

		#region Private Methods
		
		private MemoryStream GetEncryptedStream()
		{
			return new MemoryStream(mProvider.Encrypt(mRawDataByteArray, true));
		} 

		#endregion Private Methods

		#region IDisposable

		void IDisposable.Dispose()
		{
			if (mRawDataStream != null)
				mRawDataStream.Dispose();
			if (mProvider != null)
				mProvider.Dispose();
		}

		#endregion IDisposable
	}
}
