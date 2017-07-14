using BloombergTrader.Client.Model;
using BloombergTrader.Client.Services;
using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Reactive;

namespace BloombergTrader.Client.ViewModels
{
    // ReSharper disable once ClassNeverInstantiated.Global
    public class MainViewModel : BaseViewModel, IMainViewModel
    {
        private IEnumerable<BloombergSymbol> _selectedSymbols;
        private readonly ObservableAsPropertyHelper<ObservableCollection<BloombergSymbol>> _symbols;
        private string _keyword;



        public MainViewModel(INavigationService navigationService
            , IDialogService dialogService
            , ISymbolService symbolService
            , IHistoricalPriceViewModel historicalPriceViewModel
            , ISettingsViewModel settings) : base(navigationService, dialogService)
        {
            PriceViewModel = historicalPriceViewModel;
            Settings = settings;
            
            SearchSymbols = ReactiveCommand.CreateFromTask<object, ObservableCollection<BloombergSymbol>>(async _ =>
                {
                    IsLoading = true;
                    var result = await symbolService.SearchSymbol(Keyword);
                    IsLoading = false;
                    return new ObservableCollection<BloombergSymbol>(result);
                }, this.WhenAny(x => x.Keyword, x => !string.IsNullOrEmpty(x.Value)));

            
            SearchSymbols.ThrownExceptions.Subscribe(ex =>
                {
                    Debug.WriteLine(ex.Message);
                    IsLoading = false;
                });


            OpenPrices = ReactiveCommand.Create<Unit,Unit>(_ =>
                {
                    SelectedSymbols = Symbols.Where(s => s.Selected);
                    return Unit.Default;
                });
            
            _symbols = this.WhenAnyObservable(x => x.SearchSymbols)
                .ToProperty(this, x => x.Symbols);
        }

        public ISettingsViewModel Settings { get; set; }

        public IHistoricalPriceViewModel PriceViewModel { get; set; }
        
        public ReactiveCommand<Unit, Unit> OpenPrices { get; set; }

        public ReactiveCommand<object, ObservableCollection<BloombergSymbol>> SearchSymbols { get; set; }
        
        public ObservableCollection<BloombergSymbol> Symbols => _symbols.Value;

        public string Keyword
        {
            get { return _keyword; }
            set { this.RaiseAndSetIfChanged(ref _keyword, value); }
        }

        public IEnumerable<BloombergSymbol> SelectedSymbols
        {
            get { return _selectedSymbols; }
            set { this.RaiseAndSetIfChanged(ref _selectedSymbols, value); }
        }

    }
}
