using System;
using System.Collections.ObjectModel;
using ReactiveUI;

namespace BloombergTrader.Client.ViewModels
{
    public interface IHistoricalPriceViewModel
    {
        ISettingsViewModel Settings { get; set; }

        bool PricesLoaded { get; set; }

        ObservableCollection<PricingViewModel> Prices { get; }

        ReactiveCommand<object, ObservableCollection<PricingViewModel>>  LoadPrices { get; set; }

        DateTime? StartDate { get; set; }

        DateTime? EndDate { get; set; }

    }
}