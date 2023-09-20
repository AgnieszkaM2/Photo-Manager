using Newtonsoft.Json;
using Photo_Manager.AdditionalResources;
using Photo_Manager.ViewModels;
using System;
using System.Collections.Generic;
using System.IO;
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

            addTagControl.Visibility = Visibility.Hidden;
            removeTagControl.Visibility = Visibility.Hidden;
            editTagControl.Visibility = Visibility.Hidden;
        }

        private void PhotoView_Loaded(object sender, RoutedEventArgs e)
        {
            var imageDir = CurrentResources.ChosenImage;
            int imageDirIndex = CurrentResources.CurrentGallery.IndexOf(imageDir); 
            mediaElement.Source = (new Uri(CurrentResources.CurrentGallery[imageDirIndex]));
        }
        
        public void btnReturnToGallery_Click(object sender, RoutedEventArgs e)
        {
            
        }

        private void PhotoView_Unloaded(object sender, RoutedEventArgs e)
        {

        }

        private void btnNextImage_Click(object sender, RoutedEventArgs e)
        {
            var imageDir = CurrentResources.ChosenImage;
            int imageDirIndex = CurrentResources.CurrentGallery.IndexOf(imageDir);
            if (imageDirIndex < (CurrentResources.CurrentGallery.Count-1))
            {
                imageDirIndex += 1;
                mediaElement.Source = (new Uri(CurrentResources.CurrentGallery[imageDirIndex]));
                CurrentResources.ChosenImage = CurrentResources.CurrentGallery[imageDirIndex];

            }
            

        }

        private void btnPreviousImage_Click(object sender, RoutedEventArgs e)
        {
            var imageDir = CurrentResources.ChosenImage;
            int imageDirIndex = CurrentResources.CurrentGallery.IndexOf(imageDir);
            if (imageDirIndex != 0)
            {
                imageDirIndex -= 1;
                mediaElement.Source = (new Uri(CurrentResources.CurrentGallery[imageDirIndex]));
                CurrentResources.ChosenImage = CurrentResources.CurrentGallery[imageDirIndex];

            }
        }

        private void imageWindow_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            var _sender = ((Grid)sender);
            var _mediaElement = (MediaElement)_sender.Children[0];
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

                    if (allTags.Count > 0)
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
            if (CurrentResources.ChosenImage != null)
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

                string newTag = null;
                if (addTagComboBox.SelectedItem != null)
                {
                    newTag = addTagComboBox.SelectedItem.ToString();
                }
                else if (addTagComboBox.Text != null && addTagComboBox.SelectedItem == null)
                {
                    newTag = addTagComboBox.Text;
                }


                if ((string.IsNullOrEmpty(newTag) == false) && (tagsList.Where(x => x.PhotoPath == CurrentResources.ChosenImage).FirstOrDefault(x => x.Name == newTag)) == null)
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
                    if (string.IsNullOrEmpty(newTag) == true)
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
                    else if (string.IsNullOrEmpty(oldTag) == true)
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
