using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Security.Cryptography;
using System.IO;

namespace DotNetUtilityLibrary.Cryptography
{
	public class SymmetricEncryptor : IDisposable
	{
		#region Properties

		private MemoryStream mRawDataStream;
		private SymmetricAlgorithm mAlgorithm;
		private ICryptoTransform mTransform;

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

		private SymmetricEncryptor(SymmetricAlgorithm algorithm,
			byte[] key,
			byte[] iv)
		{
			mAlgorithm = algorithm;
			algorithm.Key = key;
			algorithm.IV = iv;
			mTransform = mAlgorithm.CreateEncryptor(key, iv);
		}

		public SymmetricEncryptor(SymmetricAlgorithm algorithm,
			MemoryStream rawData,
			byte[] key,
			byte[] iv)
			: this(algorithm, key, iv)
		{
			mRawDataStream = rawData;
		}

		public SymmetricEncryptor(SymmetricAlgorithm algorithm,
			byte[] rawData,
			byte[] key,
			byte[] iv)
			: this(algorithm, key, iv)
		{
			mRawDataStream = new MemoryStream(rawData);
			_mRawDataByteArray = rawData;
		}

		public SymmetricEncryptor(SymmetricAlgorithm algorithm,
			string rawData,
			byte[] key,
			byte[] iv)
			: this(algorithm, ConvertHelper.StringToBytes(rawData), key, iv)
		{
		}
		
		#endregion Constructors

		#region Exposed Methods

		public string GetEncryptedString()
		{
			return ConvertHelper.BytesToString(GetEncryptedBytes());
		}

		public byte[] GetEncryptedBytes()
		{
			return GetInternalEncryptedStream().ToArray();
		}

		public MemoryStream GetEncryptedStream()
		{
			MemoryStream memoryStream = new MemoryStream();
			using (CryptoStream cryptoStream = new CryptoStream(memoryStream,
				mTransform, CryptoStreamMode.Write))
			{
				StreamHelper.CopyStream(mRawDataStream, cryptoStream);
				MemoryStream result = new MemoryStream();
				memoryStream.Seek(0, SeekOrigin.Begin);
				StreamHelper.CopyStream(memoryStream, result);
				return result;
			}
		}
		private MemoryStream GetInternalEncryptedStream()
		{
			MemoryStream memoryStream = new MemoryStream();
			using (CryptoStream cryptoStream = new CryptoStream(memoryStream,
				mTransform, CryptoStreamMode.Write))
			{
				StreamHelper.CopyStream(mRawDataStream, cryptoStream);
				return memoryStream;
			}
		}

		#endregion Exposed Methods

		#region IDisposable

		void IDisposable.Dispose()
		{
			if (mRawDataStream != null)
				mRawDataStream.Dispose();
			if (mAlgorithm != null)
				mAlgorithm.Dispose();
			if (mTransform != null)
				mTransform.Dispose();
		}

		#endregion IDisposable
	}
}
