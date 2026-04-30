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
    public List<int> MergeSortOptimizedSpan()
    {
        return SolutionOptimizedSpan.SortList(_input);
    }

    [Benchmark]
    public List<int> MergeSortSpan()
    {
        return SolutionSpan.SortList(_input);
    }

    [Benchmark(Baseline = true)]
    public List<int> MergeSort()
    {
        return Solution.SortList(_input);
    }

    [Benchmark]
    public List<int> ListSort()
    {
        var copy = new List<int>(_input);
        copy.Sort();
        return copy;
    }
}