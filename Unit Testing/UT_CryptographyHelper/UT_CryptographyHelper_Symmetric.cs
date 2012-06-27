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
	public abstract class UT_CryptographyHelper_Symmetric<T>
		where T : SymmetricAlgorithm
	{
		#region Fields
		
		string mSampleString;
		byte[] mSampleBytes;

		SymmetricAlgorithm algorithm;
		string mEncodedText;
		string mDecodedText;
		byte[] mEncodedBytes;
		byte[] mDecodedBytes;
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
		}

		#endregion Setup

		#region Teardown

		[TearDown]
		public void Teardown()
		{
			if (algorithm != null)
				algorithm.Dispose();

			mKey = null;
			mIv = null;
		}
		
		#endregion Teardown

		#region Tests

		[Test]
		public virtual void test_string()
		{
			TestSymmetricEncrypt(AssignMetadata, SymmetricEncryptString);
			TestSymmetricDecrypt(AssignMetadata, SymmetricDecryptString);
			AssertionsString();
		}

		[Test]
		public virtual void test_string_abbreviated()
		{
			TestSymmetricEncrypt(AssignMetadata, SymmetricEncryptString_Abbreviated1);
			TestSymmetricDecrypt(AssignMetadata, SymmetricDecryptString_Abbreviated1);
			AssertionsString();
		}

		[Test]
		public virtual void test_string_badkey()
		{
			try
			{
				TestSymmetricEncrypt(AssignMetadata, SymmetricEncryptString);
				TestSymmetricDecrypt(AssignMetadataFalseKey, SymmetricDecryptString);
				AssertionsString_fail();
			}
			catch (CryptographicException)
			{

			}
		}

		[Test]
		public virtual void test_string_badkey_abbreviated()
		{
			try
			{
				TestSymmetricEncrypt(AssignMetadata, SymmetricEncryptString_Abbreviated1);
				TestSymmetricDecrypt(AssignMetadataFalseKey, SymmetricDecryptString_Abbreviated1);
				AssertionsString_fail();
			}
			catch (CryptographicException)
			{

			}
		}

		[Test]
		public virtual void test_string_badiv()
		{
			try
			{
				TestSymmetricEncrypt(AssignMetadata, SymmetricEncryptString);
				TestSymmetricDecrypt(AssignMetadataFalseIV, SymmetricDecryptString);
				AssertionsString_fail();
			}
			catch (CryptographicException)
			{

			}
		}

		[Test]
		public virtual void test_string_badiv_abbreviated()
		{
			try
			{
				TestSymmetricEncrypt(AssignMetadata, SymmetricEncryptString_Abbreviated1);
				TestSymmetricDecrypt(AssignMetadataFalseIV, SymmetricDecryptString_Abbreviated1);
				AssertionsString_fail();
			}
			catch (CryptographicException)
			{

			}
		}

		[Test]
		public virtual void test_bytes()
		{
			TestSymmetricEncrypt(AssignMetadata, SymmetricEncryptBytes);
			TestSymmetricDecrypt(AssignMetadata, SymmetricDecryptBytes);
			AssertionsBytes();
		}

		[Test]
		public virtual void test_bytes_abbreviated()
		{
			TestSymmetricEncrypt(AssignMetadata, SymmetricEncryptBytes_Abbreviated1);
			TestSymmetricDecrypt(AssignMetadata, SymmetricDecryptBytes_Abbreviated1);
			AssertionsBytes();
		}

		[Test]
		public virtual void test_bytes_badkey()
		{
			try
			{
				TestSymmetricEncrypt(AssignMetadata, SymmetricEncryptBytes);
				TestSymmetricDecrypt(AssignMetadataFalseKey, SymmetricDecryptBytes);
				AssertionsBytes_fail();
			}
			catch (CryptographicException)
			{

			}
		}

		[Test]
		public virtual void test_bytes_badkey_abbreviated()
		{
			try
			{
				TestSymmetricEncrypt(AssignMetadata, SymmetricEncryptBytes_Abbreviated1);
				TestSymmetricDecrypt(AssignMetadataFalseKey, SymmetricDecryptBytes_Abbreviated1);
				AssertionsBytes_fail();
			}
			catch (CryptographicException)
			{

			}
		}

		[Test]
		public virtual void test_bytes_badiv()
		{
			try
			{
				TestSymmetricEncrypt(AssignMetadata, SymmetricEncryptBytes);
				TestSymmetricDecrypt(AssignMetadataFalseIV, SymmetricDecryptBytes);
				AssertionsBytes_fail();
			}
			catch (CryptographicException)
			{

			}
		}

		[Test]
		public virtual void test_bytes_badiv_abbreviated()
		{
			try
			{
				TestSymmetricEncrypt(AssignMetadata, SymmetricEncryptBytes_Abbreviated1);
				TestSymmetricDecrypt(AssignMetadataFalseKey, SymmetricDecryptBytes_Abbreviated1);
				AssertionsBytes_fail();
			}
			catch (CryptographicException)
			{

			}
		}

		#endregion Tests

		#region Private Methods

		protected void TestSymmetricEncrypt(Action assignMetadataMethod,
			Action encryptionMethod)
		{
			using (algorithm = Activator.CreateInstance<T>())
			{
				assignMetadataMethod();
				encryptionMethod();
			}
		}

		protected void TestSymmetricDecrypt(Action assignMetadataMethod,
			Action decryptionMethod)
		{
			using (algorithm = Activator.CreateInstance<T>())
			{
				assignMetadataMethod();
				decryptionMethod();
			}
		}

		#region Encryption/Decryption Methods
		protected void SymmetricEncryptString()
		{
			mEncodedText = CryptographyHelper.SymmetricEncrypt<T>(mSampleString, mKey, mIv);
		}

		protected void SymmetricEncryptString_Abbreviated1()
		{
			SymmetricAlgorithm algorithm = Activator.CreateInstance<T>();
			algorithm.Key = mKey;
			algorithm.IV = mIv;
			mEncodedText = CryptographyHelper.SymmetricEncrypt(algorithm, mSampleString);
		}

		protected void SymmetricEncryptBytes()
		{
			mEncodedBytes = CryptographyHelper.SymmetricEncrypt<T>(mSampleBytes, mKey, mIv);
		}

		protected void SymmetricEncryptBytes_Abbreviated1()
		{
			SymmetricAlgorithm algorithm = Activator.CreateInstance<T>();
			algorithm.Key = mKey;
			algorithm.IV = mIv;
			mEncodedBytes = CryptographyHelper.SymmetricEncrypt(algorithm, mSampleBytes);
		}

		protected void SymmetricDecryptString()
		{
			mDecodedText = CryptographyHelper.SymmetricDecrypt<T>(mEncodedText, mKey, mIv);
		}

		protected void SymmetricDecryptString_Abbreviated1()
		{
			SymmetricAlgorithm algorithm = Activator.CreateInstance<T>();
			algorithm.Key = mKey;
			algorithm.IV = mIv;
			mDecodedText = CryptographyHelper.SymmetricDecrypt(algorithm, mEncodedText);
		}

		protected void SymmetricDecryptBytes()
		{
			mDecodedBytes = CryptographyHelper.SymmetricDecrypt<T>(mEncodedBytes, mKey, mIv);
		}

		protected void SymmetricDecryptBytes_Abbreviated1()
		{
			SymmetricAlgorithm algorithm = Activator.CreateInstance<T>();
			algorithm.Key = mKey;
			algorithm.IV = mIv;
			mDecodedBytes = CryptographyHelper.SymmetricDecrypt(algorithm, mEncodedBytes);
		}

		#endregion Encryption/Decryption Methods

		#region AssignMetadata Methods

		protected void AssignMetadata()
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

		protected void AssignMetadataFalseKey()
		{
			AssignMetadata();
			byte[] temp = new byte[mKey.Length];
			Array.Copy(mKey, temp, mKey.Length);
			temp[0]++;
			temp[6]--;
			temp[3]+= 5;
			mKey = temp;
		}

		protected void AssignMetadataFalseIV()
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

		#endregion Private Methods
	}

	#region Implementations
	
	public class UT_CryptographyHelper_Symmetric_AesManaged :
	UT_CryptographyHelper_Symmetric<AesManaged> { }

	public class UT_CryptographyHelper_Symmetric_DESCryptoServiceProvider :
	UT_CryptographyHelper_Symmetric<DESCryptoServiceProvider> { }

	public class UT_CryptographyHelper_Symmetric_AesCryptoServiceProvider :
		UT_CryptographyHelper_Symmetric<AesCryptoServiceProvider> { }
	
	public class UT_CryptographyHelper_Symmetric_RC2CryptoServiceProvider :
	UT_CryptographyHelper_Symmetric<RC2CryptoServiceProvider> { }
	
	public class UT_CryptographyHelper_Symmetric_RijndaelManaged :
	UT_CryptographyHelper_Symmetric<RijndaelManaged> { }

	public class UT_CryptographyHelper_Symmetric_TripleDESCryptoServiceProvider :
	UT_CryptographyHelper_Symmetric<TripleDESCryptoServiceProvider> { }
	
	#endregion Implementations
}