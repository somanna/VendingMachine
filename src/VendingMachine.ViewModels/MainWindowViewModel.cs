namespace VendingMachine.ViewModels
{
    using System.Collections.ObjectModel;
    using System.ComponentModel;

    using VendingMachine.Common;
    using VendingMachine.Data;
    using VendingMachine.ViewModels.Commands;

    /// <summary>
    /// The main window view model that is bound to the main window view
    /// </summary>
    public class MainWindowViewModel : IMainWindowViewModel, INotifyPropertyChanged
    {
        private ItemStock selectedItemStock;

        private CoinStock selectedCoinStock;

        private string statusMessage;

        private Item givenItem;

        private State currentState;

        private Item selectedItem;

        public MainWindowViewModel(IStockProvider stockProvider, State statingState)
        {
            this.StockProvider = stockProvider;
            this.AvailableCoinStock = new ObservableCollection<CoinStock>(this.StockProvider.GetCoinStock());
            this.AvailableItemStock = new ObservableCollection<ItemStock>(this.StockProvider.GetItemStock());
            this.ReturnedCoins = new ObservableCollection<CoinStock>();
            this.InsertedCoinStock = new ObservableCollection<CoinStock>();
            this.CurrentState = statingState;
        }

        public IStockProvider StockProvider { get; set; }

        public IMoneyManager MoneyManager { get; set; }

        public IMessageBox MessageBox { get; set; }

        public CancelCommand CancelCommand { get; set; }

        public ConfirmPaymentCommand ConfirmPaymentCommand { get; set; }

        public InsertCoinCommand InsertCoinCommand { get; set; }

        public SelectItemCommand SelectItemCommand { get; set; }

        public State CurrentState
        {
            get
            {
                return this.currentState;
            }

            set
            {
                this.currentState = value;
                this.OnPropertyChanged("CurrentState");
            }
        }

        public Item SelectedItem    
        {
            get
            {
                return this.selectedItem;
            }

            set
            {
                this.selectedItem = value;
                this.OnPropertyChanged("SelectedItem");
            }
        }

        public CoinStock SelectedCoinStock
        {
            get
            {
                return this.selectedCoinStock;
            }

            set
            {
                this.selectedCoinStock = value;
                this.OnPropertyChanged("SelectedCoinStock");
            }
        }

        public ItemStock SelectedItemStock
        {
            get
            {
                return this.selectedItemStock;
            }

            set
            {
                this.selectedItemStock = value;
                this.OnPropertyChanged("SelectedItemStock");
            }
        }

        public ObservableCollection<ItemStock> AvailableItemStock { get; private set; }

        public ObservableCollection<CoinStock> AvailableCoinStock { get; private set; }

        public ObservableCollection<CoinStock> InsertedCoinStock { get; private set; }

        public ObservableCollection<CoinStock> ReturnedCoins { get; private set; }

        public Item GivenItem
        {
            get
            {
                return this.givenItem;
            }

            set
            {
                this.givenItem = value;
                this.OnPropertyChanged("GivenItem");
            }
        }

        public void DisplayMessage(string message)
        {
            this.MessageBox.DisplayMessage(message);
        }

        public string StatusMessage
        {
            get
            {
                return this.statusMessage;
            }

            set
            {
                this.statusMessage = value;
                this.OnPropertyChanged("StatusMessage");
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            var handler = this.PropertyChanged;
            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}