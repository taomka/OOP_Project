namespace Service_order_service
{
    public class Order
    {
        public int OrderId;
        public ServiceCategory Category;
        public string? Description;
        public double Price;
        public string? Location;
        public OrderStatus Status;
        public Customer? Customer;
        public Specialist? Specialist;

        public Order(int orderId, ServiceCategory category, string? description, double price, string? location, OrderStatus status, Customer? customer, Specialist? specialist)
        {
            throw new NotImplementedException();
        }

        public ServiceCategory GetCategory()
        {
            throw new NotImplementedException();
        }

        public string? GetDescription()
        {
            throw new NotImplementedException();
        }

        public double GetPrice()
        {
            throw new NotImplementedException();
        }

        public string? GetLocation()
        {
            throw new NotImplementedException();
        }

        public OrderStatus GetStatus()
        {
            throw new NotImplementedException();
        }

        public Customer? GetCustomer()
        {
            throw new NotImplementedException();
        }

        public Specialist? GetSpecialist()
        {
            throw new NotImplementedException();
        }

        public void Publish() 
        { 
            throw new NotImplementedException(); 
        }

        public void Edit(string newDescription, double newPrice)
        {
            throw new NotImplementedException();
        }

        public void Cancel()
        {
            throw new NotImplementedException();
        }

        public void Complete()
        {
            throw new NotImplementedException();
        }
    }
}
