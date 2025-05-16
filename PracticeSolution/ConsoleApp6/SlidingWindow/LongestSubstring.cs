/*Problem
Given a string s, find the length of the longest substring without repeating characters.

Input: "abcabcbb"  
Output: 3  
Explanation: The longest substring is "abc" with length 3.*/

/*int LongestSubstring(string s)
{
    int n = s.Length;  
    int maxLength = 0;
    Dictionary<char, int> charIndexMap = new Dictionary<char, int>();
    int left = 0;

    for (int right = 0; right < n; right++)
    {
        if (charIndexMap.ContainsKey(s[right]))
        {
            left = Math.Max(charIndexMap[s[right]] + 1, left);
        }
        charIndexMap[s[right]] = right;  //a -> 0,b -> 1, c -> 2
        maxLength = Math.Max(maxLength, right - left + 1);
    }

    return maxLength;
}

int longerSubstring = LongestSubstring("abcabcbbxzyabcdefgabcdefghijk");

Console.WriteLine(longerSubstring); */

/*Time: O(n)

Space: O(k) where k is character set (e.g., 26 for lowercase) */

