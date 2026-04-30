using System.Runtime.InteropServices;

namespace merge_sort;

internal class SolutionSpan
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

internal class SolutionOptimizedSpan
{
    public static List<int> SortList(List<int> unsortedList)
    {
        var span = CollectionsMarshal.AsSpan(unsortedList);

        if (span.Length <= 1)
            return unsortedList;

        var buffer = new int[span.Length];
        MergeSort(span, buffer);

        return unsortedList;
    }

    private static void MergeSort(Span<int> span, Span<int> buffer)
    {
        if (span.Length <= 1)
            return;

        if (span.Length <= 32)
        {
            InsertionSort(span);
            return;
        }

        var mid = span.Length / 2;

        var left = span[..mid];
        var right = span[mid..];

        MergeSort(left, buffer[..mid]);
        MergeSort(right, buffer[mid..]);

        Merge(left, right, buffer[..span.Length]);

        buffer[..span.Length].CopyTo(span);
    }

    private static void InsertionSort(Span<int> span)
    {
        for (var i = 1; i < span.Length; i++)
        {
            var key = span[i];
            var j = i - 1;

            while (j >= 0 && span[j] > key)
            {
                span[j + 1] = span[j];
                j--;
            }

            span[j + 1] = key;
        }
    }

    private static void Merge(
        ReadOnlySpan<int> left,
        ReadOnlySpan<int> right,
        Span<int> destination)
    {
        var l = 0;
        var r = 0;
        var i = 0;

        while (l < left.Length && r < right.Length)
        {
            destination[i++] = left[l] <= right[r]
                ? left[l++]
                : right[r++];
        }

        left[l..].CopyTo(destination[i..]);
        right[r..].CopyTo(destination[i..]);
    }
}

internal class Solution
{
    public static List<int> SortListInterval(List<int> unsortedList, int start, int end)
    {
        if (end - start <= 1) return unsortedList.GetRange(start, end - start);
        var midpoint = (start + end) / 2;
        var leftList = SortListInterval(unsortedList, start, midpoint);
        var rightList = SortListInterval(unsortedList, midpoint, end);
        List<int> resultList = new();
        int leftPointer = 0, rightPointer = 0;
        while (leftPointer < leftList.Count || rightPointer < rightList.Count)
        {
            if (leftPointer == leftList.Count)
                resultList.Add(rightList[rightPointer++]);
            else if (rightPointer == rightList.Count)
                resultList.Add(leftList[leftPointer++]);
            else if (leftList[leftPointer] <= rightList[rightPointer])
                resultList.Add(leftList[leftPointer++]);
            else
                resultList.Add(rightList[rightPointer++]);
        }

        return resultList;
    }

    public static List<int> SortList(List<int> unsortedList)
    {
        return SortListInterval(unsortedList, 0, unsortedList.Count);
    }
}