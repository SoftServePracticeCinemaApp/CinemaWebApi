using Cinema.BlazorUI.Model.DTO;

namespace Cinema.BlazorUI.Model;


public class TokenInfo
{
  public required UserDto User { get; set; }
  public required string Token { get; set; }
}
