namespace VendingMachine
{
    using System.Windows;

    using Autofac;

    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App
    {
        /// <summary>
        /// The dependency injection container.
        /// </summary>
        private IContainer container;

        /// <summary>
        /// Called on application start up. We register all the modules that are registered with autofac
        /// and build the container.
        /// </summary>
        protected override void OnStartup(StartupEventArgs e)
        {
            var builder = new ContainerBuilder();

            builder.RegisterModule<AutofacModule>();
            builder.RegisterModule<Client.AutofacModule>();
            builder.RegisterModule<ViewModels.AutofacModule>();
            builder.RegisterModule<Utilities.AutofacModule>();
            builder.RegisterModule<States.AutofacModule>();
            
            this.container = builder.Build();

            Current.Exit += this.OnCurrentOnExit;
        }

        /// <summary>
        /// Called when the current application exits. We dispose the container.
        /// The container in turn will dispose all the objects it has resolved.
        /// </summary>
        private void OnCurrentOnExit(object sender, ExitEventArgs args)
        {
            this.container.Dispose();
        }
    }
}
