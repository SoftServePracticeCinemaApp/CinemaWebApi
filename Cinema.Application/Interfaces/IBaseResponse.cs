using Cinema.Application.Enums;

namespace Cinema.Application.Interfaces
{
    public interface IBaseResponse<T>
    {
        string Description { get; }
        StatusCode StatusCode { get; }
        int ResultsCount { get; }
        T? Data { get; }
    }
}
