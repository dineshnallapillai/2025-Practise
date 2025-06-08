/*Input: s = "egg", t = "add"      → Output: true
Input: s = "foo", t = "bar"      → Output: false
Input: s = "paper", t = "title"  → Output: true */



bool IsIsomorphic(string s, string t)
{
    if (s.Length != t.Length)
        return false;

    Dictionary<char, char> mapST = new Dictionary<char, char>();
    Dictionary<char, char> mapTS = new Dictionary<char, char>();

    for (int i = 0; i < s.Length; i++)
    {
        char c1 = s[i];
        char c2 = t[i];

        // If the character is already in the map, check if it maps to the same character
        if (mapST.ContainsKey(c1) && mapST[c1] != c2)
            return false;
        if (mapTS.ContainsKey(c2) && mapTS[c2] != c1)
            return false;

        // If the character is not in the map, add it
        mapST[c1] = c2;
        mapTS[c2] = c1;
    }
    return true;
}

/*Time: O(n)

Space: O(1) — At most 256 keys for ASCII characters */

