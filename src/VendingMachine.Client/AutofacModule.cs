namespace VendingMachine.Client
{
    using Autofac;

    using VendingMachine.Common;

    public class AutofacModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            base.Load(builder);

            builder.RegisterType<MainWindow>().As<MainWindow>().AsImplementedInterfaces().SingleInstance();
            builder.RegisterType<WindowsMessageBox>().As<IMessageBox>().SingleInstance();
        }
    }
}