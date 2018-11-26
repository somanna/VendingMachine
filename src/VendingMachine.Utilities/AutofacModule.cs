namespace VendingMachine.Utilities
{
    using Autofac;

    using VendingMachine.Common;

    public class AutofacModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            base.Load(builder);

            builder.RegisterType<StockProvider>().As<IStockProvider>().PropertiesAutowired().SingleInstance();
            builder.RegisterType<MoneyManager>().As<IMoneyManager>().PropertiesAutowired().SingleInstance();
            builder.RegisterType<ChangeCalculator>().As<IChangeCalculator>().PropertiesAutowired().SingleInstance();
        }
    }
}