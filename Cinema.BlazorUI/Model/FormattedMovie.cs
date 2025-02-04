namespace Cinema.BlazorUI.Model
{
    public class FormattedMovie
    {
        public int Id { get; set; }
        public int SearchId { get; set; }
        public string? Title { get; set; }
        public string? Genre { get; set; }
        public string? PosterPath { get; set; }
        public List<FormattedSession>? Sessions { get; set; }
    }
}
