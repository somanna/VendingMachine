namespace VendingMachine.Data
{
    using System.ComponentModel;

    public class ItemStock : INotifyPropertyChanged
    {
        private int quantity;

        public ItemStock(Item item, int quantity)
        {
            this.Item = item;
            this.Quantity = quantity;
        }

        public Item Item { get; private set; }

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

        public void DecrementStock()
        {
            this.Quantity--;
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