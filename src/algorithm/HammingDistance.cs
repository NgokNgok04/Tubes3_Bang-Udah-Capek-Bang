using System;
using System.Collections.Generic;

class HammingDistance {
    public static float CalculateHammingDistance(char[,] str1, char[,] str2) {
        // function to calculate hamming distance between two matrices of characters
        int numberOfCharacters = str1.GetLength(0) * str1.GetLength(1);
        float distance = 0;
        for (int i = 0; i < str1.GetLength(0); i++) {
            for (int j = 0; j < str1.GetLength(1); j++) {
                if (str1[i, j] != str2[i, j]) {
                    distance += 1;
                }
            }
        }
        return (float)(1 - (distance / numberOfCharacters));
    }
}