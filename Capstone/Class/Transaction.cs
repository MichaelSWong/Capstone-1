using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Capstone.Class;
using Capstone.Class.Products;

namespace Capstone.Class
{
    public class Transaction
    {
        public decimal Price { get; }
        

        public decimal Balance { get; private set; }

        public Transaction()
        {
            Balance = 0;
        }

        /// <summary>
        /// Performs the action to add to the Balance
        /// </summary>
        /// <param name="money">Adds money to the Balance</param>

        public void FeedMoney(decimal money)
        {

            string message = $"FEED MONEY\t{Balance.ToString("C")}\t{money.ToString("C")}\t";
            switch (money)
            {

                case (1):
                    Balance += 1;
                    break;
                case (2):
                    Balance += 2;
                    break;
                case (5):
                    Balance += 5;
                    break;
                case (10):
                    Balance += 10;
                    break;
                default:
                    Console.WriteLine("Invalid Entry");
                    break;
            }
            message += Balance.ToString("c");
            AuditLog.Log(message);
        }

        //public Product Purchase (Product pPrice)
        //{
        //    return Balance - pPrice;
        //}

        /*
         * remainingBalance = Balance * 100
         * int quarters = remainingBalance / 25
         * remainingBalance = (remainingBalance / 100) - (quarters * .25) * 100
         * return if remainingBalance is not > 0
         * int dimes = remainingBalance / 10
         * remainingBalance = (remainingBalance / 100) - (dimes * .1) * 100
         * return if remainingBalance is not > 0
         * int nickels = remainingBalance / 5
         * remainingBalance = (remainingBalance / 100) - (nickels * .05) * 100
         * Throw an error if remaining != 0
         */


        /// <summary>
        /// Returns the remaining balance to the user
        /// </summary>
        /// <returns>Returns string with quarters, dimes and nickels</returns>
        public string ReturnChange()
        {
            decimal remainingBalance;
            string message = $"GIVE CHANGE\t{Balance.ToString("c")}\t{Balance.ToString("c")}\t";
            remainingBalance = Balance * 100;
            int quarters = (int)(remainingBalance / 25);
            remainingBalance = ((remainingBalance / 100) - (quarters * .25M)) * 100;
            if (remainingBalance <= 0)
            {
                decimal temp = Balance;
                Balance = 0;
                message += Balance.ToString("c");
                AuditLog.Log(message);
                return temp.ToString("c") + $" in {quarters} quarters";                
            }
            int dimes = (int)(remainingBalance / 10);
            remainingBalance = ((remainingBalance / 100) - (dimes * .10M)) * 100;
            if (remainingBalance <= 0)
            {
                decimal temp = Balance;
                Balance = 0;
                message += Balance.ToString("c");
                AuditLog.Log(message);
                return temp.ToString("c") + $" in {quarters} quarters and {dimes} dimes";
            }
            int nickels = (int)(remainingBalance / 5);
            remainingBalance = ((remainingBalance / 100) - (nickels * .05M)) * 100;
            if(remainingBalance <= 0)
            {
                decimal temp = Balance;
                Balance = 0;
                message += Balance.ToString("c");
                AuditLog.Log(message);
                return temp.ToString("c") + $"in {quarters} quarters, {dimes} dimes and {nickels} nickels";
            }
            AuditLog.Log("ERROR: Money is still there");
            return "";

        }


        /// <summary>
        /// Performs the transaction of the given Product.
        /// </summary>
        /// <param name="item">The Product to be purchased.</param>
        /// <returns>True if successful.</returns>
        public bool Purchase(Slot itemSlot)
        {
            if (Balance >= itemSlot.Item.Price)
            {
                string message = $"{itemSlot.Item.Name} {itemSlot.Row}{itemSlot.Column}\t{Balance.ToString("c")}\t{itemSlot.Item.Price.ToString("c")}\t";
                Balance -= itemSlot.Item.Price;
                message += Balance.ToString("c");
                AuditLog.Log (message);
                SalesReport.UpdateProductCount(itemSlot.Item.Name);
                return true;
            }

            return false;
        }
    }
}
