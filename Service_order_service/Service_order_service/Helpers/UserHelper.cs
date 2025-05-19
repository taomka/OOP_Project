using System.Linq;

namespace Service_order_service.Helpers
{
    public static class UserHelper
    {
        public static User ReloadUser(User user)
        {
            var fileName = user.GetUserFileName();
            if (user is Customer)
            {
                var users = JsonStorageService.LoadFromFile<Customer>(fileName);
                return users.FirstOrDefault(u => u._userId == user._userId) ?? user;
            }
            else if (user is Specialist)
            {
                var users = JsonStorageService.LoadFromFile<Specialist>(fileName);
                return users.FirstOrDefault(u => u._userId == user._userId) ?? user;
            }
            else if (user is Admin)
            {
                var users = JsonStorageService.LoadFromFile<Admin>(fileName);
                return users.FirstOrDefault(u => u._userId == user._userId) ?? user;
            }
            return user;
        }
    }
}