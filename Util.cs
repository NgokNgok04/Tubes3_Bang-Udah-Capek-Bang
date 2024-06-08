using System;
using System.Drawing;
using System.Dynamic;
using System.Globalization;
using System.IO;

class Util
{
    static void Main(string[] args)
    {
        // bitmapToBinary();

        // Console.WriteLine(hammingDistance("aaab", "aaa"));
        // Console.WriteLine(hammingDistance("aaab", "aaab"));
        // Console.WriteLine(hammingDistance("aaab", "aaac"));
        Console.Write("Path to Text: ");
        String pathText = Console.ReadLine();
        Console.Write("Path to Pattern: ");
        String pathPattern = Console.ReadLine();
        if (imageKMP(pathText, pathPattern))
        {
            Console.Write("Cocok");
        }
    }

    static int hammingDistance(String text, String pat)
    {
        if (text.Length != pat.Length)
        {
            return -1;
        }
        int result = 0;
        for (int i = 0; i < text.Length; i++)
        {
            if (text[i].Equals(pat[i]))
            {
                result++;
            }
        }
        return result;
    }

    static bool imageKMP(String imageText, String imagePattern)
    {
        string selectedPattern = "";
        try
        {
            // Pattern
            // Load the image
            using (Image image = Image.FromFile(imagePattern))
            {
                // image to binary
                byte[,] imageBytes2d = ImageToByteArray(image);
                imageBytes2d = twoDPattern(imageBytes2d, image.Width, image.Height);

                using (StreamWriter writer = new StreamWriter("2dPattern.txt"))
                {
                    for (int y = 0; y < image.Height; y++)
                    {
                        for (int x = 0; x < (image.Width / 8) * 8; x++)
                        {
                            writer.Write(imageBytes2d[y, x]);
                        }
                        writer.WriteLine(); // Move to the next line after each row
                    }
                }

                // 2d to 1d
                byte[] imageBytes1d = twoToOneCut(imageBytes2d, image.Width, image.Height);
                using (StreamWriter writer = new StreamWriter("1dPattern.txt"))
                {
                    for (int y = 0; y < imageBytes1d.Length; y++)
                    {
                        writer.Write(imageBytes1d[y]); // Move to the next line after each row

                    }
                }
                // binary to ascii 
                // string base64String = Convert.ToBase64String(imageBytes1d);
                string base64String = bitsToString(imageBytes1d);
                selectedPattern = base64String;
                // write into file
                File.WriteAllText("asciiPattern.txt", selectedPattern);

                Console.WriteLine("Selected Pattern: " + selectedPattern);
            }
            bool result = false;
            // Text
            using (Image image = Image.FromFile(imageText))
            {
                // image to binary
                byte[,] imageBytes2d = ImageToByteArray(image);
                // imageBytes2d = twoDPattern(imageBytes2d, image.Width, image.Height);

                using (StreamWriter writer = new StreamWriter("2dText.txt"))
                {
                    for (int y = 0; y < image.Height; y++)
                    {
                        for (int x = 0; x < image.Width; x++)
                        {
                            writer.Write(imageBytes2d[y, x]);
                            // if (x < image.Width - 1)
                            // {
                            //     writer.Write(","); // Use a comma as a delimiter
                            // }
                        }
                        writer.WriteLine(); // Move to the next line after each row
                    }
                }
                // 2d to 1d
                byte[] imageBytes1d = twoToOne(imageBytes2d, image.Width, image.Height);
                using (StreamWriter writer = new StreamWriter("1dText.txt"))
                {
                    for (int y = 0; y < imageBytes1d.Length; y++)
                    {
                        writer.Write(imageBytes1d[y]); // Move to the next line after each row

                    }
                }

                // binary to ascii
                string base64String = Convert.ToBase64String(imageBytes1d);

                // write into file
                File.WriteAllText("asciiText.txt", base64String);
                Console.WriteLine(image.Width);
                for (int i = 0; i < (image.Width % 8) + 1; i++)
                {
                    String[] mtx = getText(imageBytes2d, i);
                    for (int j = 0; j < mtx.GetLength(0); j++)
                    {
                        Console.WriteLine("loop " + i + "-" + j);
                        Console.WriteLine(mtx[j]);

                        if (KMP(mtx[j], selectedPattern))
                        {
                            return true;
                        }
                    }
                }
            }
            return false;
        }
        catch (Exception ex) // handle error
        {
            Console.WriteLine($"Error: {ex.Message}");
            return false;
        }
        return false;
    }


    static byte[,] ImageToByteArray(Image img) // function to convert image into binary
    {
        // string imagePath = pic  ;
        int threshold = 127;

        Bitmap image = new Bitmap(img);

        int width = image.Width;
        int height = image.Height;
        byte[,] binaryPixels = new byte[height, width];

        for (int y = 0; y < height; y++)
        {
            for (int x = 0; x < width; x++)
            {
                Color pixelColor = image.GetPixel(x, y);
                int grayValue = (int)(0.299 * pixelColor.R + 0.587 * pixelColor.G + 0.114 * pixelColor.B);
                if (grayValue > threshold)
                {
                    binaryPixels[y, x] = 1;
                }
                else
                {
                    binaryPixels[y, x] = 0;
                }
            }
        }
        return binaryPixels;
    }

    static String[] getText(byte[,] mtx, int idx)
    {
        String[] result = new string[mtx.Length];
        int w = 8 * (mtx.GetLength(1) / 8);
        byte[] line = new byte[w / 8];
        for (int i = 0; i < mtx.GetLength(0); i++)
        {
            byte temp = 0;
            for (int j = 0; j < w; j++)
            {
                temp <<= 1;
                temp += mtx[i, j + idx];
                if (j % 8 == 7)
                {
                    line[j / 8] = temp;
                    temp = 0;
                }
            }
            string s = System.Text.Encoding.Latin1.GetString(line);
            result[i] = s;
        }
        return result;
    }
    static byte[] twoToOne(byte[,] mtx, int w, int h) // function to convert image into binary
    {
        byte[] result = new byte[h * w];

        for (int y = 0; y < h; y++)
        {
            for (int x = 0; x < w; x++)
            {
                result[y * w + x] = mtx[y, x];
            }
        }
        return result;
    }
    static byte[] twoToOneCut(byte[,] mtx, int w, int h) // function to convert image into binary wit cutting border
    {
        byte[] result = new byte[(w / 8) * 8];

        for (int x = 0; x < (w / 8) * 8; x++)
        {
            result[x] = mtx[(h / 2), x];
        }
        return result;
    }
    static byte[,] twoDPattern(byte[,] mtx, int w, int h) // function to convert image into binary wit cutting border
    {
        byte[,] result = new byte[h, (w / 8) * 8];

        for (int y = 0; y < h; y++)
        {
            for (int x = 0; x < (w / 8) * 8; x++)
            {
                result[y, x] = mtx[y, x];
            }
        }

        return result;
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
    public static bool KMP(string text, string pattern)
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
                return true;
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
        return false;
    }
    public static string[] bitsToString2D(byte[,] mtx)
    {
        string[] result = new string[mtx.GetLength(0)];
        int border = mtx.GetLength(0) - 30;
        int up = border / 2;
        for (int i = 0; i < 30; i++)
        {
            byte[] list = new byte[mtx.GetLength(1)];
            for (int j = 0; j < mtx.GetLength(1); j++)
            {
                list[j] = mtx[i + up, j];
            }
            string s = bitsToString(list);
            result[i] = s;
        }
        return result;
    }
    public static string bitsToString(byte[] list)
    {
        byte[] line = new byte[list.GetLength(0) / 8];
        byte temp = 0;
        for (int i = 0; i < list.GetLength(0); i++)
        {
            temp <<= 1;
            temp += list[i];
            if (i % 8 == 7)
            {

                line[i / 8] = temp;
                // if (temp > 127)
                // {
                //     line[i / 8] -= 127;
                // }
                temp = 0;
            }
        }

        int border = line.GetLength(0) - 4;
        int left = border / 2;

        byte[] finalLine = new byte[4];
        for (byte i = 0; i < 4; i++)
        {
            finalLine[i] = line[i + left];
        }
        return System.Text.Encoding.Latin1.GetString(finalLine);
    }
}
