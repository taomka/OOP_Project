using System.Windows;
using System.Windows.Controls;

namespace Service_order_service
{
    public partial class LoginWindow : Window
    {
        public LoginWindow()
        {
            InitializeComponent();
        }

        private void Login_Click(object sender, RoutedEventArgs e)
        {
            string email = EmailTextBox.Text.Trim();
            string password = PasswordBox.Password;
            string? userType = ((ComboBoxItem)UserTypeComboBox.SelectedItem).Content.ToString();

            if (string.IsNullOrWhiteSpace(email) || string.IsNullOrWhiteSpace(password))
            {
                ShowMessage("Please enter both email and password.");
                return;
            }

            User? user = userType switch
            {
                "Customer" => FindUser<Customer>(email, password),
                "Specialist" => FindUser<Specialist>(email, password),
                "Admin" => FindUser<Admin>(email, password),
                _ => null
            };

            if (user == null)
            {
                ShowMessage("Login failed. Check email and password.");
                return;
            }

            ShowMessage("Login successful!", true);
            if (user is Admin admin)
            {
                AdminWindow adminWindow = new (admin);
                adminWindow.Show();
            }
            else
            {
                MainWindowService mainWindow = new (user);
                mainWindow.Show();
            }
            this.Close();
        }

        private static T? FindUser<T>(string email, string password) where T : User
        {
            var users = JsonStorageService.LoadFromFile<T>(Activator.CreateInstance<T>().GetUserFileName());
            return users.FirstOrDefault(u => u.Email == email && u.Password == password);
        }

        private static void ShowMessage(string message, bool success = false)
        {
            _ = success ? "Green" : "Red";
            MessageBox.Show(message, "Login", MessageBoxButton.OK, success ? MessageBoxImage.Information : MessageBoxImage.Warning);
        }

        private void Back_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new ();
            mainWindow.Show();
            this.Close();
        }
    }
}
