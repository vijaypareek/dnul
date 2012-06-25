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
