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
    /// Interaction logic for AddDirectoryView.xaml
    /// </summary>
    public partial class AddDirectoryView : UserControl
    {
        public AddDirectoryView()
        {
            InitializeComponent();
            mainView.Visibility = Visibility.Hidden;
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            formGrid.Visibility = Visibility.Hidden;
            mainView.Visibility = Visibility.Visible;
        }

        private void AddDirectory_Loaded(object sender, RoutedEventArgs e)
        {
            mainView.Visibility = Visibility.Hidden;
            formGrid.Visibility = Visibility.Visible;
        }
    }
}
