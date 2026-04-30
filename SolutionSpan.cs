using System.Runtime.InteropServices;

namespace merge_sort;

public class SolutionSpan
{
    public static ReadOnlySpan<int> SortListInterval(ReadOnlySpan<int> unsortedList, int start, int end)
    {
        if (end - start <= 1) return unsortedList.Slice(start, end - start);
        var midpoint = (start + end) / 2;
        var leftList = SortListInterval(unsortedList, start, midpoint);
        var rightList = SortListInterval(unsortedList, midpoint, end);
        var resultList = new int[leftList.Length + rightList.Length];
        int leftPointer = 0, rightPointer = 0;
        for (var i = 0; i < leftList.Length + rightList.Length; i++)
        {
            if (leftPointer == leftList.Length)
                resultList[i] = rightList[rightPointer++];
            else if (rightPointer == rightList.Length)
                resultList[i] = leftList[leftPointer++];
            else if (leftList[leftPointer] <= rightList[rightPointer])
                resultList[i] = leftList[leftPointer++];
            else
                resultList[i] = rightList[rightPointer++];
        }

        return resultList.AsSpan();
    }

    public static List<int> SortList(List<int> unsortedList)
    {
        return SortListInterval(CollectionsMarshal.AsSpan(unsortedList), 0, unsortedList.Count).ToArray().ToList();
    }
}
