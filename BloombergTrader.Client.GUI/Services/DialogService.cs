using BloombergTrader.Client.Services;
using MahApps.Metro.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace BloombergTrader.Client.GUI.Services
{
    public class DialogService : IDialogService
    {
        public void Show(string message, string title, string cancelText = null, Action<bool> confirm = null)
        {
            var result = MessageBox.Show(message, title, MessageBoxButton.YesNo);
            if(result == MessageBoxResult.Yes)
            {
                confirm?.Invoke(true);
            }
        }

        public void ShowLoading(string message = null)
        {
            App.MetroWindow.ShowOverlay();
        }

        public void HideLoading()
        {
            if (App.MetroWindow != null && App.MetroWindow.IsOverlayVisible())
                App.MetroWindow?.HideOverlay();
        }
    }
}
