namespace Result;

public class Result : Result<object, string>
{
    public bool IsSuccess { get; }
    public string? ErrorMessage => ErrorModel;
    
    protected Result(bool isSuccess, string? errorMessage) : base(isSuccess, null, errorMessage)
    {
        IsSuccess = isSuccess;
    }

    public static Result Success()
    {
        return new Result(true, string.Empty);
    }

    public static Result Fail(string? errorMessage = null)
    {
        return new Result(false, string.Empty);
    }
}