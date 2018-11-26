namespace VendingMachine.Tests
{
    using System;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using Moq;

    using VendingMachine.Common;
    using VendingMachine.ViewModels.Commands;

    [TestClass]
    public class CancelCommandTests
    {
        private Mock<IMainWindowViewModel> mainWindowViewModelMock;

        private Mock<State> CurrentStateMock;

        private CancelCommand cancelCommand;

        [TestInitialize]
        public void TestInitialise()
        {
            this.mainWindowViewModelMock = new Mock<IMainWindowViewModel>();
            this.CurrentStateMock = new Mock<State>();
            this.mainWindowViewModelMock.SetupGet(x => x.CurrentState).Returns(this.CurrentStateMock.Object);
            
            this.cancelCommand = new CancelCommand();
        }

        [TestMethod]
        public void ExecuteWhenCurrentStateExecuteThrowsExceptionCallsDisplayMessageOnViewModel()
        {
            // Arrange
            this.CurrentStateMock.Setup(x => x.Cancel(this.mainWindowViewModelMock.Object)).Throws<NotSupportedException>();

            // Act
            this.cancelCommand.Execute(this.mainWindowViewModelMock.Object);

            // Assert
            this.mainWindowViewModelMock.Verify(x => x.DisplayMessage(It.IsAny<string>()));
        }

        [TestMethod]
        public void ExecuteSetViewModelCurrentStateWithNewState()
        {
            // Arrange
            this.CurrentStateMock.Setup(x => x.Cancel(this.mainWindowViewModelMock.Object)).Returns(this.CurrentStateMock.Object);

            // Act
            this.cancelCommand.Execute(this.mainWindowViewModelMock.Object);

            // Assert
            this.mainWindowViewModelMock.VerifySet(x => x.CurrentState = this.CurrentStateMock.Object);
        }
    }
}