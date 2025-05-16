/*Check if any permutation of the string can form a palindrome.*/

/*Input: "tactcoa"
Output: true  // permutations: "tacocat", "atcocta", etc. */


/*Count frequency of each character.

    A string can form a palindrome if:

At most one character has an odd count.*/


using System.Reflection.Metadata;

/*bool CanFormPalindrome(string str)
{
    Dictionary<char, int> charCount = new Dictionary<char, int>(); 

    foreach (char c in str)
    {
        if (charCount.ContainsKey(c))
            charCount[c]++;
        else
            charCount[c] = 1;
    }

    int oddCount = 0;

    foreach (var count in charCount.Values)
    {
        if (count % 2 != 0)
            oddCount++;
    }

    return oddCount <= 1;
}



List<string> FindAllPalindromes(string str)
{
    List<string> palindromes = new List<string>();

    for (int i = 0; i < str.Length; i++)   //ctc
    {
        for (int j = i + 1; j <= str.Length; j++)
        {
            string substring = str.Substring(i, j - i);
            if (IsPalindrome(substring))
            {
                palindromes.Add(substring);
            }
        }
    }

    return palindromes;
}

bool IsPalindrome(string str)
{
    int left = 0;
    int right = str.Length - 1;

    while (left < right)
    {
        if (str[left] != str[right])
            return false;

        left++;
        right--;
    }

    return true;
}*/