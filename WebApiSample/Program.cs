using Microsoft.AspNetCore.Mvc;
using Result;
using Result.Api.ApiResult;
using WebApiSample.Services;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddSingleton<UserService>();

var app = builder.Build();

app.MapGet("/", () => "Hello World!");

app.MapGet("/{userId:Guid}", ([FromRoute]Guid userId, UserService userService) =>
{
    var result = userService.GetUserById(userId).ToApiResult();

    return result.GetResult();
});

app.Run();