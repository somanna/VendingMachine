namespace VendingMachine.Utilities
{
    using System.Collections.Generic;

    using VendingMachine.Common;
    using VendingMachine.Data;

    /// <summary>
    /// Provides methods to do calculations with coins 
    /// </summary>
    public class MoneyManager : IMoneyManager
    {
        public IChangeCalculator ChangeCalculator { get; set; }

        public bool IsPaymentSufficient(Item itemToBuy, IEnumerable<CoinStock> insertedCoins)
        {
            return insertedCoins.GetTotalAmount() >= itemToBuy.Price;
        }

        public int CalculateChange(int itemPrice, IEnumerable<CoinStock> insertedCoins)
        {
            return insertedCoins.GetTotalAmount() - itemPrice;
        }

        public IList<CoinStock> GetChange(IList<CoinStock> availableCoins, int changeAmount)
        {
            var change = this.ChangeCalculator.GetChange(availableCoins, changeAmount);
            if (change.Count > 0)
            {
                return change;
            }

            return null;
        }
    }
}