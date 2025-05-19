namespace Service_order_service
{
    public class Specialist : User
    {
        public override string GetUserFileName() => "F:\\Documents\\Програмирование\\Лабораторные универа\\2 курс\\2 семестр\\OOP_Project\\Service_order_service\\Service_order_service\\JsonFiles\\specialists.json";

        public List<Rating> Ratings { get; set; } = [];
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
            Ratings = [];
            AverageRating = 0;
        }

        public void ApplyForOrder(Order order)
        {
            ArgumentNullException.ThrowIfNull(order);

            if (order.Specialist != null)
                throw new InvalidOperationException("This order already has a specialist.");

            var orders = JsonStorageService.LoadFromFile<Order>("F:\\Documents\\Програмирование\\Лабораторные универа\\2 курс\\2 семестр\\OOP_Project\\Service_order_service\\Service_order_service\\JsonFiles\\orders.json");
            var existingOrder = orders.FirstOrDefault(o => o.OrderId == order.OrderId) ?? throw new InvalidOperationException("Order not found.");
            if (order.PaymentTerm == PaymentTerm.Prepayment)
            {
                var customer = order.Customer ?? throw new InvalidOperationException("Customer information is missing.");
                if (customer.Balance < order.Price)
                    throw new InvalidOperationException("Customer does not have enough balance for prepayment.");

                customer.Balance -= order.Price;
                this.Balance += order.Price;

                var customers = JsonStorageService.LoadFromFile<Customer>("F:\\Documents\\Програмирование\\Лабораторные универа\\2 курс\\2 семестр\\OOP_Project\\Service_order_service\\Service_order_service\\JsonFiles\\customers.json");
                var existingCustomer = customers.FirstOrDefault(c => c._userId == customer._userId);
                if (existingCustomer != null)
                {
                    existingCustomer.Balance = customer.Balance;
                    JsonStorageService.SaveToFile("F:\\Documents\\Програмирование\\Лабораторные универа\\2 курс\\2 семестр\\OOP_Project\\Service_order_service\\Service_order_service\\JsonFiles\\customers.json", customers);
                }

                var specialists = JsonStorageService.LoadFromFile<Specialist>("F:\\Documents\\Програмирование\\Лабораторные универа\\2 курс\\2 семестр\\OOP_Project\\Service_order_service\\Service_order_service\\JsonFiles\\specialists.json");
                var existingSpecialist = specialists.FirstOrDefault(s => s._userId == this._userId);
                if (existingSpecialist != null)
                {
                    existingSpecialist.Balance = this.Balance;
                    JsonStorageService.SaveToFile("F:\\Documents\\Програмирование\\Лабораторные универа\\2 курс\\2 семестр\\OOP_Project\\Service_order_service\\Service_order_service\\JsonFiles\\specialists.json", specialists);
                }
            }
            existingOrder.Specialist = this;
            order.Specialist = this;
            existingOrder.Status = OrderStatus.InProgress;
            order.Status = OrderStatus.InProgress;
            JsonStorageService.SaveToFile("F:\\Documents\\Програмирование\\Лабораторные универа\\2 курс\\2 семестр\\OOP_Project\\Service_order_service\\Service_order_service\\JsonFiles\\orders.json", orders);
        }

        public void CompleteOrder(Order order)
        {
            ArgumentNullException.ThrowIfNull(order);

            if (order.Specialist == null || order.Specialist._userId != this._userId)
                throw new InvalidOperationException("You are not assigned to this order.");

            if (order.Status == OrderStatus.Completed)
                throw new InvalidOperationException("Order is already completed.");

            var customer = order.Customer ?? throw new InvalidOperationException("Customer information is missing.");
            if (customer.Balance < order.Price)
                throw new InvalidOperationException("Customer does not have enough balance to complete the order.");

            var orders = JsonStorageService.LoadFromFile<Order>("F:\\Documents\\Програмирование\\Лабораторные универа\\2 курс\\2 семестр\\OOP_Project\\Service_order_service\\Service_order_service\\JsonFiles\\orders.json");
            var existingOrder = orders.FirstOrDefault(o => o.OrderId == order.OrderId);
            if (existingOrder != null)
            {
                existingOrder.Status = OrderStatus.Completed;
                order.Status = OrderStatus.Completed;
            }
            JsonStorageService.SaveToFile("F:\\Documents\\Програмирование\\Лабораторные универа\\2 курс\\2 семестр\\OOP_Project\\Service_order_service\\Service_order_service\\JsonFiles\\orders.json", orders);

            customer.Balance -= order.Price;
            this.Balance += order.Price;

            var customers = JsonStorageService.LoadFromFile<Customer>("F:\\Documents\\Програмирование\\Лабораторные универа\\2 курс\\2 семестр\\OOP_Project\\Service_order_service\\Service_order_service\\JsonFiles\\customers.json");
            var existingCustomer = customers.FirstOrDefault(c => c._userId == customer._userId);
            if (existingCustomer != null)
            {
                existingCustomer.Balance = customer.Balance;
            }
            JsonStorageService.SaveToFile("F:\\Documents\\Програмирование\\Лабораторные универа\\2 курс\\2 семестр\\OOP_Project\\Service_order_service\\Service_order_service\\JsonFiles\\customers.json", customers);

            var specialists = JsonStorageService.LoadFromFile<Specialist>("F:\\Documents\\Програмирование\\Лабораторные универа\\2 курс\\2 семестр\\OOP_Project\\Service_order_service\\Service_order_service\\JsonFiles\\specialists.json");
            var existingSpecialist = specialists.FirstOrDefault(s => s._userId == this._userId);
            if (existingSpecialist != null)
            {
                existingSpecialist.Balance = this.Balance;
            }
            JsonStorageService.SaveToFile("F:\\Documents\\Програмирование\\Лабораторные универа\\2 курс\\2 семестр\\OOP_Project\\Service_order_service\\Service_order_service\\JsonFiles\\specialists.json", specialists);
        }

        public void AddRating(Rating rating)
        {
            ArgumentNullException.ThrowIfNull(rating);

            Ratings ??= [];
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
