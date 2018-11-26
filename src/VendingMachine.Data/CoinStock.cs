namespace VendingMachine.Data
{
    using System.ComponentModel;

    public class CoinStock : INotifyPropertyChanged
    {
        private int quantity;

        public CoinStock(Coin coin, int quantity)
        {
            this.Coin = coin;
            this.Quantity = quantity;
        }

        public Coin Coin { get; private set; }

        public int Quantity
        {
            get
            {
                return this.quantity;
            }

            private set
            {
                this.quantity = value;
                this.OnPropertyChanged("Quantity");
            }
        }

        public void IncrementStock(int increment = 1)
        {
            this.Quantity += increment;
        }

        public void DecrementStock(int decrement = 1)
        {
            this.Quantity -= decrement;
        }

        public CoinStock MakeCopy()
        {
            return new CoinStock(this.Coin, this.Quantity);
        }

        public override string ToString()
        {
            return string.Format("Coin: {0}, Qty: {1}", this.Coin, this.Quantity);
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