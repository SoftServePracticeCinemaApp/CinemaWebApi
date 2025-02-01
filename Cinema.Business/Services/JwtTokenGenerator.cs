using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Cinema.Business.Options;
using Cinema.Business.Services.IServices;
using Cinema.Domain.Entities;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace Cinema.Business.Services
{
	public class JwtTokenGenerator : IJwtTokenGenerator
	{
		private readonly JwtOptions _jwtOptions;
		public JwtTokenGenerator(IOptions<JwtOptions> jwtOptions)
		{
			_jwtOptions = jwtOptions.Value;
		}
		public string GenerateToken(UserEntity user, IEnumerable<string> roles)
		{
			var tokenHandler = new JwtSecurityTokenHandler();
			var key = Encoding.ASCII.GetBytes(_jwtOptions.Secret);
			var claims = new List<Claim>
			{
				new Claim(JwtRegisteredClaimNames.Email,user.Email),
				new Claim(JwtRegisteredClaimNames.Sub,user.Id),
				new Claim(JwtRegisteredClaimNames.Name,user.Name),
				new Claim(JwtRegisteredClaimNames.UniqueName, user.PhoneNumber)
			};
			claims.AddRange(roles.Select(role => new Claim(ClaimTypes.Role, role)));
			var descriptor = new SecurityTokenDescriptor()
			{
				Audience = _jwtOptions.Audience,
				Issuer = _jwtOptions.Issuer,
				Subject = new ClaimsIdentity(claims),
				Expires = DateTime.UtcNow.AddDays(7),
				SigningCredentials = new(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
			};
			var token = tokenHandler.CreateToken(descriptor);
			return tokenHandler.WriteToken(token);
		}
	}
}
