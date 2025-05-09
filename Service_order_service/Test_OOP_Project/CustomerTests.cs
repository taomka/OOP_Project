using Service_order_service;
using System.Text.Json;

namespace Test_OOP_Project
{
    [TestClass]
    public sealed class CustomerTests
    {
        private Customer customer = null!;
        private Specialist specialist = null!;
        private Order order = null!;

        [TestInitialize]
        public void Setup()
        {
            customer = new Customer(1, "Alice", "Smith", "alice.smith@example.com", new DateTime(1995, 8, 15), "password123", "+38(099)1234567", 200.75);
            specialist = new Specialist(2, "Charlie", "White", "charlie.white@example.com", new DateTime(1992, 6, 18), "Pass999", "+38(066)1224567", 500.0);
            order = new Order(
                1,
                "Fix plumbing",
                ServiceCategory.HomeRepair,
                "Fix plumbing in the kitchen",
                50.0,
                "123 Main St",
                new DateTime(2025, 12, 31),
                PaymentTerm.Prepayment,
                OrderStatus.Pending,
                customer,
                null
            );
            var ordersJson = JsonSerializer.Serialize(new List<Order> { order });
            File.WriteAllText("orders.json", ordersJson);
        }

        [TestMethod]
        [DoNotParallelize]
        public void CreateOrder()
        {
            // Act
            customer.CreateOrder(order);

            // Assert
            Assert.AreEqual(customer, order.Customer);
            Assert.AreEqual(OrderStatus.Pending, order.Status);
        }

        [TestMethod]
        [DoNotParallelize]
        public void CancelOrder()
        {
            // Arrange
            order.Status = OrderStatus.InProgress;
            order.Specialist = specialist;

            // Act
            customer.CancelOrder(order);

            // Assert
            Assert.AreEqual(OrderStatus.Canceled, order.Status);
        }

        [TestMethod]
        [DoNotParallelize]
        public void RateSpecialist()
        {
            // Arrange
            order.Status = OrderStatus.Completed;
            order.Specialist = specialist;
            int ratingValue = 5;
            string comment = "Excellent work!";

            // Act
            customer.RateSpecialist(order, ratingValue, comment);

            // Assert
            Assert.AreEqual(1, specialist.Ratings.Count);
            Assert.AreEqual(ratingValue, specialist.Ratings[0].Score);
            Assert.AreEqual(comment, specialist.Ratings[0].Comment);
        }

        [TestMethod]
        [DoNotParallelize]
        public void GetName()
        {
            // Act
            var name = customer.Name;

            // Assert
            Assert.AreEqual("Alice", name);
        }

        [TestMethod]
        [DoNotParallelize]
        public void GetSurname()
        {
            // Act
            var surname = customer.Surname;

            // Assert
            Assert.AreEqual("Smith", surname);
        }

        [TestMethod]
        [DoNotParallelize]
        public void GetEmail()
        {
            // Act
            var email = customer.Email;

            // Assert
            Assert.AreEqual("alice.smith@example.com", email);
        }

        [TestMethod]
        [DoNotParallelize]
        public void GetDateOfBirth()
        {
            // Act
            var dateOfBirth = customer.DateOfBirth;

            // Assert
            Assert.AreEqual(new DateTime(1995, 8, 15), dateOfBirth);
        }

        [TestMethod]
        [DoNotParallelize]
        public void GetPassword()
        {
            // Act
            var password = customer.Password;

            // Assert
            Assert.AreEqual("password123", password);
        }

        [TestMethod]
        [DoNotParallelize]
        public void GetPhoneNumber()
        {
            // Act
            var phoneNumber = customer.PhoneNumber;

            // Assert
            Assert.AreEqual("+38(099)1234567", phoneNumber);
        }

        [TestMethod]
        [DoNotParallelize]
        public void GetBalance()
        {
            // Act
            var balance = customer.Balance;

            // Assert
            Assert.AreEqual(200.75, balance);
        }
    }
}
