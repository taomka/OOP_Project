namespace Service_order_service
{
    public class Customer : User
    {
        public Customer(int userId, string name, string surname, string email, DateTime dateOfBirth, string password, string phoneNumber, double balance)
        { 
            throw new NotImplementedException();
        }

        public void CreateOrder(Order order)
        {
            throw new NotImplementedException();
        }

        public void CancelOrder(Order order)
        {
            throw new NotImplementedException();
        }

        public void RateSpecialist(Specialist specialist, int rating)
        {
            throw new NotImplementedException();
        }
    }
}
