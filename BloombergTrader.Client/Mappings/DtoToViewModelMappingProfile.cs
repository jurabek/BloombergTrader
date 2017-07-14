using AutoMapper;
using BloombergTrader.Client.Model;
using BloombergTrader.Client.ViewModels;
using BloombergTrader.DataTransferObjects;

namespace BloombergTrader.Client.Mappings
{
    public class DtoToViewModelMappingProfile : Profile
    {
        public DtoToViewModelMappingProfile()
        {
            CreateMap<PriceResponse, PricingViewModel>()
                .ForMember(vm => vm.Ask,
                    map => map.MapFrom(r => new OneWayPriceViewModel(Direction.Buy, r.Ask.ToString())))
                .ForMember(vm => vm.Bid,
                    map => map.MapFrom(r => new OneWayPriceViewModel(Direction.Sell, r.Bid.ToString())))
                .ForMember(vm => vm.Mid,
                    map => map.MapFrom(r => (r.Ask + r.Bid) / 2))
                .ForMember(vm => vm.SpotDate, map => map.MapFrom(r => "SP. " + r.Date.ToString("dd MMM")));

            CreateMap<BloombergSymbol, SymbolRequest>();
        }
    }
}
