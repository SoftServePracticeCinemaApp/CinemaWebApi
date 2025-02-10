namespace Cinema.BlazorUI.Model.TMDb;

public class MovieTrailerResponse
{
    public List<VideoResult> Results { get; set; } = new();
}

public class VideoResult
{
    public string Key { get; set; } = string.Empty;
    public string Type { get; set; } = string.Empty;
} 