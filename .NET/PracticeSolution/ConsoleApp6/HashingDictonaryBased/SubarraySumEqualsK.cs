/*Given an integer array nums and an integer k, return the total number of continuous subarrays
 whose sum equals to k.*/


/*Input: nums = [1, 1, 1], k = 2
Output: 2
Explanation: Subarrays[1, 1](twice) sum to 2*/


int SubarraySum(int[] nums, int k)
{
    int count = 0;
    int sum = 0;
    Dictionary<int, int> sumCount = new Dictionary<int, int>();
    sumCount[0] = 1; // Initialize with sum 0 to handle the case when a subarray starts from index 0

    for (int i = 0; i < nums.Length; i++)
    {
        sum += nums[i];

        if (sumCount.ContainsKey(sum - k))
        {
            count += sumCount[sum - k];
        }

        if (sumCount.ContainsKey(sum))
        {
            sumCount[sum]++;
        }
        else
        {
            sumCount[sum] = 1;
        }
    }

    return count;
}


/*Time: O(n)

Space: O(n) – for dictionary*/