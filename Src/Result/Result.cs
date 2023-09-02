namespace Result;

public class Result<TSuccess>
    where TSuccess : class
{
    public TSuccess? SuccessModel { get; }
    public string? ErrorModel { get; }
    public bool IsSuccess { get; }


    private Result(bool isSuccess, TSuccess? successModel, string? error)
    {
        SuccessModel = successModel;
        ErrorModel = error;
        IsSuccess = isSuccess;
    }

    public static implicit operator TSuccess?(Result<TSuccess?> result)
    {
        return result.SuccessModel;
    }

    public static implicit operator string?(Result<TSuccess> result)
    {
        return result.ErrorModel;
    }

    public static Result<TSuccess> Success(TSuccess success)
    {
        return new Result<TSuccess>(true, success, null);
    }

    public static Result<TSuccess> Fail(string errorMessage)
    {
        return new Result<TSuccess>(false, null, errorMessage);
    }

    public Result<TSuccess> OnSuccess(Action<TSuccess> action)
    {
        if (IsSuccess)
        {
            action(SuccessModel!);
        }

        return this;
    }

    public Result<TSuccess> OnError(Action<string> action)
    {
        if (!IsSuccess)
        {
            action(ErrorModel!);
        }

        return this;
    }

    public (TSuccess?, string?) Match(Func<TSuccess, TSuccess> onSuccess, Func<string, string> onError)
    {
        if (IsSuccess)
        {
            return (onSuccess(SuccessModel!), null);
        }

        return (null, onError(ErrorModel!));
    }

    public async Task<(TSuccess?, string?)> MatchAsync(Func<TSuccess, Task<TSuccess>> onSuccess,
        Func<string, Task<string>> onError)
    {
        if (IsSuccess)
        {
            return (await onSuccess(SuccessModel!), null);
        }

        return (null, await onError(ErrorModel!));
    }

    public (TSuccessResponse?, string?) Match<TSuccessResponse>(Func<TSuccess, TSuccessResponse> onSuccess,
        Func<string, string> onError)
    where TSuccessResponse: class
    {
        if (IsSuccess)
        {
            return (onSuccess(SuccessModel!), null);
        }
        

        return (null, onError(ErrorModel!));
    }
    
    public async Task<(TSuccessResponse?, string?)> MatchAsync<TSuccessResponse>(Func<TSuccess, Task<TSuccessResponse>> onSuccess,
        Func<string, Task<string>> onError)
    where TSuccessResponse: class
    {
        if (IsSuccess)
        {
            return (await onSuccess(SuccessModel!), null);
        }
        

        return (null, await onError(ErrorModel!));
    }
    
    public (TSuccessResponse?, TErrorResponse?) Match<TSuccessResponse, TErrorResponse>(
        Func<TSuccess, TSuccessResponse> onSuccess,
        Func<string, TErrorResponse> onError)
    where TSuccessResponse: class
    where TErrorResponse: class
    {
        if (IsSuccess)
        {
            return (onSuccess(SuccessModel!), null);
        }
        

        return (null, onError(ErrorModel!));
    }
    
    public async Task<(TSuccessResponse?, TErrorResponse?)> MatchAsync<TSuccessResponse, TErrorResponse>(Func<TSuccess, Task<TSuccessResponse>> onSuccess,
        Func<string, Task<TErrorResponse>> onError)
    where TSuccessResponse: class
    where TErrorResponse: class
    {
        if (IsSuccess)
        {
            return (await onSuccess(SuccessModel!), null);
        }
        

        return (null, await onError(ErrorModel!));
    }
}