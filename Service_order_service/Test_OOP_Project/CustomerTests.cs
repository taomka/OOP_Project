namespace Test_OOP_Project
{
    [TestClass]
    public sealed class CustomerTests
    {
        private Customer customer;

        [TestInitialize]
        public void Setup()
        {
            customer = new Customer(2, "Alice", "Smith", "alice.smith@example.com", new DateTime(1995, 8, 15), "password123", "+38(099)-1234567", 200.75);
        }

        [TestMethod]
        public void GetName_ReturnsCorrectName()
        {
            Assert.AreEqual("Alice", customer.GetName());
        }

        [TestMethod]
        public void GetSurname_ReturnsCorrectSurname()
        {
            Assert.AreEqual("Smith", customer.GetSurname());
        }

        [TestMethod]
        public void GetEmail_ReturnsCorrectEmail()
        {
            Assert.AreEqual("alice.smith@example.com", customer.GetEmail());
        }

        [TestMethod]
        public void GetDateOfBirth_ReturnsCorrectDate()
        {
            Assert.AreEqual(new DateTime(1995, 8, 15), customer.GetDateOfBirth());
        }

        [TestMethod]
        public void GetPassword_ReturnsCorrectPassword()
        {
            Assert.AreEqual("password123", customer.GetPassword());
        }

        [TestMethod]
        public void GetPhoneNumber_ReturnsCorrectPhoneNumber()
        {
            Assert.AreEqual("+38(099)-1234567", customer.GetPhoneNumber());
        }

        [TestMethod]
        public void GetBalance_ReturnsCorrectBalance()
        {
            Assert.AreEqual(200.75, customer.GetBalance());
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void SetPhoneNumber_InvalidFormat_ThrowsException()
        {
            new Customer(3, "Bob", "Brown", "bob.brown@example.com", new DateTime(1992, 3, 10), "securePass456", "1234567890", 150.25);
        }
    }
}
