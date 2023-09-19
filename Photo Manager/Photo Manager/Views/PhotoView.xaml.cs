using Photo_Manager.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Photo_Manager.Views
{
    /// <summary>
    /// Interaction logic for PhotoView.xaml
    /// </summary>
    public partial class PhotoView : UserControl
    {

        public PhotoView()
        {
            InitializeComponent();
            
        }

        private void PhotoView_Loaded(object sender, RoutedEventArgs e)
        {
            var imageDir = BaseResource.ChosenImage;
            int imageDirIndex = BaseResource.CurrentGallery.IndexOf(imageDir);
            MediaElement mediaElement = new MediaElement();
            
            mediaElement.Source = (new Uri(BaseResource.CurrentGallery[imageDirIndex]));
            imageWindow.Children.Add(mediaElement);
        }
        
        public void btnReturnToGallery_Click(object sender, RoutedEventArgs e)
        {
            
        }

        private void PhotoView_Unloaded(object sender, RoutedEventArgs e)
        {

        }

        private void btnNextImage_Click(object sender, RoutedEventArgs e)
        {
            var imageDir = BaseResource.ChosenImage;
            int imageDirIndex = BaseResource.CurrentGallery.IndexOf(imageDir);
            if(imageDirIndex < (BaseResource.CurrentGallery.Count-1))
            {
                imageDirIndex += 1;
                displayedPhoto.Source = new BitmapImage(new Uri(BaseResource.CurrentGallery[imageDirIndex]));
                BaseResource.ChosenImage = BaseResource.CurrentGallery[imageDirIndex];

            }
            

        }

        private void btnPreviousImage_Click(object sender, RoutedEventArgs e)
        {
            var imageDir = BaseResource.ChosenImage;
            int imageDirIndex = BaseResource.CurrentGallery.IndexOf(imageDir);
            if (imageDirIndex != 0)
            {
                imageDirIndex -= 1;
                displayedPhoto.Source = new BitmapImage(new Uri(BaseResource.CurrentGallery[imageDirIndex]));
                BaseResource.ChosenImage = BaseResource.CurrentGallery[imageDirIndex];

            }
        }

        private void imageWindow_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            var _sender = ((Grid)sender);
            var _mediaElement = (MediaElement)_sender.Children[1];
            if (Regex.IsMatch(_mediaElement.Source.ToString(), @"\.mp4|\.webm"))
            {
                if (_mediaElement.LoadedBehavior != MediaState.Manual)
                {
                    _mediaElement.LoadedBehavior = MediaState.Manual;
                }
                _mediaElement.Close();
                _mediaElement.Position = TimeSpan.Zero;
                _mediaElement.Play();
            }
        }

        private void imageWindow_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            var _sender = ((Grid)sender);
            var _mediaElement = (MediaElement)_sender.Children[1];
            if (Regex.IsMatch(_mediaElement.Source.ToString(), @"\.mp4|\.webm"))
            {
                if (_mediaElement.LoadedBehavior != MediaState.Manual)
                {
                    _mediaElement.LoadedBehavior = MediaState.Manual;
                }
                _mediaElement.Close();
                _mediaElement.Position = TimeSpan.Zero;
                _mediaElement.Play();
            }
        }
    }
}
