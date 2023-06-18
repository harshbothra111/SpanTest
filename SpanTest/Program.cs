using BenchmarkDotNet.Running;

namespace SpanTest
{
    internal class Program
    {
        static void Main(string[] args)
        {
            BenchmarkRunner.Run<Benchmarks>();
        }
    }
}