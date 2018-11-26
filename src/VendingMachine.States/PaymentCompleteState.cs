namespace VendingMachine.States
{
    using System.Collections.Generic;

    using VendingMachine.Common;
    using VendingMachine.Data;

    /// <summary>
    /// This the state of the vending machine when the payment is completed and user can confirm to get the item and change
    /// </summary>
    public class PaymentCompleteState : State
    {
        public override string StateName
        {
            get
            {
                return "PAYMENT COMPLETE STATE";
            }
        }

         public StartState StartState { get; set; }

         public IMoneyManager MoneyManager { get; set; }

        // we are not moving to this state from PaymentComplete state but we can use the cancel operation.
        public AcceptingCoinsState AcceptingCoinsState { get; set; }

        public override State Cancel(IMainWindowViewModel viewModel)
        {
            return this.AcceptingCoinsState.Cancel(viewModel);
        }

        public override State ConfirmPayment(IMainWindowViewModel viewModel)
        {
            // we need to check if change has to be give. If so, do we have enough coins to give back the change?
            var changeAmount = this.MoneyManager.CalculateChange(
                viewModel.SelectedItem.Price, viewModel.InsertedCoinStock);
            var combinedCoinStock = this.GetCombinedCoinStock(viewModel);

            IList<CoinStock> coinStockToReturn = null;
            if (changeAmount > 0)
            {
                coinStockToReturn = this.MoneyManager.GetChange(combinedCoinStock, changeAmount);
                if (coinStockToReturn == null)
                {
                    viewModel.DisplayMessage("No change to give back. Coins returned");
                    return this.Cancel(viewModel);
                }

                // give back the change
                foreach (var coinStock in coinStockToReturn)
                {
                    viewModel.ReturnedCoins.Add(coinStock);
                }
            }

            // We have to take out the change from the available coin stock and
            // add the inserted coins into the available coin stock
            this.UpdateCoinStock(viewModel, coinStockToReturn ?? new List<CoinStock>());

            // Remvoe the item that is being given to user from the available stock
            viewModel.AvailableItemStock.RemoveItem(viewModel.SelectedItem);

            viewModel.GivenItem = viewModel.SelectedItem;
            viewModel.StatusMessage = "Item given";

            // in all cases, we go back to start state
            return this.StartState;
        }

        private IList<CoinStock> GetCombinedCoinStock(IMainWindowViewModel viewModel)
        {
            // first make a copy of available coin stocks
            var availableCoinStocks = viewModel.AvailableCoinStock.Clone();

            // Add the coins that are inserted into the machine
            foreach (var coinStock in viewModel.InsertedCoinStock)
            {
                availableCoinStocks.AddCoin(coinStock.Coin, coinStock.Quantity);
            }

            return availableCoinStocks;
        }

        private void UpdateCoinStock(IMainWindowViewModel viewModel, IEnumerable<CoinStock> coinStockToReturn)
        {
            // Add inserted coins to stock
            foreach (var coinStock in viewModel.InsertedCoinStock)
            {
                viewModel.AvailableCoinStock.AddCoin(coinStock.Coin, coinStock.Quantity);
            }

            // remove returned coins
            foreach (var coinStock in coinStockToReturn)
            {
                viewModel.AvailableCoinStock.RemoveCoin(coinStock.Coin, coinStock.Quantity);
            }
        }
    }
}