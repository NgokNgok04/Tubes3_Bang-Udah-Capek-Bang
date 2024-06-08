using System;
using System.Collections.Generic;

class HammingDistance {
    public static float CalculateHammingDistance(char[][] str1, char[][] str2) {
        // function to calculate hamming distance between two matrices of characters
        int numberOfCharacters = str1.Length * str1.Length;
        float distance = 0;
        for (int i = 0; i < str1.Length; i++) {
            for (int j = 0; j < str1.Length; j++) {
                if (str1[i][j] != str2[i][j]) {
                    distance += 1;
                }
            }
        }
        return (float)(1 - (distance / numberOfCharacters));
    }
}