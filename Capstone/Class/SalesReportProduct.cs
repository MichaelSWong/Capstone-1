namespace Capstone.Class
{
    public class SalesReportProduct
    {
        public string Name { get; }
        public int Count { get; private set; }
        public decimal Price { get; set; }

        public SalesReportProduct(string name, int count, decimal price)
        {
            Name = name;
            Count = count;
            Price = price;
        }

        /// <summary>
        /// Add one to the count od this item.
        /// </summary>
        public void AddOne()
        {
            Count++;
        }
    }
}
