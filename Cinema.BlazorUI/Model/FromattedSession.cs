namespace Cinema.BlazorUI.Model
{
    public class FromattedSession
    {
        public int Id { get; set; }
        public int MovieId { get; set; }
        public DateTime Date { get; set; }
        public int HallId { get; set; }
        public double TicketPrice { get; set; }
    }
}
