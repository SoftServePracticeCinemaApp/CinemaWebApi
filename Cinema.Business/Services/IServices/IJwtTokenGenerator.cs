using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cinema.Domain.Entities;

namespace Cinema.Business.Services.IServices
{
	public interface IJwtTokenGenerator
	{
		string GenerateToken(UserEntity user, IEnumerable<string> roles);
	}
}
