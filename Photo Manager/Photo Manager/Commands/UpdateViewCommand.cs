using Photo_Manager.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Photo_Manager.Commands
{
    public class UpdateViewCommand : ICommand

    {
        private MainWindowViewModel viewModel;
        public UpdateViewCommand(MainWindowViewModel viewModel) 
        {
            this.viewModel = viewModel;
        }

        public event EventHandler? CanExecuteChanged;

        public bool CanExecute(object? parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            Console.WriteLine("test");
            if (parameter.ToString() == "Home")
            {
                viewModel.SelectedViewModel = new MainViewModel();
            }
            else if (parameter.ToString() == "AddDirectory")
            {
                viewModel.SelectedViewModel = new AddDirectoryViewModel();
            }
            else if (parameter.ToString() == "PhotoGallery")
            {
                viewModel.SelectedViewModel = new PhotoGalleryViewModel();
            }
            else if (parameter.ToString() == "Photo")
            {
                viewModel.SelectedViewModel = new PhotoViewModel();
            }
            //else if (parameter.ToString() == "SideMenu")
            //{
            //    viewModel.SelectedSideMenu = new SideMenuViewModel();
            //}
            else if (parameter.ToString() == "Error")
            {
                viewModel.SelectedViewModel = new ErrorViewModel();
            }
        }
    }
}
