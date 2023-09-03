namespace ResultSample.Pipes;

public class PipesSample
{
    public static void PipeSample()
    {
        Console.WriteLine("Pipes");
        var result = ResultSample.SuccessBase();

        result
            .OnSuccess(Console.WriteLine)
            .OnSuccess((response) => { response.Name = "Jhon"; })
            .OnSuccess((response) => { response.Surname = "Doe"; })
            .OnSuccess(Console.WriteLine)
            .OnError((error) =>
            {
                // Do some stuff when we got error
            });
    }
}