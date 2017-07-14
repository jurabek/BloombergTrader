using BloombergTrader.Client.Services;

namespace BloombergTrader.Client.ViewModels
{
    public interface INavigatableViewModel
    {
        string Title { get; set; }

        INavigationService NavigationService { get; }

    }
}