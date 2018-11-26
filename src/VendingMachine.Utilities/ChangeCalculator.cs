namespace VendingMachine.Utilities
{
    using System.Collections.Generic;
    using System.Linq;

    using VendingMachine.Common;
    using VendingMachine.Data;

    /// <summary>
    /// This class returns the required change by picking higher denominations first
    /// </summary>
    public class ChangeCalculator : IChangeCalculator
    {
        /// <summary>
        /// Returned change will be empty if we do not have enough coins to give change
        /// </summary>
        public IList<CoinStock> GetChange(IList<CoinStock> availableCoinStock, int changeAmount)
        {
            IList<CoinStock> change = new List<CoinStock>();
            
            if (changeAmount == 0)
            {
                return change;
            }

            // we dont have enough to give back change
            if (changeAmount > availableCoinStock.GetTotalAmount())
            {
                return change;
            }

            // we are only interested in those coins with quantity > 0 and the coin value is <= change required
            // we dont have to worry about those coins whose denomination is higher than the change amount
            foreach (var coinStock in
                availableCoinStock.Where(cs => cs.Quantity > 0 && cs.Coin.Value <= changeAmount).OrderByDescending(cs => cs.Coin.Value))
            {
                // we assume this coin can be part of the change
                change.AddCoin(coinStock.Coin, 1);
                if (coinStock.Coin.Value == changeAmount)
                {
                    // we have found the required change
                    return change;
                }
                
                // Create a copy of available coins, remove one coin (the coin that we are handling in this iteration)
                var availableStockCopy = availableCoinStock.Clone();
                availableStockCopy.RemoveCoin(coinStock.Coin, 1);
                var changeFromRemainingCoins = this.GetChange(availableStockCopy, changeAmount - coinStock.Coin.Value);
                if (changeFromRemainingCoins.Count > 0)
                {
                    // We have got the required change from the required coins
                    foreach (var newStock in changeFromRemainingCoins)
                    {
                        change.AddCoin(newStock.Coin, newStock.Quantity);
                    }

                    return change;
                }
            }

            // we might have come out without having the required change. Dont give anything back.
            if (change.GetTotalAmount() != changeAmount)
            {
                change.Clear();
            }

            return change;
        }
    }
}