namespace Capstone.Class.Products
{
    public class Candy : Product
    {
        public Candy(string name, decimal price) : base(name, price) { }

        public override string Consume()
        {
            return "Munch Munch, Yum!";
        }
    }
}
