using System;
using System.IO;
using System.Collections.Generic;
using Capstone.Class.Products;

namespace Capstone.Class
{
    public class Inventory
    {
        public List<Slot> Slots { get; private set; }
        public List<Product> CurrentProducts
        {
            get
            {
                List<Product> products = new List<Product>();
                List<string> names = new List<string>();

                foreach(Slot slot in Slots)
                {
                    if(!names.Contains(slot.Item.Name))
                    {
                        products.Add(slot.Item);
                        names.Add(slot.Item.Name);
                    }
                }

                return products;
            }
        }

        public Inventory()
        {
            ReStock();
        }

        /// <summary>
        /// Re-stocks the machine via the re-stock file.
        /// </summary>
        public void ReStock()
        {
            string currDirectory = Environment.CurrentDirectory;
            string stockInputFile = "vendingmachine.csv";
            string fullPath = Path.Combine(currDirectory, stockInputFile);

            if (File.Exists(fullPath))
            {
                try
                {
                    using (StreamReader sr = new StreamReader(fullPath))
                    {
                        Slots = new List<Slot>();

                        while (!sr.EndOfStream)
                        {
                            // Get the line
                            string line = sr.ReadLine();
                            string[] splitStr = line.Split("|", StringSplitOptions.RemoveEmptyEntries);

                            if (splitStr.Length == 4)
                            {
                                // Get the row char
                                char row = splitStr[0].ToUpper().Substring(0, 1)[0];

                                // Get the valid column int
                                int column = -1;
                                if (GeneralAssistance.CheckIfIntNumber(splitStr[0].Substring(1)))
                                {
                                    column = int.Parse(splitStr[0].Substring(1));
                                }
                                else
                                {
                                    AuditLog.Log("ERROR: Re-stock file column was not a number!\t" + line);
                                    continue;
                                }

                                bool notValid = false;
                                foreach(Slot slot in Slots)
                                {
                                    if(slot.Row == row && slot.Column == column)
                                    {
                                        AuditLog.Log("ERROR: Re-stock file contains a product with the same location as another!\t" + line);
                                        notValid = true;
                                        break;
                                    }
                                }
                                if(notValid)
                                {
                                    continue;
                                }

                                // Get product name
                                string name = splitStr[1];

                                // Get the valid price decimal
                                decimal price = -1;
                                if (GeneralAssistance.CheckIfFloatingPointNumber(splitStr[2]))
                                {
                                    price = Decimal.Parse(splitStr[2]);
                                }
                                else
                                {
                                    AuditLog.Log("ERROR: Re-stock file price was not a valid floating point number!\t" + line);
                                    continue;
                                }

                                // Determine Product type
                                switch (splitStr[3].ToLower())
                                {
                                    case "chip":
                                        Slots.Add(new Slot(row, column, new Chip(name, price)));
                                        Slots[Slots.Count - 1].Add(5);
                                        break;
                                    case "candy":
                                        Slots.Add(new Slot(row, column, new Candy(name, price)));
                                        Slots[Slots.Count - 1].Add(5);
                                        break;
                                    case "drink":
                                        Slots.Add(new Slot(row, column, new Drink(name, price)));
                                        Slots[Slots.Count - 1].Add(5);
                                        break;
                                    case "gum":
                                        Slots.Add(new Slot(row, column, new Gum(name, price)));
                                        Slots[Slots.Count - 1].Add(5);
                                        break;
                                    default:
                                        AuditLog.Log("ERROR: Re-stock file Product type was not a valid type!\t" + line);
                                        break;
                                }
                            }
                            else
                            {
                                AuditLog.Log("ERROR: Re-stock file line does not have the right amount of peices!\t" + line);
                            }
                        }
                    }
                }
                catch(IOException e)
                {
                    AuditLog.Log("ERROR: Ran into a IOException.\t" + e.Message);
                }
            }
            else
            {
                AuditLog.Log("ERROR: Re-stock file not found!");
            }
        }

        /// <summary>
        /// Attempts to take one of the item in the given slot.
        /// </summary>
        /// <param name="row">The row the item is in.</param>
        /// <param name="column">The column the item is in.</param>
        /// <param name="transObj">The Transaction object that the program is using to track the user's balance and purchases.</param>
        /// <returns>The Product taken if successful. Null if not successful.</returns>
        public Product TakeOne(char row, int column, Transaction transObj)
        {
            if(Slots.Count > 0)
            {
                foreach(Slot slot in Slots)
                {
                    if(slot.Row == row && slot.Column == column)
                    {
                        return slot.TakeOne(transObj);
                    }
                }
            }

            Console.WriteLine("Slot does not exist.");
            return null;
        }

        /// <summary>
        /// Returns all the slots and thier products as a string.
        /// </summary>
        /// <returns>Format: "{slot.ToString()}\n{slot.ToString()}\n..."</returns>
        public override string ToString()
        {
            string returnStr = "";

            foreach(Slot slot in Slots)
            {
                returnStr += slot.ToString() + "\n";
            }

            return returnStr;
        }
    }
}
