namespace Service_order_service
{
    public interface IUserManagement
    {
        void AddUser(User user);
        void RemoveUser(User user);
        void UpdateUser(int userId, string? newPassword = null, string? newPhoneNumber = null);
    }
}
