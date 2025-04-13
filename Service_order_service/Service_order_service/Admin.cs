namespace Service_order_service
{
    public class Admin : User
    {
        private HashSet<int> BlockedUsers = new HashSet<int>();
        private List<Order> Orders = new List<Order>();

        public Admin(int userId, string name, string surname, string email, DateTime dateOfBirth, string password, string phoneNumber, double balance)
        {
            throw new NotImplementedException();
        }

        public void BlockUser(int UserId)
        {
            throw new NotImplementedException();
        }

        public void UnblockUser(int UserId)
        {
            throw new NotImplementedException();
        }

        public bool IsUserBlocked(int userId)
        {
            throw new NotImplementedException();
        }

        public void DeleteOrder(int OrderId, string NewStatus)
        {
            throw new NotImplementedException();
        }
    }
}
