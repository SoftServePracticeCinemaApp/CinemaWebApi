using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cinema.Business.DTO;

namespace Cinema.Business.Services.IServices
{
	public interface IAuthService
	{
		Task Register(RegistrationRequestDto registrationrequestDto);
		Task<LoginResponceDto> Login(LoginRequestDto loginrequestDto);
		Task<bool> AssignRole(string email, string role);
	}
}
