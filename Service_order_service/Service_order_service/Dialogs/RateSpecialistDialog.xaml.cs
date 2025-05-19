using System.Windows;
using System.Windows.Controls;

namespace Service_order_service.Dialogs
{
    public partial class RateSpecialistDialog : Window
    {
        public int RatingValue { get; private set; }
        public string? Comment { get; private set; }

        public RateSpecialistDialog()
        {
            InitializeComponent();
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
            Close();
        }

        private void Submit_Click(object sender, RoutedEventArgs e)
        {
            if (RatingComboBox.SelectedItem is ComboBoxItem selectedItem &&
                int.TryParse(selectedItem.Content.ToString(), out int rating))
            {
                RatingValue = rating;
                Comment = CommentTextBox.Text;
                DialogResult = true;
                Close();
            }
            else
            {
                MessageBox.Show("Please select a valid rating.", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }
    }
}
