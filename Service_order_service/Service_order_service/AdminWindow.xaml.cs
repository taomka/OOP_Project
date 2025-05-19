using System.Windows;
using Service_order_service.Helpers;

namespace Service_order_service
{
    public partial class AdminWindow : Window
    {
        private readonly Admin _admin;
        public AdminWindow(Admin admin)
        {
            InitializeComponent();
            _admin = (Admin)UserHelper.ReloadUser(admin);
            LoadUsers();
            LoadOrders();
            AvatarHelper.LoadAvatarImage(AvatarImage, _admin.AvatarPath);
        }

        private void Button_MyProfile(object sender, RoutedEventArgs e)
        {
            MyProfileWindow myProfileWindow = new(_admin);
            myProfileWindow.Show();
            this.Close();
        }

        private void LoadUsers()
        {
            CustomersDataGrid.ItemsSource = JsonStorageService.LoadFromFile<Customer>(_admin.GetUserFileName().Replace(
                "F:\\Documents\\Програмирование\\Лабораторные универа\\2 курс\\2 семестр\\OOP_Project\\Service_order_service\\Service_order_service\\JsonFiles\\admins.json",
                "F:\\Documents\\Програмирование\\Лабораторные универа\\2 курс\\2 семестр\\OOP_Project\\Service_order_service\\Service_order_service\\JsonFiles\\customers.json"));
            SpecialistsDataGrid.ItemsSource = JsonStorageService.LoadFromFile<Specialist>(_admin.GetUserFileName().Replace(
                "F:\\Documents\\Програмирование\\Лабораторные универа\\2 курс\\2 семестр\\OOP_Project\\Service_order_service\\Service_order_service\\JsonFiles\\admins.json",
                "F:\\Documents\\Програмирование\\Лабораторные универа\\2 курс\\2 семестр\\OOP_Project\\Service_order_service\\Service_order_service\\JsonFiles\\specialists.json"));
        }

        private void LoadOrders()
        {
            OrdersDataGrid.ItemsSource = JsonStorageService.LoadFromFile<Order>(
                "F:\\Documents\\Програмирование\\Лабораторные универа\\2 курс\\2 семестр\\OOP_Project\\Service_order_service\\Service_order_service\\JsonFiles\\orders.json");
        }

        private void BlockCustomer_Click(object sender, RoutedEventArgs e)
        {
            if (CustomersDataGrid.SelectedItem is Customer customer)
            {
                Admin.BlockUser(customer.UserId);
                MessageBox.Show("Customer blocked.");
            }
        }

        private void UnblockCustomer_Click(object sender, RoutedEventArgs e)
        {
            if (CustomersDataGrid.SelectedItem is Customer customer)
            {
                Admin.UnblockUser(customer.UserId);
                MessageBox.Show("Customer unblocked.");
            }
        }

        private void AddCustomer_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string name = Microsoft.VisualBasic.Interaction.InputBox("Enter name:", "Add Customer");
                string surname = Microsoft.VisualBasic.Interaction.InputBox("Enter surname:", "Add Customer");
                string email = Microsoft.VisualBasic.Interaction.InputBox("Enter email:", "Add Customer");
                string dobStr = Microsoft.VisualBasic.Interaction.InputBox("Enter date of birth (yyyy-MM-dd):", "Add Customer");
                string password = Microsoft.VisualBasic.Interaction.InputBox("Enter password (min 6 chars):", "Add Customer");
                string phone = Microsoft.VisualBasic.Interaction.InputBox("Enter phone (+38(XXX)XXXXXXX):", "Add Customer");
                string balanceStr = Microsoft.VisualBasic.Interaction.InputBox("Enter balance:", "Add Customer");

                if (string.IsNullOrWhiteSpace(name) || string.IsNullOrWhiteSpace(surname) ||
                    string.IsNullOrWhiteSpace(email) || string.IsNullOrWhiteSpace(dobStr) ||
                    string.IsNullOrWhiteSpace(password) || string.IsNullOrWhiteSpace(phone) ||
                    string.IsNullOrWhiteSpace(balanceStr))
                {
                    MessageBox.Show("All fields are required.");
                    return;
                }

                DateTime dob = DateTime.Parse(dobStr);
                double balance = double.Parse(balanceStr);

                var customers = JsonStorageService.LoadFromFile<Customer>(new Customer().GetUserFileName());
                int newUserId = customers.Count != 0 ? customers.Max(c => c.UserId) + 1 : 1;

                var newCustomer = new Customer(newUserId, name, surname, email, dob, password, phone, balance);
                _admin.AddUser(newCustomer);
                LoadUsers();
                MessageBox.Show("Customer added successfully.");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error adding customer: " + ex.Message);
            }
        }

        private void UpdateCustomer_Click(object sender, RoutedEventArgs e)
        {
            if (CustomersDataGrid.SelectedItem is not Customer customer)
            {
                MessageBox.Show("Please select a customer to update.");
                return;
            }

            try
            {
                string newPassword = Microsoft.VisualBasic.Interaction.InputBox("Enter new password (min 6 chars, leave empty to keep current):", "Update Customer");
                string newPhone = Microsoft.VisualBasic.Interaction.InputBox("Enter new phone (+38(XXX)XXXXXXX, leave empty to keep current):", "Update Customer");

                if (!string.IsNullOrWhiteSpace(newPassword) && newPassword.Length < 6)
                {
                    MessageBox.Show("Password must be at least 6 characters.");
                    return;
                }

                if (!string.IsNullOrWhiteSpace(newPhone))
                {
                    string pattern = @"^\+38\(\d{3}\)\d{7}$";
                    if (!System.Text.RegularExpressions.Regex.IsMatch(newPhone, pattern))
                    {
                        MessageBox.Show("Phone number must be in the format +38(XXX)XXXXXXX");
                        return;
                    }
                }

                _admin.UpdateCustomer(customer.UserId, string.IsNullOrWhiteSpace(newPassword) ? null : newPassword, string.IsNullOrWhiteSpace(newPhone) ? null : newPhone);
                LoadUsers();
                MessageBox.Show("Customer updated successfully.");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error updating customer: " + ex.Message);
            }
        }

        private void DeleteCustomer_Click(object sender, RoutedEventArgs e)
        {
            if (CustomersDataGrid.SelectedItem is Customer customer)
            {
                _admin.RemoveUser(customer);
                LoadUsers();
                MessageBox.Show("Customer deleted successfully.");
            }
        }

        private void BlockSpecialist_Click(object sender, RoutedEventArgs e)
        {
            if (SpecialistsDataGrid.SelectedItem is Specialist specialist)
            {
                Admin.BlockUser(specialist.UserId);
                MessageBox.Show("Specialist blocked.");
            }
        }

        private void UnblockSpecialist_Click(object sender, RoutedEventArgs e)
        {
            if (SpecialistsDataGrid.SelectedItem is Specialist specialist)
            {
                Admin.UnblockUser(specialist.UserId);
                MessageBox.Show("Specialist unblocked.");
            }
        }

        private void AddSpecialist_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string name = Microsoft.VisualBasic.Interaction.InputBox("Enter name:", "Add Specialist");
                string surname = Microsoft.VisualBasic.Interaction.InputBox("Enter surname:", "Add Specialist");
                string email = Microsoft.VisualBasic.Interaction.InputBox("Enter email:", "Add Specialist");
                string dobStr = Microsoft.VisualBasic.Interaction.InputBox("Enter date of birth (yyyy-MM-dd):", "Add Specialist");
                string password = Microsoft.VisualBasic.Interaction.InputBox("Enter password (min 6 chars):", "Add Specialist");
                string phone = Microsoft.VisualBasic.Interaction.InputBox("Enter phone (+38(XXX)XXXXXXX):", "Add Specialist");
                string balanceStr = Microsoft.VisualBasic.Interaction.InputBox("Enter balance:", "Add Specialist");

                if (string.IsNullOrWhiteSpace(name) || string.IsNullOrWhiteSpace(surname) ||
                    string.IsNullOrWhiteSpace(email) || string.IsNullOrWhiteSpace(dobStr) ||
                    string.IsNullOrWhiteSpace(password) || string.IsNullOrWhiteSpace(phone) ||
                    string.IsNullOrWhiteSpace(balanceStr))
                {
                    MessageBox.Show("All fields are required.");
                    return;
                }

                DateTime dob = DateTime.Parse(dobStr);
                double balance = double.Parse(balanceStr);

                var specialists = JsonStorageService.LoadFromFile<Specialist>(new Specialist().GetUserFileName());
                int newUserId = specialists.Count != 0 ? specialists.Max(s => s.UserId) + 1 : 1;

                var newSpecialist = new Specialist(newUserId, name, surname, email, dob, password, phone, balance);
                _admin.AddUser(newSpecialist);
                LoadUsers();
                MessageBox.Show("Specialist added successfully.");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error adding specialist: " + ex.Message);
            }
        }

        private void UpdateSpecialist_Click(object sender, RoutedEventArgs e)
        {
            if (SpecialistsDataGrid.SelectedItem is not Specialist specialist)
            {
                MessageBox.Show("Please select a specialist to update.");
                return;
            }

            try
            {
                string newPassword = Microsoft.VisualBasic.Interaction.InputBox("Enter new password (min 6 chars, leave empty to keep current):", "Update Specialist");
                string newPhone = Microsoft.VisualBasic.Interaction.InputBox("Enter new phone (+38(XXX)XXXXXXX, leave empty to keep current):", "Update Specialist");

                if (!string.IsNullOrWhiteSpace(newPassword) && newPassword.Length < 6)
                {
                    MessageBox.Show("Password must be at least 6 characters.");
                    return;
                }

                if (!string.IsNullOrWhiteSpace(newPhone))
                {
                    string pattern = @"^\+38\(\d{3}\)\d{7}$";
                    if (!System.Text.RegularExpressions.Regex.IsMatch(newPhone, pattern))
                    {
                        MessageBox.Show("Phone number must be in the format +38(XXX)XXXXXXX");
                        return;
                    }
                }

                _admin.UpdateSpecialist(specialist.UserId, string.IsNullOrWhiteSpace(newPassword) ? null : newPassword, string.IsNullOrWhiteSpace(newPhone) ? null : newPhone);
                LoadUsers();
                MessageBox.Show("Specialist updated successfully.");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error updating specialist: " + ex.Message);
            }
        }

        private void DeleteSpecialist_Click(object sender, RoutedEventArgs e)
        {
            if (SpecialistsDataGrid.SelectedItem is Specialist specialist)
            {
                _admin.RemoveUser(specialist);
                LoadUsers();
                MessageBox.Show("Specialist deleted successfully.");
            }
        }

        private void EditOrder_Click(object sender, RoutedEventArgs e)
        {
            if (OrdersDataGrid.SelectedItem is not Order order)
            {
                MessageBox.Show("Please select an order to edit.");
                return;
            }

            try
            {
                string newDescription = Microsoft.VisualBasic.Interaction.InputBox(
                    "Enter new description (leave empty to keep current):", "Edit Order", order.Description ?? "");
                string newPriceStr = Microsoft.VisualBasic.Interaction.InputBox(
                    "Enter new price (leave empty to keep current):", "Edit Order", order.Price.ToString());

                string? descriptionToSet = string.IsNullOrWhiteSpace(newDescription) ? order.Description : newDescription;
                double priceToSet = order.Price;
                if (!string.IsNullOrWhiteSpace(newPriceStr))
                {
                    if (!double.TryParse(newPriceStr, out priceToSet) || priceToSet < 0)
                    {
                        MessageBox.Show("Invalid price value.");
                        return;
                    }
                }

                order.Edit(descriptionToSet ?? string.Empty, priceToSet);
                LoadOrders();
                MessageBox.Show("Order updated successfully.");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error editing order: " + ex.Message);
            }
        }

        private void DeleteOrder_Click(object sender, RoutedEventArgs e)
        {
            if (OrdersDataGrid.SelectedItem is Order order)
            {
                Admin.DeleteOrder(order.OrderId);
                MessageBox.Show("Order deleted successfully.");
                LoadOrders();
            }
        }
    }
}
