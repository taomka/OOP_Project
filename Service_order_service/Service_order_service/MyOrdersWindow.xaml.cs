using System.Windows;
using Service_order_service.Helpers;

namespace Service_order_service
{
    public partial class MyOrdersWindow : Window
    {
        private readonly User _loggedInUser;
        private readonly bool _isCustomer;
        private readonly bool _isSpecialist;
        private List<Order>? _allOrders;

        public MyOrdersWindow(Customer customer)
        {
            InitializeComponent();
            _loggedInUser = customer;
            _isCustomer = true;
            _isSpecialist = false;

            DataContext = this;
            LoadOrders();
            AvatarHelper.LoadAvatarImage(AvatarImage, _loggedInUser.AvatarPath);
        }

        public MyOrdersWindow(Specialist specialist)
        {
            InitializeComponent();
            _loggedInUser = specialist;
            _isCustomer = false;
            _isSpecialist = true;

            DataContext = this;
            LoadOrders();
            AvatarHelper.LoadAvatarImage(AvatarImage, _loggedInUser.AvatarPath);
        }

        private void Button_MainPage(object sender, RoutedEventArgs e)
        {
            if (_isCustomer && _loggedInUser is Customer customer)
            {
                MainWindowService mainWindow = new (customer);
                mainWindow.Show();
                this.Close();
            }
            else if (_isSpecialist && _loggedInUser is Specialist specialist)
            {
                MainWindowService mainWindow = new (specialist);
                mainWindow.Show();
                this.Close();
            }
        }

        private void Button_CreateOrder(object sender, RoutedEventArgs e)
        {
            if (_isCustomer && _loggedInUser is Customer customer)
            {
                CreateOrderWindow createOrderWindow = new (customer);
                createOrderWindow.Show();
                this.Close();
            }
        }

        private void Button_MyProfile(object sender, RoutedEventArgs e)
        {
            MyProfileWindow myProfileWindow = new (_loggedInUser);
            myProfileWindow.Show();
            this.Close();
        }

        private void Button_ViewOrders(object sender, RoutedEventArgs e)
        {
            LoadOrders();
        }

        private bool CanCancelOrder(Order? order)
        {
            return _isCustomer && order != null && order.Status == OrderStatus.Pending;
        }

        private bool CanRateSpecialist(Order? order)
        {
            return _isCustomer && order != null && order.Status == OrderStatus.Completed && order.Specialist != null;
        }

        private void CancelOrder_Click(object sender, RoutedEventArgs e)
        {
            if (_loggedInUser is Specialist)
            {
                MessageBox.Show("Specialists cannot cancel orders.", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            if (OrdersDataGrid.SelectedItem is Order selectedOrder)
            {
                if (CanCancelOrder(selectedOrder))
                {
                    CancelOrder(selectedOrder);
                }
                else
                {
                    MessageBox.Show("You cannot cancel this order.", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
            }
            else
            {
                MessageBox.Show("Please select an order to cancel.", "Info", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        private void RateSpecialist_Click(object sender, RoutedEventArgs e)
        {
            if (_loggedInUser is Specialist)
            {
                MessageBox.Show("Specialists cannot rate specialists.", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            if (OrdersDataGrid.SelectedItem is Order selectedOrder)
            {
                if (CanRateSpecialist(selectedOrder))
                {
                    RateSpecialist(selectedOrder);
                }
                else
                {
                    MessageBox.Show("You cannot rate the specialist for this order.", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
            }
            else
            {
                MessageBox.Show("Please select an order to rate.", "Info", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        private void CancelOrder(Order order)
        {
            try
            {
                if (_isCustomer && _loggedInUser is Customer customer)
                {
                    customer.CancelOrder(order);
                    MessageBox.Show("Order canceled successfully.", "Info", MessageBoxButton.OK, MessageBoxImage.Information);
                    LoadOrders();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void RateSpecialist(Order order)
        {
            try
            {
                if (_isCustomer && _loggedInUser is Customer customer)
                {
                    var ratingWindow = new Dialogs.RateSpecialistDialog();
                    if (ratingWindow.ShowDialog() == true)
                    {
                        int ratingValue = ratingWindow.RatingValue;
                        string? comment = ratingWindow.Comment;
                        customer.RateSpecialist(order, ratingValue, comment);
                        MessageBox.Show("Specialist rated successfully.", "Info", MessageBoxButton.OK, MessageBoxImage.Information);
                        LoadOrders();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void LoadOrders()
        {
            _allOrders = JsonStorageService.LoadFromFile<Order>(
                "F:\\Documents\\Програмирование\\Лабораторные универа\\2 курс\\2 семестр\\OOP_Project\\Service_order_service\\Service_order_service\\JsonFiles\\orders.json");

            List<Order> myOrders;
            if (_isCustomer && _loggedInUser is Customer customer)
            {
                myOrders = _allOrders.Where(o => o.Customer != null && o.Customer.UserId == customer.UserId).ToList();
            }
            else if (_isSpecialist && _loggedInUser is Specialist specialist)
            {
                myOrders = _allOrders.Where(o => o.Specialist != null && o.Specialist.UserId == specialist.UserId).ToList();
            }
            else
            {
                myOrders = [];
            }

            OrdersDataGrid.ItemsSource = myOrders;
        }

        private void SaveOrders()
        {
            if (_allOrders != null)
            {
                JsonStorageService.SaveToFile(
                    "F:\\Documents\\Програмирование\\Лабораторные универа\\2 курс\\2 семестр\\OOP_Project\\Service_order_service\\Service_order_service\\JsonFiles\\orders.json",
                    _allOrders);
            }
        }

        private void CompleteOrder_Click(object sender, RoutedEventArgs e)
        {
            if (_isSpecialist && _loggedInUser is Specialist specialist)
            {
                if (OrdersDataGrid.SelectedItem is Order selectedOrder)
                {
                    if (selectedOrder.Status == OrderStatus.InProgress && selectedOrder.Specialist?.Email == specialist.Email)
                    {
                        try
                        {
                            specialist.CompleteOrder(selectedOrder);
                            SaveOrders();
                            LoadOrders();
                            MessageBox.Show("Order completed.", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show($"Error: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                        }
                    }
                    else
                    {
                        MessageBox.Show("Order cannot be completed.", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
                    }
                }
                else
                {
                    MessageBox.Show("Please select an order to complete.", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
            }
            else
            {
                MessageBox.Show("Only specialists can complete orders.", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }
    }
}
