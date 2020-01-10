using System;
using System.IdentityModel.Tokens.Jwt;

namespace Yugen.Toolkit.Web.TokenProvider.Models
{
    public class Token
    {
        public Token(JwtSecurityToken token)
        {
            ValidTo = token.ValidTo;
            Value = new JwtSecurityTokenHandler().WriteToken(token);
        }

        public string Value { get; set; }

        public DateTimeOffset ValidTo { get; set; }
    }
}