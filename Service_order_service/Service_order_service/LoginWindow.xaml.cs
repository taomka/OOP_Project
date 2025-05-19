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
                "Customer" => new Customer(),
                "Specialist" => new Specialist(),
                "Admin" => new Admin(),
                _ => null
            };

            if (user == null)
            {
                ShowMessage("Invalid user type selected.");
                return;
            }

            bool success = user.Login(email, password);
            if (success)
            {
                ShowMessage("Login successful!", true);
                if (user is Admin admin)
                {
                    AdminWindow adminWindow = new AdminWindow(admin);
                    adminWindow.Show();
                }
                else
                {
                    MainWindowService mainWindow = new MainWindowService(user);
                    mainWindow.Show();
                }
                this.Close();
            }
            else
            {
                ShowMessage("Login failed. Check email and password.");
            }
        }

        private void ShowMessage(string message, bool success = false)
        {
            var color = success ? "Green" : "Red";
            MessageBox.Show(message, "Login", MessageBoxButton.OK, success ? MessageBoxImage.Information : MessageBoxImage.Warning);
        }

        private void Back_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
            this.Close();
        }
    }
}
