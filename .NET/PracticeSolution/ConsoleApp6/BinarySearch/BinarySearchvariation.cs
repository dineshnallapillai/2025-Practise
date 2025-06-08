/*Given an array of integers nums sorted in ascending order, find the starting and ending position of a given target.
    If the target is not found, return [-1, -1].

Given an array of integers nums sorted in ascending order, find the starting and ending position of a given target.
    If the target is not found, return [-1, -1].*/


int[] SearchRange(int[] nums, int target)
{
    return new int[] { FindFirst(nums, target), FindLast(nums, target) };
}

int FindFirst(int[] nums, int target)
{
    int left = 0, right = nums.Length - 1;
    int index = -1;

    while (left <= right)
    {
        int mid = left + (right - left) / 2;

        if (nums[mid] >= target)
            right = mid - 1;
        else
            left = mid + 1;

        if (nums[mid] == target)
            index = mid;
    }

    return index;
}

int FindLast(int[] nums, int target)
{
    int left = 0, right = nums.Length - 1;
    int index = -1;

    while (left <= right)
    {
        int mid = left + (right - left) / 2;

        if (nums[mid] <= target)
            left = mid + 1;
        else
            right = mid - 1;

        if (nums[mid] == target)
            index = mid;
    }

    return index;
}

int[] output = SearchRange( new[]{ 5, 7, 7, 8, 8, 10 },8);

/*Time Complexity
O(log n) for each binary search

Total: O(log n)*/

