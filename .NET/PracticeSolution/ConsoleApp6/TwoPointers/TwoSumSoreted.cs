/*Given a sorted array of integers, find two numbers such that they add up to a specific target. 
    Return their 1-based indices.*/

/*Input: numbers = [2, 7, 11, 15], target = 9
Output: [1, 2]
Explanation: 2 + 7 = 9*/


int[] TwoSumSorted(int[] numbers, int target)
{
    int left = 0, right = numbers.Length - 1;

    while (left < right)
    {
        int sum = numbers[left] + numbers[right];

        if (sum == target)
            return new int[] { left + 1, right + 1 };
        else if (sum < target)
            left++;
        else
            right--;
    }

    return Array.Empty<int>(); // no solution
}


/*Time: O(n) — single pass

Space: O(1) */
