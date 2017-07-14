using Autofac;
using BloombergTrader.Client.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BloombergTrader.Client
{
    public abstract class BootstrapperBase
    {
        public static IContainer Container { get; private set; } 

        public IContainer Build()
        {
            var builder = new ContainerBuilder();
            RegisterTypes(builder);
            builder.RegisterType<MainViewModel>().As<IMainViewModel>().SingleInstance();
            builder.RegisterType<HistoricalPriceViewModel>().As<IHistoricalPriceViewModel>().SingleInstance();
            var build = builder.Build();
            Container = build;
            return build;
        }

        protected abstract void RegisterTypes(ContainerBuilder builder);
    }
}
