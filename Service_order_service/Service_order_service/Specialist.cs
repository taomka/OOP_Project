namespace Service_order_service
{
    public class Specialist : User
    {
        //public List<WorkExample> Portfolio = new List<WorkExample>();
        public List<Rating> Ratings = new List<Rating>();

        public Specialist(int userId, string name, string surname, string email, DateTime dateOfBirth, string password, string phoneNumber, double balance)
        {
            throw new NotImplementedException();
        }

        public void ApplyForOrder(Order order)
        {
            throw new NotImplementedException();
        }

        public void CompleteOrder(Order order)
        {
            throw new NotImplementedException();
        }
    }
}
