using System;
using System.Collections.Generic;
using Capstone.Class.Products;

namespace Capstone.Class
{
    public class VendingMachine
    {
        private Inventory inventory = null;
        private Transaction transObj = null;

        public VendingMachine()
        {
            inventory = new Inventory();
            transObj = new Transaction();
        }

        /// <summary>
        /// Start the vending machine by reading in the sales report file and showing the main menu.
        /// </summary>
        public void Start()
        {
            Console.WriteLine("Welecome to Vending Machine!");
            SalesReport.ReadIn(inventory.CurrentProducts);
            SalesReport.SaveToFile();
            MainMenu();
        }

        /// <summary>
        /// The main menu of the vending machine.
        /// </summary>
        public void MainMenu()
        {
            bool isDone = false;
            while (!isDone)
            {
                // Menu
                Console.WriteLine("\n\t(1) Display Vending Machine Items");
                Console.WriteLine("\t(2) Purchase\n");

                // Get input
                ConsoleKeyInfo key = Console.ReadKey();
                while (key.KeyChar != '1' && key.KeyChar != '2' && key.Key != ConsoleKey.Escape)
                {
                    Console.Write("\nMust enter either '1' or '2': ");
                    key = Console.ReadKey();
                }

                // Process input
                if (key.KeyChar == '1')
                {
                    DisplayProducts();
                }
                else if (key.KeyChar == '2')
                {
                    PurchaseMenu();
                }
                else
                {
                    isDone = true;
                }
            }
        }

        /// <summary>
        /// The menu where the user can select to feed money or purchase a product.
        /// </summary>
        public void PurchaseMenu()
        {
            DisplayProducts();

            bool isDone = false;
            List<Product> productsBought = new List<Product>();
            while(!isDone)
            {
                // Menu
                Console.WriteLine("\n\n\t(1) Feed Money");
                Console.WriteLine("\t(2) Select Product");
                Console.WriteLine("\t(3) Finish Transaction");
                Console.WriteLine($"\tCurrent money: {transObj.Balance.ToString("C")}\n");

                // Get input
                ConsoleKeyInfo key = Console.ReadKey();
                while (key.KeyChar != '1' && key.KeyChar != '2' && key.KeyChar != '3')
                {
                    Console.Write("\nMust enter either '1', '2', or '3': ");
                    key = Console.ReadKey();
                }

                // Process input
                if (key.KeyChar == '1')
                {
                    FeedMoneyMenu();
                }
                else if (key.KeyChar == '2')
                {
                    Product boughtProduct = SelectProductMenu();
                    if(boughtProduct != null)
                    {
                        productsBought.Add(boughtProduct);
                    }
                }
                else
                {
                    isDone = true;
                }
            }

            Console.WriteLine($"\n\nChange returned: {transObj.ReturnChange()}");
            if (productsBought.Count > 0)
            {
                foreach(Product product in productsBought)
                {
                    Console.WriteLine(product.Consume());
                }
            }
        }

        /// <summary>
        /// The feed money menu of the vending machine. Will continue to allow user to feed money until user says theu're done.
        /// </summary>
        public void FeedMoneyMenu()
        {
            bool isDone = false;
            while (!isDone)
            {
                // Menu
                Console.WriteLine("\n\n\t(1) $1");
                Console.WriteLine("\t(2) $2");
                Console.WriteLine("\t(3) $5");
                Console.WriteLine("\t(4) $10");
                Console.WriteLine("\t(5) Done\n");

                // Get input
                ConsoleKeyInfo key = Console.ReadKey();
                while (key.KeyChar != '1' && key.KeyChar != '2' && key.KeyChar != '3' && key.KeyChar != '4' && key.KeyChar != '5')
                {
                    Console.Write("\nMust enter either '1', '2', '3', '4', or '5': ");
                    key = Console.ReadKey();
                }

                // Process input
                if (key.KeyChar != '5')
                {
                    if (key.KeyChar == '1' || key.KeyChar == '2')
                    {
                        transObj.FeedMoney(decimal.Parse(key.KeyChar.ToString()));
                    }
                    else if (key.KeyChar == '3')
                    {
                        transObj.FeedMoney(5);
                    }
                    else if (key.KeyChar == '4')
                    {
                        transObj.FeedMoney(10);
                    }

                    Console.WriteLine($"\tCurrent money: {transObj.Balance.ToString("C")}\n");
                }
                else
                {
                    isDone = true;
                }
            }
        }

        /// <summary>
        /// The select product menu. This is a one pass menu.
        /// </summary>
        /// <returns>Returns the product purchased. Null if not successful.</returns>
        public Product SelectProductMenu()
        {
            Console.Write("\n\nEnter your selection (ex. \"A1\"): ");

            string loc = Console.ReadLine().ToUpper();
            while(loc == "")
            {
                loc = Console.ReadLine();
            }

            if (loc.Length >= 2)
            {
                char row = loc[0];

                string columnStr = loc.Substring(1);
                int column = -1;
                if (GeneralAssistance.CheckIfIntNumber(columnStr))
                {
                    column = int.Parse(columnStr);
                    if(column <= 0)
                    {
                        Console.WriteLine("Invalid slot.");
                        return null;
                    }
                    else
                    {
                        return inventory.TakeOne(row, column, transObj);
                    }
                }
                else
                {
                    Console.WriteLine("Invalid slot.");
                    return null;
                }
            }

            Console.WriteLine("Invalid slot.");
            return null;
        }

        /// <summary>
        /// Display the options of stocked products.
        /// </summary>
        public void DisplayProducts()
        {
            Console.WriteLine($"\n\nThere are {inventory.Slots.Count} items to choose from.\n");
            Console.WriteLine(inventory.ToString());
        }
    }
}
