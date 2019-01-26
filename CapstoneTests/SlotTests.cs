using Microsoft.VisualStudio.TestTools.UnitTesting;
using Capstone.Class;
using Capstone.Class.Products;

namespace CapstoneTests
{
    [TestClass]
    public class SlotTests
    {
        private Slot testObj = null;

        [TestMethod]
        public void TakeOneTests()
        {
            testObj = new Slot('A', 1, new Candy("ChocolateBar", 2));
            Transaction transObj = new Transaction();
            
            Product returnProd = testObj.TakeOne(transObj);
            Assert.IsNull(returnProd, "User had no money and should not have been given a Product.");

            transObj.FeedMoney(10);
            returnProd = testObj.TakeOne(transObj);
            Assert.IsNull(returnProd, "There is no Products in slot. It should return nothing.");
            Assert.AreEqual(0, testObj.Count, "Nothing was successfully taken and should therefore be 0.");

            testObj.Add(2);
            returnProd = testObj.TakeOne(transObj);
            Assert.IsNotNull(returnProd, "TakeOne should return the Product when one is successfully taken..");
            Assert.AreEqual(1, testObj.Count, "TakeOne should only take 1 from Product.");
        }

        [TestMethod]
        public void AddTests()
        {
            testObj = new Slot('A', 1, new Candy("ChocolateBar", 2));

            int excess = testObj.Add(2);
            Assert.AreEqual(2, testObj.Count, "Just added 2 so there should be 2.");
            Assert.AreEqual(0, excess, "Should not have had excess added.");

            excess = testObj.Add(10);
            Assert.AreEqual(5, testObj.Count, "Just added 10 when there should be a max of 5.");
            Assert.AreEqual(7, excess, "There should have been excess after adding 10.");
        }
        [TestMethod]
        public void AddInvalidInputTests()
        {
            testObj = new Slot('A', 1, new Candy("ChocolateBar", 2));
            testObj.Add(2);

            testObj.Add(0);
            Assert.AreEqual(2, testObj.Count, "None were added.");

            testObj.Add(-2);
            Assert.AreEqual(2, testObj.Count, "Can't take anything with Add.");
        }
    }
}
