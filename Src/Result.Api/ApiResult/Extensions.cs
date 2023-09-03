namespace Result.Api.ApiResult;

public static class Extensions
{
    public static ApiResult<TSuccess, TError> ToApiResult<TSuccess, TError>(this Result<TSuccess, TError> result)
    where TSuccess: class
    where TError: class
    {
        if (result.IsSuccess)
        {
            return ApiResult<TSuccess, TError>.Success(result);
        }

        return ApiResult<TSuccess, TError>.Fail(result);
    }
    
    public static ApiResult<TSuccess, string> ToApiResult<TSuccess>(Result<TSuccess> result)
        where TSuccess: class
    {
        if (result.IsSuccess)
        {
            return ApiResult<TSuccess, string>.Success(result);
        }

        return ApiResult<TSuccess, string>.Fail(result);
    }
}