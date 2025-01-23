using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cinema.Business.DTO
{
	public class LoginResponceDto
	{
		public UserDto User { get; set; }
		public string Token { get; set; }
	}
}
