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
	public class UT_CryptographyHelper
	{
		#region Fields
		
		string mSampleString;
		byte[] mSampleBytes;
		MemoryStream mSampleStream;

		SymmetricAlgorithm algorithm;
		string mEncodedText;
		string mDecodedText;
		byte[] mEncodedBytes;
		byte[] mDecodedBytes;
		MemoryStream mEncodedStream;
		MemoryStream mDecodedStream;
		byte[] mKey;
		byte[] mIv;

		#endregion Fields

		#region Setup

		[SetUp]
		public void Setup()
		{
			mSampleString = @"
“You ain’t gonna believe this, but you used to fit right here. I’d hold you up to say to your mother, ‘This kid’s gonna be the best kid in the world. This kid’s gonna be somebody better than anybody I ever knew.’ And you grew up good and wonderful. It was great just watchin’ you, every day was like a privilege. Then the time come for you to be your own man and take on the world, and you did. But somewhere along the line, you changed. You stopped being you. You let people stick a finger in your face and tell you you’re no good. And when things got hard, you started lookin’ for something to blame, like a big shadow.
Let me tell you something you already know.The world ain’t all sunshine and rainbows. It’s a very mean and nasty place, and I don’t care how tough you are, it will beat you to your knees and keep you there permanently if you let it. You, me, or nobody is gonna hit as hard as life. But it ain’t about how hard you hit, it’s about how hard you can get hit and keep moving forward. How much you can take and keep moving forward. That’s how winning is done!
Now if you know what you’re worth, then go out and get what you’re worth! But you gotta be willing to take the hits. And not pointing fingers saying you ain’t where you wanna be because of him, or her, or anybody! Cowards do that and that ain’t you! You’re better than that!
I’m always gonna love you no matter what. No matter what happens. You’re my son and you’re my blood. You’re the best thing in my life. But until you start believing in yourself, you ain’t gonna have a life.
Don’t forget to visit your mother.”
";
			mSampleBytes = Encoding.ASCII.GetBytes(mSampleString);
			byte[] memoryStreamBuffer = new byte[mSampleBytes.Length];
			mSampleBytes.CopyTo(memoryStreamBuffer, 0);
			mSampleStream = new MemoryStream(memoryStreamBuffer, true);
			mSampleStream.Position = 0;
		}

		#endregion Setup

		#region Teardown

		[TearDown]
		public void Teardown()
		{
			if (algorithm != null)
				algorithm.Dispose();
			if (mSampleStream != null) 
				mSampleStream.Dispose();
			if (mEncodedStream != null) 
				mEncodedStream.Dispose();
			if (mDecodedStream != null) 
				mDecodedStream.Dispose();

			mKey = null;
			mIv = null;
		}
		
		#endregion Teardown

		#region Tests

		#region AesManaged

		[Test]
		public void AesManaged_string()
		{
			TestSymmetricEncrypt<AesManaged>(AssignMetadata, SymmetricEncryptString);
			TestSymmetricDecrypt<AesManaged>(AssignMetadata, SymmetricDecryptString);
			AssertionsString();
		}

		[Test]
		[ExpectedException(typeof(CryptographicException))]
		public void AesManaged_string_badkey()
		{
			TestSymmetricEncrypt<AesManaged>(AssignMetadata, SymmetricEncryptString);
			TestSymmetricDecrypt<AesManaged>(AssignMetadataFalseKey, SymmetricDecryptString);
			AssertionsString();
		}

		[Test]
		public void AesManaged_string_badiv()
		{
			TestSymmetricEncrypt<AesManaged>(AssignMetadata, SymmetricEncryptString);
			TestSymmetricDecrypt<AesManaged>(AssignMetadataFalseIV, SymmetricDecryptString);
			AssertionsString_badiv();
		}

		[Test]
		public void AesManaged_bytes()
		{
			TestSymmetricEncrypt<AesManaged>(AssignMetadata, SymmetricEncryptBytes);
			TestSymmetricDecrypt<AesManaged>(AssignMetadata, SymmetricDecryptBytes);
			AssertionsBytes();
		}

		[Test]
		[ExpectedException(typeof(CryptographicException))]
		public void AesManaged_bytes_badkey()
		{
			TestSymmetricEncrypt<AesManaged>(AssignMetadata, SymmetricEncryptBytes);
			TestSymmetricDecrypt<AesManaged>(AssignMetadataFalseKey, SymmetricDecryptBytes);
			AssertionsBytes_badiv();
		}

		[Test]
		public void AesManaged_bytes_badiv()
		{
			TestSymmetricEncrypt<AesManaged>(AssignMetadata, SymmetricEncryptBytes);
			TestSymmetricDecrypt<AesManaged>(AssignMetadataFalseIV, SymmetricDecryptBytes);
			AssertionsBytes_badiv();
		}

		[Test]
		public void AesManaged_stream()
		{
			TestSymmetricEncrypt<AesManaged>(AssignMetadata, SymmetricEncryptStream);
			TestSymmetricDecrypt<AesManaged>(AssignMetadata, SymmetricDecryptStream);
			AssertionsStream();
		}

		[Test]
		[ExpectedException(typeof(CryptographicException))]
		public void AesManaged_stream_badkey()
		{
			TestSymmetricEncrypt<AesManaged>(AssignMetadata, SymmetricEncryptStream);
			TestSymmetricDecrypt<AesManaged>(AssignMetadataFalseKey, SymmetricDecryptStream);
			AssertionsStream_badiv();
		}

		[Test]
		[ExpectedException(typeof(CryptographicException))]
		public void AesManaged_stream_badiv()
		{
			TestSymmetricEncrypt<AesManaged>(AssignMetadata, SymmetricEncryptStream);
			TestSymmetricDecrypt<AesManaged>(AssignMetadataFalseIV, SymmetricDecryptStream);
			AssertionsStream_badiv();
		}

		#endregion AesManaged

		#region Rindjael

		[Test]
		public void RijndaelManaged_string()
		{
			TestSymmetricEncrypt<RijndaelManaged>(AssignMetadata, SymmetricEncryptString);
			TestSymmetricDecrypt<RijndaelManaged>(AssignMetadata, SymmetricDecryptString);
			AssertionsString();
		}

		[Test]
		[ExpectedException(typeof(CryptographicException))]
		public void RijndaelManaged_string_badkey()
		{
			TestSymmetricEncrypt<RijndaelManaged>(AssignMetadata, SymmetricEncryptString);
			TestSymmetricDecrypt<RijndaelManaged>(AssignMetadataFalseKey, SymmetricDecryptString);
			AssertionsString();
		}

		[Test]
		public void RijndaelManaged_string_badiv()
		{
			TestSymmetricEncrypt<RijndaelManaged>(AssignMetadata, SymmetricEncryptString);
			TestSymmetricDecrypt<RijndaelManaged>(AssignMetadataFalseIV, SymmetricDecryptString);
			AssertionsString_badiv();
		}

		[Test]
		public void RijndaelManaged_bytes()
		{
			TestSymmetricEncrypt<RijndaelManaged>(AssignMetadata, SymmetricEncryptBytes);
			TestSymmetricDecrypt<RijndaelManaged>(AssignMetadata, SymmetricDecryptBytes);
			AssertionsBytes();
		}

		[Test]
		[ExpectedException(typeof(CryptographicException))]
		public void RijndaelManaged_bytes_badkey()
		{
			TestSymmetricEncrypt<RijndaelManaged>(AssignMetadata, SymmetricEncryptBytes);
			TestSymmetricDecrypt<RijndaelManaged>(AssignMetadataFalseKey, SymmetricDecryptBytes);
			AssertionsBytes_badiv();
		}

		[Test]
		public void RijndaelManaged_bytes_badiv()
		{
			TestSymmetricEncrypt<RijndaelManaged>(AssignMetadata, SymmetricEncryptBytes);
			TestSymmetricDecrypt<RijndaelManaged>(AssignMetadataFalseIV, SymmetricDecryptBytes);
			AssertionsBytes_badiv();
		}

		[Test]
		public void RijndaelManaged_stream()
		{
			TestSymmetricEncrypt<RijndaelManaged>(AssignMetadata, SymmetricEncryptStream);
			TestSymmetricDecrypt<RijndaelManaged>(AssignMetadata, SymmetricDecryptStream);
			AssertionsStream();
		}

		[Test]
		[ExpectedException(typeof(CryptographicException))]
		public void RijndaelManaged_stream_badkey()
		{
			TestSymmetricEncrypt<RijndaelManaged>(AssignMetadata, SymmetricEncryptStream);
			TestSymmetricDecrypt<RijndaelManaged>(AssignMetadataFalseKey, SymmetricDecryptStream);
			AssertionsStream_badiv();
		}

		[Test]
		[ExpectedException(typeof(CryptographicException))]
		public void RijndaelManaged_stream_badiv()
		{
			TestSymmetricEncrypt<RijndaelManaged>(AssignMetadata, SymmetricEncryptStream);
			TestSymmetricDecrypt<RijndaelManaged>(AssignMetadataFalseIV, SymmetricDecryptStream);
			AssertionsStream_badiv();
		}

		#endregion Rindjael

		#region DESCryptoServiceProvider

		[Test]
		public void DESCryptoServiceProvider_string()
		{
			TestSymmetricEncrypt<DESCryptoServiceProvider>(AssignMetadata, SymmetricEncryptString);
			TestSymmetricDecrypt<DESCryptoServiceProvider>(AssignMetadata, SymmetricDecryptString);
			AssertionsString();
		}

		[Test]
		[ExpectedException(typeof(CryptographicException))]
		public void DESCryptoServiceProvider_string_badkey()
		{
			TestSymmetricEncrypt<DESCryptoServiceProvider>(AssignMetadata, SymmetricEncryptString);
			TestSymmetricDecrypt<DESCryptoServiceProvider>(AssignMetadataFalseKey, SymmetricDecryptString);
			AssertionsString();
		}

		[Test]
		public void DESCryptoServiceProvider_string_badiv()
		{
			TestSymmetricEncrypt<DESCryptoServiceProvider>(AssignMetadata, SymmetricEncryptString);
			TestSymmetricDecrypt<DESCryptoServiceProvider>(AssignMetadataFalseIV, SymmetricDecryptString);
			AssertionsString_badiv();
		}

		[Test]
		public void DESCryptoServiceProvider_bytes()
		{
			TestSymmetricEncrypt<DESCryptoServiceProvider>(AssignMetadata, SymmetricEncryptBytes);
			TestSymmetricDecrypt<DESCryptoServiceProvider>(AssignMetadata, SymmetricDecryptBytes);
			AssertionsBytes();
		}

		[Test]
		[ExpectedException(typeof(CryptographicException))]
		public void DESCryptoServiceProvider_bytes_badkey()
		{
			TestSymmetricEncrypt<DESCryptoServiceProvider>(AssignMetadata, SymmetricEncryptBytes);
			TestSymmetricDecrypt<DESCryptoServiceProvider>(AssignMetadataFalseKey, SymmetricDecryptBytes);
			AssertionsBytes_badiv();
		}

		[Test]
		public void DESCryptoServiceProvider_bytes_badiv()
		{
			TestSymmetricEncrypt<DESCryptoServiceProvider>(AssignMetadata, SymmetricEncryptBytes);
			TestSymmetricDecrypt<DESCryptoServiceProvider>(AssignMetadataFalseIV, SymmetricDecryptBytes);
			AssertionsBytes_badiv();
		}

		[Test]
		public void DESCryptoServiceProvider_stream()
		{
			TestSymmetricEncrypt<DESCryptoServiceProvider>(AssignMetadata, SymmetricEncryptStream);
			TestSymmetricDecrypt<DESCryptoServiceProvider>(AssignMetadata, SymmetricDecryptStream);
			AssertionsStream();
		}

		[Test]
		[ExpectedException(typeof(CryptographicException))]
		public void DESCryptoServiceProvider_stream_badkey()
		{
			TestSymmetricEncrypt<DESCryptoServiceProvider>(AssignMetadata, SymmetricEncryptStream);
			TestSymmetricDecrypt<DESCryptoServiceProvider>(AssignMetadataFalseKey, SymmetricDecryptStream);
			AssertionsStream_badiv();
		}

		[Test]
		[ExpectedException(typeof(CryptographicException))]
		public void DESCryptoServiceProvider_stream_badiv()
		{
			TestSymmetricEncrypt<DESCryptoServiceProvider>(AssignMetadata, SymmetricEncryptStream);
			TestSymmetricDecrypt<DESCryptoServiceProvider>(AssignMetadataFalseIV, SymmetricDecryptStream);
			AssertionsStream_badiv();
		}

		#endregion DESCryptoServiceProvider

		#region RC2CryptoServiceProvider

		[Test]
		public void RC2CryptoServiceProvider_string()
		{
			TestSymmetricEncrypt<RC2CryptoServiceProvider>(AssignMetadata, SymmetricEncryptString);
			TestSymmetricDecrypt<RC2CryptoServiceProvider>(AssignMetadata, SymmetricDecryptString);
			AssertionsString();
		}

		[Test]
		[ExpectedException(typeof(CryptographicException))]
		public void RC2CryptoServiceProvider_string_badkey()
		{
			TestSymmetricEncrypt<RC2CryptoServiceProvider>(AssignMetadata, SymmetricEncryptString);
			TestSymmetricDecrypt<RC2CryptoServiceProvider>(AssignMetadataFalseKey, SymmetricDecryptString);
			AssertionsString();
		}

		[Test]
		public void RC2CryptoServiceProvider_string_badiv()
		{
			TestSymmetricEncrypt<RC2CryptoServiceProvider>(AssignMetadata, SymmetricEncryptString);
			TestSymmetricDecrypt<RC2CryptoServiceProvider>(AssignMetadataFalseIV, SymmetricDecryptString);
			AssertionsString_badiv();
		}

		[Test]
		public void RC2CryptoServiceProvider_bytes()
		{
			TestSymmetricEncrypt<RC2CryptoServiceProvider>(AssignMetadata, SymmetricEncryptBytes);
			TestSymmetricDecrypt<RC2CryptoServiceProvider>(AssignMetadata, SymmetricDecryptBytes);
			AssertionsBytes();
		}

		[Test]
		[ExpectedException(typeof(CryptographicException))]
		public void RC2CryptoServiceProvider_bytes_badkey()
		{
			TestSymmetricEncrypt<RC2CryptoServiceProvider>(AssignMetadata, SymmetricEncryptBytes);
			TestSymmetricDecrypt<RC2CryptoServiceProvider>(AssignMetadataFalseKey, SymmetricDecryptBytes);
			AssertionsBytes_badiv();
		}

		[Test]
		public void RC2CryptoServiceProvider_bytes_badiv()
		{
			TestSymmetricEncrypt<RC2CryptoServiceProvider>(AssignMetadata, SymmetricEncryptBytes);
			TestSymmetricDecrypt<RC2CryptoServiceProvider>(AssignMetadataFalseIV, SymmetricDecryptBytes);
			AssertionsBytes_badiv();
		}

		[Test]
		public void RC2CryptoServiceProvider_stream()
		{
			TestSymmetricEncrypt<RC2CryptoServiceProvider>(AssignMetadata, SymmetricEncryptStream);
			TestSymmetricDecrypt<RC2CryptoServiceProvider>(AssignMetadata, SymmetricDecryptStream);
			AssertionsStream();
		}

		[Test]
		[ExpectedException(typeof(CryptographicException))]
		public void RC2CryptoServiceProvider_stream_badkey()
		{
			TestSymmetricEncrypt<RC2CryptoServiceProvider>(AssignMetadata, SymmetricEncryptStream);
			TestSymmetricDecrypt<RC2CryptoServiceProvider>(AssignMetadataFalseKey, SymmetricDecryptStream);
			AssertionsStream_badiv();
		}

		[Test]
		[ExpectedException(typeof(CryptographicException))]
		public void RC2CryptoServiceProvider_stream_badiv()
		{
			TestSymmetricEncrypt<RC2CryptoServiceProvider>(AssignMetadata, SymmetricEncryptStream);
			TestSymmetricDecrypt<RC2CryptoServiceProvider>(AssignMetadataFalseIV, SymmetricDecryptStream);
			AssertionsStream_badiv();
		}

		#endregion RC2CryptoServiceProvider

		#region TripleDESCryptoServiceProvider

		[Test]
		public void TripleDESCryptoServiceProvider_string()
		{
			TestSymmetricEncrypt<TripleDESCryptoServiceProvider>(AssignMetadata, SymmetricEncryptString);
			TestSymmetricDecrypt<TripleDESCryptoServiceProvider>(AssignMetadata, SymmetricDecryptString);
			AssertionsString();
		}

		[Test]
		[ExpectedException(typeof(CryptographicException))]
		public void TripleDESCryptoServiceProvider_string_badkey()
		{
			TestSymmetricEncrypt<TripleDESCryptoServiceProvider>(AssignMetadata, SymmetricEncryptString);
			TestSymmetricDecrypt<TripleDESCryptoServiceProvider>(AssignMetadataFalseKey, SymmetricDecryptString);
			AssertionsString();
		}

		[Test]
		public void TripleDESCryptoServiceProvider_string_badiv()
		{
			TestSymmetricEncrypt<TripleDESCryptoServiceProvider>(AssignMetadata, SymmetricEncryptString);
			TestSymmetricDecrypt<TripleDESCryptoServiceProvider>(AssignMetadataFalseIV, SymmetricDecryptString);
			AssertionsString_badiv();
		}

		[Test]
		public void TripleDESCryptoServiceProvider_bytes()
		{
			TestSymmetricEncrypt<TripleDESCryptoServiceProvider>(AssignMetadata, SymmetricEncryptBytes);
			TestSymmetricDecrypt<TripleDESCryptoServiceProvider>(AssignMetadata, SymmetricDecryptBytes);
			AssertionsBytes();
		}

		[Test]
		[ExpectedException(typeof(CryptographicException))]
		public void TripleDESCryptoServiceProvider_bytes_badkey()
		{
			TestSymmetricEncrypt<TripleDESCryptoServiceProvider>(AssignMetadata, SymmetricEncryptBytes);
			TestSymmetricDecrypt<TripleDESCryptoServiceProvider>(AssignMetadataFalseKey, SymmetricDecryptBytes);
			AssertionsBytes_badiv();
		}

		[Test]
		public void TripleDESCryptoServiceProvider_bytes_badiv()
		{
			TestSymmetricEncrypt<TripleDESCryptoServiceProvider>(AssignMetadata, SymmetricEncryptBytes);
			TestSymmetricDecrypt<TripleDESCryptoServiceProvider>(AssignMetadataFalseIV, SymmetricDecryptBytes);
			AssertionsBytes_badiv();
		}

		[Test]
		public void TripleDESCryptoServiceProvider_stream()
		{
			TestSymmetricEncrypt<TripleDESCryptoServiceProvider>(AssignMetadata, SymmetricEncryptStream);
			TestSymmetricDecrypt<TripleDESCryptoServiceProvider>(AssignMetadata, SymmetricDecryptStream);
			AssertionsStream();
		}

		[Test]
		[ExpectedException(typeof(CryptographicException))]
		public void TripleDESCryptoServiceProvider_stream_badkey()
		{
			TestSymmetricEncrypt<TripleDESCryptoServiceProvider>(AssignMetadata, SymmetricEncryptStream);
			TestSymmetricDecrypt<TripleDESCryptoServiceProvider>(AssignMetadataFalseKey, SymmetricDecryptStream);
			AssertionsStream_badiv();
		}

		[Test]
		[ExpectedException(typeof(CryptographicException))]
		public void TripleDESCryptoServiceProvider_stream_badiv()
		{
			TestSymmetricEncrypt<TripleDESCryptoServiceProvider>(AssignMetadata, SymmetricEncryptStream);
			TestSymmetricDecrypt<TripleDESCryptoServiceProvider>(AssignMetadataFalseIV, SymmetricDecryptStream);
			AssertionsStream_badiv();
		}

		#endregion TripleDESCryptoServiceProvider

		#endregion Tests

		#region Private Methods

		private void TestSymmetricEncrypt<T>(Action assignMetadataMethod,
			Action encryptionMethod)
			where T : SymmetricAlgorithm
		{
			using (algorithm = Activator.CreateInstance<T>())
			{
				assignMetadataMethod();
				encryptionMethod();
			}
		}

		private void TestSymmetricDecrypt<T>(Action assignMetadataMethod,
			Action decryptionMethod)
			where T : SymmetricAlgorithm
		{
			using (algorithm = Activator.CreateInstance<T>())
			{
				assignMetadataMethod();
				decryptionMethod();
			}
		}

		#region Encryption/Decryption Methods
		private void SymmetricEncryptString()
		{
			mEncodedText = CryptographyHelper.SymmetricEncrypt(algorithm,
				mSampleString, mKey, mIv);
		}

		private void SymmetricEncryptBytes()
		{
			mEncodedBytes = CryptographyHelper.SymmetricEncrypt(algorithm,
				mSampleBytes, mKey, mIv);
		}

		private void SymmetricEncryptStream()
		{
			mEncodedStream = CryptographyHelper.SymmetricEncrypt(algorithm,
				mSampleStream, mKey, mIv);
		}

		private void SymmetricDecryptString()
		{
			mDecodedText = CryptographyHelper.SymmetricDecrypt(algorithm, 
				mEncodedText, mKey, mIv);
		}

		private void SymmetricDecryptBytes()
		{
			mDecodedBytes = CryptographyHelper.SymmetricDecrypt(algorithm,
				mEncodedBytes, mKey, mIv);
		}

		private void SymmetricDecryptStream()
		{
			mDecodedStream = CryptographyHelper.SymmetricDecrypt(algorithm,
				mEncodedStream, mKey, mIv);
		}
		#endregion Encryption/Decryption Methods

		#region AssignMetadata Methods
		private void AssignMetadata()
		{
			if (mKey == null)
			{
				algorithm.GenerateKey();
				mKey = algorithm.Key;
			}
			if (mIv == null)
			{
				algorithm.GenerateIV();
				mIv = algorithm.IV;
			}
		}

		private void AssignMetadataFalseKey()
		{
			AssignMetadata();
			byte[] temp = new byte[mKey.Length];
			Array.Copy(mKey, temp, mKey.Length);
			temp[0]++;
			temp[6]--;
			temp[3]+= 5;
			mKey = temp;
		}

		private void AssignMetadataFalseIV()
		{
			AssignMetadata();
			byte[] temp = new byte[mIv.Length];
			Array.Copy(mKey, temp, mIv.Length);
			temp[0]++;
			temp[6]--;
			temp[3] += 5;
			mIv = temp;
		}
		#endregion AssignMetadata Methods

		#region Assertions
		private void AssertionsString()
		{
			Assert.AreNotEqual(mSampleString, mEncodedText);
			Assert.AreEqual(mSampleString, mDecodedText);
		}

		private void AssertionsBytes()
		{
			Assert.AreNotEqual(mSampleBytes, mEncodedBytes);
			Assert.AreEqual(mSampleBytes, mDecodedBytes);
		}

		private void AssertionsStream()
		{
			Assert.AreNotEqual(mSampleStream, mEncodedStream);
			Assert.AreEqual(mSampleStream, mDecodedStream);
		}

		private void AssertionsString_badiv()
		{
			Assert.AreNotEqual(mSampleString, mEncodedText);
			Assert.AreNotEqual(mSampleString, mDecodedText);
		}

		private void AssertionsBytes_badiv()
		{
			Assert.AreNotEqual(mSampleBytes, mEncodedBytes);
			Assert.AreNotEqual(mSampleBytes, mDecodedBytes);
		}

		private void AssertionsStream_badiv()
		{
			Assert.AreNotEqual(mSampleStream, mEncodedStream);
			Assert.AreNotEqual(mSampleStream, mDecodedStream);
		}
		#endregion Assertions

		#endregion Private Methods
	}
}