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
using System.Diagnostics;
using System.Data.SQLite;
using System.Linq;
using System.Data;
namespace Fingerprint_Detection
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private bool isBM = false;
        private String[] imagePaths;
        private String imagePathToMatch;
        public MainWindow()
        {
            InitializeComponent();
            imagePaths = GetAllImagePaths();
            Console.WriteLine(imagePaths.Length);

        }

        private string[] GetAllImagePaths()
        {
            string folderPath = "D:\\Kuliah\\Semester 4\\Stima\\Tubes 3 (Fingerprint Detection)\\Tubes\\Tubes3_Bang-Udah-Capek-Bang\\test\\";
            Console.WriteLine("WOIIIIIIIIIIII");
            var imagePathss = Directory.GetFiles(folderPath).ToArray();
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
                ButtonInserted.Source = bitmap;
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
                Console.WriteLine("BM");
                Toggle.Margin = new Thickness(44,0, 0, 0);
            } else
            {
                Console.WriteLine("KMP");
                Toggle.Margin = new Thickness(2,0,0,0);
            }
        }
        private void SearchButton_Click(object sender, RoutedEventArgs e)
        {
            BitmapImage bitmapinserted = new BitmapImage(new Uri(imagePathToMatch));
            ButtonInserted.Source = bitmapinserted;

            ButtonOutput.Visibility = Visibility.Visible;
            ButtonInput.Visibility = Visibility.Visible;
            ButtonUpload.Visibility = Visibility.Hidden;
            TitleInput.Visibility = Visibility.Visible;
            TitleOutput.Visibility = Visibility.Visible;

            float found;
            String maxPath  = string.Empty;
            float maxPercentage = 0f;

            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            for (int i = 0; i < imagePaths.Length; i++)
            {
                Console.WriteLine("TESTTTTTTTTT");
                if (isBM)
                {
                    found = BM_2D.imageBM_Algorithm(imagePaths[i], imagePathToMatch);
                } else
                {
                    // found = 1;
                    found = KMP.imageKMP_Algorithm(imagePaths[i], imagePathToMatch);
                }
                
                Console.WriteLine(i + ") " + imagePaths[i]);
                
                if (found == 1)
                {
                    BitmapImage bitmapmatch = new BitmapImage(new Uri(imagePaths[i]));
                    ButtonMatch.Source = bitmapmatch;
                    maxPercentage = found;
                    MatchPercentage.Visibility = Visibility.Visible;
                    MatchPercentage.Content = "Match Percentage: " + maxPercentage*100 +"%";
                    Console.WriteLine(maxPercentage.ToString());
                    TimeExecution.Visibility = Visibility.Visible;
                    
                    String matchPath = imagePaths[i].Substring(imagePaths[i].Length - 19);
                    Console.WriteLine($"{matchPath}");

                    String nameMatch = FindMatchPathFromDatabase(matchPath);

                    String[,] biodata = ExtractAllNameFromDatabase();
                    nameMatch = Regex.StringToRegex_TrueToAlay(nameMatch);
                    bool foundDB = false;
                    for (int j = 0; j < biodata.GetLength(0); j++)
                    {
                        // Console.WriteLine(j + " )    " + biodata[j,0].ToString());
                        foundDB = Regex.IsMatch(biodata[j, 1], nameMatch);
                        if (foundDB)
                        {
                            BiodataMatch.Visibility = Visibility.Visible;
                            NIK.Text = "NIK               : " + biodata[j,0];
                            Name.Text = "Name           : " + biodata[j, 1];
                            DOB.Text = "DOB              : " + biodata[j, 2];
                            Gender.Text = "Gender         : " + biodata[j, 3];
                            BloodType.Text = "Blood Type : " +biodata[j, 4];
                            Address.Text = "Address       : " +biodata[j, 5];
                            Religion.Text = "Religion       : "+biodata[j, 6];
                            Status.Text = "Status          : "+biodata[j, 7];
                            Job.Text = "Job               : "+biodata[j, 8];
                            Citizen.Text = "Citizen         : " +biodata[j, 9];
                        }
                    }
                    Console.WriteLine(nameMatch);



                    stopwatch.Stop();
                    TimeSpan ts = stopwatch.Elapsed;
                    long timewaste = (ts.Minutes * 60000) + (ts.Seconds * 1000) + ts.Milliseconds;
                    TimeExecution.Content = "Time Execution: " + timewaste + "ms";
                    break;
                } else
                {
                    if (found > maxPercentage)
                    {
                       
                        maxPercentage = found;
                        maxPath = imagePaths[i];
                    }
                }
            }
            
            if (maxPercentage != 1 && maxPercentage >= 0.9f) {
                BitmapImage bitmapmatch = new BitmapImage(new Uri(maxPath));
                ButtonMatch.Source = bitmapmatch;
                MatchPercentage.Visibility = Visibility.Visible;
                MatchPercentage.Content = "Match Percentage: " + maxPercentage*100 +"%";

                TimeExecution.Content = imagePathToMatch;
            } 
            else if (maxPercentage < 0.9f)
            {
                ButtonOutput.Content = "No fingerprint\n      match";
                MatchPercentage.Visibility = Visibility.Hidden;
                BiodataMatch.Visibility = Visibility.Hidden;
                
            }
        }


        private String FindMatchPathFromDatabase(String input)
        {
            string connectionString = "Data Source=D:\\Kuliah\\Semester 4\\Stima\\Tubes 3 (Fingerprint Detection)\\Tubes\\Tubes3_Bang-Udah-Capek-Bang\\database\\test.db;Version=3;";
            using (SQLiteConnection connection = new SQLiteConnection(connectionString))
            {
                try
                {
                    // Open the connection
                    connection.Open();

                    // Define the query
                    string query = "SELECT nama FROM sidik_jari WHERE berkas_citra = @berkasCitra";

                    // Create a command object
                    using (SQLiteCommand command = new SQLiteCommand(query, connection))
                    {
                        // Set the parameter value
                        command.Parameters.AddWithValue("@berkasCitra", input);

                        // Execute the query and read the results
                        using (SQLiteDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                // Get the value of the "nama" column
                                string nama = reader["nama"].ToString();
                                Console.WriteLine(nama);
                                return nama;
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("An error occurred: " + ex.Message);
                }
            }
            return "gamungkinmasuksini";
        }

        private String[,] ExtractAllNameFromDatabase()
        {
            string connectionString = "Data Source=D:\\Kuliah\\Semester 4\\Stima\\Tubes 3 (Fingerprint Detection)\\Tubes\\Tubes3_Bang-Udah-Capek-Bang\\database\\test.db;Version=3;";
            using (SQLiteConnection connection = new SQLiteConnection(connectionString))
            {
                try
                {
                    connection.Open();


                    int rowCount = 0;
                    string countQuery = "SELECT COUNT(*) FROM biodata";
                    using (SQLiteCommand countCommand = new SQLiteCommand(countQuery, connection))
                    {
                        rowCount = Convert.ToInt32(countCommand.ExecuteScalar());
                    }

                    Console.WriteLine("ROWWWWWWWWWWWWWWWWW: " + rowCount);


                    // Initialize the matrix
                    string[,] dataMatrix = new string[rowCount, 10];
                    int row = 0;

                    string query = "SELECT * FROM biodata";
                    using (SQLiteCommand command = new SQLiteCommand(query, connection))
                    {
                        using (SQLiteDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                for (int col = 0; col < 10; col++)
                                {
                                    dataMatrix[row, col] = reader[col].ToString();
                                }
                                row++;
                            }
                        }
                    }
                    Console.WriteLine("TERAKHIRRRRRRRRRRRRRRRRR: "+  dataMatrix.GetLength(0));
                    return dataMatrix;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("An error occurred: " + ex.Message);
                }
            }
            return new string[10,10];
        }
        private void Frame_Navigated(object sender, NavigationEventArgs e)
        {

        }
    }
}
