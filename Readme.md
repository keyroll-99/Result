# General about Result packages

The Result and Api.Result are packages witch to have help programmer works
with heavy exceptions

## Result

It's a lighter version of the result, with this package instead of
throwing an exception, we can just return an error in the result object.
When we return result objects we can use pipes, for different action on success and error.

## ApiResult

It's a package with works exactly same as Result, but this package has
a additional methods to work with asp.net like ``GetObjectResult()``

## Types of object

1. Result - This object the base object should be used to return feedback on whether the action was successful
2. Result<TSuccessModel> - This object has a custom successModel, but error still is a string
3. Result<TSuccessModel, TErrorModel> - This object has a custom successModel like a previous model, but this have also
   custom errorModel
4. ApiResult - It's extension of Result object, with additional methods to work with IResul and ObjectResult
5. ApiResult<TSuccessModel> - It's extension of the Result<TSuccessModel> with additional methods to work with IResul and
   ObjectResult
6. ApiResult<TSuccessModel, TErrorModel> - Like previous object, but have also custom error model

## Match

Match has a few overloading. Each methods have two variants one asynchronic and second synchronic. And match always
return a tuple with two values, first is a success value, second is a error value.

1. Match - Give a two functions, first is a function to execute on success, second is a function to execute on error.
   And return TSuccesModel and TErrorModel
2. Match - Give a two functions, first is a function to execute on success, second is a function to execute on error.
   And return CustomSuccessModel and TErrorModel
3. Match - Give a two functions, first is a function to execute on success, second is a function to execute on error.
   And return CustomSuccessModel and CustomErrorModel

Example of usage:

```csharp
        var (user, error) = await result.MatchAsync(async response =>
            {
                Console.WriteLine(response);
                await Task.CompletedTask;
                return response;
            },
            async error =>
            {
                Console.WriteLine($"error {error}");
                await Task.CompletedTask;
                return error;
            });

```

## Pipes

Pipes are a method to execute a few methods on success or error. Pipes are an extension method for Result and ApiResult
objects.
Like a Match pipes have asynchronic and synchronic variants.

1. OnSuccess method as a parameter takes a function to execute on success, and returns this
2. OnError method as a parameter take a function to execute on error, and return this

Example of usage:

```csharp
        var result = await result.OnSuccessAsync(async response =>
            {
                Console.WriteLine(response);
                await Task.CompletedTask;
                return response;
            })
            .OnErrorAsync(async error =>
            {
                Console.WriteLine($"error {error}");
                await Task.CompletedTask;
                return error;
            });
```

## Special Empty result type

This type was created to return an empty result with errorMessage as a string. This object has props like

1. IsSuccess
2. ErrorMessage - which returns the same as ErrorModel
3. ErrorModel - keep error message
4. SuccessModel - keep an empty object

## Implicit operators

All objects have implicit operators to convert to TSuccessModel, TErrorModel. So you don't have to create the object. If
TSuccessModel is different that TErrorModel you can just return the model then the result class automatically casts to
result<TSuccessModel, TError> which set props
