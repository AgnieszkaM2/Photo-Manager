using Photo_Manager.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Photo_Manager.ViewModels
{
    public class MainWindowViewModel : BaseViewModel
    {
        private BaseViewModel _selectedViewModel;
        //private BaseViewModel _selectedSideMenu;

        public BaseViewModel SelectedViewModel
        {
            get { return _selectedViewModel; }
            set
            {
                _selectedViewModel = value;
                OnPropertyChanged(nameof(SelectedViewModel));
            }
        }

        //public BaseViewModel SelectedSideMenu 
        //{
        //    get { return _selectedSideMenu; }
        //    set 
        //    { 
        //        _selectedSideMenu = value;
        //        OnPropertyChanged(nameof(SelectedSideMenu));
        //    }
        //}

        public ICommand UpdateViewCommand { get; set; }

        public MainWindowViewModel()
        {
            UpdateViewCommand = new UpdateViewCommand(this);
        }
    }
}
