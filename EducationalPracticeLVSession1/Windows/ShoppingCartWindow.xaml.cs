using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace EducationalPracticeLVSession1
{
    /// <summary>
    /// Логика взаимодействия для ShoppingCartWindow.xaml
    /// </summary>
    public partial class ShoppingCartWindow : Window
    {

        private List<Products> _orderProducts = new List<Products>();

        private int _returnCode = 0;

        public ShoppingCartWindow(List<Products> orderProducts)
        {
            InitializeComponent();

            _orderProducts = orderProducts;

            UploadedInformation();
            CalculateCostOrder();
        }

        private float _costAllProducts, _amountDiscount, _costWithDiscount;

        private Users _user;

        public ShoppingCartWindow(List<Products> orderProducts, Users user)
        {
            InitializeComponent();

            _user = user;
            _orderProducts = orderProducts;
            userName.Text = $"{user.UserSurname} {user.UserName} {user.UserPatronymic}";
            userName.Visibility = Visibility.Visible;

            UploadedInformation();
            CalculateCostOrder();
        }

        private void UploadedInformation()
        {
            productList.ItemsSource = _orderProducts.ToList();

            pointDelivery.Items.Add("Не выбрано");

            List<DeliveryPoints> deliveryPoints = DataBaseClass.dataBaseEntities.DeliveryPoints.ToList();

            foreach (DeliveryPoints deliveryPoint in deliveryPoints)
            {
                pointDelivery.Items.Add(deliveryPoint.PointName.ToString());
            }

            pointDelivery.SelectedIndex = 0;
        }

        private void CalculateCostOrder()   
        {
            _costAllProducts = 0;
            _amountDiscount = 0;
            _costWithDiscount = 0;

            fullAmount.Visibility = Visibility.Collapsed;
            amountDiscount.Visibility = Visibility.Collapsed;

            foreach (Products product in productList.Items)
            {
                _costAllProducts += product.ProductCost * product.Count;
                _amountDiscount += product.ProductCost * (product.ProductCurrentDiscount / 100) * product.Count;
            }

            if (_amountDiscount > 0)
            {
                _costWithDiscount = _costAllProducts - _amountDiscount;

                fullAmount.Text = $"Общая стоимость: {_costAllProducts}";
                fullAmount.Visibility = Visibility.Visible;

                amountDiscount.Text = $"Сумма скидки: {_amountDiscount} ({(_amountDiscount * 100) / _costAllProducts}%)";
                amountDiscount.Visibility = Visibility.Visible;

                readyAmount.Text = $"К оплате: {_costWithDiscount}";
            }
            else
            {
                readyAmount.Text = $"К оплате: {_costWithDiscount}";
            }

            if (_orderProducts.Count == 0)
            {
                MessageBox.Show("Корзина пустая. Окно будет закрыто", "Внимание", MessageBoxButton.OK, MessageBoxImage.Information);
                _returnCode = 1;
                this.Close();
            }


        }

        private Products _product;

        private int _index;
        private void priceBlock_Loaded(object sender, RoutedEventArgs e)
        {
            TextBlock priceBlock = (TextBlock)sender;

            _index = Convert.ToInt32(priceBlock.Uid);
            _product = DataBaseClass.dataBaseEntities.Products.FirstOrDefault(x => x.ProductID == _index);

            if (_product.ProductCurrentDiscount != 0)
            {
                priceBlock.Text = $"Цена: {_product.ProductCost - (_product.ProductCost * (_product.ProductCurrentDiscount / 100))}";
            }
            else
            {
                priceBlock.Text = $"Цена: {_product.ProductCost}";
            }
        }

        private void discountPriceBlock_Loaded(object sender, RoutedEventArgs e)
        {
            TextBlock discountPriceBlock = (TextBlock)sender;

            _index = Convert.ToInt32(discountPriceBlock.Uid);
            _product = DataBaseClass.dataBaseEntities.Products.FirstOrDefault(x => x.ProductID == _index);

            if (_product.ProductCurrentDiscount != 0)
            {
                discountPriceBlock.Text = $"{_product.ProductCost}";

                discountPriceBlock.Visibility = Visibility.Visible;
            }
        }

        private void currentDiscount_Loaded(object sender, RoutedEventArgs e)
        {
            TextBlock discountPriceBlock = (TextBlock)sender;

            _index = Convert.ToInt32(discountPriceBlock.Uid);
            _product = DataBaseClass.dataBaseEntities.Products.FirstOrDefault(x => x.ProductID == _index);

            if (_product.ProductCurrentDiscount != 0)
            {
                discountPriceBlock.Text = $" Скидка {_product.ProductCurrentDiscount}%";

                discountPriceBlock.Visibility = Visibility.Visible;
            }
        }

        private void countBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            TextBox countBox = (TextBox)sender;

            string index = countBox.Uid;

            if (countBox.Text == "0")
            {
                DeleteRecord(index);
            }

        }

        private void deleteButton_Click(object sender, RoutedEventArgs e)
        {
            Button deleteButton = (Button)sender;

            string index = deleteButton.Uid;

            DeleteRecord(index);
        }

        private void DeleteRecord(string index)
        {
            Products product = DataBaseClass.dataBaseEntities.Products.FirstOrDefault(x => x.ProductArticleNumber == index);
            
            _orderProducts.Remove(product);
           
            productList.ItemsSource = _orderProducts.ToList();
            CalculateCostOrder();
        }

        public int GetReturnCode
        {
            get
            {
                return _returnCode;
            }
        }

        private void acceptOrder_Click(object sender, RoutedEventArgs e)
        {
            if (pointDelivery.SelectedIndex != 0)
            {
                List<Order> orders = DataBaseClass.dataBaseEntities.Order.ToList();
                orders.Reverse();
                Order lastOrder = orders.FirstOrDefault();

                int lastOrderNumber = lastOrder.OrderNumber + 1;

                List<Products> products = DataBaseClass.dataBaseEntities.Products.ToList();

                Random getCodeRandom = new Random();

                int getCode = getCodeRandom.Next(100,1000);

                bool checkAbundanceQuanity = true;

                foreach (Products product in productList.Items)
                {
                    if (product.ProductQuantity < 3)
                    {
                        checkAbundanceQuanity = false;
                        break;
                    }
                }

                DateTime dateDelivery;
                int daysDelivery;

                if (checkAbundanceQuanity)
                {
                    dateDelivery = DateTime.Now.AddDays(3);
                    daysDelivery = 3;
                }
                else
                {
                    dateDelivery = DateTime.Now.AddDays(6);
                    daysDelivery = 6;
                }

                int? codeUser = null;

                if (_user != null)
                {
                    codeUser = _user.UserID;
                }

                foreach (Products product in productList.Items)
                {
                    Order order = new Order()
                    {
                        OrderNumber = lastOrderNumber,
                        OrderProduct = product.ProductID,
                        OrderCountProduct = product.Count,
                        OrderDate = DateTime.Now,
                        DateDelivery = dateDelivery,
                        PointDelivery = pointDelivery.SelectedIndex,
                        CustomerID = codeUser,
                        GetCode = getCode,
                        OrderStatus = 1
                    };

                    DataBaseClass.dataBaseEntities.Order.Add(order);
                }
                DataBaseClass.dataBaseEntities.SaveChanges();

                OrderTicketWindow orderTicketWindow = new OrderTicketWindow(DateTime.Now, daysDelivery, lastOrderNumber, dateDelivery, _orderProducts, _costWithDiscount, _amountDiscount, pointDelivery.SelectedItem.ToString(), getCode, _user);
                orderTicketWindow.Show();
            }
            else
            {
                MessageBox.Show("Укажите пункт выдачи", "Внимание", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }
    }
}
