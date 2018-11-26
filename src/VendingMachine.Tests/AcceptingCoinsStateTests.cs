namespace VendingMachine.Tests
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using Moq;

    using VendingMachine.Common;
    using VendingMachine.Data;
    using VendingMachine.States;

    [TestClass]
    public class AcceptingCoinsStateTests
    {
        private Mock<IMainWindowViewModel> mainWindowViewModelMock;

        private Mock<StartState> startStateMock;

        private Mock<PaymentCompleteState> paymentCompleteStateMock;

        private Mock<IMoneyManager> moneyManagerMock;

        private AcceptingCoinsState acceptingCoinsState;

        [TestInitialize]
        public void TestInitialise()
        {
            this.mainWindowViewModelMock = new Mock<IMainWindowViewModel>();
            this.startStateMock = new Mock<StartState>();
            this.paymentCompleteStateMock = new Mock<PaymentCompleteState>();
            this.moneyManagerMock = new Mock<IMoneyManager>();
            this.mainWindowViewModelMock.SetupGet(x => x.SelectedCoinStock).Returns(new CoinStock(new Coin("50p", 50), 1));
            this.mainWindowViewModelMock.SetupGet(x => x.InsertedCoinStock).Returns(new ObservableCollection<CoinStock>());

            this.acceptingCoinsState = new AcceptingCoinsState
                                           {
                                               StartState = this.startStateMock.Object,
                                               PaymentCompleteState =
                                                   this.paymentCompleteStateMock.Object,
                                               MoneyManager = this.moneyManagerMock.Object
                                           };
        }
            
        [TestMethod]
        public void StateNameReturnsCorrectStateName()
        {
            // Arrange
            var state = new AcceptingCoinsState();

            // Act
            var stateName = state.StateName;

            // Assert
            Assert.AreEqual("ACCEPTING COINS", stateName);
        }

        [TestMethod]
        [ExpectedException(typeof(NotSupportedException))]
        public void SelectItemThrowsException()
        {
            // Arrange

            // Act
            this.acceptingCoinsState.SelectItem(this.mainWindowViewModelMock.Object);

            // Assert
        }

        [TestMethod]
        public void InsertCoinWhenCoinNotSelectedCallsDisplayMessageOnViewModelAndReturnsSameState()
        {
            // Arrange
            this.mainWindowViewModelMock.SetupGet(x => x.SelectedCoinStock).Returns((CoinStock)null);
            
            // Act
            var newState = this.acceptingCoinsState.InsertCoin(this.mainWindowViewModelMock.Object);

            // Assert
            this.mainWindowViewModelMock.Verify(x => x.DisplayMessage(It.IsAny<string>()));
            Assert.AreEqual(this.acceptingCoinsState, newState);
        }

        [TestMethod]
        public void InsertCoinPaymentIsNotSufficientReturnsSameState()
        {
            // Arrange
            this.moneyManagerMock.Setup(
                x => x.IsPaymentSufficient(It.IsAny<Item>(), It.IsAny<IEnumerable<CoinStock>>())).Returns(false);
            
            // Act
            var newState = this.acceptingCoinsState.InsertCoin(this.mainWindowViewModelMock.Object);

            // Assert
            this.moneyManagerMock.Verify(x => x.IsPaymentSufficient(It.IsAny<Item>(), It.IsAny<IEnumerable<CoinStock>>()));
            Assert.AreEqual(this.acceptingCoinsState, newState);
        }

        [TestMethod]
        public void InsertCoinPaymentIsSufficientReturnsPaymentCompleteSate()
        {
            // Arrange
            this.moneyManagerMock.Setup(
                x => x.IsPaymentSufficient(It.IsAny<Item>(), It.IsAny<IEnumerable<CoinStock>>())).Returns(true);

            // Act
            var newState = this.acceptingCoinsState.InsertCoin(this.mainWindowViewModelMock.Object);

            // Assert
            this.moneyManagerMock.Verify(x => x.IsPaymentSufficient(It.IsAny<Item>(), It.IsAny<IEnumerable<CoinStock>>()));
            Assert.AreEqual(this.paymentCompleteStateMock.Object, newState);
        }

        [TestMethod]
        public void CancelReturnsStartState()
        {
            // Arrange
            
            // Act
            var newState = this.acceptingCoinsState.Cancel(this.mainWindowViewModelMock.Object);

            // Assert
            Assert.AreEqual(this.startStateMock.Object, newState);
        }
    }
}