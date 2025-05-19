using System.Windows;
using Service_order_service.Helpers;

namespace Service_order_service
{
    public partial class ListSpecialistsWindow : Window
    {
        private readonly User? _loggedInUser;

        public ListSpecialistsWindow(User user)
        {
            InitializeComponent();
            _loggedInUser = user;
            if (_loggedInUser is Customer)
            {
                LoadSpecialists();
                CreateOrderButton.Visibility = Visibility.Visible;
            }
            else if (_loggedInUser is Specialist)
            {
                LoadOrdersForSpecialist();
                CreateOrderButton.Visibility = Visibility.Collapsed;
            }

            AvatarHelper.LoadAvatarImage(AvatarImage, _loggedInUser.AvatarPath);
        }

        private void LoadSpecialists()
        {
            var specialists = JsonStorageService.LoadFromFile<Specialist>(
                "F:\\Documents\\Програмирование\\Лабораторные универа\\2 курс\\2 семестр\\OOP_Project\\Service_order_service\\Service_order_service\\JsonFiles\\specialists.json");

            SpecialistDataGrid.ItemsSource = specialists;
        }

        private void ViewProfile_Click(object sender, RoutedEventArgs e)
        {
            //if (sender is Button btn && btn.Tag is SpecialistViewModel specialistVm)
            //{
            //    var specialists = JsonStorageService.LoadFromFile<Specialist>(
            //        "F:\\Documents\\Програмирование\\Лабораторные универа\\2 курс\\2 семестр\\OOP_Project\\Service_order_service\\Service_order_service\\JsonFiles\\specialists.json");
            //    var specialist = specialists.FirstOrDefault(s => s.Email == specialistVm.Email);
            //    if (specialist != null)
            //    {
            //        MyProfileWindow profileWindow = new MyProfileWindow(specialist);
            //        profileWindow.Show();
            //        this.Close();
            //    }
            //}
        }

        private void LoadOrdersForSpecialist()
        {
            if (_loggedInUser is not Specialist specialist)
                return;

            var allOrders = JsonStorageService.LoadFromFile<Order>(
                "F:\\Documents\\Програмирование\\Лабораторные универа\\2 курс\\2 семестр\\OOP_Project\\Service_order_service\\Service_order_service\\JsonFiles\\orders.json");

            var myOrders = allOrders
                .Where(o => o.Specialist == null || (o.Specialist != null && o.Specialist.UserId == specialist.UserId))
                .ToList();

            SpecialistDataGrid.ItemsSource = myOrders;
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

        private void Button_MyProfile(object sender, RoutedEventArgs e)
        {
            if (_loggedInUser is not null)
            {
                MyProfileWindow myProfileWindow = new (_loggedInUser);
                myProfileWindow.Show();
                this.Close();
            }
            else
            {
                MessageBox.Show("User is not loaded.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
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
