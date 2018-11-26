namespace VendingMachine.Tests
{
    using System.Collections.Generic;
    using System.Linq;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using VendingMachine.Data;
    using VendingMachine.Utilities;

    /// <summary>
    /// Tests the ChangeCalculator class.
    /// </summary>
    [TestClass]
    public class ChangeCalculatorTests
    {
        private static ChangeCalculator changeCalculator = new ChangeCalculator();

        [TestMethod]
        public void GetChangeReturnsChangeWhenChangeAvailable()
        {
            // Arrange
            var availableCoinStock = GetAvailableCoinStockForTest();
            var changeAmount = 30;

            // Act
            var change = changeCalculator.GetChange(availableCoinStock, changeAmount);

            // Assert
            Assert.AreEqual(changeAmount, change.GetTotalAmount());
        }

        [TestMethod]
        public void GetChangeReturnsRightDenominationWhenChangeAvailable()
        {
            // Arrange
            var availableCoinStock = GetAvailableCoinStockForTest();
            var changeAmount = 30;

            // Act
            var change = changeCalculator.GetChange(availableCoinStock, changeAmount);

            // Assert
            // Should get 1 20p and 2 5p
            var coinStock = change.Single(cs => cs.Coin.Value == 20);
            Assert.AreEqual(1, coinStock.Quantity, "20p quantity is not as expected");
            coinStock = change.Single(cs => cs.Coin.Value == 5);
            Assert.AreEqual(2, coinStock.Quantity, "5p quantity not as expected");
        }

        [TestMethod]
        public void GetChangeReturnsHigherDenominationWhenChangeAvailable()
        {
            // Arrange
            var availableCoinStock = GetAvailableCoinStockForTest();
            var changeAmount = 55;

            // Act
            var change = changeCalculator.GetChange(availableCoinStock, changeAmount);

            // Assert
            // Should get 1 50p and 1 5p
            var coinStock = change.Single(cs => cs.Coin.Value == 50);
            Assert.AreEqual(1, coinStock.Quantity, "50p quantity is not as expected");
            coinStock = change.Single(cs => cs.Coin.Value == 5);
            Assert.AreEqual(1, coinStock.Quantity, "5p quantity not as expected");
        }

        [TestMethod]
        public void GetChangeReturnsHigherDenominationWhenMultipleOptionsAvailable()
        {
            // Arrange
            var availableCoinStock = GetAvailableCoinStockForTest();
            var changeAmount = 40;

            // Act
            var change = changeCalculator.GetChange(availableCoinStock, changeAmount);

            // Assert
            // Should get 2 20p (instead of 1 20p and 4 5p)
            var coinStock = change.Single(cs => cs.Coin.Value == 20);
            Assert.AreEqual(2, coinStock.Quantity, "20p quantity is not as expected");
        }

        [TestMethod]
        public void GetChangeSkipsDenominationWithZeroQuantity()
        {
            // Arrange
            var availableCoinStock = GetAvailableCoinStockForTest();
            availableCoinStock.AddCoin(new Coin("10p", 10), 0);
            var changeAmount = 30;

            // Act
            var change = changeCalculator.GetChange(availableCoinStock, changeAmount);

            // Assert
            // Should get 2 20p (instead of 1 20p and 4 5p)
            Assert.AreEqual(changeAmount, change.GetTotalAmount());
            var coinStock = change.SingleOrDefault(cs => cs.Coin.Value == 10);
            Assert.IsNull(coinStock, "Not expecting 10p in change");
        }

        [TestMethod]
        public void GetChangeReturnsNoChangeWhenAvailableAmountIsSufficientButNotRightDenomination()
        {
            // Arrange
            var availableCoinStock = GetAvailableCoinStockForTest();
            var changeAmount = 42;

            // Act
            var change = changeCalculator.GetChange(availableCoinStock, changeAmount);

            // Assert
            Assert.AreEqual(0, change.GetTotalAmount(), "Not expecting change");
        }

        [TestMethod]
        public void GetChangeReturnsNoChangeWhenAvailableAmountIsInsufficient()
        {
            // Arrange
            var availableCoinStock = GetAvailableCoinStockForTest();
            var changeAmount = availableCoinStock.GetTotalAmount() + 5;

            // Act
            var change = changeCalculator.GetChange(availableCoinStock, changeAmount);

            // Assert
            Assert.AreEqual(0, change.GetTotalAmount(), "Not expecting change");
        }

        [TestMethod]
        public void GetChangeReturnsNoChangeWhenChangeAmountIsZero()
        {
            // Arrange
            var availableCoinStock = GetAvailableCoinStockForTest();
            var changeAmount = 0;

            // Act
            var change = changeCalculator.GetChange(availableCoinStock, changeAmount);

            // Assert
            Assert.AreEqual(0, change.GetTotalAmount(), "Not expecting change");
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
