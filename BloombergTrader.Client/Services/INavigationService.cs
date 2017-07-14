using BloombergTrader.Client.ViewModels;

namespace BloombergTrader.Client.Services
{
    public interface INavigationService
    {
        void Navigate(INavigatableViewModel viewModel);

        void NavigateAndClean(INavigatableViewModel viewModel);

        void Back();
    }
}
