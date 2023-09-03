
namespace Result;

public class Result<TSuccess> : Result<TSuccess, string>
    where TSuccess : class
{
    protected Result(bool isSuccess, TSuccess? successModel, string? error) : base(isSuccess, successModel, error)
    {
    }

    public new static Result<TSuccess> Success(TSuccess success)
    {
        return new Result<TSuccess>(true, success, null);
    }

    public new static Result<TSuccess> Fail(string error)
    {
        return new Result<TSuccess>(false, null, error);
    }
    
    public static implicit operator Result<TSuccess>(TSuccess successModel)
    {
        return Success(successModel);
    }

    public static implicit operator Result<TSuccess>(string error)
    {
        return Fail(error);
    }
}