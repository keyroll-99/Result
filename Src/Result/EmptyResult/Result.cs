namespace Result;

public class Result
{
    public bool IsSuccess { get; }

    protected Result(bool isSuccess)
    {
        IsSuccess = isSuccess;
    }

    public static Result Success()
    {
        return new Result(true);
    }

    public static Result Fail()
    {
        return new Result(false);
    }
}