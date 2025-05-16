/*Find the length of the longest substring that
 contains at most K distinct characters.*/

/*Input: s = "eceba", k = 2
Output: 3 → "ece"*/

/*Use a sliding window.

    Use a dictionary to count characters.

    Shrink the window when the dictionary exceeds k keys*/


/*int LengthOfLongestSubstringKDistinct(string s, int k)
{
        if (k == 0 || s.Length == 0) return 0;

    Dictionary<char, int> charCount = new Dictionary<char, int>();
    int left = 0, maxLength = 0;

    for (int right = 0; right < s.Length; right++)
    {
        if (charCount.ContainsKey(s[right]))
            charCount[s[right]]++;
        else
            charCount[s[right]] = 1;

        while (charCount.Count > k)
        {
            charCount[s[left]]--;
            if (charCount[s[left]] == 0)
                charCount.Remove(s[left]);
            left++;
        }

        maxLength = Math.Max(maxLength, right - left + 1);
    }

    return maxLength;
}

int result = LengthOfLongestSubstringKDistinct("eceba", 2);
Console.WriteLine(result); */

/*Time: O(n)

Space: O(k) */