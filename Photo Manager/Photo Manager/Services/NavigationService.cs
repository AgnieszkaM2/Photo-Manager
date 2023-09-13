using Photo_Manager.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Photo_Manager.Services
{
    public interface INavigationService
    {
        BaseViewModel CurrentViewModel { get; }

        void NavigateTO<T>() where T : BaseViewModel;
    }
    public class NavigationService : BaseViewModel, INavigationService
    {
        private BaseViewModel _currentViewModel;
        private readonly Func<Type, BaseViewModel> _viewModelFactory;

        public BaseViewModel CurrentViewModel
        {
            get => _currentViewModel;
            private set
            {
                _currentViewModel = value;
                OnPropertyChanged();
            }
        }

        public NavigationService(Func<Type, BaseViewModel> viewModelFactory) 
        {
            _viewModelFactory = viewModelFactory;
        }

        public void NavigateTO<TViewModel>() where TViewModel : BaseViewModel
        {
            BaseViewModel viewModel = _viewModelFactory.Invoke(typeof(TViewModel));
            CurrentViewModel = viewModel;
        }

    }

}
