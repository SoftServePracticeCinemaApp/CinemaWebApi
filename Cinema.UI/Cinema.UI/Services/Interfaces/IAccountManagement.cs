using Cinema.UI.Model;

namespace Cinema.UI.Services.Interfaces;

public interface IAccountManagement
{
  Task<FormResult> RegisterAsync(string email, string password, string phone, string name);
  Task<FormResult> LoginAsync(string email, string password);
  Task LogoutAsync();
  Task<bool> CheckAuthenticatedAsync();
}

