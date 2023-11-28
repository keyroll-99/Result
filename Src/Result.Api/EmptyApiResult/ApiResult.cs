﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Result.ApiResult;


public class ApiResult : Result
{
    public int StatusCode { get; private set; }


    private ApiResult(bool isSuccess, int statusCode, string errorMessage) : base(isSuccess, errorMessage)
    {
        StatusCode = statusCode;
    }

    public static ApiResult Success(int statusCode = StatusCodes.Status200OK)
    {
        return new ApiResult(true, statusCode, string.Empty);
    }

    public static ApiResult Fail(int statusCode = StatusCodes.Status400BadRequest, string? errorMessage = null)
    {
        return new ApiResult(false, statusCode, errorMessage);
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