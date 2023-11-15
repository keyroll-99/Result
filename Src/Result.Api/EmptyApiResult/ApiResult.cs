using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Result.ApiResult;


public class ApiResult : Result
{
    public int StatusCode { get; private set; }


    protected ApiResult(bool isSuccess, int statusCode) : base(isSuccess)
    {
        StatusCode = statusCode;
    }

    public static ApiResult Success(int statusCode = StatusCodes.Status200OK)
    {
        return new ApiResult(true, statusCode);
    }

    public static ApiResult Fail(int statusCode = StatusCodes.Status400BadRequest)
    {
        return new ApiResult(false, statusCode);
    }
    
    public ObjectResult GetObjectResult()
    {
        return new ObjectResult(null)
        {
            StatusCode = StatusCode
        };
    }

    public IResult GetResult()
    {
        return Results.Json(null , statusCode: StatusCode);
    }
    
    public static implicit operator ObjectResult(ApiResult result)
        => result.GetObjectResult();
}