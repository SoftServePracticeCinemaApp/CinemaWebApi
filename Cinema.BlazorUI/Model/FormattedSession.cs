namespace Cinema.BlazorUI.Model
{
    public class FormattedSession
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public int HallId { get; set; }
        public string? HallName { get; set; }
        public int MovieId { get; set; }
        public string? MovieTitle { get; set; }
        public string? MovieImageUrl { get; set; }
    }
}
