using System;
using System.Windows;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Media.Imaging;
using Microsoft.Win32;
using System.Windows.Controls;
using System.Windows.Media;
using System.Xml.Serialization;

namespace Fingerprint_Detection
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private bool isBM = true;
        public MainWindow()
        {
            InitializeComponent();
        }

        private void UploadButton_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Image files (*.jpg, *.jpeg, *.png)|*.jpg;*.jpeg;*.png";

            if (openFileDialog.ShowDialog() == true)
            {
                BitmapImage bitmap = new BitmapImage(new Uri(openFileDialog.FileName));

                ButtonImage.Source = bitmap;
                ButtonImage.Width = 240;
                ButtonImage.Height = 320;
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

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
        }


        private void Frame_Navigated(object sender, NavigationEventArgs e)
        {

        }
    }
}
