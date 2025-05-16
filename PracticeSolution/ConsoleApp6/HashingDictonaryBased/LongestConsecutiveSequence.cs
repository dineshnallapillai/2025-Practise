/*Input: [100, 4, 200, 1, 3, 2]  
Output: 4  
Explanation: The longest consecutive sequence is [1, 2, 3, 4].*/

/*int LongestConsecutive(int[] nums)
{
    HashSet<int> result = new HashSet<int>(nums);

    int longestStreak = 0;

    foreach (int num in result)
    {
        // Check if it's the start of a sequence
        if (!result.Contains(num - 1))
        {
            int currentNum = num;
            int currentStreak = 1;

            // Count the length of the streak
            while (result.Contains(currentNum + 1))
            {
                currentNum++;
                currentStreak++;
            }

            longestStreak = Math.Max(longestStreak, currentStreak);
        }
    }
}

int[] nums = { 100, 4, 200, 1, 3, 2 };
int result = LongestConsecutive(nums);*/

/*Time: O(n)

Space: O(n) – for the set */