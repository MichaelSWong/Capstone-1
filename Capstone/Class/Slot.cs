using System;
using Capstone.Class;
using Capstone.Class.Products;

namespace Capstone.Class
{
    public class Slot
    {
        public char Row { get; }
        public int Column { get; }
        public Product Item { get; }
        public int Count { get; private set; }

        /// <summary>
        /// Set a new slot for the vending machine.
        /// </summary>
        /// <param name="r">The row the slot is located in.</param>
        /// <param name="c">The column the slot is located in.</param>
        /// <param name="i">The Product that this slot will hold.</param>
        public Slot(char r, int c, Product i)
        {
            Row = r;
            Column = c;
            Item = i;
            Count = 0;
        }

        /// <summary>
        /// Take one Product from this slot if some remain and they are able to make the purchase.
        /// </summary>
        /// <param name="transObj">The Transaction object that the program is using to track the user's balance and purchases.</param>
        /// <returns>The product taken. Null if not successful.</returns>
        public Product TakeOne(Transaction transObj)
        {
            if (Count > 0)
            {
                if (transObj.Purchase(this))
                {
                    Count--;
                    return Item;
                }
                else
                {
                    Console.WriteLine("You don't have enough to purchase this item.");
                }
            }
            else
            {
                Console.WriteLine("This item is sold out.");
            }

            return null;
        }

        /// <summary>
        /// Add more of this product to this slot. Slot can hold up to five, return the excess amount.
        /// </summary>
        /// <param name="amount">The amount to add.</param>
        /// <returns>The amount of excess that didn't fit.</returns>
        public int Add(int amount)
        {
            if (amount <= 0)
            {
                return 0;
            }

            Count += amount;
            int excess = Count - 5;
            if(excess > 0)
            {
                Count = 5;
            }

            return (excess >= 0) ? excess : 0;
        }

        /// <summary>
        /// Returns this Slot as a string.
        /// </summary>
        /// <returns>Format: "{Row}{Column} {Item.ToString()} {Count}"</returns>
        public override string ToString()
        {
            string count = (Count > 0) ? Count.ToString() : "SOLD OUT";
            return $"{Row}{Column} {Item.ToString()} Amount Remaining: {count}";
        }
    }
}
