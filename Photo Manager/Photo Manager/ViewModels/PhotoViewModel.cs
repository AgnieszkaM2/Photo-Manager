using Photo_Manager.Commands;
using Photo_Manager.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Photo_Manager.ViewModels
{
    public class PhotoViewModel : BaseViewModel
    {
        private INavigationService _navigation;

        public INavigationService Navigation
        {
            get => _navigation;
            set
            {
                _navigation = value;
                OnPropertyChanged();
            }
        }
        

        public ICommand NavigateMainViewCommand { get; set; }
        public ICommand NavigateAddDirectoryViewCommand { get; set; }
        public ICommand NavigatePhotoGalleryViewCommand { get; set; }
        public ICommand NavigatePhotoViewCommand { get; set; }

        public PhotoViewModel(INavigationService navService)
        {
            _navigation = navService;
            NavigateMainViewCommand = new UpdateViewCommand(o => { Navigation.NavigateTO<MainViewModel>(); }, o => true);
            NavigateAddDirectoryViewCommand = new UpdateViewCommand(o => { Navigation.NavigateTO<AddDirectoryViewModel>(); }, o => true);
            NavigatePhotoGalleryViewCommand = new UpdateViewCommand(o => { Navigation.NavigateTO<PhotoGalleryViewModel>(); }, o => true);
            NavigatePhotoViewCommand = new UpdateViewCommand(o => { Navigation.NavigateTO<PhotoViewModel>(); }, o => true);

        }

    }
}
