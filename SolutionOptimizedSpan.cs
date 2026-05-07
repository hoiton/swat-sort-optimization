using System.Runtime.InteropServices;

namespace merge_sort;

public class SolutionOptimizedSpan
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

        if (span.Length == 2)
        {
            if (span[0] > span[1])
            {
                var temp = span[0];
                span[0] = span[1];
                span[1] = temp;
            }

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
