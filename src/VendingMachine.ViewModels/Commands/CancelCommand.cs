namespace VendingMachine.ViewModels.Commands
{
    using System;
    using System.Windows.Input;

    using VendingMachine.Common;

    public class CancelCommand : ICommand
    {
        public event EventHandler CanExecuteChanged;

        public void Execute(object parameter)
        {
            var viewModel = (IMainWindowViewModel)parameter;
            try
            {
                viewModel.CurrentState = viewModel.CurrentState.Cancel(viewModel);
            }
            catch (NotSupportedException ex)
            {
                viewModel.DisplayMessage(ex.Message);
            }
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }
    }
}