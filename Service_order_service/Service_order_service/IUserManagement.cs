namespace Service_order_service
{
    public interface IUserManagement
    {
        void AddUser(User user);
        void RemoveUser(User user);
        void UpdateCustomer(int userId, string? newPassword = null, string? newPhoneNumber = null);
        void UpdateSpecialist(int userId, string? newPassword = null, string? newPhoneNumber = null);
    }
}
