﻿using Microsoft.Win32;
using Photo_Manager.ViewModels;
using System;
using System.Collections.Generic;
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
        
        public int indexer = 0;

        private void PhotoGalleryView_Loaded(object sender, RoutedEventArgs e)
        {
            BaseResource.CurrentGallery.Clear();
            string imagesDir = BaseResource.ChosenPath;
            string[] filesindirectory = Directory.GetFiles(@imagesDir);
            foreach (var (s, newBtn, contextMenu) in from string s in filesindirectory
                                                     where Regex.IsMatch(s, @"\.jpg|\.png|\.jpeg")
                                                     let newBtn = new Button()
                                                     let contextMenu = new ContextMenu()
                                                     select (s, newBtn, contextMenu))
            {
                MenuItem menuItem = new MenuItem();
                menuItem.Header = "clipboard";
                menuItem.Click += new RoutedEventHandler(clipboard_onclick);
                menuItem.Tag = s;

                contextMenu.Items.Add(menuItem);

                newBtn.Content = new Image
                {
                    Width = 180,
                    Height = 180,
                    Source = new BitmapImage(new Uri(s)),
                    VerticalAlignment = VerticalAlignment.Center
                    
                };
                newBtn.Background = new SolidColorBrush(Colors.Transparent);
                newBtn.Margin = new Thickness(5);
                newBtn.Click += new RoutedEventHandler(btnPhotoView);
                newBtn.SetBinding(Button.CommandProperty, new Binding("NavigatePhotoViewCommand"));

                newBtn.ContextMenu = contextMenu;
                
                newBtn.Tag = s;


                PhotoGalleryStackPanel.Children.Add(newBtn);
                BaseResource.CurrentGallery.Add(s);
                indexer++;
            }
        }
        private void clipboard_onclick(object sender, RoutedEventArgs e)
        {
            Clipboard.SetImage(new BitmapImage(new Uri(((MenuItem)sender).Tag.ToString())));
        }

        private void btnPhoto(object sender, RoutedEventArgs e)
        {
            

        }

        private void btnPhotoView(object sender, RoutedEventArgs e)
        {
            var selectedImgDir = ((Button)sender).Tag;
            BaseResource.ChosenImage= selectedImgDir.ToString();
        }

        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
