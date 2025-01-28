using Cinema.Application.Enums;

namespace Cinema
{
    public class BaseResponse<T>
    {
        public string Description { get; set; } = null!;
        public StatusCode StatusCode { get; set; }
        public int ResultsCount { get; set; }
        public T? Data { get; set; }
    }
}
