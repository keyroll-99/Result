namespace Result.Api.ApiResult;

public static class Extensions
{
    public static ApiResult<TSuccess, TError> ToApiResult<TSuccess, TError>(this Result<TSuccess, TError> result)
    where TSuccess: class
    where TError: class
    {
        return result.IsSuccess ? ApiResult<TSuccess, TError>.Success(result) : ApiResult<TSuccess, TError>.Fail(result);
    }
    
    public static ApiResult<TSuccess, string> ToApiResult<TSuccess>(Result<TSuccess> result)
        where TSuccess: class
    {
        return result.IsSuccess ? ApiResult<TSuccess, string>.Success(result) : ApiResult<TSuccess, string>.Fail(result);
    }
}