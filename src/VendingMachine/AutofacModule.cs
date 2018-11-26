namespace VendingMachine
{
    using System.Windows;

    using Autofac;

    public class AutofacModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            base.Load(builder);

            // This is the entry point to the application.
            builder.RegisterType<ApplicationStartup>().As<IStartable>().PropertiesAutowired().SingleInstance();
            
            builder.Register(c => Application.Current).SingleInstance();
        }
    }
}