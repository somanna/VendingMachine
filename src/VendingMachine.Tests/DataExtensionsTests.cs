namespace VendingMachine.Tests
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using VendingMachine.Data;

    /// <summary>
    /// Tests the DataExtensions class.
    /// </summary>
    [TestClass]
    public class DataExtensionsTests
    {
        [TestMethod]
        public void GetTotalAmountReturnsCorrectAmount()
        {
            // Arrange
            var coinStock = GetAvailableCoinStockForTest();

            // Act
            var total = coinStock.GetTotalAmount();

            // Assert
            Assert.AreEqual(160, total);
        }

        [TestMethod]
        public void AddCoinAddsCoinWhenCoinIsNotAlreadyInStock()
        {
            // Arrange
            var coinStock = GetAvailableCoinStockForTest();
            var coinToAdd = new Coin("10p", 10);

            // Act
            coinStock.AddCoin(coinToAdd, 2);

            // Assert
            var coinStockAdded = coinStock.SingleOrDefault(cs => cs.Coin.Value == coinToAdd.Value);
            Assert.IsNotNull(coinStockAdded);
            Assert.AreEqual(2, coinStockAdded.Quantity);
        }

        [TestMethod]
        public void AddCoinIncrementsCoinStockWhenCoinIsAlreadyInStock()
        {
            // Arrange
            var coinStock = GetAvailableCoinStockForTest();
            var coinToAdd = new Coin("50p", 50);

            // Act
            coinStock.AddCoin(coinToAdd, 2);

            // Assert
            var coinStockAdded = coinStock.Single(cs => cs.Coin.Value == coinToAdd.Value);
            Assert.AreEqual(4, coinStockAdded.Quantity);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void RemoveCoinThrowsExceptionWhenCoinNotInStock()
        {
            // Arrange
            var coinStock = GetAvailableCoinStockForTest();
            var coinToRemove = new Coin("10p", 10);

            // Act
            coinStock.RemoveCoin(coinToRemove, 1);

            // Assert
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void RemoveCoinThrowsExceptionWhenQuantityOfCoinIsLess()
        {
            // Arrange
            var coinStock = GetAvailableCoinStockForTest();
            var coinToRemove = new Coin("50p", 50);

            // Act
            coinStock.RemoveCoin(coinToRemove, 3);

            // Assert
        }

        [TestMethod]
        public void RemoveCoinDecrementsCoinStock()
        {
            // Arrange
            var coinStock = GetAvailableCoinStockForTest();
            var coinToRemove = new Coin("50p", 50);

            // Act
            coinStock.RemoveCoin(coinToRemove, 1);

            // Assert
            var coinStockRemoved = coinStock.Single(cs => cs.Coin.Value == coinToRemove.Value);
            Assert.AreEqual(1, coinStockRemoved.Quantity);
        }

        private static IList<CoinStock> GetAvailableCoinStockForTest()
        {
            return
                new List<CoinStock>(
                    new[]
                        {
                            new CoinStock(new Coin("50p", 50), 2), 
                            new CoinStock(new Coin("20p", 20), 2),
                            new CoinStock(new Coin("5p", 5), 4),
                        });
        }
    }
}