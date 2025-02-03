using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Cinema.Business.DTO;
using Cinema.Business.Services.IServices;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace Cinema.Auth.Controllers
{
	[Route("api/auth")]
	[ApiController]
	public class AuthController : ControllerBase
	{
		private readonly IAuthService _authService;

		public AuthController(IAuthService authService)
		{
			_authService = authService;
		}

		[HttpGet("manage/info")]
		public async Task<UserInfoDto> GetInfo(string token)
		{
			var tokenHandler = new JwtSecurityTokenHandler();
			var securityToken = (JwtSecurityToken)tokenHandler.ReadToken(token);
			if (securityToken.ValidTo < DateTime.UtcNow)
			{
				var user = new UserInfoDto();
				user.Name = securityToken.Claims.FirstOrDefault(u => u.Type == JwtRegisteredClaimNames.Name).Value;
				user.Email = securityToken.Claims.FirstOrDefault(u => u.Type == JwtRegisteredClaimNames.Email).Value;
				user.Role = securityToken.Claims.FirstOrDefault(u => u.Type == ClaimTypes.Role).Value;
				user.PhoneNumber = securityToken.Claims.FirstOrDefault(u => u.Type == JwtRegisteredClaimNames.UniqueName).Value;
				return user;
			}
			return null;
		}

		[HttpPost("register")]
		public async Task<IActionResult> Register([FromBody] RegistrationRequestDto registrationRequestDto)
		{
						Console.WriteLine(registrationRequestDto.Email);
			System.Console.WriteLine(registrationRequestDto.Name);
			System.Console.WriteLine(registrationRequestDto.LastName);
			System.Console.WriteLine(registrationRequestDto.PhoneNumber);
			System.Console.WriteLine(registrationRequestDto.Role);
			try
			{
				await _authService.Register(registrationRequestDto);
				return Ok();
			}
			catch (Exception ex)
			{
				return BadRequest(ex.Message);
			}
		}

		[HttpPost("login")]
		public async Task<IActionResult> Login([FromBody] LoginRequestDto loginRequestDto)
		{
			try
			{
				var loginresponce = await _authService.Login(loginRequestDto);
				if (loginresponce.User == null)
				{
					return BadRequest("Username or password is incorrect");
				}
				return Ok(loginresponce);
			}
			catch (Exception ex)
			{
				return BadRequest(ex.Message);
			}
		}

		[HttpPost("AssignRole")]
		public async Task<IActionResult> AssignRole([FromBody] RegistrationRequestDto registrationRequestDto)
		{
			try
			{
				var assignRoleSuccessful = await _authService.AssignRole(registrationRequestDto.Email, registrationRequestDto.Role.ToUpper());

				if (!assignRoleSuccessful)
				{
					return BadRequest("Error occured while assigning the role");
				}
				return Ok();
			}
			catch (Exception ex)
			{
				return BadRequest(ex.Message);
			}
		}
	}
}
