/*
 Problem Statement
Given an array of integers and a number k, find the maximum sum of a subarray of size k.
Input: arr = [2, 1, 5, 1, 3, 2], k = 3
Output: 9
Explanation: Subarray[5, 1, 3] has the maximum sum = 9*/
int MaxSumSubarray(int[] nums, int k)
{
    if (nums.Length < k) return 0;

    int maxSum = 0, windowSum = 0;

    // Sum of first window
    for (int i = 0; i < k; i++)
        windowSum += nums[i];

    maxSum = windowSum;

    // Slide the window
    for (int i = k; i < nums.Length; i++)
    {
        windowSum += nums[i] - nums[i - k];
        maxSum = Math.Max(maxSum, windowSum);
    }

    return maxSum;
}

// Test
int[] arr = { 2, 1, 5, 1, 3, 2 };
int k = 3;
Console.WriteLine(MaxSumSubarray(arr, k)); // Output: 9*/

/*Complexity
Time: O(n)

Space: O(1)*/