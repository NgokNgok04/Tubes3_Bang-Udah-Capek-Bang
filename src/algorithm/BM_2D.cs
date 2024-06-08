using System;
using System.Collections.Generic;

class BM_2D : BM {
    private static int FindMatches(string[] pattern, string textNotMatched) {
    // function to find the number of matches of pattern in matriks
    // replacement for the last occurrence in one-dimensional boyer moore
        for (int i = pattern.Length - 1; i >= 0; i--) {
            if (BoyerMooer(textNotMatched, pattern[i])) {
                return i;
            }
        }
        return -1;
    }

    public static bool BoyerMoore2D(string[] text, string[] pattern) {
    // function to search for pattern in text (2 dimensional array) using Boyer-Moore algorithm
        int m = pattern.Length;
        int n = text.Length;

        int s = 0; // s is position of start character of pattern relative to text
        while (s <= (n - m)) {
            int j = m - 1;
            while (j >= 0 && BoyerMooer(text[s + j], pattern[j])) {    // while characters of pattern and text are same
                j--;
            }

            if (j < 0) {          // pattern found
                return true;
            }
            else {                // character mismatch
                s += Math.Max(1, j - FindMatches(pattern, text[s + j]));
            }
        }

        return false;
    }
}
