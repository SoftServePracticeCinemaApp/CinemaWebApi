using Cinema.Application.Enums;
using Cinema.Application.Helpers.Interfaces;

namespace Cinema
{
    public class BaseResponse<T> : IBaseResponse<T>
    {
        public string Description { get; set; } = null!;
        public StatusCode StatusCode { get; set; }
        public int ResultsCount { get; set; }
        public T? Data { get; set; }
    }
}
