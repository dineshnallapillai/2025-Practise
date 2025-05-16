/*Given an array of positive integers nums and an integer target, return the minimum length of a subarray whose sum is ≥ target.
 If no such subarray exists, return 0.*/


/*Input: target = 7, nums = [2,3,1,2,4,3]  
Output: 2 → [4,3]Input: target = 7, nums = [2,3,1,2,4,3]  
Output: 2 → [4,3]*/

int MinSubArrayLen(int target, int[] nums)
{
    int minLen = int.MaxValue, sum = 0, start = 0;

    for (int end = 0; end < nums.Length; end++)
    {
        sum += nums[end];

        while (sum >= target)
        {
            minLen = Math.Min(minLen, end - start + 1);
            sum -= nums[start++];
        }
    }

    return minLen == int.MaxValue ? 0 : minLen;
}


/*Time: O(n)

Space: O(1)*/


