using System;
using System.Collections.Generic;
using System.Drawing;
// [,]
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

    public static bool imageKMP_Algorithm(String imageText, String imagePattern) {
    // function to search for pattern in text using Knuth-Morris-Pratt algorithm
    // text and pattern get from image
    // imageText is path to image text
    // imagePattern is path to image pattern

        string selectedPattern = "";
        try {
            /** *********************** Pattern *********************** **/
            // Load the image
            using (Image image = Image.FromFile(imagePattern)) {
                // image to binary
                byte[][] imageBytes2d = Util.ImageToByteArray(image);
                imageBytes2d = Util.twoDPattern(imageBytes2d, image.Width, image.Height);

                using (StreamWriter writer = new StreamWriter("2dPattern.txt")) {
                    for (int y = 0; y < image.Height; y++) {
                        for (int x = 0; x < (image.Width / 8) * 8; x++) {
                            writer.Write(imageBytes2d[y][x]);
                        }
                        writer.WriteLine(); // Move to the next line after each row
                    }
                }

                // get 1d pattern (take from middle of image)
                byte[] imageBytes1d = Util.get1DPattern(imageBytes2d, image.Width, image.Height);
                using (StreamWriter writer = new StreamWriter("1dPattern.txt")) {
                    for (int y = 0; y < imageBytes1d.Length; y++) {
                        writer.Write(imageBytes1d[y]); // Move to the next line after each row

                    }
                }
                // binary to ascii 

                string base64String = Util.bitsToString(imageBytes1d);
                selectedPattern = base64String;

                // write into file (testing)
                // File.WriteAllText("asciiPattern.txt", selectedPattern);
                // Console.WriteLine("Selected Pattern: " + selectedPattern);
            }

            /** *********************** Text *********************** **/
            using (Image image = Image.FromFile(imageText)) {
                // image to binary
                byte[][] imageBytes2d = Util.ImageToByteArray(image);

                using (StreamWriter writer = new StreamWriter("2dText.txt")) {
                    for (int y = 0; y < image.Height; y++) {
                        for (int x = 0; x < image.Width; x++) {
                            writer.Write(imageBytes2d[y][x]);
                        }
                        writer.WriteLine(); // Move to the next line after each row
                    }
                }

                // change 2d to 1d
                byte[] imageBytes1d = Util.arrayOfStringToString(imageBytes2d, image.Width, image.Height);
                using (StreamWriter writer = new StreamWriter("1dText.txt")) {
                    for (int y = 0; y < imageBytes1d.Length; y++) {
                        writer.Write(imageBytes1d[y]); // Move to the next line after each row
                    }
                }

                // binary to ascii
                string base64String = Convert.ToBase64String(imageBytes1d);

                // write into file
                // File.WriteAllText("asciiText.txt", base64String);
                // Console.WriteLine(image.Width);
                
                /** *********************** KMP *********************** **/
                // search for pattern in text
                for (int i = 0; i < (image.Width % 8) + 1; i++) {
                    String[] mtx = Util.getText(imageBytes2d, i);
                    for (int j = 0; j < mtx.Length; j++) {
                        if (KMP_Algorithm(mtx[j], selectedPattern)) {
                            return true;
                        }
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