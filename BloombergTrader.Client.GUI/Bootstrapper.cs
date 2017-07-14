using BloombergTrader.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autofac;
using BloombergTrader.Client.GUI.Services;
using BloombergTrader.Client.Services;
using ReactiveUI;
using AutoMapper;
using BloombergTrader.Client.GUI.Mappings;
using BloombergTrader.Client.Mappings;
using Refit;

namespace BloombergTrader.Client.GUI
{
    public class Bootstrapper : BootstrapperBase
    {
        private const string ServerAddress = "http://localhost:13098/";
        public SettingsViewModel UserSettings { get; set; }

        protected override void RegisterTypes(ContainerBuilder builder)
        {
            builder.RegisterType<NavigationService>().As<INavigationService>().SingleInstance();
            builder.RegisterType<DialogService>().As<IDialogService>().SingleInstance();
            builder.RegisterType<SymbolService>().As<ISymbolService>().SingleInstance();
            UserSettings = SettingsViewModel.Load();

            this.WhenAnyObservable(x => x.UserSettings.Changed)
                .Subscribe(_ =>
                {
                    SettingsViewModel.Save(UserSettings);
                });

            builder.Register(x => UserSettings).As<ISettingsViewModel>().SingleInstance();

            builder.Register(x => RestService.For<IBloombergTraderApi>(ServerAddress)).AsSelf().SingleInstance();

            Mapper.Initialize(cfg => {
                cfg.AddProfile<ModelToViewModelMappingProfile>();
                cfg.AddProfile<DtoToViewModelMappingProfile>();
            });
            
        }
    }
}
