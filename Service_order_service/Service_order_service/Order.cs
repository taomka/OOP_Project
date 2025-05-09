using System.Text.Json;
using System.Text.Json.Serialization;
using System.IO;

namespace Service_order_service
{
    public class Order : IOrderManagement
    {
        private int _orderId;
        private string? _title;
        private ServiceCategory _category;
        private string? _description;
        private double _price;
        private string? _location;
        private DateTime _deadline;
        private PaymentTerm _paymentTerm;
        private OrderStatus _status;
        private Customer? _customer;
        private Specialist? _specialist;

        public int OrderId
        {
            get => _orderId;
            set => _orderId = value;
        }

        public string? Title
        {
            get => _title;
            set => _title = !string.IsNullOrWhiteSpace(value)
                ? value
                : throw new ArgumentException("Title cannot be empty.");
        }

        public ServiceCategory Category
        {
            get => _category;
            set => _category = Enum.IsDefined(typeof(ServiceCategory), value)
                ? value
                : throw new ArgumentException("Invalid service category.");
        }

        public string? Description
        {
            get => _description;
            set => _description = !string.IsNullOrWhiteSpace(value)
                ? value
                : throw new ArgumentException("Description cannot be empty.");
        }

        public double Price
        {
            get => _price;
            set => _price = value >= 0
                ? value
                : throw new ArgumentException("Price cannot be negative.");
        }

        public string? Location
        {
            get => _location;
            set => _location = !string.IsNullOrWhiteSpace(value)
                ? value
                : throw new ArgumentException("Location cannot be empty.");
        }

        [JsonIgnore]
        public DateTime Deadline
        {
            get => _deadline;
            set
            {
                if (value <= DateTime.Now)
                    throw new ArgumentException("Deadline must be a future date.");
                _deadline = value;
            }
        }

        [JsonPropertyName("Deadline")]
        public DateTime DeadlineForJson
        {
            get => _deadline;
            set => _deadline = value;
        }

        public PaymentTerm PaymentTerm
        {
            get => _paymentTerm;
            set => _paymentTerm = Enum.IsDefined(typeof(PaymentTerm), value)
                ? value
                : throw new ArgumentException("Invalid payment term.");
        }

        public OrderStatus Status
        {
            get => _status;
            set => _status = Enum.IsDefined(typeof(OrderStatus), value)
                ? value
                : throw new ArgumentException("Invalid order status.");
        }

        public Customer? Customer
        {
            get => _customer;
            set => _customer = value ?? throw new ArgumentNullException(nameof(Customer), "Customer cannot be null.");
        }

        public Specialist? Specialist
        {
            get => _specialist;
            set => _specialist = value;
        }

        public Order() { }

        public Order(
            int orderId,
            string? title,
            ServiceCategory category,
            string? description,
            double price,
            string? location,
            DateTime deadline,
            PaymentTerm paymentTerm,
            OrderStatus status,
            Customer? customer,
            Specialist? specialist)
        {
            OrderId = orderId;
            Title = title;
            Category = category;
            Description = description;
            Price = price;
            Location = location;
            Deadline = deadline;
            PaymentTerm = paymentTerm;
            Status = status;
            Customer = customer;
            Specialist = specialist;
        }

        public void Publish()
        {
            var orders = JsonStorageService.LoadFromFile<Order>("orders.json");
            orders.Add(this);
            JsonStorageService.SaveToFile("orders.json", orders);
        }

        public void Edit(string newDescription, double newPrice)
        {
            var orders = JsonStorageService.LoadFromFile<Order>("orders.json");
            var existingOrder = orders.FirstOrDefault(o => o.OrderId == this.OrderId);

            if (existingOrder == null)
                throw new InvalidOperationException("Order not found.");

            if (existingOrder.Status == OrderStatus.Completed)
                throw new InvalidOperationException("Cannot edit a completed order.");

            if (existingOrder.Status == OrderStatus.Canceled)
                throw new InvalidOperationException("Cannot edit a canceled order.");

            existingOrder.Description = newDescription;
            this.Description = newDescription;
            existingOrder.Price = newPrice;
            this.Price = newPrice;

            JsonStorageService.SaveToFile("orders.json", orders);
        }
    }
}
