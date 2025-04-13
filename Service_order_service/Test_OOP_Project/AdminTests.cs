using Service_order_service;

namespace Test_OOP_Project
{
    [TestClass]
    public sealed class AdminTests
    {
        private Admin admin;

        [TestInitialize]
        public void Setup()
        {
            admin = new Admin(1, "Admin", "User", "admin@example.com", new DateTime(1990, 1, 1), "password", "+38(066)9234567", 0);
        }

        [TestMethod]
        public void BlockUser()
        {
            int userId = 2;
            admin.BlockUser(userId);

            Assert.IsTrue(admin.IsUserBlocked(userId), "Користувач має бути заблокований.");
        }

        [TestMethod]
        public void UnblockUser()
        {
            int userId = 3;
            admin.BlockUser(userId);
            admin.UnblockUser(userId);

            Assert.IsFalse(admin.IsUserBlocked(userId), "Користувач має бути розблокований.");
        }

        [TestMethod]
        public void DeleteOrder()
        {
            Assert.ThrowsException<NotImplementedException>(() => admin.DeleteOrder(101, "Canceled"));
        }

        [TestMethod]
        public void GetName()
        {
            Assert.AreEqual("Eve", admin.GetName());
        }

        [TestMethod]
        public void GetSurname()
        {
            Assert.AreEqual("Johnson", admin.GetSurname());
        }

        [TestMethod]
        public void GetEmail()
        {
            Assert.AreEqual("eve.johnson@example.com", admin.GetEmail());
        }

        [TestMethod]
        public void GetDateOfBirth()
        {
            Assert.AreEqual(new DateTime(1990, 6, 12), admin.GetDateOfBirth());
        }

        [TestMethod]
        public void GetPassword()
        {
            Assert.AreEqual("adminPass123", admin.GetPassword());
        }

        [TestMethod]
        public void GetPhoneNumber()
        {
            Assert.AreEqual("+38(050)5555555", admin.GetPhoneNumber());
        }

        [TestMethod]
        public void GetBalance()
        {
            Assert.AreEqual(1000.00, admin.GetBalance());
        }
    }
}
