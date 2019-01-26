namespace Capstone.Class.Products
{
    public class Drink : Product
    {
        public Drink(string name, decimal price) : base(name, price) { }

        public override string Consume()
        {
            return "Glug Glug, Yum!";
        }
    }
}
