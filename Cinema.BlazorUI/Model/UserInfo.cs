namespace Cinema.BlazorUI.Model;

public class UserInfo
{
  public string Email { get; set; } = string.Empty;
  public string Name { get; set; } = string.Empty;
  public string PhoneNumber { get; set; } = string.Empty;
  public bool IsEmailConfirmed { get; set; }
  public Dictionary<string, string> Claims { get; set; } = [];
}