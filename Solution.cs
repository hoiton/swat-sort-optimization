namespace merge_sort;

public class Solution
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