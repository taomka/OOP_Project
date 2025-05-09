using System.IO;
using System.Text.Json;

namespace Service_order_service
{
    public class Customer : User
    {
        public override string GetUserFileName() => "customers.json";

        public Customer () { }

        public Customer(int userId, string name, string surname, string email, DateTime dateOfBirth, string password, string phoneNumber, double balance)
        {
            _userId = userId;
            Name = name;
            Surname = surname;
            Email = email;
            DateOfBirth = dateOfBirth;
            Password = password;
            PhoneNumber = phoneNumber;
            Balance = balance;
        }

        public void CreateOrder(Order order)
        {
            if (order == null)
                throw new ArgumentNullException(nameof(order));

            order.Customer = this;

            var orders = JsonStorageService.LoadFromFile<Order>("orders.json");
            orders.Add(order);
            JsonStorageService.SaveToFile("orders.json", orders);
        }

        public void CancelOrder(Order order)
        {
            if (order == null)
                throw new ArgumentNullException(nameof(order));

            if (order.Customer == null || order.Customer._userId != this._userId)
                throw new InvalidOperationException("You are not the owner of this order.");

            var orders = JsonStorageService.LoadFromFile<Order>("orders.json");
            var existingOrder = orders.FirstOrDefault(o => o.OrderId == order.OrderId);
            if (existingOrder == null)
                throw new InvalidOperationException("Order not found.");

            // Повернення коштів
            if (existingOrder.Specialist != null)
            {
                var specialist = existingOrder.Specialist;
                double refund = 0;

                switch (existingOrder.PaymentTerm)
                {
                    case PaymentTerm.Prepayment:
                        refund = existingOrder.Price;
                        break;
                    case PaymentTerm.Partially:
                        refund = existingOrder.Price / 2;
                        break;
                }

                if (refund > 0)
                {
                    var specialists = JsonStorageService.LoadFromFile<Specialist>("specialists.json");
                    var existingSpecialist = specialists.FirstOrDefault(s => s._userId == specialist._userId);
                    if (existingSpecialist != null)
                    {
                        existingSpecialist.Balance -= refund;
                        JsonStorageService.SaveToFile("specialists.json", specialists);
                    }

                    var customers = JsonStorageService.LoadFromFile<Customer>("customers.json");
                    var existingCustomer = customers.FirstOrDefault(c => c._userId == this._userId);
                    if (existingCustomer != null)
                    {
                        existingCustomer.Balance += refund;
                        JsonStorageService.SaveToFile("customers.json", customers);
                    }
                }
            }
            existingOrder.Status = OrderStatus.Canceled;
            order.Status = OrderStatus.Canceled;
            JsonStorageService.SaveToFile("orders.json", orders);
        }

        public void RateSpecialist(Order order, int ratingValue, string? comment = null)
        {
            if (order == null)
                throw new ArgumentNullException(nameof(order));

            if (order.Customer == null || order.Customer._userId != this._userId)
                throw new InvalidOperationException("You are not the owner of this order.");

            if (order.Status != OrderStatus.Completed)
                throw new InvalidOperationException("Cannot rate a specialist for an uncompleted order.");

            var specialist = order.Specialist;
            if (specialist == null)
                throw new InvalidOperationException("This order has no assigned specialist.");

            var rating = new Rating(ratingValue, comment);
            specialist.AddRating(rating);

            var specialists = JsonStorageService.LoadFromFile<Specialist>("specialists.json");
            var existingSpecialist = specialists.FirstOrDefault(s => s._userId == specialist._userId);
            if (existingSpecialist != null)
            {
                existingSpecialist.Ratings = specialist.Ratings;
                existingSpecialist.AverageRating = specialist.AverageRating;
                JsonStorageService.SaveToFile("specialists.json", specialists);
            }
        }
    }
}
