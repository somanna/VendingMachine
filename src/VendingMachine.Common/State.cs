namespace VendingMachine.Common
{
    using System;

    /// <summary>
    /// An abstract state class. All methods in the state class are the actions that can be performed on the vending machine.
    /// These methods are virtual and throws a default not supported exception.
    /// Any concrete state class that want to support an action should override this method 
    /// and not bother about the actions that it doesnt want to support.
    /// Howerer, State name is an abstract method which has to be implemented by the concrete classes to give out its state name.
    /// </summary>
    public abstract class State
    {
        public abstract string StateName { get; }

        public virtual State SelectItem(IMainWindowViewModel viewModel)
        {
            throw new NotSupportedException(string.Format("Cannot select item in '{0}' state", this.StateName));
        }

        public virtual State InsertCoin(IMainWindowViewModel viewModel)
        {
            throw new NotSupportedException(string.Format("Cannot insert coins in '{0}' state", this.StateName));
        }

        public virtual State Cancel(IMainWindowViewModel viewModel)
        {
            throw new NotSupportedException(string.Format("Cannot cancel in '{0}' state", this.StateName));
        }

        public virtual State ConfirmPayment(IMainWindowViewModel viewModel)
        {
            throw new NotSupportedException(string.Format("Cannot confirm payment in '{0}' state", this.StateName));
        }

        public override string ToString()
        {
            return this.StateName;
        }
    }
}