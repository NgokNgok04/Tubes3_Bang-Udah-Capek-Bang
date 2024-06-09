using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Printing;
using System.Security.Cryptography;
using System.Security.Principal;

class KMP {
    private static void ComputeLPSArray(string pattern, int m, int[] lps) {
        // function to compute the longest proper prefix which is also suffix
        int length = 0;
        lps[0] = 0;

        int i = 1;
        while (i < m) {
            if (pattern[i] == pattern[length]) {
                length++;
                lps[i] = length;
                i++;
            }

            else {
                if (length != 0) {
                    length = lps[length - 1];
                }
                else {
                    lps[i] = 0;
                    i++;
                }
            }
        }
    }
    public static bool KMP_Algorithm(string text, string pattern) {
        // function to search for pattern in text using Knuth-Morris-Pratt algorithm
        int m = pattern.Length;
        int n = text.Length;
        int[] lps = new int[m];
        ComputeLPSArray(pattern, m, lps);

        int i = 0; // index for text
        int j = 0; // index for pattern
        while (i < n) {
            if (pattern[j] == text[i]) {
                i++;
                j++;
            }

            if (j == m) {
                return true;
            }
            else if (i < n && pattern[j] != text[i]) {
                if (j != 0) {
                    j = lps[j - 1];
                }
                else {
                    i++;
                }
            }
        }
        return false;
    }

    public static float imageKMP_Algorithm(string imageText, string imagePattern) {
        // function to search for pattern in text using Knuth-Morris-Pratt algorithm
        // text and pattern get from image
        // imageText is path to image text
        // imagePattern is path to image pattern
        string[] selectedPattern = new string[30];
        byte[][] l1;
        byte[][] l2;
    
        try {
            /** *********************** Pattern *********************** **/
            // Load the image
            using (Image image = Image.FromFile(imagePattern)) {
                // image to binary
                byte[][] imageBytes2d = Util.ImageToByteArray(image);
                l1 = imageBytes2d;
                imageBytes2d = Util.twoDPattern(imageBytes2d, image.Width, image.Height);

                // get 1d pattern (take from middle of image)
                byte[] imageBytes1d = Util.get1DPattern(imageBytes2d, image.Width, image.Height);
                
                // binary to ascii 
                string[] base64String = Util.bitsToString2D(imageBytes2d, "KMP");
                for (byte i = 0; i < 30; i++) {
                    selectedPattern[i] = base64String[i];
                }
            }

            /** *********************** Text *********************** **/
            using (Image image = Image.FromFile(imageText)) {
                // image to binary
                byte[][] imageBytes2d = Util.ImageToByteArray(image);
                l2 = imageBytes2d;
                Console.WriteLine("Persen%: " + HammingDistance.CalculateHammingDistance(l1, l2));
                
                // change 2d to 1d
                byte[] imageBytes1d = Util.arrayOfStringToString(imageBytes2d, image.Width, image.Height);
                /** *********************** KMP *********************** **/
                // search for pattern in text
                for (int i = 0; i < (image.Width % 8) + 1; i++)
                {
                    String[] mtx = Util.getText(imageBytes2d, i, "KMP");
                    int a = 0;
                    int b = 0;
                    bool notFound = true;
                    while (a < mtx.Length - selectedPattern.Length + 1 && notFound)
                    {
                        while (b < selectedPattern.Length && notFound && a < mtx.Length - selectedPattern.Length + 1)
                        {
                            if (KMP_Algorithm(mtx[a + b], selectedPattern[b]))
                            {
                                b++;
                            }
                            else
                            {
                                b = 0;
                                a++;
                            }
                        }
                        
                        if (b == selectedPattern.Length)
                        {
                            notFound = false;
                            return 1.0f;
                        }
                    }
                    // return !notFound;
                }
            }
            return HammingDistance.CalculateHammingDistance(l1, l2);

        }
        catch (Exception ex)
        { // handle error
            Console.WriteLine($"Error: {ex.Message}");
            return 0;
        }
    }
}