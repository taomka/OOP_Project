using Microsoft.Win32;
using System.Windows;
using Service_order_service.Helpers;

namespace Service_order_service
{
    public partial class MyProfileWindow : Window
    {
        private readonly User? _loggedInUser;
        public MyProfileWindow(User user)
        {
            InitializeComponent();
            _loggedInUser = UserHelper.ReloadUser(user);
            if (_loggedInUser is Customer)
            {
                CreateOrderButton.Visibility = Visibility.Visible;
                ViewOrdersButton.Visibility = Visibility.Visible;
            }
            else if (_loggedInUser is Specialist)
            {
                CreateOrderButton.Visibility = Visibility.Collapsed;
                ViewOrdersButton.Visibility = Visibility.Visible;
            }
            else if (_loggedInUser is Admin)
            {
                ViewOrdersButton.Visibility = Visibility.Collapsed;
            }
            UpdateBalanceText();
            AvatarHelper.LoadAvatarImage(AvatarImage, _loggedInUser.AvatarPath);
        }

        private void Button_CreateOrder(object sender, RoutedEventArgs e)
        {
            if (_loggedInUser is Customer customer)
            {
                CreateOrderWindow createOrderWindow = new (customer);
                createOrderWindow.Show();
                this.Close();
            }
        }

        private void Button_MainPage(object sender, RoutedEventArgs e)
        {
            if (_loggedInUser is Customer customer)
            {
                MainWindowService MainWindowService = new (customer);
                MainWindowService.Show();
                this.Close();
            }
            else if (_loggedInUser is Specialist specialist)
            {
                MainWindowService MainWindowService = new (specialist);
                MainWindowService.Show();
                this.Close();
            }
            else if (_loggedInUser is Admin admin)
            {
                AdminWindow adminWindow = new (admin);
                adminWindow.Show();
                this.Close();
            }
        }

        private void Button_MyProfile(object sender, RoutedEventArgs e)
        {

        }

        private void Button_Recharge(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(TopUpAmountTextBox.Text))
            {
                MessageBox.Show("Please enter an amount to top up.");
                return;
            }

            if (!double.TryParse(TopUpAmountTextBox.Text, out double amount))
            {
                MessageBox.Show("Invalid amount format.");
                return;
            }

            try
            {
                _loggedInUser?.AddFunds(amount);
                MessageBox.Show("Balance successfully updated!", "Success", MessageBoxButton.OK, MessageBoxImage.Information);

                var updatedUser = _loggedInUser != null ? UserHelper.ReloadUser(_loggedInUser) : null;
                if (updatedUser != null)
                {
                    UpdateBalanceText(updatedUser.Balance);
                }
                else
                {
                    UpdateBalanceText();
                }
                TopUpAmountTextBox.Clear();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Failed to top up balance: " + ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void UpdateBalanceText()
        {
            BalanceTextBlock.Text = $"My balance: {_loggedInUser?.Balance:F2} UAH";
        }

        private void UpdateBalanceText(double balance)
        {
            BalanceTextBlock.Text = $"My balance: {balance:F2} UAH";
        }

        private void ChoosePhotoButton_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new()
            {
                Filter = "Image Files (*.png;*.jpg;*.jpeg)|*.png;*.jpg;*.jpeg"
            };

            if (openFileDialog.ShowDialog() == true)
            {
                string path = openFileDialog.FileName;

                try
                {
                    _loggedInUser?.SetAvatarPath(path);
                    AvatarHelper.DisplayAvatar(AvatarImage, path);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error saving avatar: " + ex.Message);
                }
            }
        }

        private void Button_ChangePassword(object sender, RoutedEventArgs e)
        {
            string newPassword = NewPasswordTextBox.Text;
            string repeatPassword = RepeatPasswordTextBox.Text;

            if (string.IsNullOrWhiteSpace(newPassword) || newPassword != repeatPassword)
            {
                MessageBox.Show("Passwords do not match or are empty.");
                return;
            }

            if (_loggedInUser == null)
            {
                MessageBox.Show("User not found.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            try
            {
                _loggedInUser.Password = newPassword;
                _loggedInUser._password = newPassword;

                var fileName = _loggedInUser.GetUserFileName();
                if (_loggedInUser is Customer)
                {
                    var users = JsonStorageService.LoadFromFile<Customer>(fileName);
                    var user = users.FirstOrDefault(u => u._userId == _loggedInUser._userId);
                    if (user != null)
                    {
                        user.Password = newPassword;
                        user._password = newPassword;
                        JsonStorageService.SaveToFile(fileName, users);
                    }
                }
                else if (_loggedInUser is Specialist)
                {
                    var users = JsonStorageService.LoadFromFile<Specialist>(fileName);
                    var user = users.FirstOrDefault(u => u._userId == _loggedInUser._userId);
                    if (user != null)
                    {
                        user.Password = newPassword;
                        user._password = newPassword;
                        JsonStorageService.SaveToFile(fileName, users);
                    }
                }
                else if (_loggedInUser is Admin)
                {
                    var users = JsonStorageService.LoadFromFile<Admin>(fileName);
                    var user = users.FirstOrDefault(u => u._userId == _loggedInUser._userId);
                    if (user != null)
                    {
                        user.Password = newPassword;
                        user._password = newPassword;
                        JsonStorageService.SaveToFile(fileName, users);
                    }
                }

                MessageBox.Show("Password changed successfully.", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                NewPasswordTextBox.Clear();
                RepeatPasswordTextBox.Clear();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Failed to change password: " + ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void LogoutButton_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new ();
            mainWindow.Show();
            this.Close();
        }

        private void Button_SuggestionChosen(object sender, RoutedPropertyChangedEventArgs<object> e)
        {

        }

        private void Button_ViewOrders(object sender, RoutedEventArgs e)
        {
            if (_loggedInUser is Customer customer)
            {
                MyOrdersWindow myOrdersWindow = new (customer);
                myOrdersWindow.Show();
                this.Close();
            }
            else if (_loggedInUser is Specialist specialist)
            {
                MyOrdersWindow myOrdersWindow = new (specialist);
                myOrdersWindow.Show();
                this.Close();
            }
        }
    }
}
