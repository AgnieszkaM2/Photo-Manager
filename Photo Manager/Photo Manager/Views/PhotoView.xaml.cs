using Photo_Manager.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
            displayedPhoto.Source = new BitmapImage(new Uri(imageDir));
        }
        
        public void btnReturnToGallery_Click(object sender, RoutedEventArgs e)
        {
            
        }

        private void PhotoView_Unloaded(object sender, RoutedEventArgs e)
        {

        }
    }
}
