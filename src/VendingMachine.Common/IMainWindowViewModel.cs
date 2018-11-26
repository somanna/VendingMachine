namespace VendingMachine.Common
{
    using System.Collections.ObjectModel;

    using VendingMachine.Data;

    /// <summary>
    /// The MainWindowViewModel interface. This is used by all states and commands to get the MainWindowViewModel.
    /// </summary>
    public interface IMainWindowViewModel
    {
        /// <summary>
        /// Gets or sets the current state of the vending machine
        /// </summary>
        State CurrentState { get; set; }

        /// <summary>
        /// Item that has been selected from the vending machine
        /// </summary>
        Item SelectedItem { get; set; }

        /// <summary>
        /// The selected items stock which the user has selected to buy
        /// </summary>
        ItemStock SelectedItemStock { get; set; }

        /// <summary>
        /// The coin the user has selected to insert into the vending machine
        /// </summary>
        CoinStock SelectedCoinStock { get; set; }

        /// <summary>
        /// Available item stock in the vending machine
        /// </summary>
        ObservableCollection<ItemStock> AvailableItemStock { get; }

        /// <summary>
        /// Available coin stock in the vending machine
        /// </summary>
        ObservableCollection<CoinStock> AvailableCoinStock { get; }

        /// <summary>
        /// Stock of coins the user has inserted into the vending machine
        /// </summary>
        ObservableCollection<CoinStock> InsertedCoinStock { get; }

        /// <summary>
        /// Stock of coins that has been returned to the user
        /// </summary>
        ObservableCollection<CoinStock> ReturnedCoins { get; }

        /// <summary>
        /// The item that was given to the user
        /// </summary>
        Item GivenItem { get; set; }

        /// <summary>
        /// Display a message to the user
        /// </summary>
        void DisplayMessage(string message);

        /// <summary>
        /// Status of the vending machine
        /// </summary>
        string StatusMessage { get; set; }
    }
}