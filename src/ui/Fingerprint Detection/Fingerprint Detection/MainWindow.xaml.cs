using System;
using System.Windows;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using Microsoft.Win32;
using System.Windows.Controls;
using System.Windows.Media;
using System.Xml.Serialization;
using System.IO;
using System.Drawing;
using System.Linq;
namespace Fingerprint_Detection
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private bool isBM = true;
        private String[] imagePaths;
        private String imagePathToMatch;
        public MainWindow()
        {
            InitializeComponent();
            imagePaths = GetAllImagePaths();

        }

        private string[] GetAllImagePaths()
        {
            string folderPath = "D:\\Kuliah\\Semester 4\\Stima\\Tubes 3 (Fingerprint Detection)\\Tubes\\Tubes3_Bang-Udah-Capek-Bang\\database\\cobadulubang\\";

            var imagePathss = Directory.GetFiles(folderPath).ToArray();
            
            foreach ( var imagePath in imagePathss )
            {
                Console.WriteLine( imagePath );
            }
            return imagePathss;
        }
        private void UploadButton_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Image files (*.BMP)|*.BMP";

            if (openFileDialog.ShowDialog() == true)
            {
                string filePath = openFileDialog.FileName;
                imagePathToMatch = filePath;
                BitmapImage bitmap = new BitmapImage(new Uri(openFileDialog.FileName));

                ButtonImage.Source = bitmap;
                ButtonImage.Width = 240;
                ButtonImage.Height = 320;
            }
        }

        private void ToggleButton_Click(object sender, RoutedEventArgs e)
        {
            isBM = !isBM;
            if (isBM)
            {
                Toggle.Margin = new Thickness(44,0, 0, 0);
            } else
            {
                Toggle.Margin = new Thickness(2,0,0,0);
            }
        }
        private void SearchButton_Click(object sender, RoutedEventArgs e)
        {
            ButtonOutput.Visibility = Visibility.Visible;
            ButtonInput.Visibility = Visibility.Visible;
            ButtonUpload.Visibility = Visibility.Hidden;
            TitleInput.Visibility = Visibility.Visible;
            TitleOutput.Visibility = Visibility.Visible;

            float found = 0f;
            String maxPath  = string.Empty;
            float maxPercentage = 0f;
            for (int i = 0; i < imagePaths.Length; i++)
            {
                found = BM_2D.imageBM_Algorithm(imagePaths[i], imagePathToMatch);
                
                if (found == 1)
                {
                    maxPercentage = found;
                    MatchPercentage.Content = i +") " + imagePaths[i] +"      " + maxPercentage;
                    TimeExecution.Content = imagePathToMatch;
                    Console.WriteLine(imagePaths[i] + " " + imagePathToMatch);
                    Console.WriteLine("KETEMUUUUUUUUUUUUUUUUUUUUUUUUUUUUUUUUUUUUUUUUUUUUUUUUUUUUUU");
                   

                    break;
                } else
                {
                    Console.WriteLine(found + "WOIIIIII \n");
                    if (found > maxPercentage)
                    {
                       
                        maxPercentage = found;
                        maxPath = imagePaths[i];
                    }
                    // Console.Write(imagePaths[i] + " " + imagePathToMatch + " ");
                    // Console.WriteLine("GA KETEMU\n");
                }
            }
            
            string[] imagePath = GetAllImagePaths();
            if (maxPercentage != 1 && maxPercentage > 0.9f) {

                TimeExecution.Content = imagePathToMatch;

                ButtonOutput.HorizontalContentAlignment = HorizontalAlignment.Center;
            } else
            {
                MatchPercentage.Visibility = Visibility.Visible;
                BiodataMatch.Visibility = Visibility.Hidden;
                
            }
        }


        private void Frame_Navigated(object sender, NavigationEventArgs e)
        {

        }
    }
}
