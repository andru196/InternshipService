using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace InternshipService
{
	internal sealed class AuthOptions
	{
		public const string ISSUER = "InternshipServiceAPI";
		public const string AUDIENCE = "InternshipServiceSite";
		const string KEY = "mysupersecret_secretkey!123";
		public static SymmetricSecurityKey GetSymmetricSecurityKey() =>
			new SymmetricSecurityKey(Encoding.UTF8.GetBytes(KEY));
	}
}
