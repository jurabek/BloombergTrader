using Autofac;
using BloombergTrader.Client.GUI.UI;
using BloombergTrader.Client.ViewModels;
using log4net;
using MahApps.Metro.Controls;
using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace BloombergTrader.Client.GUI
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private static readonly ILog Log = LogManager.GetLogger(typeof(App));
        public IContainer Container { get; set; }

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            log4net.Config.XmlConfigurator.Configure();
            Start();
        }
        public IMainViewModel ViewModel { get; set; }
        private void Start()
        {
            Log.Info("Initializing bootsrapper...");
            var bootstrapper = new Bootstrapper();
            IContainer container = bootstrapper.Build();
            ViewModel = container.Resolve<IMainViewModel>();
            MainWindow = new MainWindow(ViewModel);

            this.WhenAnyObservable(x => x.ViewModel.OpenPrices).Subscribe(_ =>
                {
                    MainWindow.Content = new PricesView();
                });

            MainWindow.Content = new SearchView();
            MainWindow.Show();
            MetroWindow = MainWindow as MetroWindow;
        }

        internal static MetroWindow MetroWindow { get; private set; }
    }
}
