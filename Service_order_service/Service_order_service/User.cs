using System.IO;
using System.Text.RegularExpressions;

namespace Service_order_service
{
    public abstract class User
    {
        public int UserId
        {
            get => _userId;
            set => _userId = value;
        }

        public int _userId;
        public string? _name;
        public string? _surname;
        public string? _email;
        public DateTime _dateOfBirth;
        public string? _password;
        public string? _phoneNumber;
        public double _balance;

        public string? AvatarPath { get; set; }

        public abstract string GetUserFileName();

        public string? Name
        {
            get => _name;
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                    throw new ArgumentException("Name cannot be empty.");
                _name = value;
            }
        }

        public string? Surname
        {
            get => _surname;
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                    throw new ArgumentException("Surname cannot be empty.");
                _surname = value;
            }
        }

        public string? Email
        {
            get => _email;
            set
            {
                if (string.IsNullOrWhiteSpace(value) || !value.Contains('@'))
                    throw new ArgumentException("Invalid email.");
                _email = value;
            }
        }

        public DateTime DateOfBirth
        {
            get => _dateOfBirth;
            set
            {
                if (value > DateTime.Now.AddYears(-16))
                    throw new ArgumentException("User must be at least 16 years old.");
                _dateOfBirth = value;
            }
        }

        public string? Password
        {
            get => _password;
            set
            {
                if (string.IsNullOrWhiteSpace(value) || value.Length < 6)
                    throw new ArgumentException("Password must be at least 6 characters.");
                _password = value;
            }
        }

        public string? PhoneNumber
        {
            get => _phoneNumber;
            set
            {
                if (string.IsNullOrWhiteSpace(value) || value.Length < 10)
                    throw new ArgumentException("Invalid phone number.");
                string pattern = @"^\+38\(\d{3}\)\d{7}$";
                if (!Regex.IsMatch(value, pattern))
                    throw new ArgumentException("Phone number must be in the format +38(XXX)XXXXXXX");

                _phoneNumber = value;
            }
        }

        public double Balance
        {
            get => _balance;
            set
            {
                if (value < 0)
                    throw new ArgumentException("Balance cannot be negative.");
                _balance = value;
            }
        }

        public virtual bool Login(string email, string password, string filePath = "")
        {
            var users = this switch
            {
                Customer => JsonStorageService.LoadFromFile<Customer>("F:\\Documents\\Програмирование\\Лабораторные универа\\2 курс\\2 семестр\\OOP_Project\\Service_order_service\\Service_order_service\\JsonFiles\\customers.json").Cast<User>(),
                Specialist => JsonStorageService.LoadFromFile<Specialist>("F:\\Documents\\Програмирование\\Лабораторные универа\\2 курс\\2 семестр\\OOP_Project\\Service_order_service\\Service_order_service\\JsonFiles\\specialists.json").Cast<User>(),
                Admin => JsonStorageService.LoadFromFile<Admin>("F:\\Documents\\Програмирование\\Лабораторные универа\\2 курс\\2 семестр\\OOP_Project\\Service_order_service\\Service_order_service\\JsonFiles\\admins.json").Cast<User>(),
                _ => throw new InvalidOperationException("Unknown user type")
            };

            return users.Any(u => u.Email == email && u.Password == password);
        }

        public virtual void Register()
        {
            var users = this switch
            {
                Customer => JsonStorageService.LoadFromFile<Customer>("F:\\Documents\\Програмирование\\Лабораторные универа\\2 курс\\2 семестр\\OOP_Project\\Service_order_service\\Service_order_service\\JsonFiles\\customers.json").Cast<User>().ToList(),
                Specialist => JsonStorageService.LoadFromFile<Specialist>("F:\\Documents\\Програмирование\\Лабораторные универа\\2 курс\\2 семестр\\OOP_Project\\Service_order_service\\Service_order_service\\JsonFiles\\specialists.json").Cast<User>().ToList(),
                Admin => JsonStorageService.LoadFromFile<Admin>("F:\\Documents\\Програмирование\\Лабораторные универа\\2 курс\\2 семестр\\OOP_Project\\Service_order_service\\Service_order_service\\JsonFiles\\admins.json").Cast<User>().ToList(),
                _ => throw new InvalidOperationException("Unknown user type")
            };

            _userId = users.Count != 0 ? users.Max(u => u._userId) + 1 : 1;
            users.Add(this);

            var fileName = GetUserFileName();
            if (this is Customer)
                JsonStorageService.SaveToFile(fileName, users.Cast<Customer>().ToList());
            else if (this is Specialist)
                JsonStorageService.SaveToFile(fileName, users.Cast<Specialist>().ToList());
            else if (this is Admin)
                JsonStorageService.SaveToFile(fileName, users.Cast<Admin>().ToList());
        }

        public virtual void AddFunds(double amount)
        {
            if (amount <= 0)
                throw new ArgumentException("Amount must be greater than zero.");

            Balance += amount;

            var fileName = GetUserFileName();

            if (this is Customer)
            {
                var users = JsonStorageService.LoadFromFile<Customer>(fileName);
                var user = users.FirstOrDefault(u => u._userId == this._userId);
                if (user != null)
                {
                    user.Balance = this.Balance;
                    JsonStorageService.SaveToFile(fileName, users);
                }
            }
            else if (this is Specialist)
            {
                var users = JsonStorageService.LoadFromFile<Specialist>(fileName);
                var user = users.FirstOrDefault(u => u._userId == this._userId);
                if (user != null)
                {
                    user.Balance = this.Balance;
                    JsonStorageService.SaveToFile(fileName, users);
                }
            }
            else if (this is Admin)
            {
                var users = JsonStorageService.LoadFromFile<Admin>(fileName);
                var user = users.FirstOrDefault(u => u._userId == this._userId);
                if (user != null)
                {
                    user.Balance = this.Balance;
                    JsonStorageService.SaveToFile(fileName, users);
                }
            }
        }

        public virtual void SetAvatarPath(string path)
        {
            if (string.IsNullOrWhiteSpace(path) || !File.Exists(path))
                throw new ArgumentException("Invalid path to avatar.");

            AvatarPath = path;

            var fileName = GetUserFileName();

            if (this is Customer)
            {
                var users = JsonStorageService.LoadFromFile<Customer>(fileName);
                var user = users.FirstOrDefault(u => u._userId == this._userId);
                if (user != null)
                {
                    user.AvatarPath = this.AvatarPath;
                    JsonStorageService.SaveToFile(fileName, users);
                }
            }
            else if (this is Specialist)
            {
                var users = JsonStorageService.LoadFromFile<Specialist>(fileName);
                var user = users.FirstOrDefault(u => u._userId == this._userId);
                if (user != null)
                {
                    user.AvatarPath = this.AvatarPath;
                    JsonStorageService.SaveToFile(fileName, users);
                }
            }
            else if (this is Admin)
            {
                var users = JsonStorageService.LoadFromFile<Admin>(fileName);
                var user = users.FirstOrDefault(u => u._userId == this._userId);
                if (user != null)
                {
                    user.AvatarPath = this.AvatarPath;
                    JsonStorageService.SaveToFile(fileName, users);
                }
            }
        }
    }
}
