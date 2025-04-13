using Service_order_service;

namespace Test_OOP_Project
{
    [TestClass]
    public sealed class SpecialistTests
    {
        private Specialist specialist;

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
            string phoneNumber = "+38(066)1233567";
            double balance = 100.0;

            // Act
            var specialist = new Specialist(userId, name, surname, email, dateOfBirth, password, phoneNumber, balance);

            // Assert
            Assert.AreEqual(userId, specialist.UserId);
            Assert.AreEqual(name, specialist.Name);
            Assert.AreEqual(surname, specialist.Surname);
            Assert.AreEqual(email, specialist.Email);
            Assert.AreEqual(dateOfBirth, specialist.DateOfBirth);
            Assert.AreEqual(password, specialist.Password);
            Assert.AreEqual(phoneNumber, specialist.PhoneNumber);
            Assert.AreEqual(balance, specialist.Balance);
        }

        [TestMethod]
        public void ApplyForOrder()
        {
            // Arrange
            var specialist = new Specialist(2, "Jane", "Smith", "jane.smith@example.com", new DateTime(1985, 8, 10), "Pass123", "+38(066)5234567", 200.0);
            var order = new Order(1, ServiceCategory.HomeRepair, "Fix plumbing", 50.0, "123 Main St", OrderStatus.Pending, null, null);

            // Act
            specialist.ApplyForOrder(order);

            // Assert
            Assert.AreEqual(specialist, order.Specialist);
        }

        [TestMethod]
        public void CompleteOrder()
        {
            // Arrange
            var specialist = new Specialist(3, "Alice", "Johnson", "alice.johnson@example.com", new DateTime(1995, 3, 15), "Pass456", "+38(066)1264567", 150.0);
            var order = new Order(2, ServiceCategory.Design, "Design logo", 100.0, "456 Elm St", OrderStatus.InProgress, null, null);

            // Act
            specialist.CompleteOrder(order);

            // Assert
            Assert.AreEqual(OrderStatus.Completed, order.Status);
        }

        [TestMethod]
        public void GetName()
        {
            Assert.AreEqual("John", specialist.GetName());
        }

        [TestMethod]
        public void GetSurnamee()
        {
            Assert.AreEqual("Doe", specialist.GetSurname());
        }

        [TestMethod]
        public void GetEmail()
        {
            Assert.AreEqual("john.doe@example.com", specialist.GetEmail());
        }

        [TestMethod]
        public void GetDateOfBirth()
        {
            Assert.AreEqual(new DateTime(1988, 5, 22), specialist.GetDateOfBirth());
        }

        [TestMethod]
        public void GetPassword()
        {
            Assert.AreEqual("securePass789", specialist.GetPassword());
        }

        [TestMethod]
        public void GetPhoneNumber()
        {
            Assert.AreEqual("+38(067)9876543", specialist.GetPhoneNumber());
        }

        [TestMethod]
        public void GetBalance()
        {
            Assert.AreEqual(500.00, specialist.GetBalance());
        }
    }
}
