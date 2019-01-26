using Microsoft.VisualStudio.TestTools.UnitTesting;
using Capstone.Class;
using Capstone.Class.Products;

namespace CapstoneTests
{
    [TestClass]
    public class TransactionTests
    {
        private Transaction testObj = null;

        [TestMethod]
        public void FeedMoneyTest()
        {
            // ACT
            testObj = new Transaction();

            // ARRANGE
            testObj.FeedMoney(7);

            // ASSERT
            Assert.AreEqual(0, testObj.Balance, "Invalid Entry, balance should have never increased");

            testObj.FeedMoney(5);
            Assert.AreEqual(5,testObj.Balance, "Just added $5, damn vending machine ate my money.");

            
            testObj.FeedMoney(10);            
            Assert.AreEqual(15, testObj.Balance, "Just added $10, where is my money!!!");

            
            
        }

        [TestMethod]
        public void ReturnChangeTest()
        {
            // ACT

            testObj = new Transaction();
            Candy candyBar = new Candy("ChocolateBar", .05M);
            Slot testSlot = new Slot('A', 1, candyBar);
            testObj.FeedMoney(1);
            testObj.Purchase(testSlot);

            
            // Arrange
            


            // ASSERT
            Assert.AreEqual("$0.95 in 3 quarters and 2 dimes", testObj.ReturnChange(), "You need 95c in 3 quarters and 2 dimes");

           

            

            
        }
        [TestMethod]
        public void PurchaseTest()
        {
            testObj = new Transaction();
            Candy candyBar = new Candy("ChocolateBar", .05M);
            Slot testSlot = new Slot('A', 1, candyBar);
            testObj.FeedMoney(1);

            Assert.AreEqual(true, testObj.Purchase(testSlot), "Should return true");
        }
    }
}
