namespace Cinema.BlazorUI.Model.TMDb;

public class MovieCreditsResponse
{
    public List<CastMember> Cast { get; set; } = new();
}

public class CastMember
{
    public string Name { get; set; } = string.Empty;
    public string Character { get; set; } = string.Empty;
} 