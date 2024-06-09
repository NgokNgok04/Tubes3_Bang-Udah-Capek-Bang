using System;
using System.Drawing;
using System.Dynamic;
using System.Globalization;
using System.IO;

class Util {
    public static byte[] arrayOfStringToString(byte[][] mtx, int w, int h) {
    // function array of string (2D) to string (1D)
        byte[] result = new byte[h * w];
        for (int y = 0; y < h; y++) {
            for (int x = 0; x < w; x++) {
                result[y * w + x] = mtx[y][x];
            }
        }
        return result;
    }

    public static byte[][] ImageToByteArray(Image img) {
    // function to convert image into binary
    // string imagePath = pic  ;
        int THRESHOLD = 127;

        Bitmap image = new Bitmap(img);

        int width = image.Width;
        int height = image.Height;

        byte[][] binaryPixels = new byte[height][];
        for (int i = 0; i < height; i++) {
            binaryPixels[i] = new byte[width];
        }

        for (int y = 0; y < height; y++) {
            for (int x = 0; x < width; x++) {
                Color pixelColor = image.GetPixel(x, y);
                int grayValue = (int)(0.299 * pixelColor.R + 0.587 * pixelColor.G + 0.114 * pixelColor.B);
                
                if (grayValue > THRESHOLD) {
                    binaryPixels[y][x] = 1;
                }
                else {
                    binaryPixels[y][x] = 0;
                }
            }
        }
        return binaryPixels;
    }

    public static String[] getText(byte[][] mtx, int idx, string algoritma) {
    // function to binary text into ASCII
        int width = 8 * (mtx[0].Length / 8);
        String[] result = new string[mtx.Length];
        
        byte[] line = new byte[width / 8];
        byte temp = 0;
        for (int i = 0; i < mtx.Length; i++) {
            for (int j = 0; j < width; j++) {
                temp <<= 1;
                temp += mtx[i][j + idx];
                if (j % 8 == 7) {
                    line[j / 8] = temp;
                    temp = 0;
                }
            }

            string s;
            if (algoritma == "KMP") {
                Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
                s = System.Text.Encoding.GetEncoding("Windows-1252").GetString(line);
            } else {
                s = System.Text.Encoding.ASCII.GetString(line);
            }
            result[i] = s;
        }
        return result;
    }

    public static byte[] get1DPattern(byte[][] mtx, int w, int h) {
    // function to convert image into binary with cutting border
    // get the middle part of the image 
        byte[] result = new byte[(w / 8) * 8];

        for (int x = 0; x < (w / 8) * 8; x++) {
            result[x] = mtx[(h / 2)][x];
        }
        return result;
    }

    public static byte[][] twoDPattern(byte[][] mtx, int w, int h) {
    // cut the image to make the width multiple of 8
        byte[][] result = new byte[h][];
        for (int y = 0; y < h; y++) {
            result[y] = new byte[(w / 8) * 8];
            for (int x = 0; x < (w / 8) * 8; x++) {
                result[y][x] = mtx[y][x];
            }
        }
        return result;
    }

    public static string[] bitsToString2D(byte[][] mtx, string algoritma) {
    // get the middle 30*32 matrix of binary
    // the convert it to array of string
        string[] result = new string[mtx.Length];
        int border = mtx.Length - 30;
        int up = border / 2;

        for (int i = 0; i < 30; i++) {
            byte[] list = new byte[mtx[0].Length];
            
            for (int j = 0; j < mtx[0].Length; j++) {
                list[j] = mtx[i + up][j];
            }

            string s;
            if (algoritma == "KMP") {
                s = bitsToString(list, "KMP");
            } else {
                s = bitsToString(list, "BM");
            }
            result[i] = s;
        }
        return result;
    }

    public static string bitsToString(byte[] list, string algoritma) {
    // convert binary to string
    // then cut the middle 4 bytes
        byte[] line = new byte[list.Length / 8];
        byte temp = 0;
        for (int i = 0; i < list.Length; i++) {
            temp <<= 1;
            temp += list[i];
            if (i % 8 == 7) {
                line[i / 8] = temp;
                temp = 0;
            }
        }

        int border = line.Length - 4;
        int left = border / 2;

        byte[] finalLine = new byte[4];
        for (byte i = 0; i < 4; i++) {
            finalLine[i] = line[i + left];
        }

        string s;
        if (algoritma == "KMP") {
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
            s = System.Text.Encoding.GetEncoding("Windows-1252").GetString(line);
        } else {
            s = System.Text.Encoding.ASCII.GetString(line);
        }
        return s;
    }
}