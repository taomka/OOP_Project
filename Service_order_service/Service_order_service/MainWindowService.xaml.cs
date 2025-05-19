using MaterialDesignThemes.Wpf;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using Service_order_service.Helpers;

namespace Service_order_service
{
    public partial class MainWindowService : Window
    {
        private readonly User? _loggedInUser;
        public MainWindowService(User user)
        {
            InitializeComponent();
            _loggedInUser = UserHelper.ReloadUser(user);

            if (user is Customer)
            {
                CreateOrderButton.Visibility = Visibility.Visible;
            }
            if (user is Specialist)
            {
                FindSpecialist.Content = "Find Orders";
                HintAssist.SetHint(SearchTextBox, "Search for orders");
            }
            LoadCategories();
            AvatarHelper.LoadAvatarImage(AvatarImage, _loggedInUser.AvatarPath);
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

        private void LoadCategories()
        {
            foreach (ServiceCategory category in Enum.GetValues(typeof(ServiceCategory)))
            {
                var card = new Card
                {
                    Width = 180,
                    Height = 120,
                    Margin = new Thickness(10),
                    Padding = new Thickness(10),
                    Background = Brushes.LightGreen,
                    Content = new TextBlock
                    {
                        Text = GetCategoryDisplayName(category),
                        HorizontalAlignment = HorizontalAlignment.Center,
                        VerticalAlignment = VerticalAlignment.Center,
                        TextWrapping = TextWrapping.Wrap,
                        FontSize = 16,
                        FontWeight = FontWeights.Medium
                    }
                };
                card.MouseLeftButtonUp += CategoryCard_Click;
                CategoryPanel.Children.Add(card);
            }
        }

        private void CategoryCard_Click(object sender, MouseButtonEventArgs e)
        {
            if (sender is Card card && card.Tag is ServiceCategory)
            {
                if (_loggedInUser is not null)
                {
                    var specialistsWindow = new ListSpecialistsWindow(_loggedInUser);
                    specialistsWindow.ShowDialog();
                }
                else
                {
                    MessageBox.Show("User is not loaded.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        private static string GetCategoryDisplayName(ServiceCategory category)
        {
            return category switch
            {
                ServiceCategory.HomeRepair => "HomeRepair",
                ServiceCategory.Delivery => "Delivery",
                ServiceCategory.Cleaning => "Cleaning",
                ServiceCategory.IT => "IT",
                ServiceCategory.Design => "Design",
                ServiceCategory.Tutoring => "Tutoring",
                ServiceCategory.Marketing => "Marketing",
                ServiceCategory.PhotoVideo => "PhotoVideo",
                ServiceCategory.Other => "Other",
                _ => category.ToString()
            };
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

        private void Button_FindSpecialist(object sender, RoutedEventArgs e)
        {
            if (_loggedInUser is Customer customer)
            {
                ListSpecialistsWindow specialistsByCategoryWindow = new (customer);
                specialistsByCategoryWindow.Show();
                this.Close();
            }
            else if (_loggedInUser is Specialist specialist)
            {
                ListOrdersWindow listOrdersWindow = new (specialist);
                listOrdersWindow.Show();
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
