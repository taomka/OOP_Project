using Service_order_service;

namespace Test_OOP_Project
{
    [TestClass]
    public sealed class CustomerTests
    {
        private Customer customer;

        [TestMethod]
        public void Constructor()
        {
            // Arrange
            int userId = 1;
            string name = "John";
            string surname = "Doe";
            string email = "john.doe@example.com";
            DateTime dateOfBirth = new DateTime(1990, 5, 20);
            string password = "SecurePass123";
            string phoneNumber = "+38(066)1234567";
            double balance = 100.0;

            // Act
            var customer = new Customer(userId, name, surname, email, dateOfBirth, password, phoneNumber, balance);

            // Assert
            Assert.AreEqual(userId, customer.UserId);
            Assert.AreEqual(name, customer.Name);
            Assert.AreEqual(surname, customer.Surname);
            Assert.AreEqual(email, customer.Email);
            Assert.AreEqual(dateOfBirth, customer.DateOfBirth);
            Assert.AreEqual(password, customer.Password);
            Assert.AreEqual(phoneNumber, customer.PhoneNumber);
            Assert.AreEqual(balance, customer.Balance);
        }

        [TestMethod]
        public void CreateOrder()
        {
            // Arrange
            var customer = new Customer(2, "Jane", "Smith", "jane.smith@example.com", new DateTime(1985, 8, 10), "Pass123", "+38(066)2234567", 200.0);
            var order = new Order(1, ServiceCategory.HomeRepair, "Fix plumbing", 50.0, "123 Main St", OrderStatus.Pending, customer, null);

            // Act
            customer.CreateOrder(order);

            // Assert
            Assert.AreEqual(customer, order.Customer);
            Assert.AreEqual(OrderStatus.Pending, order.Status);
        }

        [TestMethod]
        public void CancelOrder()
        {
            // Arrange
            var customer = new Customer(3, "Alice", "Johnson", "alice.johnson@example.com", new DateTime(1995, 3, 15), "Pass456", "+38(066)1134567", 150.0);
            var order = new Order(2, ServiceCategory.Design, "Design logo", 100.0, "456 Elm St", OrderStatus.InProgress, customer, null);

            // Act
            customer.CancelOrder(order);

            // Assert
            Assert.AreEqual(OrderStatus.Canceled, order.Status);
        }

        [TestMethod]
        public void RateSpecialist()
        {
            // Arrange
            var customer = new Customer(4, "Bob", "Brown", "bob.brown@example.com", new DateTime(1988, 7, 22), "Pass789", "+38(066)3234567", 300.0);
            var specialist = new Specialist(5, "Charlie", "White", "charlie.white@example.com", new DateTime(1992, 6, 18), "Pass999", "+38(066)1224567", 500.0);
            int ratingScore = 5;

            // Act
            customer.RateSpecialist(specialist, ratingScore);

            // Assert
            Assert.AreEqual(1, specialist.Ratings.Count);
            Assert.AreEqual(ratingScore, specialist.Ratings[0].Score);
        }

        [TestMethod]
        public void GetName()
        {
            Assert.AreEqual("Alice", customer.GetName());
        }

        [TestMethod]
        public void GetSurname()
        {
            Assert.AreEqual("Smith", customer.GetSurname());
        }

        [TestMethod]
        public void GetEmail()
        {
            Assert.AreEqual("alice.smith@example.com", customer.GetEmail());
        }

        [TestMethod]
        public void GetDateOfBirth()
        {
            Assert.AreEqual(new DateTime(1995, 8, 15), customer.GetDateOfBirth());
        }

        [TestMethod]
        public void GetPassword()
        {
            Assert.AreEqual("password123", customer.GetPassword());
        }

        [TestMethod]
        public void GetPhoneNumber()
        {
            Assert.AreEqual("+38(099)1234567", customer.GetPhoneNumber());
        }

        [TestMethod]
        public void GetBalance()
        {
            Assert.AreEqual(200.75, customer.GetBalance());
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void SetPhoneNumber()
        {
            new Customer(3, "Bob", "Brown", "bob.brown@example.com", new DateTime(1992, 3, 10), "securePass456", "1234567890", 150.25);
        }
    }
}
