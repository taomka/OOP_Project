using System.IO;
using System.Text.Json;

namespace Service_order_service
{
    public class Specialist : User
    {
        public override string GetUserFileName() => "specialists.json";

        public List<Rating> Ratings { get; set; }
        public double AverageRating { get; set; }

        public Specialist() { }

        public Specialist(int userId, string name, string surname, string email, DateTime dateOfBirth, string password, string phoneNumber, double balance)
        {
            _userId = userId;
            Name = name;
            Surname = surname;
            Email = email;
            DateOfBirth = dateOfBirth;
            Password = password;
            PhoneNumber = phoneNumber;
            Balance = balance;
            Ratings = new List<Rating>();
            AverageRating = 0;
        }

        public void ApplyForOrder(Order order)
        {
            if (order == null)
                throw new ArgumentNullException(nameof(order));

            if (order.Specialist != null)
                throw new InvalidOperationException("This order already has a specialist.");

            var orders = JsonStorageService.LoadFromFile<Order>("orders.json");
            var existingOrder = orders.FirstOrDefault(o => o.OrderId == order.OrderId);
            if (existingOrder == null)
                throw new InvalidOperationException("Order not found.");

            existingOrder.Specialist = this;
            order.Specialist = this;
            JsonStorageService.SaveToFile("orders.json", orders);
        }

        public void CompleteOrder(Order order)
        {
            if (order == null)
                throw new ArgumentNullException(nameof(order));

            if (order.Specialist == null || order.Specialist._userId != this._userId)
                throw new InvalidOperationException("You are not assigned to this order.");

            if (order.Status == OrderStatus.Completed)
                throw new InvalidOperationException("Order is already completed.");

            var customer = order.Customer;
            if (customer == null)
                throw new InvalidOperationException("Customer information is missing.");

            if (customer.Balance < order.Price)
                throw new InvalidOperationException("Customer does not have enough balance to complete the order.");

            var orders = JsonStorageService.LoadFromFile<Order>("orders.json");
            var existingOrder = orders.FirstOrDefault(o => o.OrderId == order.OrderId);
            if (existingOrder != null)
            {
                existingOrder.Status = OrderStatus.Completed;
                order.Status = OrderStatus.Completed;
            }
            JsonStorageService.SaveToFile("orders.json", orders);

            customer.Balance -= order.Price;
            this.Balance += order.Price;

            var customers = JsonStorageService.LoadFromFile<Customer>("customers.json");
            var existingCustomer = customers.FirstOrDefault(c => c._userId == customer._userId);
            if (existingCustomer != null)
            {
                existingCustomer.Balance = customer.Balance;
            }
            JsonStorageService.SaveToFile("customers.json", customers);

            var specialists = JsonStorageService.LoadFromFile<Specialist>("specialists.json");
            var existingSpecialist = specialists.FirstOrDefault(s => s._userId == this._userId);
            if (existingSpecialist != null)
            {
                existingSpecialist.Balance = this.Balance;
            }
            JsonStorageService.SaveToFile("specialists.json", specialists);
        }

        public void AddRating(Rating rating)
        {
            if (rating == null)
                throw new ArgumentNullException(nameof(rating));

            Ratings.Add(rating);
            UpdateAverageRating();
        }

        private void UpdateAverageRating()
        {
            if (Ratings.Count == 0)
            {
                AverageRating = 0;
                return;
            }

            AverageRating = Ratings.Average(r => r.Score);
        }
    }
}
