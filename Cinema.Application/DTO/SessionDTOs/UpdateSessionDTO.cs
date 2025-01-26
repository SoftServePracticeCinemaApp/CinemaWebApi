namespace Cinema.Application.DTO.SessionDTOs
{
    public class UpdateSessionDTO
    {
        public long Id { get; set; }
        public long MovieId { get; set; }
        public DateTime Date { get; set; }
        public int HallId { get; set; }
    }
}
