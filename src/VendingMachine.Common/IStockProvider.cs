namespace VendingMachine.Common
{
    using System.Collections.Generic;

    using VendingMachine.Data;

    /// <summary>
    /// The StockProvider to provide the items and coins for the vending machine to stock up.
    /// </summary>
    public interface IStockProvider
    {
        /// <summary>
        /// Gets the item stock
        /// </summary>
        IEnumerable<ItemStock> GetItemStock();

        /// <summary>
        /// Gets the coin stock
        /// </summary>
        IEnumerable<CoinStock> GetCoinStock();
    }
}