using Microsoft.IdentityModel.Tokens;
using System.Security.Cryptography;

namespace IdentityServer
{
	public class Crypto
	{
		public static RsaSecurityKey GenerateRsaKey()
		{
			using (var rsaCryptoServiceProvider = new RSACryptoServiceProvider(dwKeySize: 4096))
			{
				var rsaKeyInfo = rsaCryptoServiceProvider.ExportParameters(true);
				var rsaKey = new RsaSecurityKey(rsaKeyInfo);
				return rsaKey;
			}
		}
	}
}
