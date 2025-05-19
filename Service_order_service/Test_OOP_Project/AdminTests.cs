using Service_order_service;
using System.Text.Json;

namespace Test_OOP_Project
{
    [TestClass]
    public sealed class AdminTests
    {
        private Admin admin = null!;

        [TestInitialize]
        public void Setup()
        {
            var customer = new Customer(2, "Jane", "Smith", "jane.smith@example.com", new DateTime(1985, 8, 10), "Pass123", "+38(066)2234567", 200.0);
            var order = new Order(10, "Fix plumbing", ServiceCategory.HomeRepair, "Fix plumbing in the kitchen", 50.0, "123 Main St", new DateTime(2025, 12, 31), PaymentTerm.Prepayment, OrderStatus.Pending, customer, null);
            admin = new Admin(1, "Eve", "Johnson", "eve.johnson@example.com", new DateTime(1990, 6, 12), "adminPass123", "+38(050)5555555", 1000.00);
            var ordersJson = JsonSerializer.Serialize(new List<Order> { order });
            File.WriteAllText("orders.json", ordersJson);
        }

        [TestMethod]
        [DoNotParallelize]
        public void BlockUser()
        {
            int userId = 2;
            Admin.BlockUser(userId);

            Assert.IsTrue(Admin.IsUserBlocked(userId), "User should be blocked.");
        }

        [TestMethod]
        [DoNotParallelize]
        public void UnblockUser()
        {
            int userId = 3;
            Admin.BlockUser(userId);
            Admin.UnblockUser(userId);

            Assert.IsFalse(Admin.IsUserBlocked(userId), "User should be unblocked.");
        }

        [TestMethod]
        [DoNotParallelize]
        public void DeleteOrder()
        {
            // Act
            Admin.DeleteOrder(10);

            // Assert
            bool isDeleted = Admin.IsOrderDeleted(10);
            Assert.IsTrue(isDeleted, $"Order with ID {10} should be deleted.");
        }

        [TestMethod]
        [DoNotParallelize]
        public void GetName()
        {
            Assert.AreEqual("Eve", admin.Name);
        }

        [TestMethod]
        [DoNotParallelize]
        public void GetSurname()
        {
            Assert.AreEqual("Johnson", admin.Surname);
        }

        [TestMethod]
        [DoNotParallelize]
        public void GetEmail()
        {
            Assert.AreEqual("eve.johnson@example.com", admin.Email);
        }

        [TestMethod]
        [DoNotParallelize]
        public void GetDateOfBirth()
        {
            Assert.AreEqual(new DateTime(1990, 6, 12), admin.DateOfBirth);
        }

        [TestMethod]
        [DoNotParallelize]
        public void GetPassword()
        {
            Assert.AreEqual("adminPass123", admin.Password);
        }

        [TestMethod]
        [DoNotParallelize]
        public void GetPhoneNumber()
        {
            Assert.AreEqual("+38(050)5555555", admin.PhoneNumber);
        }

        [TestMethod]
        [DoNotParallelize]
        public void GetBalance()
        {
            Assert.AreEqual(1000.00, admin.Balance);
        }
    }
}
