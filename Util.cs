using System;
using System.Drawing;
using System.IO;

class Util
{
    static void Main(string[] args)
    {
        bitmapToBinary();
    }
    static void bitmapToBinary()
    {
        Console.WriteLine("Picture (path): ");
        string imagePath = Console.ReadLine();
        Console.WriteLine("Output: ");
        string outFile = Console.ReadLine();
        try
        {
            // Load the image
            using (Image image = Image.FromFile(imagePath))
            {
                // image to binary
                byte[] imageBytes = ImageToByteArray(image);

                // binary to ascii
                string base64String = Convert.ToBase64String(imageBytes);

                // write into file
                File.WriteAllText(outFile, base64String);

                Console.WriteLine($"Out: {outFile}");
            }
        }
        catch (Exception ex) // handle error
        {
            Console.WriteLine($"Error: {ex.Message}");
        }
    }

    static byte[] ImageToByteArray(Image image) // function to convert image into binary
    {
        using (MemoryStream ms = new MemoryStream())
        {
            image.Save(ms, image.RawFormat);
            return ms.ToArray();
        }
    }
}
