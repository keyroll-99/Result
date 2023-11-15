namespace Result;

public class Result<TSuccess, TError> : Result
    where TSuccess : class
    where TError : class
{
    public TSuccess? SuccessModel { get; }
    public TError? ErrorModel { get; }


    protected Result(bool isSuccess, TSuccess? successModel, TError? error) : base(isSuccess)
    {
        SuccessModel = successModel;
        ErrorModel = error;
    }

    public static implicit operator TSuccess?(Result<TSuccess, TError> result)
    {
        return result.SuccessModel;
    }

    public static implicit operator TError?(Result<TSuccess, TError> result)
    {
        return result.ErrorModel;
    }

    public static implicit operator Result<TSuccess, TError>(TSuccess successModel)
    {
        return Success(successModel);
    }

    public static implicit operator Result<TSuccess, TError>(TError error)
    {
        return Fail(error);
    }

    public static Result<TSuccess, TError> Success(TSuccess success)
    {
        return new Result<TSuccess, TError>(true, success, null);
    }

    public static Result<TSuccess, TError> Fail(TError error)
    {
        return new Result<TSuccess, TError>(false, null, error);
    }

    public Result<TSuccess, TError> OnSuccess(Action<TSuccess> action)
    {
        if (IsSuccess)
        {
            action(SuccessModel!);
        }

        return this;
    }

    public Result<TSuccess, TError> OnError(Action<TError> action)
    {
        if (!IsSuccess)
        {
            action(ErrorModel!);
        }

        return this;
    }

    public (TSuccess?, TError?) Match(Func<TSuccess, TSuccess> onSuccess, Func<TError, TError> onError)
    {
        if (IsSuccess)
        {
            return (onSuccess(SuccessModel!), null);
        }

        return (null, onError(ErrorModel!));
    }

    public async Task<(TSuccess?, TError?)> MatchAsync(
        Func<TSuccess, Task<TSuccess>> onSuccess,
        Func<TError, Task<TError>> onError)
    {
        if (IsSuccess)
        {
            return (await onSuccess(SuccessModel!), null);
        }

        return (null, await onError(ErrorModel!));
    }

    public (TSuccessResponse?, TError?) Match<TSuccessResponse>(
        Func<TSuccess, TSuccessResponse> onSuccess,
        Func<TError, TError> onError
    )
        where TSuccessResponse : class
    {
        if (IsSuccess)
        {
            return (onSuccess(SuccessModel!), null);
        }


        return (null, onError(ErrorModel!));
    }

    public async Task<(TSuccessResponse?, TError?)> MatchAsync<TSuccessResponse>(
        Func<TSuccess, Task<TSuccessResponse>> onSuccess,
        Func<TError, Task<TError>> onError)
        where TSuccessResponse : class
    {
        if (IsSuccess)
        {
            return (await onSuccess(SuccessModel!), null);
        }


        return (null, await onError(ErrorModel!));
    }

    public (TSuccessResponse?, TErrorResponse?) Match<TSuccessResponse, TErrorResponse>(
        Func<TSuccess, TSuccessResponse> onSuccess,
        Func<TError, TErrorResponse> onError)
        where TSuccessResponse : class
        where TErrorResponse : class
    {
        if (IsSuccess)
        {
            return (onSuccess(SuccessModel!), null);
        }

        return (null, onError(ErrorModel!));
    }

    public async Task<(TSuccessResponse?, TErrorResponse?)> MatchAsync<TSuccessResponse, TErrorResponse>(
        Func<TSuccess, Task<TSuccessResponse>> onSuccess,
        Func<TError, Task<TErrorResponse>> onError)
        where TSuccessResponse : class
        where TErrorResponse : class
    {
        if (IsSuccess)
        {
            return (await onSuccess(SuccessModel!), null);
        }

        return (null, await onError(ErrorModel!));
    }
}