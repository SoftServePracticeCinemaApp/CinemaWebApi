namespace Cinema.Application.DTO.HallDTOs
{
    public class GetHallDTO
    {
        public int Id { get; set; }
        public List<List<int>> Seats { get; set; }
    }
}
