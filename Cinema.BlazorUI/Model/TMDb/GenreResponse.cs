public class GenreResponse
{
    public List<Genre> Genres { get; set; } = new();
}

public class Genre
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
} 