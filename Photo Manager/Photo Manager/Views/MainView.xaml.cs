using Microsoft.VisualBasic;
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
            //string[] filesindirectory = Directory.GetFiles(@"C:\Git\Nowy folder");

            //List<string> images = new List<string>(filesindirectory.Count());
            //foreach (string s in filesindirectory)
            //{
            //    images.Add(String.Format("~/Images/{0}", System.IO.Path.GetFileName(s)));
            //}

            mainViewGridFrame.Visibility = Visibility.Hidden;
           // addDirectory.Visibility = Visibility.Visible;
        

        }
    }
}
