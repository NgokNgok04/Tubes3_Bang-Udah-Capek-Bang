using System;
using System.Collections.Generic;
class BM {
    protected static int NO_OF_CHARS = 256;
    protected static int[] LastOccurrence = new int[NO_OF_CHARS];
    
    protected static void Preprocess(string pattern) {
    // find the last occurrence of each character in the pattern
        for (int i = 0; i < NO_OF_CHARS; i++) {
            LastOccurrence[i] = -1;
        }
        for (int i = 0; i < pattern.Length; i++) {
            LastOccurrence[(int)pattern[i]] = i;
        }
    }
    
    public static bool BoyerMooer(string text, string pattern) {
    // function to search for pattern in text using Boyer-Moore algorithm
        Preprocess(pattern);
        int m = pattern.Length;
        int n = text.Length;
        
        int s = 0;              // s is position of start character of pattern relative to text
        while (s <= (n - m)) {
            int j = m - 1;
            while (j >= 0 && pattern[j] == text[s + j]) {   // while characters of pattern and text match
                j--;
            }

            if (j < 0) {        // pattern found
                return true;
            }
            else {              // character mismatch
                s += Math.Max(1, j - LastOccurrence[text[s + j]]);
            }
        }
        return false;
    }
}