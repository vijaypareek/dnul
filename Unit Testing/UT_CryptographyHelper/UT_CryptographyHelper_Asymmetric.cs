using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

using DotNetUtilityLibrary;
using NUnit.Framework;
using System.Security.Cryptography;

namespace UT_CryptographyHelper
{
	[TestFixture]
	public class UT_CryptographyHelper_Asymmetric
	{
		#region Fields

		string mSampleString;
		byte[] mSampleBytes;
		MemoryStream mSampleStream;

		RSACryptoServiceProvider provider;
		RSACryptoServiceProvider falseProvider;
		string mEncodedText;
		string mDecodedText;
		byte[] mEncodedBytes;
		byte[] mDecodedBytes;

		#endregion Fields

		#region Setup

		[SetUp]
		public void Setup()
		{
			mSampleString = @"A (short) symmetric key.";
			mSampleBytes = Encoding.ASCII.GetBytes(mSampleString);
			byte[] memoryStreamBuffer = new byte[mSampleBytes.Length];
			mSampleBytes.CopyTo(memoryStreamBuffer, 0);
			mSampleStream = new MemoryStream(memoryStreamBuffer, true);
			mSampleStream.Position = 0;
			provider = new RSACryptoServiceProvider(2048);
			provider.PersistKeyInCsp = false;
			falseProvider = new RSACryptoServiceProvider(2048);
			falseProvider.PersistKeyInCsp = false;
		}

		#endregion Setup

		#region Teardown

		[TearDown]
		public void Teardown()
		{
			if (provider != null)
				provider.Dispose();
			if (falseProvider != null)
				falseProvider.Dispose();
			if (mSampleStream != null)
				mSampleStream.Dispose();
		}

		#endregion Teardown

		#region Tests

		[Test]
		public virtual void test_string()
		{
			AsymmetricEncryptString(provider);
			AsymmetricDecryptString(provider);
			AssertionsString();
		}

		[Test]
		public virtual void test_string_fail()
		{
			try
			{
				AsymmetricEncryptString(provider);
				AsymmetricDecryptString(falseProvider);
				AssertionsBytes_fail();
			}
			catch (CryptographicException)
			{

			}
		}

		[Test]
		public virtual void test_bytes()
		{
			AsymmetricEncryptBytes(provider);
			AsymmetricDecryptBytes(provider);
			AssertionsBytes();
		}

		[Test]
		public virtual void test_bytes_fail()
		{
			try
			{
				AsymmetricEncryptBytes(provider);
				AsymmetricDecryptBytes(falseProvider);
				AssertionsBytes_fail();
			}
			catch (CryptographicException)
			{

			}
		}

		#endregion Tests

		#region Encryption/Decryption Methods

		protected void AsymmetricEncryptString(RSACryptoServiceProvider provider)
		{
			mEncodedText = CryptographyHelper.AsymmetricEncrypt(provider,
				mSampleString);
		}

		protected void AsymmetricEncryptBytes(RSACryptoServiceProvider provider)
		{
			mEncodedBytes = CryptographyHelper.AsymmetricEncrypt(provider,
				mSampleBytes);
		}

		protected void AsymmetricDecryptString(RSACryptoServiceProvider provider)
		{
			mDecodedText = CryptographyHelper.AsymmetricDecrypt(provider,
				mEncodedText);
		}

		protected void AsymmetricDecryptBytes(RSACryptoServiceProvider provider)
		{
			mDecodedBytes = CryptographyHelper.AsymmetricDecrypt(provider,
				mEncodedBytes);
		}

		#endregion Encryption/Decryption Methods

		#region Assertions

		protected void AssertionsString()
		{
			Assert.AreNotEqual(mSampleString, mEncodedText);
			Assert.AreEqual(mSampleString, mDecodedText);
		}

		protected void AssertionsBytes()
		{
			Assert.AreNotEqual(mSampleBytes, mEncodedBytes);
			Assert.AreEqual(mSampleBytes, mDecodedBytes);
		}

		protected void AssertionsString_fail()
		{
			Assert.AreNotEqual(mSampleString, mEncodedText);
			Assert.AreNotEqual(mSampleString, mDecodedText);
		}

		protected void AssertionsBytes_fail()
		{
			Assert.AreNotEqual(mSampleBytes, mEncodedBytes);
			Assert.AreNotEqual(mSampleBytes, mDecodedBytes);
		}

		#endregion Assertions

	}
}