using Service_order_service.Helpers;
using System.Windows;

namespace Service_order_service
{
    public partial class ListOrdersWindow : Window
    {
        private readonly Specialist? _specialist;
        private List<Order>? _allOrders;

        public ListOrdersWindow(Specialist specialist)
        {
            InitializeComponent();
            _specialist = specialist ?? throw new ArgumentNullException(nameof(specialist));

            LoadOrders();
            AvatarHelper.LoadAvatarImage(AvatarImage, _specialist.AvatarPath);
        }

        private void Button_MainPage(object sender, RoutedEventArgs e)
        {
            MainWindowService mainWindowService = new (_specialist!);
            mainWindowService.Show();
            this.Close();
        }

        private void Button_ViewOrders(object sender, RoutedEventArgs e)
        {
            MyOrdersWindow myOrdersWindow = new (_specialist!);
            myOrdersWindow.Show();
            this.Close();
        }

        private void AcceptOrder_Click(object sender, RoutedEventArgs e)
        {
            if (OrdersDataGrid.SelectedItem is Order selectedOrder && _specialist != null)
            {
                if (selectedOrder.Status == OrderStatus.Pending)
                {
                    _specialist.ApplyForOrder(selectedOrder);
                    SaveOrders();
                    LoadOrders();
                    MessageBox.Show("Order accepted successfully.", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                else
                {
                    MessageBox.Show("Order cannot be accepted.", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
            }
            else
            {
                MessageBox.Show("Please select an order to accept.", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        private void Button_MyProfile(object sender, RoutedEventArgs e)
        {
            MyProfileWindow myProfileWindow = new (_specialist!);
            myProfileWindow.Show();
            this.Close();
        }

        private void LoadOrders()
        {
            _allOrders = JsonStorageService.LoadFromFile<Order>(
                "F:\\Documents\\Програмирование\\Лабораторные универа\\2 курс\\2 семестр\\OOP_Project\\Service_order_service\\Service_order_service\\JsonFiles\\orders.json");

            OrdersDataGrid.ItemsSource = _allOrders;
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
    }
}
