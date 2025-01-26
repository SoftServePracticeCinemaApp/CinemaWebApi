namespace Cinema.Application.DTO.TicketDTOs
{
    public class UpdateTicketDTO
    {
        public long SessionId { get; set; }
        public string? UserId { get; set; }
        public long MovieId { get; set; }
        public int Row { get; set; }
    }
}
