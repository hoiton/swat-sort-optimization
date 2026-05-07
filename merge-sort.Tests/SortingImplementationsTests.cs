using System.Collections.Generic;

namespace merge_sort.Tests;

public class SortingImplementationsTests
{
    public static TheoryData<string, Func<List<int>, List<int>>> AllImplementations =>
        new()
        {
            { nameof(Solution), Solution.SortList },
            { nameof(SolutionSpan), SolutionSpan.SortList },
            { nameof(SolutionOptimizedSpan), SolutionOptimizedSpan.SortList },
            { nameof(SolutionOptimizedSpanWithInsertionSort), SolutionOptimizedSpanWithInsertionSort.SortList }
        };

    [Theory]
    [MemberData(nameof(AllImplementations))]
    public void SortList_SortsNumbersInAscendingOrder(
        string implementationName,
        Func<List<int>, List<int>> sort)
    {
        _ = implementationName;
        var input = new List<int> { 5, -1, 0, 5, 2, int.MinValue, 3 };

        var result = sort(input);

        Assert.Equal(
            new List<int> { int.MinValue, -1, 0, 2, 3, 5, 5 },
            result);
    }

    [Theory]
    [MemberData(nameof(AllImplementations))]
    public void SortList_HandlesEmptyAndSingleItemLists(
        string implementationName,
        Func<List<int>, List<int>> sort)
    {
        _ = implementationName;
        Assert.Equal([], sort([]));
        Assert.Equal([42], sort([42]));
    }
}
