namespace VendingMachine.Common
{
    using System.Collections.Generic;

    using VendingMachine.Data;

    /// <summary>
    /// Interface that has to be implemented by classes that want to provide the change
    /// </summary>
    public interface IChangeCalculator
    {
        IList<CoinStock> GetChange(IList<CoinStock> availableCoins, int changeAmount);
    }
}