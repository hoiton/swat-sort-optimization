using BenchmarkDotNet.Attributes;

namespace merge_sort;

[MemoryDiagnoser]
[ShortRunJob]
public class MergeSortBenchmark
{
    private List<int> _input = [];

    [Params(10, 100, 1_000, 10_000)] public int Size { get; set; }

    [GlobalSetup]
    public void Setup()
    {
        var random = new Random(42);
        _input = Enumerable.Range(0, Size)
            .Select(_ => random.Next())
            .ToList();
    }

    [Benchmark]
    public List<int> List_Sort()
    {
        var copy = new List<int>(_input);
        copy.Sort();
        return copy;
    }

    [Benchmark(Baseline = true)]
    public List<int> Example()
    {
        return Solution.SortList(_input);
    }

    [Benchmark]
    public List<int> Optimized_SimpleSpans()
    {
        return SolutionSpan.SortList(_input);
    }

    [Benchmark]
    public List<int> Optimized_SpansOptimized()
    {
        return SolutionOptimizedSpan.SortList(_input);
    }

    [Benchmark]
    public List<int> Optimized_SpansOptimized_WithInsertionSort()
    {
        return SolutionOptimizedSpanWithInsertionSort
            .SortList(_input);
    }
}