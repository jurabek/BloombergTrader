using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Reactive;
using BloombergTrader.Client.Model;
using ReactiveUI;

namespace BloombergTrader.Client.ViewModels
{
    public interface IMainViewModel : IReactiveObject
    {
        ISettingsViewModel Settings {get;set;}

        string Keyword { get; set; }

        ObservableCollection<BloombergSymbol> Symbols { get; }

        IEnumerable<BloombergSymbol> SelectedSymbols { get; set; }

        ReactiveCommand<object, ObservableCollection<BloombergSymbol>> SearchSymbols { get; set; }

        ReactiveCommand<Unit, Unit> OpenPrices { get; set; }

        IHistoricalPriceViewModel PriceViewModel { get; set; }
    }
}