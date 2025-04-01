namespace Test_OOP_Project
{
    [TestClass]
    public class OrderTests
    {
        private Order order;

        [TestInitialize]
        public void Setup()
        {
            order = new Order();
        }

        [TestMethod]
        [ExpectedException(typeof(NotImplementedException))]
        public void GetCategory_ThrowsNotImplementedException()
        {
            order.GetCategory();
        }

        [TestMethod]
        [ExpectedException(typeof(NotImplementedException))]
        public void GetDescription_ThrowsNotImplementedException()
        {
            order.GetDescription();
        }

        [TestMethod]
        [ExpectedException(typeof(NotImplementedException))]
        public void GetPrice_ThrowsNotImplementedException()
        {
            order.GetPrice();
        }

        [TestMethod]
        [ExpectedException(typeof(NotImplementedException))]
        public void GetLocation_ThrowsNotImplementedException()
        {
            order.GetLocation();
        }

        [TestMethod]
        [ExpectedException(typeof(NotImplementedException))]
        public void GetStatus_ThrowsNotImplementedException()
        {
            order.GetStatus();
        }

        [TestMethod]
        [ExpectedException(typeof(NotImplementedException))]
        public void Publish_ThrowsNotImplementedException()
        {
            order.Publish();
        }

        [TestMethod]
        [ExpectedException(typeof(NotImplementedException))]
        public void Edit_ThrowsNotImplementedException()
        {
            order.Edit();
        }

        [TestMethod]
        [ExpectedException(typeof(NotImplementedException))]
        public void Cancel_ThrowsNotImplementedException()
        {
            order.Cancel();
        }

        [TestMethod]
        [ExpectedException(typeof(NotImplementedException))]
        public void Complete_ThrowsNotImplementedException()
        {
            order.Complete();
        }
    }
}
