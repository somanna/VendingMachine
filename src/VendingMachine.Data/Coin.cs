namespace VendingMachine.Data
{
    public class Coin
    {
        public Coin(string name, int value)
        {
            this.Name = name;
            this.Value = value;
        }

        public string Name { get; private set; }

        public int Value { get; private set; }

        public override string ToString()
        {
            return string.Format("{0}-{1}", this.Name, this.Value);
        }
    }
}