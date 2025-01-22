using Cinema.Business.DTO;
using Cinema.Business.Services.IServices;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

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

		[HttpPost("register")]
		public async Task<IActionResult> Register([FromBody] RegistrationRequestDto registrationRequestDto)
		{
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
