using Autofac;
using BloombergTrader.Client.Services;
using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using AutoMapper;
using BloombergTrader.DataTransferObjects;

namespace BloombergTrader.Client.ViewModels
{
    // ReSharper disable once ClassNeverInstantiated.Global
    public class HistoricalPriceViewModel : BaseViewModel, IHistoricalPriceViewModel
    {
        private readonly ObservableAsPropertyHelper<ObservableCollection<PricingViewModel>> _prices;
        private DateTime? _endDate;
        private DateTime? _startDate;
        private bool _pricesLoaded;


        public HistoricalPriceViewModel( INavigationService navigationService
                                       , IDialogService dialogService
                                       , ISettingsViewModel settings
                                       , IBloombergTraderApi clientApi) : base(navigationService, dialogService)
        {

            var canLoad = this.WhenAny(x => x.StartDate, x => x.EndDate, (s, e) => s.Value != null && e.Value != null);
            LoadPrices = ReactiveCommand.CreateFromTask<object, ObservableCollection<PricingViewModel>>(async x =>
                {
                    var mainVieModel = BootstrapperBase.Container.Resolve<IMainViewModel>();

                    var symbols = Mapper.Map<IEnumerable<SymbolRequest>>(mainVieModel.SelectedSymbols);

                    var response = await clientApi.GetPriceForRequests(symbols, StartDate, EndDate);

                    var result = Mapper.Map<IEnumerable<PricingViewModel>>(response);

                    return new ObservableCollection<PricingViewModel>(result);
                }, canLoad);

            _prices = this.WhenAnyObservable(x => x.LoadPrices).ToProperty(this, x => x.Prices);
            Settings = settings;
        }

        public ISettingsViewModel Settings { get; set; }

        public ReactiveCommand<object, ObservableCollection<PricingViewModel>> LoadPrices { get; set; }

        public ObservableCollection<PricingViewModel> Prices => _prices.Value;

        public bool PricesLoaded
        {
            get { return _pricesLoaded; }
            set { this.RaiseAndSetIfChanged(ref _pricesLoaded, value); }
        }

        public DateTime? StartDate
        {
            get { return _startDate; }
            set { this.RaiseAndSetIfChanged(ref _startDate, value); }
        }

        public DateTime? EndDate
        {
            get { return _endDate; }
            set { this.RaiseAndSetIfChanged(ref _endDate, value); }
        }
    }
}
