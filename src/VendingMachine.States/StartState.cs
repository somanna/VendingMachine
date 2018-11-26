namespace VendingMachine.States
{
    using VendingMachine.Common;

    /// <summary>
    /// This is the start state of the vending machine.
    /// </summary>
    public class StartState : State
    {
        public override string StateName
        {
            get
            {
                return "START";
            }
        }

        public AcceptingCoinsState AcceptingCoinsState { get; set; }

         public override State SelectItem(IMainWindowViewModel viewModel)
         {
             if (viewModel.SelectedItemStock == null)
             {
                 viewModel.DisplayMessage("Please select item from stock");
                 return this;
             }

             // We need to reset the coins returned/inserted coins and item given data
             viewModel.ReturnedCoins.Clear();
             viewModel.InsertedCoinStock.Clear();
             viewModel.GivenItem = null;
             viewModel.SelectedCoinStock = null;

             viewModel.SelectedItem = viewModel.SelectedItemStock.Item;
             viewModel.StatusMessage = "Waiting for coins";
             return this.AcceptingCoinsState;
         }
    }
}