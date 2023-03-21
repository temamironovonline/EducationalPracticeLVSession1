using System.Windows;
using System.Windows.Controls;


namespace EducationalPracticeLVSession1
{
    /// <summary>
    /// Логика взаимодействия для OrderListPage.xaml
    /// </summary>
    public partial class OrderListPage : Page
    {
        public OrderListPage()
        {
            InitializeComponent();
        }

        private void backButton_Click(object sender, RoutedEventArgs e)
        {
            FrameNavigation.frame.GoBack();
        }
    }
}
