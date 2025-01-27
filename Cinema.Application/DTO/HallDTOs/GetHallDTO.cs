namespace Cinema.Application.DTO.HallEntityDTOs
{
    public class GetHallDTO
    {
        public int Id { get; set; }
        public List<List<int>> Seats { get; set; }
    }
}
