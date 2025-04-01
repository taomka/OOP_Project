namespace Service_order_service
{
    class Order
    {
        private int OrderId;
        private ServiceCategory Category;
        private string? Description;
        private double Price;
        private string? Location;
        private OrderStatus Status;
        private Customer? Customer;
        private Specialist? Specialist;

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

        public void Edit()
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
