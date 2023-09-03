namespace ResultSample.Match;

public static class MatchSample
{
    public static void Match()
    {
        Console.WriteLine("Match");
        var result = ResultSample.SuccessBase();

        var (user, error) = result.Match(response =>
            {
                Console.WriteLine(response);
                return response;
            },
            error =>
            {
                Console.WriteLine($"error {error}");
                return error;
            });
    }

    public static async Task MatchAsync()
    {
        Console.WriteLine("Match async");
        var result = ResultSample.SuccessBase();

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
    }
}