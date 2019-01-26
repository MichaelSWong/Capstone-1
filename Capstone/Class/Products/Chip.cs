namespace Capstone.Class.Products
{
    public class Chip : Product
    {
        public Chip(string name, decimal price) : base(name, price) { }

        public override string Consume()
        {
            return "Crunch Crunch, Yum!";
        }
    }
}
