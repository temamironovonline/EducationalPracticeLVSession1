using System;
using System.Windows;
using System.Windows.Controls;


namespace EducationalPracticeLVSession1
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            FrameNavigation.frame = mainFrame;
            DataBaseClass.dataBaseEntities = new DataBaseEntities();
            FrameNavigation.frame.Navigate(new LoginPage());
        }

        private void exitButton_Click(object sender, RoutedEventArgs e)
        {
            navigationBar.Height = new GridLength(0, GridUnitType.Star);
            Title = "ПИШИ-СТИРАЙ - Авторизация";
            FrameNavigation.frame.Navigate(new LoginPage());
        }

        private void openOrder_Click(object sender, RoutedEventArgs e)
        {
            ShoppingCartWindow shoppingCartWindow;
            if (ProductsList.currentUser == null)
            {
                shoppingCartWindow = new ShoppingCartWindow(ProductsList.productsOrder);
            }
            else
            {
                shoppingCartWindow = new ShoppingCartWindow(ProductsList.productsOrder, ProductsList.currentUser);
            }
            shoppingCartWindow.ShowDialog();
        }
    }
}
