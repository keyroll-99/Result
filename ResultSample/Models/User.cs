namespace ResultSample.Models;

public class User
{
    public Guid Id { get; set; }
    public required string Name { get; set; }
    public required string Surname { get; set; }
}