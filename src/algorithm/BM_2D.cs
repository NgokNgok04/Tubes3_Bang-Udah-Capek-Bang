using System;
using System.Collections.Generic;
using System.Drawing;

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

    public static bool imageBM_Algorithm(String imageText, String imagePattern) {
    // function to search for pattern in text using Knuth-Morris-Pratt algorithm
    // text and pattern get from image
    // imageText is path to image text
    // imagePattern is path to image pattern

        string[] selectedPattern = new string[30];
        try {
            /** *********************** Pattern *********************** **/
            // Load the image
            using (Image image = Image.FromFile(imagePattern)) {
                // image to binary
                byte[][] imageBytes2d = Util.ImageToByteArray(image);
                imageBytes2d = Util.twoDPattern(imageBytes2d, image.Width, image.Height);

                // using (StreamWriter writer = new StreamWriter("2dPattern.txt")) {
                //     for (int y = 0; y < image.Height; y++) {
                //         for (int x = 0; x < (image.Width / 8) * 8; x++) {
                //             writer.Write(imageBytes2d[y][x]);
                //         }
                //         writer.WriteLine(); // Move to the next line after each row
                //     }
                // }
                
                // binary to ascii 
                string[] base64String = Util.bitsToString2D(imageBytes2d);
                selectedPattern = base64String;

                // write into file (testing)
                // File.WriteAllText("asciiPattern.txt", selectedPattern);
                // Console.WriteLine("Selected Pattern: " + selectedPattern);
            }

            /** *********************** Text *********************** **/
            using (Image image = Image.FromFile(imageText)) {
                // image to binary
                byte[][] imageBytes2d = Util.ImageToByteArray(image);

                // using (StreamWriter writer = new StreamWriter("2dText.txt")) {
                //     for (int y = 0; y < image.Height; y++) {
                //         for (int x = 0; x < image.Width; x++) {
                //             writer.Write(imageBytes2d[y][x]);
                //         }
                //         writer.WriteLine(); // Move to the next line after each row
                //     }
                // }
                                
                /** *********************** KMP *********************** **/
                // search for pattern in text
                for (int i = 0; i < (image.Width % 8) + 1; i++) {
                    String[] mtx = Util.getText(imageBytes2d, i);
                    if (BoyerMoore2D(mtx, selectedPattern)) {
                        return true;
                    }
                }
            }
            return false;
        }

        catch (Exception ex) { // handle error
            // Console.WriteLine($"Error: {ex.Message}");
            return false;
        }
    }
}