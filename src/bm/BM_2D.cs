using System;
using System.Collections.Generic;

class BM_2D : BM {
    private static string ArrayOfCharsToString(char[] arr) {
    // function to convert array of characters to string
        string result = "";
        foreach (char c in arr) {
            result += c;
        }
        return result;
    }

    private static string[] MatriksOfCharsToArrayOfStrings(char[,] matriks) {
    // function to convert 2D array of characters to array of strings
        string[] result = new string[matriks.GetLength(0)];
        for (int i = 0; i < matriks.GetLength(0); i++) {
            char[] arr = new char[matriks.GetLength(1)];

            for (int j = 0; j < matriks.GetLength(1); j++) {
                arr[j] = matriks[i, j];
            }

            result[i] = ArrayOfCharsToString(arr);
        }
        return result;
    }

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

    public static bool BoyerMoore2D(char[,] text, char[,] pattern) {
    // function to search for pattern in text (2 dimensional array) using Boyer-Moore algorithm
        string[] matrix = MatriksOfCharsToArrayOfStrings(text);
        string[] patternArr = MatriksOfCharsToArrayOfStrings(pattern);

        int m = patternArr.Length;
        int n = matrix.Length;

        int s = 0; // s is position of start character of pattern relative to text
        while (s <= (n - m)) {
            int j = m - 1;
            while (j >= 0 && BoyerMooer(matrix[s + j], patternArr[j])) {    // while characters of pattern and text are same
                j--;
            }

            if (j < 0) {          // pattern found
                return true;
            }
            else {                // character mismatch
                s += Math.Max(1, j - FindMatches(patternArr, matrix[s + j]));
            }
        }

        return false;
    }
}
