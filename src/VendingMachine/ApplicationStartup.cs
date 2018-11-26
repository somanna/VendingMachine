namespace VendingMachine
{
    using System.Windows;

    using Autofac;

    using VendingMachine.Client;
    using VendingMachine.Common;

    /// <summary>
    /// IStartable is an Autofac interface whose Start method will be called by the container during the build
    /// </summary>
    public class ApplicationStartup : IStartable
    {
        public MainWindow MainWindow { get; set; }

        public Application Application { get; set; }

        public IMainWindowViewModel MainWindowViewModel { get; set; }

        public void Start()
        {
            this.MainWindow.Closed += (sender, args) => this.Exit();
            this.MainWindow.DataContext = this.MainWindowViewModel;
            this.MainWindow.Show();
        }

        private void Exit()
        {
            this.Application.Shutdown();
        }
    }
}