using Result;
using WebApiSample.Models;

namespace WebApiSample.Services;

public class UserService
{
    public Result<User> GetUserById(Guid id)
    {
        if (id == Guid.Empty)
        {
            return "Guid cannot be empty";
        }
        
        return new User
        {
            Name = "Test",
            Surname = "Wow",
            Id = id
        };
    }
}