// See https://aka.ms/new-console-template for more information

using ResultSample.Match;
using ResultSample.Pipes;

namespace ResultSample;

internal class Program
{
    public static void Main(string[] args)
    {
        PipesSample.PipeSample();
        
        MatchSample.Match();

        MatchSample.MatchAsync().Wait();
    }
}

