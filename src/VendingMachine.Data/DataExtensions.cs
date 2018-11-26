namespace VendingMachine.Data
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    /// Extension helper methos that can be used on item stock and coin stock data structures
    /// </summary>
    public static class DataExtensions
    {
        /// <summary>
        /// The get total amount from the coin stock
        /// </summary>
        public static int GetTotalAmount(this IEnumerable<CoinStock> coinStocks)
        {
            return coinStocks.Sum(coinStock => coinStock.Coin.Value * coinStock.Quantity);
        }

        /// <summary>
        /// Adds specified no. of coins into the coin stock
        /// </summary>
        public static void AddCoin(this IList<CoinStock> coinStocks, Coin coinToAdd, int quantity)
        {
            // find if a coin of that denomination is already inserted. if so, increment quantity. else add new.
            var previouslyInsertedCoinStock =
                coinStocks.SingleOrDefault(coinStock => coinStock.Coin.Value == coinToAdd.Value);
            if (previouslyInsertedCoinStock == null)
            {
                coinStocks.Add(new CoinStock(coinToAdd, quantity));
            }
            else
            {
                previouslyInsertedCoinStock.IncrementStock(quantity);
            }
        }

        /// <summary>
        /// Removes specified no. of coins from the coin stock
        /// </summary>
        public static void RemoveCoin(this IEnumerable<CoinStock> coinStocks, Coin coinToRemove, int quantity)
        {
            // if coin is not found in the stock, signal error
            // else decrement if we hanve enough quantity. If quantity is not right, throw  error.
            var coinStockToRemove = coinStocks.SingleOrDefault(coinStock => coinStock.Coin.Value == coinToRemove.Value);
            if (coinStockToRemove == null)
            {
                throw new InvalidOperationException("Cannot remove coins. Coins not in stock.");
            }

            if (coinStockToRemove.Quantity < quantity)
            {
                throw new InvalidOperationException("Cannot remove coins. Not enough coins in stock.");
            }

            coinStockToRemove.DecrementStock(quantity);
        }

        /// <summary>
        /// Removes an item from the item stock
        /// </summary>
        public static void RemoveItem(this IList<ItemStock> itemStocks, Item itemToRemove)
        {
            // We have to remove one item from the stock. If item stock is not found or there isn't enough quantity, raise error.
            var itemStockToRemove = itemStocks.SingleOrDefault(itemStock => itemStock.Item.Name == itemToRemove.Name);
            if (itemStockToRemove == null)
            {
                throw new InvalidOperationException("Cannot remove item. Item not in stock.");
            }

            if (itemStockToRemove.Quantity < 1)
            {
                throw new InvalidOperationException("Cannot remove item. Not enough item in stock.");
            }

            itemStockToRemove.DecrementStock();
        }

        /// <summary>
        /// Creates a clone of the coin stock
        /// </summary>
        public static IList<CoinStock> Clone(this IEnumerable<CoinStock> coinStocks)
        {
            return coinStocks.Select(coinStock => coinStock.MakeCopy()).ToList();
        }
    }
}