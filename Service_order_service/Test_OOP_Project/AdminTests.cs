namespace Test_OOP_Project
{
    [TestClass]
    public class AdminTests
    {
        private Admin admin;

        [TestInitialize]
        public void Setup()
        {
            admin = new Admin(1, "Eve", "Johnson", "eve.johnson@example.com", new DateTime(1990, 6, 12), "adminPass123", "+38(050)-5555555", 1000.00);
        }

        [TestMethod]
        public void GetName_ReturnsCorrectName()
        {
            Assert.AreEqual("Eve", admin.GetName());
        }

        [TestMethod]
        public void GetSurname_ReturnsCorrectSurname()
        {
            Assert.AreEqual("Johnson", admin.GetSurname());
        }

        [TestMethod]
        public void GetEmail_ReturnsCorrectEmail()
        {
            Assert.AreEqual("eve.johnson@example.com", admin.GetEmail());
        }

        [TestMethod]
        public void GetDateOfBirth_ReturnsCorrectDate()
        {
            Assert.AreEqual(new DateTime(1990, 6, 12), admin.GetDateOfBirth());
        }

        [TestMethod]
        public void GetPassword_ReturnsCorrectPassword()
        {
            Assert.AreEqual("adminPass123", admin.GetPassword());
        }

        [TestMethod]
        public void GetPhoneNumber_ReturnsCorrectPhoneNumber()
        {
            Assert.AreEqual("+38(050)-5555555", admin.GetPhoneNumber());
        }

        [TestMethod]
        public void GetBalance_ReturnsCorrectBalance()
        {
            Assert.AreEqual(1000.00, admin.GetBalance());
        }
    }
}
