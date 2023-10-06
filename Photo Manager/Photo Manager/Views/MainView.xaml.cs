using Microsoft.VisualBasic;
using Newtonsoft.Json;
using Photo_Manager.AdditionalResources;
using Photo_Manager.Commands;
using Photo_Manager.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
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
    /// Interaction logic for MainView.xaml
    /// </summary>
    public partial class MainView : UserControl
    {
       

        public MainView()
        {
            InitializeComponent();
        }

        private void btnAddNewDirectory_Click(object sender, RoutedEventArgs e)
        {       
        }

        private void btnGallery_Click(object sender, RoutedEventArgs e)
        {
        }

        private void mainViewGridItems_Loaded(object sender, RoutedEventArgs e)
        {
            string _path = ResourcesPaths.SavedDirectoriesPath;

            if (!File.Exists(_path)) File.CreateText(_path).Close();

            var jsonData = System.IO.File.ReadAllText(_path);
            var dirList = JsonConvert.DeserializeObject<List<Directories>>(jsonData) ?? new List<Directories>();

            foreach (var (dir, newBtn, _label, _stackPanel) in from dir in dirList
                                                             let newBtn = new Button()
                                                             let _label = new Label()
                                                             let _stackPanel = new StackPanel()
                                                             select (dir, newBtn, _label, _stackPanel))
            {
                _label.Content = dir.DirPath.ToString()[(dir.DirPath.ToString().LastIndexOf("\\")..)];
                _label.SetResourceReference(Control.ForegroundProperty, "TextContrast");
                
                newBtn.Content = new Image
                {
                    Width = 100,
                    Height = 100,
                    Source = new BitmapImage(new Uri(@"../Assets/icons8-photo-64.png", UriKind.Relative)),
                    VerticalAlignment = VerticalAlignment.Center,
                };

                newBtn.Width = 115;
                newBtn.Height = 190;
                newBtn.Background = new SolidColorBrush(Color.FromArgb(0x66, 0x00, 0xB4, 0xD8));
                newBtn.Margin = new Thickness(5);
                newBtn.SetResourceReference(Control.StyleProperty, "MainPanelItems");
                newBtn.SetBinding(Button.CommandProperty, new Binding("NavigatePhotoGalleryViewCommand"));
                newBtn.Click += new RoutedEventHandler(btnGalleryView);
                newBtn.Tag = dir.DirPath;
                _stackPanel.Children.Add(newBtn);
                _stackPanel.Children.Add(_label);

                MainGalleryStackPanel.Children.Add(_stackPanel);
            }
        }

        private void btnGalleryView(object sender, RoutedEventArgs e)
        {
            var selectedDir = ((Button)sender).Tag;
            CurrentResources.ChosenPath= (string)selectedDir;
        }

    }
}