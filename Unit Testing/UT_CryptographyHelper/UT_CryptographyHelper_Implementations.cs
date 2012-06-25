using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Security.Cryptography;
using NUnit.Framework;

namespace UT_CryptographyHelper
{
	public class UT_CryptographyHelper_AesManaged :
		UT_CryptographyHelper<AesManaged>
	{
	}
	public class UT_CryptographyHelper_DESCryptoServiceProvider :
	UT_CryptographyHelper<DESCryptoServiceProvider>
	{
		//[Test]
		//[ExpectedException(typeof(CryptographicException))]
		//public override void test_bytes_badkey()
		//{
		//    TestSymmetricEncrypt(AssignMetadata, SymmetricEncryptBytes);
		//    TestSymmetricDecrypt(AssignMetadataFalseKey, SymmetricDecryptBytes);
		//    AssertionsBytes_badiv();
		//}
		//[Test]
		//[ExpectedException(typeof(CryptographicException))]
		//public override void test_bytes_badiv()
		//{
		//    TestSymmetricEncrypt(AssignMetadata, SymmetricEncryptBytes);
		//    TestSymmetricDecrypt(AssignMetadataFalseIV, SymmetricDecryptBytes);
		//    AssertionsBytes_badiv();
		//}
	}
	public class UT_CryptographyHelper_RC2CryptoServiceProvider :
	UT_CryptographyHelper<RC2CryptoServiceProvider>
	{
	}
	public class UT_CryptographyHelper_RijndaelManaged :
	UT_CryptographyHelper<RijndaelManaged>
	{
	}
	public class UT_CryptographyHelper_TripleDESCryptoServiceProvider :
	UT_CryptographyHelper<TripleDESCryptoServiceProvider>
	{
	}
}
