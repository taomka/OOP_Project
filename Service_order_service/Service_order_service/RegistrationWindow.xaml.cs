using System.Windows;
using System.Windows.Controls;

namespace Service_order_service
{
    public partial class RegistrationWindow : Window
    {
        public RegistrationWindow()
        {
            InitializeComponent();
        }

        private void Register_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string name = NameTextBox.Text;
                string surname = SurnameTextBox.Text;
                string email = EmailTextBox.Text;
                string password = PasswordBox.Password;
                string phone = PhoneTextBox.Text;
                DateTime? dateOfBirth = DateOfBirthPicker.SelectedDate;
                string? userType = (UserTypeComboBox.SelectedItem as ComboBoxItem)?.Content.ToString();

                if (string.IsNullOrWhiteSpace(name) || string.IsNullOrWhiteSpace(surname) ||
                    string.IsNullOrWhiteSpace(email) || string.IsNullOrWhiteSpace(password) ||
                    string.IsNullOrWhiteSpace(phone) || !dateOfBirth.HasValue || string.IsNullOrWhiteSpace(userType))
                {
                    MessageBox.Show("Please fill in all fields.", "Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                User newUser;
                if (userType == "Customer")
                    newUser = new Customer();
                else if (userType == "Specialist")
                    newUser = new Specialist();
                else
                {
                    MessageBox.Show("Unknown user type.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                newUser.Name = name;
                newUser.Surname = surname;
                newUser.Email = email;
                newUser.Password = password;
                newUser.PhoneNumber = phone;
                newUser.DateOfBirth = dateOfBirth.Value;

                newUser.Register();

                MessageBox.Show("Registration successful!", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                MainWindowService mainWindow = new (newUser);
                mainWindow.Show();
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void Back_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new ();
            mainWindow.Show();
            this.Close();
        }
    }
}
