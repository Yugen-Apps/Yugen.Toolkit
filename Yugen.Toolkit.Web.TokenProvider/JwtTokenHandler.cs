using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using Yugen.Toolkit.Web.TokenProvider.Models;

namespace Yugen.Toolkit.Web.TokenProvider
{
    public class JwtTokenHandler
    {
        public static string TokenAudience;
        public static string TokenIssuer;
        public static SymmetricSecurityKey SymmetricSecurityKey;

        private static JwtSecurityToken _token;

        public static void Init(string jwtSigningKey, string tokenAudience, string tokenIssuer)
        {
            SymmetricSecurityKey = CreateSymmetricSecurityKey(jwtSigningKey);
            TokenAudience = tokenAudience;
            TokenIssuer = tokenIssuer;
        }

        private static SymmetricSecurityKey CreateSymmetricSecurityKey(string secret) => 
            new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secret));

        public static ClaimsPrincipal ClaimsPrincipal(string name) => new ClaimsPrincipal(new[]
        {
            new ClaimsIdentity(new[]
            {
                new Claim(ClaimTypes.Name, name)
            })
        });

        public static Token BuildJwt(string claimsPrincipalName, int expires = 1440)
        {
            var credentials = new SigningCredentials(SymmetricSecurityKey, SecurityAlgorithms.HmacSha256);

            _token = new JwtSecurityToken(
                issuer: TokenIssuer,
                audience: TokenAudience,
                claims: ClaimsPrincipal(claimsPrincipalName).Claims,
                expires: DateTime.Now.AddMinutes(expires),
                signingCredentials: credentials
            );

            return new Token(_token);
        }
    }
}