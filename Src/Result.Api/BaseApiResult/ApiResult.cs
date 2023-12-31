﻿using Microsoft.AspNetCore.Http;

namespace Result.ApiResult;

public class ApiResult<TSuccess> : ApiResult<TSuccess, string>
    where TSuccess : class
{
    protected ApiResult(bool isSuccess, TSuccess? successModel, string? error, int statusCode) : base(isSuccess,
        successModel, error, statusCode)
    {
    }
    
    public new static ApiResult<TSuccess> Success(TSuccess success, int statusCode = StatusCodes.Status200OK)
    {
        return new ApiResult<TSuccess>(true, success, null, statusCode);
    }

    public new static ApiResult<TSuccess> Fail(string error, int statusCode = StatusCodes.Status400BadRequest)
    {
        return new ApiResult<TSuccess>(false, null, error, statusCode);
    }
    
    public static implicit operator string(ApiResult<TSuccess> result)
        => result.ErrorModel!;

    public static implicit operator TSuccess(ApiResult<TSuccess> result)
        => result.SuccessModel!;
    
    public static implicit operator ApiResult<TSuccess>(string error)
        => ApiResult<TSuccess>.Fail(error);
    
    public static implicit operator ApiResult<TSuccess>(TSuccess successModel)
        => ApiResult<TSuccess>.Success(successModel);
}