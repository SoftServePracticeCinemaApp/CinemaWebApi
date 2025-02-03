using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cinema.Business.DTO;
using Cinema.Business.Interfaces;
using Cinema.Business.Services.IServices;
using Cinema.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;

namespace Cinema.Business.Services
{
	public class AuthService : IAuthService
	{
		private readonly IUserRepository _userRepository;
		private UserManager<UserEntity> _userManager;
		private RoleManager<IdentityRole> _roleManager;
		private readonly IJwtTokenGenerator _jwtTokenGenerator;

		public AuthService(IUserRepository userRepository, UserManager<UserEntity> userManager, RoleManager<IdentityRole> roleManager,
			IJwtTokenGenerator jwtTokenGenerator)
		{
			_userRepository = userRepository;
			_userManager = userManager;
			_roleManager = roleManager;
			_jwtTokenGenerator = jwtTokenGenerator;
		}

		public async Task<LoginResponceDto> Login(LoginRequestDto loginrequestDto)
		{
			var user = await _userRepository.GetAsync(u => u.UserName.ToLower() == loginrequestDto.UserName.ToLower());
			bool isValid = await _userManager.CheckPasswordAsync(user, loginrequestDto.Password);

			if (!isValid || user == null)
			{
				return new LoginResponceDto()
				{
					User = null,
					Token = ""
				};
			}
			var roles = await _userManager.GetRolesAsync(user);
			var token = _jwtTokenGenerator.GenerateToken(user, roles);
			UserDto userDto = new()
			{
				Email = user.Email,
				Id = user.Id,
				Name = user.Name,
				PhoneNumber = user.PhoneNumber

			};

			LoginResponceDto loginResponceDto = new LoginResponceDto()
			{
				User = userDto,
				Token = token
			};
			return loginResponceDto;
		}

		public async Task Register(RegistrationRequestDto registrationrequestDto)
		{
			UserEntity userToCreate = new()
			{
				UserName = registrationrequestDto.Email,
				Email = registrationrequestDto.Email,
				NormalizedEmail = registrationrequestDto.Email.ToUpper(),
				PhoneNumber = registrationrequestDto.PhoneNumber,
				Name = registrationrequestDto.Name,
			};
			try
			{
				var result = await _userManager.CreateAsync(userToCreate, registrationrequestDto.Password);
				if (result.Succeeded)
				{
					var user = await _userRepository.GetAsync(u => u.Email == registrationrequestDto.Email);
					UserDto userDto = new()
					{
						Email = user.Email,
						Id = user.Id,
						Name = user.Name,
						PhoneNumber = user.PhoneNumber

					};

					// return userDto;
					// await AssignRole(user.Email, registrationrequestDto.Role);
				}
				else {
					throw new Exception(result.Errors.FirstOrDefault().Description);
				}
			}
			catch (Exception ex)
			{
				Console.WriteLine(ex.Message);
				throw new Exception(ex.Message);
			}

		}

		public async Task<bool> AssignRole(string email, string role)
		{
			var user = await _userRepository.GetAsync(u => u.UserName.ToLower() == email.ToLower());
			if (user != null)
			{
				if (!_roleManager.RoleExistsAsync(role).GetAwaiter().GetResult())
				{
					await _roleManager.CreateAsync(new IdentityRole(role));
				}
				await _userManager.AddToRoleAsync(user, role);
				return true;
			}
			return false;
		}
	}
}
