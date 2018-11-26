namespace VendingMachine.States
{
    using VendingMachine.Common;
    using VendingMachine.Data;

    /// <summary>
    /// This the state of the vending machine when it is accepting coins from the vending machine
    /// </summary>
    public class AcceptingCoinsState : State
    {
        public override string StateName
        {
            get
            {
                return "ACCEPTING COINS";
            }
        }

        public PaymentCompleteState PaymentCompleteState { get; set; }

        public StartState StartState { get; set; }

        public IMoneyManager MoneyManager { get; set; }

        public override State Cancel(IMainWindowViewModel viewModel)
        {
            viewModel.SelectedItem = null;
            viewModel.SelectedCoinStock = null;

            // return the coins
            foreach (var coinStock in viewModel.InsertedCoinStock)
            {
                viewModel.ReturnedCoins.Add(coinStock);
            }

            viewModel.InsertedCoinStock.Clear();
            viewModel.StatusMessage = "Cancelled vending.";

            return this.StartState;
        }

        public override State InsertCoin(IMainWindowViewModel viewModel)
        {
            if (viewModel.SelectedCoinStock == null)
            {
                viewModel.DisplayMessage("Please select a coin to insert");
                return this;
            }

            viewModel.InsertedCoinStock.AddCoin(viewModel.SelectedCoinStock.Coin, 1);

            if (this.MoneyManager.IsPaymentSufficient(viewModel.SelectedItem, viewModel.InsertedCoinStock))
            {
                viewModel.StatusMessage = "Payment suffificient. Confirm payment";
                return this.PaymentCompleteState;
            }

            viewModel.StatusMessage = "Payment insufficient. Insert more coins";
            return this;
        }
    }
}