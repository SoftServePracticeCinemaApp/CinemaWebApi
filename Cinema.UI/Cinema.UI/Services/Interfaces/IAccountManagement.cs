using Cinema.UI.Model;

namespace Cinema.UI.Services.Interfaces;

public interface IAccountManagement
{
  Task<FormResult> RegisterAsync(string email, string password);
  Task<FormResult> LoginAsync(string email, string password);
  Task LogoutAsync();
  Task<bool> CheckAuthenticatedAsync();
}

