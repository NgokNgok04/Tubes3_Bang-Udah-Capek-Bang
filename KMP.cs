using System;
using System.Collections.Generic;

class KMP
{
    // public static void Main()
    // {
    //     Console.WriteLine("Text: ");
    //     string text = Console.ReadLine();

    //     Console.WriteLine("Pattern: ");
    //     string pattern = Console.ReadLine();

    //     if (string.IsNullOrEmpty(text) || string.IsNullOrEmpty(pattern))
    //     {
    //         Console.WriteLine("Text and pattern must not be empty.");
    //         return;
    //     }

    //     if (pattern.Length > text.Length)
    //     {
    //         Console.WriteLine("Pattern is longer than text. No match possible.");
    //         return;
    //     }

    //     List<int> result = iterativeKMP(text, pattern);

    //     if (result.Count == 0)
    //     {
    //         Console.WriteLine("Pattern not found in the text.");
    //     }
    //     else
    //     {
    //         Console.WriteLine("Pattern found at positions: " + string.Join(", ", result));
    //     }
    // }

    public static List<int> iterativeKMP(string text, string pattern)
    {
        int m = pattern.Length;
        int n = text.Length;

        int[] lps = new int[m];
        ComputeLPSArray(pattern, m, lps);

        List<int> occurrences = new List<int>();
        int i = 0; // index for text
        int j = 0; // index for pattern

        while (i < n)
        {
            if (pattern[j] == text[i])
            {
                i++;
                j++;
            }

            if (j == m)
            {
                occurrences.Add(i - j);
                j = lps[j - 1];
            }
            else if (i < n && pattern[j] != text[i])
            {
                if (j != 0)
                {
                    j = lps[j - 1];
                }
                else
                {
                    i++;
                }
            }
        }

        return occurrences;
    }

    private static void ComputeLPSArray(string pattern, int m, int[] lps)
    {
        int length = 0;
        lps[0] = 0;

        int i = 1;
        while (i < m)
        {
            if (pattern[i] == pattern[length])
            {
                length++;
                lps[i] = length;
                i++;
            }
            else
            {
                if (length != 0)
                {
                    length = lps[length - 1];
                }
                else
                {
                    lps[i] = 0;
                    i++;
                }
            }
        }
    }
}
