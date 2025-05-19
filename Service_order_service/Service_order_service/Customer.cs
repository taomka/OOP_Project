namespace Service_order_service
{
    public class Customer : User
    {
        public override string GetUserFileName() => "F:\\Documents\\Програмирование\\Лабораторные универа\\2 курс\\2 семестр\\OOP_Project\\Service_order_service\\Service_order_service\\JsonFiles\\customers.json";

        public delegate void OrderCreatedEventHandler(object sender, Order order);

        public event OrderCreatedEventHandler? OrderCreated;

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
            ArgumentNullException.ThrowIfNull(order);

            order.Customer = this;

            var orders = JsonStorageService.LoadFromFile<Order>("F:\\Documents\\Програмирование\\Лабораторные универа\\2 курс\\2 семестр\\OOP_Project\\Service_order_service\\Service_order_service\\JsonFiles\\orders.json");
            orders.Add(order);
            JsonStorageService.SaveToFile("F:\\Documents\\Програмирование\\Лабораторные универа\\2 курс\\2 семестр\\OOP_Project\\Service_order_service\\Service_order_service\\JsonFiles\\orders.json", orders);

            OrderCreated?.Invoke(this, order);
        }

        public void CancelOrder(Order order)
        {
            ArgumentNullException.ThrowIfNull(order);

            if (order.Customer == null || order.Customer._userId != this._userId)
                throw new InvalidOperationException("You are not the owner of this order.");

            var orders = JsonStorageService.LoadFromFile<Order>("F:\\Documents\\Програмирование\\Лабораторные универа\\2 курс\\2 семестр\\OOP_Project\\Service_order_service\\Service_order_service\\JsonFiles\\orders.json");
            var existingOrder = orders.FirstOrDefault(o => o.OrderId == order.OrderId) ?? throw new InvalidOperationException("Order not found.");

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
                    var specialists = JsonStorageService.LoadFromFile<Specialist>("F:\\Documents\\Програмирование\\Лабораторные универа\\2 курс\\2 семестр\\OOP_Project\\Service_order_service\\Service_order_service\\JsonFiles\\specialists.json");
                    var existingSpecialist = specialists.FirstOrDefault(s => s._userId == specialist._userId);
                    if (existingSpecialist != null)
                    {
                        existingSpecialist.Balance -= refund;
                        JsonStorageService.SaveToFile("F:\\Documents\\Програмирование\\Лабораторные универа\\2 курс\\2 семестр\\OOP_Project\\Service_order_service\\Service_order_service\\JsonFiles\\specialists.json", specialists);
                    }

                    var customers = JsonStorageService.LoadFromFile<Customer>("F:\\Documents\\Програмирование\\Лабораторные универа\\2 курс\\2 семестр\\OOP_Project\\Service_order_service\\Service_order_service\\JsonFiles\\customers.json");
                    var existingCustomer = customers.FirstOrDefault(c => c._userId == this._userId);
                    if (existingCustomer != null)
                    {
                        existingCustomer.Balance += refund;
                        JsonStorageService.SaveToFile("F:\\Documents\\Програмирование\\Лабораторные универа\\2 курс\\2 семестр\\OOP_Project\\Service_order_service\\Service_order_service\\JsonFiles\\customers.json", customers);
                    }
                }
            }
            existingOrder.Status = OrderStatus.Canceled;
            order.Status = OrderStatus.Canceled;
            JsonStorageService.SaveToFile("F:\\Documents\\Програмирование\\Лабораторные универа\\2 курс\\2 семестр\\OOP_Project\\Service_order_service\\Service_order_service\\JsonFiles\\orders.json", orders);
        }

        public void RateSpecialist(Order order, int ratingValue, string? comment = null)
        {
            ArgumentNullException.ThrowIfNull(order);

            if (order.Customer == null || order.Customer.UserId != this.UserId)
                throw new InvalidOperationException("You are not the owner of this order.");

            if (order.Status != OrderStatus.Completed)
                throw new InvalidOperationException("Cannot rate a specialist for an uncompleted order.");

            if (order.Specialist == null)
                throw new InvalidOperationException("This order has no assigned specialist.");

            var specialists = JsonStorageService.LoadFromFile<Specialist>(
                "F:\\Documents\\Програмирование\\Лабораторные универа\\2 курс\\2 семестр\\OOP_Project\\Service_order_service\\Service_order_service\\JsonFiles\\specialists.json");

            var specialistToRate = specialists.FirstOrDefault(s => s.UserId == order.Specialist.UserId) ?? throw new InvalidOperationException("Specialist not found.");

            var rating = new Rating(ratingValue, comment);
            specialistToRate.AddRating(rating);

            JsonStorageService.SaveToFile(
                "F:\\Documents\\Програмирование\\Лабораторные универа\\2 курс\\2 семестр\\OOP_Project\\Service_order_service\\Service_order_service\\JsonFiles\\specialists.json",
                specialists);
        }
    }
}
