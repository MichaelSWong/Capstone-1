namespace Capstone.Class.Products
{
    /// <summary>
    /// A product of the vending machine that all real products inherit from.
    /// </summary>
    public abstract class Product
    {
        public string Name { get; }
        public decimal Price { get; }

        /// <summary>
        /// A product of the vending machine.
        /// </summary>
        /// <param name="name">The name of the product.</param>
        /// <param name="price">The price of the product.</param>
        public Product(string name, decimal price)
        {
            Name = name;
            Price = price;
        }

        public abstract string Consume();

        /// <summary>
        /// Return a string of this product's values.
        /// </summary>
        /// <returns>Format: "{Name} {Price.ToString("C")}"</returns>
        public override string ToString()
        {
            return $"{Name} {Price.ToString("C")}";
        }
    }
}
