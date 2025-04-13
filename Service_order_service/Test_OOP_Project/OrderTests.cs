using Service_order_service;

namespace Test_OOP_Project
{
    [TestClass]
    public sealed class OrderTests
    {
        private Order order;
        private Customer customer;
        private Specialist specialist;

        [TestInitialize]
        public void Setup()
        {
            customer = new Customer(1, "John", "Doe", "john@example.com", new DateTime(1990, 1, 1), "password", "+38(066)8234567", 100.0);
            specialist = new Specialist(2, "Jane", "Smith", "jane@example.com", new DateTime(1985, 5, 15), "password", "+38(066)1234667", 200.0);

            order = new Order(1, ServiceCategory.IT, "Fix laptop", 150.0, "NYC", OrderStatus.Pending, customer, specialist);
        }

        [TestMethod]
        public void Publish_ChangeStatusToInProgress()
        {
            // Arrange
            order.Publish();

            // Act & Assert
            Assert.AreEqual(OrderStatus.InProgress, order.GetStatus());
        }

        [TestMethod]
        public void Publish_NotChangeStatus()
        {
            // Arrange
            order.Status = OrderStatus.Completed;

            // Act
            order.Publish();

            // Assert
            Assert.AreEqual(OrderStatus.Completed, order.GetStatus());
        }

        [TestMethod]
        public void Edit_UpdateDescriptionAndPrice()
        {
            // Arrange
            string newDescription = "Repair laptop screen";
            double newPrice = 200.0;

            // Act
            order.Edit(newDescription, newPrice);

            // Assert
            Assert.AreEqual(newDescription, order.GetDescription());
            Assert.AreEqual(newPrice, order.GetPrice());
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void Edit_ThrowException_OrderIsCompleted()
        {
            // Arrange
            order.Status = OrderStatus.Completed;

            // Act
            order.Edit("New description", 250.0);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void Edit_ThrowException_WhenOrderIsCanceled()
        {
            // Arrange
            order.Status = OrderStatus.Canceled;

            // Act
            order.Edit("New description", 250.0);
        }

        [TestMethod]
        public void Cancel_ChangeStatusToCanceled()
        {
            // Arrange
            order.Cancel();

            // Act & Assert
            Assert.AreEqual(OrderStatus.Canceled, order.GetStatus());
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void Cancel_ThrowException_WhenOrderIsCompleted()
        {
            // Arrange
            order.Status = OrderStatus.Completed;

            // Act
            order.Cancel();
        }

        [TestMethod]
        public void Complete_ChangeStatusToCompleted()
        {
            // Arrange
            order.Complete();

            // Act & Assert
            Assert.AreEqual(OrderStatus.Completed, order.GetStatus());
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void Complete_ThrowException_WhenOrderIsCanceled()
        {
            // Arrange
            order.Status = OrderStatus.Canceled;

            // Act
            order.Complete();
        }

        [TestMethod]
        public void GetCategory()
        {
            // Act & Assert
            Assert.AreEqual(ServiceCategory.IT, order.GetCategory());
        }

        [TestMethod]
        public void GetDescription()
        {
            // Act & Assert
            Assert.AreEqual("Fix laptop", order.GetDescription());
        }

        [TestMethod]
        public void GetPrice()
        {
            // Act & Assert
            Assert.AreEqual(150.0, order.GetPrice());
        }

        [TestMethod]
        public void GetLocation()
        {
            // Act & Assert
            Assert.AreEqual("NYC", order.GetLocation());
        }

        [TestMethod]
        public void GetStatus()
        {
            // Act & Assert
            Assert.AreEqual(OrderStatus.Pending, order.GetStatus());
        }

        [TestMethod]
        public void GetCustomer()
        {
            // Act & Assert
            Assert.AreEqual(customer, order.GetCustomer());
        }

        [TestMethod]
        public void GetSpecialist()
        {
            // Act & Assert
            Assert.AreEqual(specialist, order.GetSpecialist());
        }
    }
}
