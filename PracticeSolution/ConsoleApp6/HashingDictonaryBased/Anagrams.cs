/*Input: ["eat", "tea", "tan", "ate", "nat", "bat"]
Output:
[
["eat","tea","ate"],
["tan","nat"],
["bat"]
    ]*/

/*IList<IList<string>> GroupAnagrams(string[] strs)
{
    var map = new Dictionary<string, List<string>>();

    foreach (string str in strs)
    {
        char[] chars = str.ToCharArray();
        Array.Sort(chars);
        string sorted = new string(chars);

        if (!map.ContainsKey(sorted))
            map[sorted] = new List<string>();

        map[sorted].Add(str);
    }

    return map.Values.ToList<IList<string>>();
}

GroupAnagrams(new string[] { "eat", "tea", "tan", "ate", "nat", "bat" });*/

/*Time: O(n * k log k), where n = number of strings, k = max length of a string

Space: O(nk) for dictionary */

