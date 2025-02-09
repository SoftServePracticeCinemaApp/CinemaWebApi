using Cinema.Application.Enums;

namespace Cinema.Application.Helpers.Interfaces
{
    public interface IResponses
    {
        IBaseResponse<T> CreateBase<T>(string description, StatusCode statusCode, T data = default, int resultsCount = 0);
        IBaseResponse<T> CreateBaseOk<T>(T data, int resultsCount);
        IBaseResponse<T> CreateBaseOk<T>(T data, int resultsCount, string description);
        IBaseResponse<T> CreateBaseBadRequest<T>(string description);
        IBaseResponse<T> CreateBaseNotFound<T>(string description);
        IBaseResponse<T> CreateBaseServerError<T>(string exceptionMessage);
        IBaseResponse<T> CreateBaseConflict<T>(string message);
    }
}