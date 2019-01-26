namespace Capstone.Class.Products
{
    public class Gum : Product
    {
        public Gum(string name, decimal price) : base(name, price) { }

        public override string Consume()
        {
            return "Chew Chew, Yum!";
        }
    }
}
