using BenchmarkDotNet.Attributes;
using Microsoft.Diagnostics.Tracing.AutomatedAnalysis;
using System.Runtime.InteropServices;

namespace SpanTest
{
    [MemoryDiagnoser(false)]
    public class Benchmarks
    {
        private static readonly Random _random = new(8000);

        [Params(100, 100_000, 1_000_000)]
        public int Size { get; set; }

        private List<int> _items;

        [GlobalSetup]
        public void Setup()
        {
            _items = Enumerable.Range(1, Size).Select(x => _random.Next()).ToList();
        }

        [Benchmark]
        public void For()
        {
            for (int i = 0; i < _items.Count; i++)
            {
                _items[i] = _items[i] * 2;
            }
        }

        [Benchmark]
        public void ParallelForEach()
        {
            Parallel.ForEach(_items, number =>
            {
                int result = number * 2;
            });
        }

        [Benchmark]
        public void Span()
        {
            Span<int> spans = CollectionsMarshal.AsSpan(_items);
            for (int i=0; i < spans.Length; i++)
            {
                spans[i] = spans[i] * 2;
            }
        }
    }
}
