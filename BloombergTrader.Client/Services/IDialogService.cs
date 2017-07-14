using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BloombergTrader.Client.Services
{
    public interface IDialogService
    {
        void Show(string message, string title, string cancelText = null, Action<bool> confirm = null);

        void ShowLoading(string message = null);

        void HideLoading();
    }
}
