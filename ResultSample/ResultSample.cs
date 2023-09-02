using Result;
using ResultSample.Models;

namespace ResultSample;

public class ResultSample
{
    public static Result<User> SuccessBase()
    {
        return Result<User>.Success(new User
        {
            Name = "name",
            Surname = "surname",
            Id = Guid.NewGuid()
        });
    }   
    
    public static Result<User> ErrorBase()
    {
        return Result<User>.Fail("Something went wrong");
    }
}