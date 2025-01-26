using Cinema.Application.DTO.MovieDTOs;
using Cinema.Application.DTO.SessionDTOs;
namespace Cinema.Application.DTO.TicketDTOs
{
    public class GetTicketDTO
    {
        public long Id { get; set; }
        public long SessionId { get; set; }
        public string? UserId { get; set; }
        public long MovieId { get; set; }
        public int Row { get; set; }
        public GetSessionDTO? Session { get; set; }
        public GetMovieDTO? Movie { get; set; }
    }
}
