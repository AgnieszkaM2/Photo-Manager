using Microsoft.Win32;
using Newtonsoft.Json;
using Photo_Manager.AdditionalResources;
using Photo_Manager.ViewModels;
using System;
using System.Collections;
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
using System.Windows.Controls.Primitives;
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
            
            addTagControl.Visibility = Visibility.Hidden;
            removeTagControl.Visibility = Visibility.Hidden;
            editTagControl.Visibility = Visibility.Hidden;
        }
        
        ToggleButton _CurrentlyCheckedButton;


        private void PhotoGalleryView_Loaded(object sender, RoutedEventArgs e)
        {
            CurrentResources.CurrentGallery.Clear();
            string imagesDir = CurrentResources.ChosenPath;
            string[] filesindirectory = Directory.GetFiles(@imagesDir);
            foreach (var (s, newBtn, contextMenu) in from string s in filesindirectory
                                                     where Regex.IsMatch(s, @"\.jpg|\.png|\.jpeg|\.mp4|\.webm")
                                                     let newBtn = new ToggleButton()
                                                     let contextMenu = new ContextMenu()
                                                     select (s, newBtn, contextMenu))
            {
                MenuItem menuItemClipboard = new MenuItem();
                menuItemClipboard.Header = "Clipboard";
                menuItemClipboard.Click += new RoutedEventHandler(clipboard_onclick);
                menuItemClipboard.Tag = s;
                MenuItem menuItemDisplay = new MenuItem();
                menuItemDisplay.Header = "Wyświetl";
                menuItemDisplay.Click += new RoutedEventHandler(btnPhotoView);
                menuItemDisplay.Tag = s;
                menuItemDisplay.SetBinding(Button.CommandProperty, new Binding("NavigatePhotoViewCommand"));

                contextMenu.Items.Add(menuItemDisplay);
                contextMenu.Items.Add(menuItemClipboard);

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
                newBtn.Checked += ToggleBtn_Checked;
                newBtn.Unchecked += ToggleBtn_Unchecked;

                newBtn.ContextMenu = contextMenu;

                newBtn.Tag = s;


                PhotoGalleryStackPanel.Children.Add(newBtn);
                CurrentResources.CurrentGallery.Add(s);

            }
        }

        private void ToggleBtn_Unchecked(object sender, RoutedEventArgs e)
        {
            if (_CurrentlyCheckedButton == ((ToggleButton)sender))
            {
                _CurrentlyCheckedButton = null;
                CurrentResources.ChosenImage = null;
                addTagControl.Visibility = Visibility.Hidden;
                removeTagControl.Visibility = Visibility.Hidden;
                editTagControl.Visibility = Visibility.Hidden;

                //((ToggleButton)sender).BorderBrush = Brushes.Black;
            }
                
        }

        private void ToggleBtn_Checked(object sender, RoutedEventArgs e)
        {
            if (_CurrentlyCheckedButton != null)
                ((ToggleButton)sender).IsChecked = false;
            else
            {
                _CurrentlyCheckedButton = ((ToggleButton)sender);
                var selectedImgDir = ((ToggleButton)sender).Tag;
                CurrentResources.ChosenImage = selectedImgDir.ToString();
                //((ToggleButton)sender).BorderBrush = Brushes.White;
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
            var selectedImgDir = ((MenuItem)sender).Tag;
            CurrentResources.ChosenImage= selectedImgDir.ToString();
        }

        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnAddTag_Click(object sender, RoutedEventArgs e)
        {
            if (CurrentResources.ChosenImage != null)
            {
                addTagControl.Visibility = Visibility.Visible;
                removeTagControl.Visibility = Visibility.Hidden;
                editTagControl.Visibility = Visibility.Hidden;
                addTagMediaElement.Source = (new Uri(CurrentResources.ChosenImage));

                string _path = @".\tags.json";

                if (!File.Exists(_path)) File.CreateText(_path).Close();


                if (Directory.Exists(CurrentResources.ChosenPath))
                {
                    var jsonData = System.IO.File.ReadAllText(_path);
                    var tagsList = JsonConvert.DeserializeObject<List<Tag>>(jsonData) ?? new List<Tag>();

                    var allTags = (from x in tagsList
                                   select x.Name)
                                   .Distinct().ToList();

                    if(allTags.Count>0)
                    {
                        addTagComboBox.ItemsSource = allTags;
                    }

                }
                else
                {
                    var errorview = new ErrorView("Podana ścieżka nie istnieje");
                    errorview.ShowDialog();
                }
            }

        }

    private void btnDeleteTag_Click(object sender, RoutedEventArgs e)
        {
            if(CurrentResources.ChosenImage != null) 
            {
                removeTagControl.Visibility = Visibility.Visible;
                addTagControl.Visibility = Visibility.Hidden;
                editTagControl.Visibility = Visibility.Hidden;
                removeTagMediaElement.Source = (new Uri(CurrentResources.ChosenImage));

                string _path = @".\tags.json";

                if (!File.Exists(_path)) File.CreateText(_path).Close();


                if (Directory.Exists(CurrentResources.ChosenPath))
                {
                    var jsonData = System.IO.File.ReadAllText(_path);
                    var tagsList = JsonConvert.DeserializeObject<List<Tag>>(jsonData) ?? new List<Tag>();

                    var allTags = (from x in tagsList
                                   where x.PhotoPath == CurrentResources.ChosenImage
                                   select x.Name)
                                   .ToList();

                    if (allTags.Count > 0)
                    {
                        removeTagComboBox.ItemsSource = allTags;
                    }
                    

                }
                else
                {
                    var errorview = new ErrorView("Podana ścieżka nie istnieje");
                    errorview.ShowDialog();
                }
            }
            
        }

        private void btnEditTag_Click(object sender, RoutedEventArgs e)
        {
            if (CurrentResources.ChosenImage != null)
            {
                editTagControl.Visibility = Visibility.Visible;
                addTagControl.Visibility = Visibility.Hidden;
                removeTagControl.Visibility = Visibility.Hidden;
                editTagMediaElement.Source = (new Uri(CurrentResources.ChosenImage));

                string _path = @".\tags.json";

                if (!File.Exists(_path)) File.CreateText(_path).Close();


                if (Directory.Exists(CurrentResources.ChosenPath))
                {
                    var jsonData = System.IO.File.ReadAllText(_path);
                    var tagsList = JsonConvert.DeserializeObject<List<Tag>>(jsonData) ?? new List<Tag>();

                    var allImageTags = (from x in tagsList
                                   where x.PhotoPath == CurrentResources.ChosenImage
                                   select x.Name)
                                   .ToList();
                    var allTags = (from x in tagsList
                                   select x.Name)
                                   .Distinct().ToList();

                    if (allImageTags.Count > 0)
                    {
                        chooseEditTagComboBox.ItemsSource = allImageTags;
                    }

                    if (allTags.Count > 0)
                    {
                        editTagComboBox.ItemsSource = allTags;
                    }

                }
                else
                {
                    var errorview = new ErrorView("Podana ścieżka nie istnieje");
                    errorview.ShowDialog();
                }
            }
            
        }

        private void addTagSaveBtn_Click(object sender, RoutedEventArgs e)
        {
            string _path = @".\tags.json";

            if (!File.Exists(_path)) File.CreateText(_path).Close();


            if (Directory.Exists(CurrentResources.ChosenPath))
            {
                var jsonData = System.IO.File.ReadAllText(_path);
                var tagsList = JsonConvert.DeserializeObject<List<Tag>>(jsonData) ?? new List<Tag>();

                string newTag=null;
                if(addTagComboBox.SelectedItem != null)
                {
                    newTag = addTagComboBox.SelectedItem.ToString();
                }
                else if(addTagComboBox.Text != null && addTagComboBox.SelectedItem == null)
                {
                    newTag = addTagComboBox.Text;
                }
                

                if ((string.IsNullOrEmpty(newTag) == false) && (tagsList.Where(x => x.PhotoPath== CurrentResources.ChosenImage).FirstOrDefault(x => x.Name == newTag)) == null)
                {

                    tagsList.Add(new Tag()
                    {
                        Name = newTag,
                        PhotoPath = CurrentResources.ChosenImage,
                        ParentDirectory = CurrentResources.ChosenPath,
                    });

                    jsonData = JsonConvert.SerializeObject(tagsList);
                    System.IO.File.WriteAllText(_path, jsonData);
                }
                else
                {
                    if(string.IsNullOrEmpty(newTag) == true)
                    {
                        var errorview = new ErrorView("Nie wprowadzono tagu dla obrazu");
                        errorview.ShowDialog();
                    }
                    else
                    {
                        var errorview = new ErrorView("Ten tag jest już zapisany dla tego obrazu");
                        errorview.ShowDialog();
                    }
                    
                }
            }
            else
            {
                var errorview = new ErrorView("Podana ścieżka nie istnieje");
                errorview.ShowDialog();
            }

            
            addTagComboBox.Text = null;
            addTagControl.Visibility = Visibility.Hidden;
        }

        private void removeTagSaveBtn_Click(object sender, RoutedEventArgs e)
        {
            string _path = @".\tags.json";

            if (!File.Exists(_path)) File.CreateText(_path).Close();


            if (Directory.Exists(CurrentResources.ChosenPath))
            {
                var jsonData = System.IO.File.ReadAllText(_path);
                var tagsList = JsonConvert.DeserializeObject<List<Tag>>(jsonData) ?? new List<Tag>();

                string tagToRemove = null;
                if (removeTagComboBox.SelectedItem != null)
                {
                    tagToRemove = removeTagComboBox.SelectedItem.ToString();
                }

                if (string.IsNullOrEmpty(tagToRemove) == false)
                {

                    tagsList.RemoveAll(x => x.Name == tagToRemove && x.PhotoPath == CurrentResources.ChosenImage);

                    jsonData = JsonConvert.SerializeObject(tagsList);
                    System.IO.File.WriteAllText(_path, jsonData);
                }
                else
                {
                    var errorview = new ErrorView("Nie wybrano tagu do usunięcia");
                    errorview.ShowDialog();

                }
            }
            else
            {
                var errorview = new ErrorView("Podana ścieżka nie istnieje");
                errorview.ShowDialog();
            }

            removeTagControl.Visibility = Visibility.Hidden;
        }

        private void editTagSaveBtn_Click(object sender, RoutedEventArgs e)
        {
            string _path = @".\tags.json";

            if (!File.Exists(_path)) File.CreateText(_path).Close();


            if (Directory.Exists(CurrentResources.ChosenPath))
            {
                var jsonData = System.IO.File.ReadAllText(_path);
                var tagsList = JsonConvert.DeserializeObject<List<Tag>>(jsonData) ?? new List<Tag>();

                string oldTag = null;
                string newTag = null;
                if (chooseEditTagComboBox.SelectedItem != null)
                {
                    oldTag = chooseEditTagComboBox.SelectedItem.ToString();
                }


                if (editTagComboBox.SelectedItem != null)
                {
                    newTag = editTagComboBox.SelectedItem.ToString();
                }
                else if (editTagComboBox.Text != null && editTagComboBox.SelectedItem == null)
                {
                    newTag = editTagComboBox.Text;
                }


                if ((string.IsNullOrEmpty(oldTag) == false) && (string.IsNullOrEmpty(newTag) == false) && (tagsList.Where(x => x.PhotoPath == CurrentResources.ChosenImage).FirstOrDefault(x => x.Name == newTag)) == null)
                {

                    int index = tagsList.FindIndex(x => x.Name == oldTag && x.PhotoPath == CurrentResources.ChosenImage);
                    tagsList[index] = new Tag()
                    {
                        Name = newTag,
                        PhotoPath = CurrentResources.ChosenImage,
                        ParentDirectory = CurrentResources.ChosenPath,
                    };

                    jsonData = JsonConvert.SerializeObject(tagsList);
                    System.IO.File.WriteAllText(_path, jsonData);
                }
                else
                {
                    if (string.IsNullOrEmpty(newTag) == true)
                    {
                        var errorview = new ErrorView("Nie wprowadzono tagu dla obrazu");
                        errorview.ShowDialog();
                    }
                    else if(string.IsNullOrEmpty(oldTag) == true)
                    {
                        var errorview = new ErrorView("Nie wybrano tagu do edycji");
                        errorview.ShowDialog();
                    }
                    else
                    {
                        var errorview = new ErrorView("Ten tag jest już zapisany dla tego obrazu");
                        errorview.ShowDialog();
                    }

                }
            }
            else
            {
                var errorview = new ErrorView("Podana ścieżka nie istnieje");
                errorview.ShowDialog();
            }


            editTagComboBox.Text = null;
            editTagControl.Visibility = Visibility.Hidden;
        }
    }
}
