/*Input: s = "anagram", t = "nagaram"
Output: true

Input: s = "rat", t = "car"
Output: false */

bool IsValidAnagram(string s, string t)
{
    if (s.Length != t.Length)
        return false;
    Dictionary<char, int> charMapCount = new Dictionary<char, int>();

    foreach (char c in s)
    {
        if(!charMapCount.ContainsKey(c))
            charMapCount[c] = 0;

        charMapCount[c]++;
        
    }

    foreach (char c in t)
    {
        if(!charMapCount.ContainsKey(c))
            return false;

        charMapCount[c]--;

        if (charMapCount[c] < 0)
            return false;
    }

    return true;
}