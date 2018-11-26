namespace VendingMachine.Data
{
    public class Item
    {
        public Item(string name, int price)
        {
            this.Name = name;
            this.Price = price;
        }

        public string Name { get; private set; }

        public int Price { get; private set; }
    }
}