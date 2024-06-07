// using System;
// using System.Drawing;

// class Program
// {
//     static void Main(string[] args)
//     {
//         string imagePath = "t.bmp"; // Specify your image path here
//         int threshold = 128; // Adjust this value as needed

//         // Load the image
//         Bitmap image = new Bitmap(imagePath);

//         // Convert to grayscale and apply threshold
//         int width = image.Width;
//         int height = image.Height;
//         int[,] binaryPixels = new int[height, width];

//         for (int y = 0; y < height; y++)
//         {
//             for (int x = 0; x < width; x++)
//             {
//                 // Get the pixel color
//                 Color pixelColor = image.GetPixel(x, y);

//                 // Convert to grayscale using the luminance formula
//                 int grayValue = (int)(0.3 * pixelColor.R + 0.59 * pixelColor.G + 0.11 * pixelColor.B);

//                 // Apply the threshold
//                 binaryPixels[y, x] = grayValue > threshold ? 1 : 0;
//             }
//         }

//         // Output the binary representation (you can also save it to a file or use it further)
//         for (int y = 0; y < height; y++)
//         {
//             for (int x = 0; x < width; x++)
//             {
//                 Console.Write(binaryPixels[y, x]);
//             }
//             Console.WriteLine();
//         }
//     }
// }
