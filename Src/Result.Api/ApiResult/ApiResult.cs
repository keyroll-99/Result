using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Result.ApiResult;

public class ApiResult<TSuccess, TError> : Result<TSuccess, TError>
    where TSuccess : class
    where TError : class
{
    public int StatusCode { get; private set; }

    protected ApiResult(bool isSuccess, TSuccess? successModel, TError? error, int statusCode) : base(isSuccess,
        successModel, error)
    {
        StatusCode = statusCode;
    }

    public static ApiResult<TSuccess, TError> Success(TSuccess success, int statusCode = StatusCodes.Status200OK)
    {
        return new ApiResult<TSuccess, TError>(true, success, null, statusCode);
    }

    public static ApiResult<TSuccess, TError> Fail(TError error, int statusCode = StatusCodes.Status400BadRequest)
    {
        return new ApiResult<TSuccess, TError>(false, null, error, statusCode);
    }

    public ObjectResult GetObjectResult()
    {
        return new ObjectResult(IsSuccess ? SuccessModel : ErrorModel)
        {
            StatusCode = StatusCode
        };
    }

    public IResult GetResult()
    {
        return Results.Json(IsSuccess ? SuccessModel : ErrorModel, statusCode: StatusCode);
    }
    
    public static implicit operator ObjectResult(ApiResult<TSuccess, TError> result)
        => result.GetObjectResult();

    public static implicit operator ApiResult<TSuccess, TError>(TSuccess successModel)
    {
        return Success(successModel);
    }

    public static implicit operator ApiResult<TSuccess, TError>(TError error)
    {
        return Fail(error);
    }
}