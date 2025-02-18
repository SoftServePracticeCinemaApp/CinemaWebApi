namespace Cinema.BlazorUI.Model
{
    public class Ticket
    {
        public int Id { get; set; }
        public int SessionId { get; set; }
        public string? UserId { get; set; }
        public int MovieId { get; set; }
        public int Row { get; set; }
        public bool IsBooked { get; set; }
        public bool IsSelected { get; set; }
    }
}
