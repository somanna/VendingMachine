namespace VendingMachine.ViewModels
{
    using Autofac;

    using VendingMachine.Common;
    using VendingMachine.ViewModels.Commands;

    public class AutofacModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            base.Load(builder);

            builder.RegisterType<MainWindowViewModel>().As<IMainWindowViewModel>().PropertiesAutowired().SingleInstance();

            builder.RegisterType<CancelCommand>().PropertiesAutowired().SingleInstance();
            builder.RegisterType<ConfirmPaymentCommand>().PropertiesAutowired().SingleInstance();
            builder.RegisterType<InsertCoinCommand>().PropertiesAutowired().SingleInstance();
            builder.RegisterType<SelectItemCommand>().PropertiesAutowired().SingleInstance();
        }
    }
}