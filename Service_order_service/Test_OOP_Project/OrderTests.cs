using Service_order_service;
using System.Text.Json;

namespace Test_OOP_Project
{
    [TestClass]
    public sealed class OrderTests
    {
        private Order order = null!;
        private Customer customer = null!;
        private Specialist specialist = null!;

        [TestInitialize]
        public void Setup()
        {
            customer = new Customer(1, "John", "Doe", "john@example.com", new DateTime(1990, 1, 1), "password", "+38(066)8234567", 300.0);
            specialist = new Specialist(2, "Jane", "Smith", "jane@example.com", new DateTime(1985, 5, 15), "password", "+38(066)1234667", 200.0);

            order = new Order(1, "Fix laptop", ServiceCategory.IT, "Fix laptop after fire", 150.0, "NYC", new DateTime(2025, 12, 31),
                              PaymentTerm.Prepayment, OrderStatus.InProgress, customer, specialist);
            var ordersJson = JsonSerializer.Serialize(new List<Order> { order });
            File.WriteAllText("orders.json", ordersJson);
        }

        [TestMethod]
        [DoNotParallelize]
        public void Edit()
        {
            // Act
            order.Edit("Repair screen", 200.0);

            // Assert
            Assert.AreEqual("Repair screen", order.Description);
            Assert.AreEqual(200.0, order.Price);
        }

        [TestMethod]
        [DoNotParallelize]
        [ExpectedException(typeof(InvalidOperationException))]
        public void Edit_Throw_WhenOrderIsCompleted()
        {
            specialist.CompleteOrder(order);
            order.Edit("Try edit", 100.0);
        }

        [TestMethod]
        [DoNotParallelize]
        [ExpectedException(typeof(InvalidOperationException))]
        public void Edit_Throw_WhenOrderIsCanceled()
        {
            customer.CancelOrder(order);
            order.Edit("Try edit", 100.0);
        }

        [TestMethod]
        [DoNotParallelize]
        public void Cancel()
        {
            // Act
            customer.CancelOrder(order);

            // Assert
            Assert.AreEqual(OrderStatus.Canceled, order.Status);
        }

        [TestMethod]
        [DoNotParallelize]
        public void Complete()
        {
            // Act
            specialist.CompleteOrder(order);

            // Assert
            Assert.AreEqual(OrderStatus.Completed, order.Status);
        }

        [TestMethod]
        [DoNotParallelize]
        public void GetCategory()
        {
            // Act & Assert
            Assert.AreEqual(ServiceCategory.IT, order.Category);
        }

        [TestMethod]
        [DoNotParallelize]
        public void GetDescription()
        {
            // Act & Assert
            Assert.AreEqual("Fix laptop after fire", order.Description);
        }

        [TestMethod]
        [DoNotParallelize]
        public void GetPrice()
        {
            // Act & Assert
            Assert.AreEqual(150.0, order.Price);
        }

        [TestMethod]
        [DoNotParallelize]
        public void GetLocation()
        {
            // Act & Assert
            Assert.AreEqual("NYC", order.Location);
        }

        [TestMethod]
        [DoNotParallelize]
        public void GetStatus()
        {
            // Act & Assert
            Assert.AreEqual(OrderStatus.InProgress, order.Status);
        }

        [TestMethod]
        [DoNotParallelize]
        public void GetCustomer()
        {
            // Act & Assert
            Assert.AreEqual(customer, order.Customer);
        }

        [TestMethod]
        [DoNotParallelize]
        public void GetSpecialist()
        {
            // Act & Assert
            Assert.AreEqual(specialist, order.Specialist);
        }
    }
}
