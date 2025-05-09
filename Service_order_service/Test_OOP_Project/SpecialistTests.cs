using Service_order_service;
using System.Text.Json;

namespace Test_OOP_Project
{
    [TestClass]
    public sealed class SpecialistTests
    {
        private Specialist specialist = null!;
        private Customer customer = null!;
        private Order order = null!;

        [TestInitialize]
        public void Setup()
        {
            specialist = new Specialist(3, "Alice", "Johnson", "alice.johnson@example.com", new DateTime(1995, 3, 15), "Pass456", "+38(066)1264567", 150.0);
            customer = new Customer(1, "Bob", "Williams", "bob.williams@example.com", new DateTime(1980, 7, 25), "CustPass123", "+38(066)9876543", 300.0);
            order = new Order(2, "Design logo", ServiceCategory.Design, "Design logo for YT channel", 100.0, "456 Elm St", new DateTime(2025, 12, 31), PaymentTerm.Prepayment, OrderStatus.InProgress, customer, null);
            var ordersJson = JsonSerializer.Serialize(new List<Order> { order });
            File.WriteAllText("orders.json", ordersJson);
        }

        [TestMethod]
        [DoNotParallelize]
        public void ApplyForOrder()
        {
            // Act
            specialist.ApplyForOrder(order);

            // Assert
            Assert.IsNotNull(order);
            Assert.IsNotNull(order.Specialist);
            Assert.AreEqual(specialist.Email, order.Specialist.Email);
        }

        [TestMethod]
        [DoNotParallelize]
        public void CompleteOrder()
        {
            // Act
            specialist.ApplyForOrder(order);
            specialist.CompleteOrder(order);
            

            // Assert
            Assert.AreEqual(OrderStatus.Completed, order.Status);
            Assert.AreEqual(250.0, specialist.Balance); // 150 + 100 = 250
            Assert.AreEqual(200.0, customer.Balance);   // 300 - 100 = 200
        }

        [TestMethod]
        [DoNotParallelize]
        public void GetName()
        {
            Assert.AreEqual("Alice", specialist.Name);
        }

        [TestMethod]
        [DoNotParallelize]
        public void GetSurname()
        {
            Assert.AreEqual("Johnson", specialist.Surname);
        }

        [TestMethod]
        [DoNotParallelize]
        public void GetEmail()
        {
            Assert.AreEqual("alice.johnson@example.com", specialist.Email);
        }

        [TestMethod]
        [DoNotParallelize]
        public void GetDateOfBirth()
        {
            Assert.AreEqual(new DateTime(1995, 3, 15), specialist.DateOfBirth);
        }

        [TestMethod]
        [DoNotParallelize]
        public void GetPassword()
        {
            Assert.AreEqual("Pass456", specialist.Password);
        }

        [TestMethod]
        [DoNotParallelize]
        public void GetPhoneNumber()
        {
            Assert.AreEqual("+38(066)1264567", specialist.PhoneNumber);
        }

        [TestMethod]
        [DoNotParallelize]
        public void GetBalance()
        {
            Assert.AreEqual(150.0, specialist.Balance);
        }
    }
}
