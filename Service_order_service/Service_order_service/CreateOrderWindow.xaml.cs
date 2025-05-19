using System.Windows;
using Service_order_service.Helpers;

namespace Service_order_service
{
    public partial class CreateOrderWindow : Window
    {
        private readonly Customer _customer;

        public CreateOrderWindow(Customer customer)
        {
            InitializeComponent();
            _customer = customer;
            CategoryComboBox.ItemsSource = Enum.GetValues(typeof(ServiceCategory));
            PaymentComboBox.ItemsSource = Enum.GetValues(typeof(PaymentTerm));
            DeadlineDatePicker.SelectedDate = DateTime.Now.AddDays(1);

            _customer.OrderCreated += Customer_OrderCreated;
            AvatarHelper.LoadAvatarImage(AvatarImage, _customer.AvatarPath);
        }

        private void Customer_OrderCreated(object sender, Order order)
        {
            MessageBox.Show($"Order '{order.Title}' was created!", "Order Created", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private void Button_MyProfile(object sender, RoutedEventArgs e)
        {
            MyProfileWindow myProfileWindow = new(_customer);
            myProfileWindow.Show();
            this.Close();
        }

        private void Button_MainPage(object sender, RoutedEventArgs e)
        {
            MainWindowService mainWindow = new (_customer);
            mainWindow.Show();
            this.Close();
        }

        private void ConfirmButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string title = TitleTextBox.Text;
                var category = (ServiceCategory)CategoryComboBox.SelectedItem!;
                string description = DescriptionTextBox.Text;
                string location = LocationTextBox.Text;
                var deadline = DeadlineDatePicker.SelectedDate ?? throw new ArgumentException("Deadline is required.");
                double price = double.TryParse(PriceTextBox.Text, out var parsedPrice) ? parsedPrice : throw new ArgumentException("Invalid price.");
                var paymentTerm = (PaymentTerm)PaymentComboBox.SelectedItem!;

                var existingOrders = JsonStorageService.LoadFromFile<Order>("F:\\Documents\\Програмирование\\Лабораторные универа\\2 курс\\2 семестр\\OOP_Project\\Service_order_service\\Service_order_service\\JsonFiles\\orders.json");
                int OrderId = existingOrders.Count != 0 ? existingOrders.Max(o => o.OrderId) + 1 : 1;

                var newOrder = new Order
                {
                    OrderId = OrderId,
                    Title = title,
                    Category = category,
                    Description = description,
                    Location = location,
                    Deadline = deadline,
                    DeadlineForJson = deadline,
                    Price = price,
                    PaymentTerm = paymentTerm,
                    Status = OrderStatus.Pending,
                    Customer = _customer
                };

                _customer.CreateOrder(newOrder);
                MainWindowService mainWindow = new (_customer);
                mainWindow.Show();
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Failed to create order:\n{ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            MainWindowService mainWindow = new (_customer);
            mainWindow.Show();
            this.Close();
        }

        private void Button_ViewOrders(object sender, RoutedEventArgs e)
        {
            MyOrdersWindow myOrdersWindow = new (_customer);
            myOrdersWindow.Show();
            this.Close();
        }
    }
}
