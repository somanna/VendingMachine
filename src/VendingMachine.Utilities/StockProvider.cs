namespace VendingMachine.Utilities
{
    using System.Collections.Generic;

    using VendingMachine.Common;
    using VendingMachine.Data;

    /// <summary>
    /// Provides stock. It is currently harcoded but can be read from an excel/csv/xml file
    /// </summary>
    public class StockProvider : IStockProvider
    {
        public IEnumerable<ItemStock> GetItemStock()
        {
            return new[]
                       {
                           new ItemStock(new Item("Coke", 50), 1), 
                           new ItemStock(new Item("Fanta", 45), 6), 
                           new ItemStock(new Item("Soda", 30), 4), 
                       };
        }

        public IEnumerable<CoinStock> GetCoinStock()
        {
            return new[]
                       {
                           new CoinStock(new Coin("£2", 200), 0), 
                           new CoinStock(new Coin("£1", 100), 0),
                           new CoinStock(new Coin("50p", 50), 2), 
                           new CoinStock(new Coin("20p", 20), 2),
                           new CoinStock(new Coin("10p", 10), 3), 
                           new CoinStock(new Coin("5p", 5), 5),
                           new CoinStock(new Coin("2p", 2), 0), 
                           new CoinStock(new Coin("1p", 1), 0),
                       };
        }
    }
}