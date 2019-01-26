using System;
using Capstone.Class;
using Capstone.Class.Products;

namespace Capstone
{
    class Program
    {
        static void Main(string[] args)
        {
            VendingMachine machine = new VendingMachine();
            machine.Start();
        }
    }
}
