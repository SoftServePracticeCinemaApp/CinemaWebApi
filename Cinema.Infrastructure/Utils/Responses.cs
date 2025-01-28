using Cinema.Application.Enums;
using Cinema.Application.Helpers.Interfaces;

namespace Cinema.Application.Helpers
{
    public class Responses : IResponses
    {
        public IBaseResponse<T> CreateBase<T>(string description, StatusCode statusCode, T data = default, int resultsCount = 0)
        {
            return new BaseResponse<T>()
            {
                ResultsCount = resultsCount,
                Data = data!,
                Description = description,
                StatusCode = statusCode
            };
        }

        public IBaseResponse<T> CreateBaseOk<T>(T data, int resultsCount)
        {
            return new BaseResponse<T>()
            {
                ResultsCount = resultsCount,
                Data = data,
                Description = "Operation completed successfully.",
                StatusCode = StatusCode.Ok
            };
        }

        public IBaseResponse<T> CreateBaseOk<T>(T data, int resultsCount, string description)
        {
            return new BaseResponse<T>()
            {
                ResultsCount = resultsCount,
                Data = data,
                Description = description,
                StatusCode = StatusCode.Ok
            };
        }

        public IBaseResponse<T> CreateBaseBadRequest<T>(string description)
        {
            return new BaseResponse<T>()
            {
                ResultsCount = 0,
                Data = default!,
                Description = description,
                StatusCode = StatusCode.BadRequest
            };
        }

        public IBaseResponse<T> CreateBaseNotFound<T>(string description)
        {
            return new BaseResponse<T>()
            {
                ResultsCount = 0,
                Data = default!,
                Description = description,
                StatusCode = StatusCode.NotFound
            };
        }

        public IBaseResponse<T> CreateBaseServerError<T>(string exceptionMessage)
        {
            return new BaseResponse<T>()
            {
                ResultsCount = 0,
                Data = default!,
                Description = "Server error, catch exception: " + exceptionMessage,
                StatusCode = StatusCode.InternalServerError
            };
        }
    }
}
