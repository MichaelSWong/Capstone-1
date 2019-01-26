using System;
using System.IO;
using System.Collections.Generic;
using Capstone.Class.Products;

namespace Capstone.Class
{
    public static class SalesReport
    {
        private static List<SalesReportProduct> products = new List<SalesReportProduct>();

        /// <summary>
        /// Reads in the sales report (if one currently exists) and saves each object into a list of SalesReportProducts for manipulation. If file doesn't exist then it will use the provided currStockedProducts;
        /// </summary>
        /// <param name="currStockedProducts">The products currently stocked.</param>
        public static void ReadIn(List<Product> currStockedProducts)
        {
            string currentDirectory = Environment.CurrentDirectory;
            string fileName = "salesReport.txt";
            string fullPath = Path.Combine(currentDirectory, fileName);

            if (File.Exists(fullPath))
            {
                try
                {
                    using (StreamReader sr = new StreamReader(fullPath))
                    {
                        while (!sr.EndOfStream)
                        {
                            string line = sr.ReadLine();
                            if(line == "")
                            {
                                break;
                            }
                            string[] splitStr = line.Split("|", StringSplitOptions.RemoveEmptyEntries);

                            if(GeneralAssistance.CheckIfIntNumber(splitStr[1]))
                            {
                                foreach(Product item in currStockedProducts)
                                {
                                    if(item.Name == splitStr[0])
                                    {
                                        products.Add(new SalesReportProduct(splitStr[0], int.Parse(splitStr[1]), item.Price));
                                    }
                                }
                            }
                        }
                    }
                }
                catch (IOException e)
                {
                    AuditLog.Log("ERROR: Ran into a IOException.\t" + e.Message);
                }
            }
            else
            {
                foreach(Product item in currStockedProducts)
                {
                    products.Add(new SalesReportProduct(item.Name, 0, item.Price));
                }
            }
        }
        /// <summary>
        /// Save the current sale history to file.
        /// </summary>
        public static void SaveToFile()
        {
            string currentDirectory = Environment.CurrentDirectory;
            string fileName = "salesReport.txt";
            string fullPath = Path.Combine(currentDirectory, fileName);

            using (StreamWriter sw = new StreamWriter(fullPath))
            {
                decimal sum = 0;
                if (products.Count > 0)
                {
                    foreach (SalesReportProduct product in products)
                    {
                        sw.WriteLine($"{product.Name}|{product.Count}");
                        sum += product.Count * product.Price;
                    }
                }
                sw.WriteLine($"\r\n**TOTAL SALES** {sum.ToString("C")}");
            }
        }

        /// <summary>
        /// Update the count of the given product via name. Adds one.
        /// </summary>
        /// <param name="name">The name of the product to update.</param>
        public static void UpdateProductCount(string name)
        {
            foreach(SalesReportProduct item in products)
            {
                if(item.Name == name)
                {
                    item.AddOne();
                    SaveToFile();
                    return;
                }
            }

            AuditLog.Log("ERROR: No item found!");
        }
    }
}
