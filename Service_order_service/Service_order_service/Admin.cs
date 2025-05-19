namespace Service_order_service
{
    public class Admin : User, IUserManagement
    {
        public override string GetUserFileName() => "F:\\Documents\\Програмирование\\Лабораторные универа\\2 курс\\2 семестр\\OOP_Project\\Service_order_service\\Service_order_service\\JsonFiles\\admins.json";

        public Admin() { }

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

        public static void BlockUser(int userId)
        {
            var blockedUsers = JsonStorageService.LoadFromFile<int>("F:\\Documents\\Програмирование\\Лабораторные универа\\2 курс\\2 семестр\\OOP_Project\\Service_order_service\\Service_order_service\\JsonFiles\\blocked_users.json");
            if (!blockedUsers.Contains(userId))
            {
                blockedUsers.Add(userId);
                JsonStorageService.SaveToFile("F:\\Documents\\Програмирование\\Лабораторные универа\\2 курс\\2 семестр\\OOP_Project\\Service_order_service\\Service_order_service\\JsonFiles\\blocked_users.json", blockedUsers);
            }
        }

        public static void UnblockUser(int userId)
        {
            var blockedUsers = JsonStorageService.LoadFromFile<int>("F:\\Documents\\Програмирование\\Лабораторные универа\\2 курс\\2 семестр\\OOP_Project\\Service_order_service\\Service_order_service\\JsonFiles\\blocked_users.json");
            if (blockedUsers.Remove(userId))
            {
                JsonStorageService.SaveToFile("F:\\Documents\\Програмирование\\Лабораторные универа\\2 курс\\2 семестр\\OOP_Project\\Service_order_service\\Service_order_service\\JsonFiles\\blocked_users.json", blockedUsers);
            }
        }

        public static bool IsUserBlocked(int userId)
        {
            var blockedUsers = JsonStorageService.LoadFromFile<int>("F:\\Documents\\Програмирование\\Лабораторные универа\\2 курс\\2 семестр\\OOP_Project\\Service_order_service\\Service_order_service\\JsonFiles\\blocked_users.json");
            return blockedUsers.Contains(userId);
        }

        public static void DeleteOrder(int orderId)
        {
            var orders = JsonStorageService.LoadFromFile<Order>("F:\\Documents\\Програмирование\\Лабораторные универа\\2 курс\\2 семестр\\OOP_Project\\Service_order_service\\Service_order_service\\JsonFiles\\orders.json");
            var orderToRemove = orders.FirstOrDefault(o => o.OrderId == orderId) ?? throw new InvalidOperationException($"Order with ID {orderId} not found.");
            orders.Remove(orderToRemove);
            JsonStorageService.SaveToFile("F:\\Documents\\Програмирование\\Лабораторные универа\\2 курс\\2 семестр\\OOP_Project\\Service_order_service\\Service_order_service\\JsonFiles\\orders.json", orders);
        }

        public static bool IsOrderDeleted(int orderId)
        {
            var orders = JsonStorageService.LoadFromFile<Order>("F:\\Documents\\Програмирование\\Лабораторные универа\\2 курс\\2 семестр\\OOP_Project\\Service_order_service\\Service_order_service\\JsonFiles\\orders.json");
            return !orders.Any(o => o.OrderId == orderId);
        }

        public void AddUser(User user)
        {
            ArgumentNullException.ThrowIfNull(user);

            if (user.UserId == 0)
                user.UserId = user._userId;

            if (user is Customer customer)
            {
                var customers = JsonStorageService.LoadFromFile<Customer>(customer.GetUserFileName());
                if (customers.Any(u => u.UserId == customer.UserId))
                    throw new InvalidOperationException($"User with ID {user.UserId} already exists.");
                customers.Add(customer);
                JsonStorageService.SaveToFile(user.GetUserFileName(), customers);
            }

            if (user is Specialist specialist)
            {
                var specialists = JsonStorageService.LoadFromFile<Specialist>(specialist.GetUserFileName());
                if (specialists.Any(u => u.UserId == specialist.UserId))
                    throw new InvalidOperationException($"User with ID {user.UserId} already exists.");
                specialists.Add(specialist);
                JsonStorageService.SaveToFile(user.GetUserFileName(), specialists);
            }
        }

        public void RemoveUser(User user)
        {
            ArgumentNullException.ThrowIfNull(user);

            if (user is Customer customer)
            {
                var customers = JsonStorageService.LoadFromFile<Customer>(customer.GetUserFileName());
                var existing = customers.FirstOrDefault(u => u._userId == customer._userId);
                if (existing != null)
                {
                    customers.Remove(existing);
                    JsonStorageService.SaveToFile(user.GetUserFileName(), customers);
                }
            }
            if (user is Specialist specialist)
            {
                var specialists = JsonStorageService.LoadFromFile<Specialist>(specialist.GetUserFileName());
                var existing = specialists.FirstOrDefault(u => u._userId == specialist._userId);
                if (existing != null)
                {
                    specialists.Remove(existing);
                    JsonStorageService.SaveToFile(user.GetUserFileName(), specialists);
                }
            }
        }

        public void UpdateCustomer(int userId, string? newPassword = null, string? newPhoneNumber = null)
        {
            const string file = "F:\\Documents\\Програмирование\\Лабораторные универа\\2 курс\\2 семестр\\OOP_Project\\Service_order_service\\Service_order_service\\JsonFiles\\customers.json";

            var customers = JsonStorageService.LoadFromFile<Customer>(file);
            var existingCustomer = customers.FirstOrDefault(c => c.UserId == userId);
            if (existingCustomer != null)
            {
                if (!string.IsNullOrWhiteSpace(newPassword))
                    existingCustomer.Password = newPassword;

                if (!string.IsNullOrWhiteSpace(newPhoneNumber))
                    existingCustomer.PhoneNumber = newPhoneNumber;

                JsonStorageService.SaveToFile(file, customers);
            }
        }

        public void UpdateSpecialist(int userId, string? newPassword = null, string? newPhoneNumber = null)
        {
            const string file = "F:\\Documents\\Програмирование\\Лабораторные универа\\2 курс\\2 семестр\\OOP_Project\\Service_order_service\\Service_order_service\\JsonFiles\\specialists.json";

            var specialists = JsonStorageService.LoadFromFile<Specialist>(file);
            var existingSpecialist = specialists.FirstOrDefault(s => s.UserId == userId);
            if (existingSpecialist != null)
            {
                if (!string.IsNullOrWhiteSpace(newPassword))
                    existingSpecialist.Password = newPassword;

                if (!string.IsNullOrWhiteSpace(newPhoneNumber))
                    existingSpecialist.PhoneNumber = newPhoneNumber;

                JsonStorageService.SaveToFile(file, specialists);
            }
        }
    }
}
