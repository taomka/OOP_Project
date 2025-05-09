namespace Service_order_service
{
    public class Admin : User, IUserManagement
    {
        public override string GetUserFileName() => "admins.json";

        public Admin(int userId, string name, string surname, string email, DateTime dateOfBirth, string password, string phoneNumber, double balance)
        {
            _userId = userId;
            Name = name;
            Surname = surname;
            Email = email;
            DateOfBirth = dateOfBirth;
            Password = password;
            PhoneNumber = phoneNumber;
            Balance = balance;
        }

        public void BlockUser(int userId)
        {
            var blockedUsers = JsonStorageService.LoadFromFile<int>("blocked_users.json");
            if (!blockedUsers.Contains(userId))
            {
                blockedUsers.Add(userId);
                JsonStorageService.SaveToFile("blocked_users.json", blockedUsers);
            }
        }

        public void UnblockUser(int userId)
        {
            var blockedUsers = JsonStorageService.LoadFromFile<int>("blocked_users.json");
            if (blockedUsers.Contains(userId))
            {
                blockedUsers.Remove(userId);
                JsonStorageService.SaveToFile("blocked_users.json", blockedUsers);
            }
        }

        public bool IsUserBlocked(int userId)
        {
            var blockedUsers = JsonStorageService.LoadFromFile<int>("blocked_users.json");
            return blockedUsers.Contains(userId);
        }

        public void DeleteOrder(int orderId)
        {
            var orders = JsonStorageService.LoadFromFile<Order>("orders.json");
            var orderToRemove = orders.FirstOrDefault(o => o.OrderId == orderId);
            if (orderToRemove == null)
                throw new InvalidOperationException($"Order with ID {orderId} not found.");

            orders.Remove(orderToRemove);
            JsonStorageService.SaveToFile("orders.json", orders);
        }

        public bool IsOrderDeleted(int orderId)
        {
            var orders = JsonStorageService.LoadFromFile<Order>("orders.json");
            return !orders.Any(o => o.OrderId == orderId);
        }

        public void AddUser(User user)
        {
            if (user == null)
                throw new ArgumentNullException(nameof(user));

            var users = JsonStorageService.LoadFromFile<User>(user.GetUserFileName());
            users.Add(user);
            JsonStorageService.SaveToFile(user.GetUserFileName(), users);
        }

        public void RemoveUser(User user)
        {
            if (user == null)
                throw new ArgumentNullException(nameof(user));

            var users = JsonStorageService.LoadFromFile<User>(user.GetUserFileName());
            var existing = users.FirstOrDefault(u => u._userId == user._userId);
            if (existing != null)
            {
                users.Remove(existing);
                JsonStorageService.SaveToFile(user.GetUserFileName(), users);
            }
        }

        public void UpdateUser(int userId, string? newPassword = null, string? newPhoneNumber = null)
        {
            var files = new[] { "customers.json", "specialists.json", "admins.json" };

            foreach (var file in files)
            {
                var users = JsonStorageService.LoadFromFile<User>(file);
                var existingUser = users.FirstOrDefault(u => u._userId == userId);
                if (existingUser != null)
                {
                    if (!string.IsNullOrWhiteSpace(newPassword))
                        existingUser.Password = newPassword;

                    if (!string.IsNullOrWhiteSpace(newPhoneNumber))
                        existingUser.PhoneNumber = newPhoneNumber;

                    JsonStorageService.SaveToFile(file, users);
                    break;
                }
            }
        }
    }
}
