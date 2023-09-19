using Microsoft.Win32;
using Photo_Manager.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Diagnostics;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;
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
using static System.Windows.Forms.DataFormats;

namespace Photo_Manager.Views
{
    /// <summary>
    /// Interaction logic for PhotoGalleryView.xaml
    /// </summary>
    public partial class PhotoGalleryView : UserControl
    {


        public PhotoGalleryView()
        {
            InitializeComponent();
        }

        private void PhotoGalleryView_Loaded(object sender, RoutedEventArgs e)
        {
            string imagesDir = BaseResource.ChosenPath;
            string[] filesindirectory = Directory.GetFiles(@imagesDir);
            foreach (var (s, newBtn, contextMenu) in from string s in filesindirectory
                                                     where Regex.IsMatch(s, @"\.jpg|\.png|\.jpeg|\.mp4|\.webm")
                                                     let newBtn = new Button()
                                                     let contextMenu = new ContextMenu()
                                                     select (s, newBtn, contextMenu))
            {

                MenuItem menuItem = new MenuItem();
                menuItem.Header = "clipboard";
                menuItem.Click += new RoutedEventHandler(clipboard_onclick);
                menuItem.Tag = s;

                contextMenu.Items.Add(menuItem);

                if (Regex.IsMatch(s, @"\.jpg|\.png|\.jpeg"))
                {

                    newBtn.Content = new Image
                    {
                        Width = 180,
                        Height = 180,
                        Source = new BitmapImage(new Uri(s)),
                        VerticalAlignment = VerticalAlignment.Center

                    };

                }
                else if (Regex.IsMatch(s, @"\.mp4|\.webm"))
                {

                    MediaPlayer player = new MediaPlayer { Volume = 0, ScrubbingEnabled = true };
                    player.Open(new Uri(s));
                    player.Pause();

                    player.Position = TimeSpan.FromSeconds(1000);
                    System.Threading.Thread.Sleep(500);

                    RenderTargetBitmap rtb = new RenderTargetBitmap(120, 90, 96, 96, PixelFormats.Pbgra32);
                    DrawingVisual dv = new DrawingVisual();
                    using (DrawingContext dc = dv.RenderOpen())
                    {
                        dc.DrawVideo(player, new Rect(0, 0, 120, 90));
                    }
                    rtb.Render(dv);

                    BitmapFrame frame = BitmapFrame.Create(rtb).GetCurrentValueAsFrozen() as BitmapFrame;

                    newBtn.Content = new Image
                    {
                        Width = 180,
                        Height = 180,
                        Source = frame,
                        VerticalAlignment = VerticalAlignment.Center

                    };

                }
                newBtn.Background = new SolidColorBrush(Colors.Transparent);
                newBtn.Margin = new Thickness(5);
                newBtn.Click += new RoutedEventHandler(btnPhotoView);
                newBtn.SetBinding(Button.CommandProperty, new Binding("NavigatePhotoViewCommand"));

                newBtn.ContextMenu = contextMenu;

                newBtn.Tag = s;

                PhotoGalleryStackPanel.Children.Add(newBtn);

            }
        }
        private void clipboard_onclick(object sender, RoutedEventArgs e)
        {
            StringCollection path = new StringCollection();
            path.Add(((MenuItem)sender).Tag.ToString()); 
            Clipboard.SetFileDropList(path);
        }

        private void btnPhoto(object sender, RoutedEventArgs e)
        {
            

        }

        private void btnPhotoView(object sender, RoutedEventArgs e)
        {
            var selectedImgDir = ((Button)sender).Tag;
            BaseResource.ChosenImage= (string)selectedImgDir;
        }

        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
