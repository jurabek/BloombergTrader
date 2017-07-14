using BloombergTrader.Client.Services;
using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BloombergTrader.Client.ViewModels
{
    public abstract class BaseViewModel : ReactiveObject, INavigatableViewModel
    {
        public IDialogService DialogService { get; protected set; }

        private bool _isLoading;

        public bool IsLoading
        {
            get { return _isLoading; }
            set { this.RaiseAndSetIfChanged(ref _isLoading, value); }
        }

        public INavigationService NavigationService { get; protected set; }

        public string Title { get; set; }

        protected BaseViewModel(INavigationService navigationService, IDialogService dialogService)
        {
            NavigationService = navigationService;
            DialogService = dialogService;
            this.WhenAny(x => x.IsLoading, x => x.Value)
                .Subscribe(isLoading =>
                {
                    if (isLoading)
                        DialogService.ShowLoading();
                    else
                        DialogService.HideLoading();
                });
        }
    }
}
