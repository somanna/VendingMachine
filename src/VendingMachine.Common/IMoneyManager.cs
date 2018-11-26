namespace VendingMachine.Common
{
    using System.Collections.Generic;

    using VendingMachine.Data;

    /// <summary>
    /// Money manager interface that provides all money related calculations.
    /// </summary>
    public interface IMoneyManager
    {
        /// <summary>
        /// Returns true if the payment is sufficient to buy the item.
        /// </summary>
        bool IsPaymentSufficient(Item itemToBuy, IEnumerable<CoinStock> insertedCoins);

        /// <summary>
        /// Calculates the change that has to be returned to the user
        /// </summary>
        int CalculateChange(int itemPrice, IEnumerable<CoinStock> insertedCoins);

        /// <summary>
        /// Gets the required change from the available coins
        /// </summary>
        IList<CoinStock> GetChange(IList<CoinStock> availableCoins, int changeAmount);
    }
}