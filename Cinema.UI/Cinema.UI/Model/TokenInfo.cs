namespace Cinema.UI.Model;


public class TokenInfo
{
  public required string TokenType { get; set; }
  public required string AccessToken { get; set; }
  public required int ExpiresIn { get; set; }
  public required string RefreshToken { get; set; }
}
