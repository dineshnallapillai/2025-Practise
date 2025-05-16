/*Given an integer array nums, move all 0's to the end while maintaining the 
relative order of the non-zero elements. */

/*Input: [0, 1, 0, 3, 12]
Output: [1, 3, 12, 0, 0]*/

/*void MoveZeroes(int[] nums)
{
    int lastNonZeroIndex = 0;

    // First pass: move non-zero elements to the front
    for (int i = 0; i < nums.Length; i++)
    {
        if (nums[i] != 0)
        {
            nums[lastNonZeroIndex] = nums[i];
            lastNonZeroIndex++;
        }
    }

    // Second pass: fill the rest with zeros
    for (int i = lastNonZeroIndex; i < nums.Length; i++)
    {
        nums[i] = 0;
    }
}*/

/*Time: O(n)

Space: O(1) – done in-place*/

