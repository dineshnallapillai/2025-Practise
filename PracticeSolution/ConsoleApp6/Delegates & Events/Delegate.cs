using System;
using System.Collections.Generic;

public class Program
{
    public static void Main()
    {
        List<string> fruits = new() { "Apple", "Banana", "Orange", "Mango" };

        // Step 1: Func to get string length
        Func<string, int> getLength = s => s.Length;

        // Step 2: Predicate to check if string starts with a vowel
        Predicate<string> startsWithVowel = s =>
        {
            if (string.IsNullOrEmpty(s)) return false;
            char first = char.ToLower(s[0]);
            return "aeiou".Contains(first);
        };

        // Step 3: Filter and print
        foreach (var fruit in fruits)
        {
            if (startsWithVowel(fruit))
            {
                Console.WriteLine($"{fruit} (Length: {getLength(fruit)})");
            }
        }

        Predicate<int> isEven = x => x % 2 == 0;

        List<int> numbers = new() { 1, 2, 3, 4, 5 };
        var evenNumbers = numbers.FindAll(isEven);

        evenNumbers.ForEach(Console.WriteLine); // Output: 2 4
        

    }
}