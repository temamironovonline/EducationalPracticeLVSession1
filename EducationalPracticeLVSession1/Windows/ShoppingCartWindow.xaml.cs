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

        private List<Products> _checkDelete = new List<Products>();

        public ShoppingCartWindow(List<Products> orderProducts)
        {
            InitializeComponent();

            _checkDelete = orderProducts.ToList();

            productList.ItemsSource = orderProducts.ToList();

            CalculateCostOrder();
        }

        private float _costAllProducts, _amountDiscount, _costWithDiscount;

        public ShoppingCartWindow(List<Products> orderProducts, Users user)
        {
            InitializeComponent();

            productList.ItemsSource = orderProducts.ToList();
            userName.Text = $"{user.UserSurname} {user.UserName} {user.UserPatronymic}";
            userName.Visibility = Visibility.Visible;

            CalculateCostOrder();
        }

        private void CalculateCostOrder()
        {
            _costAllProducts = 0;
            _amountDiscount = 0;
            _costWithDiscount = 0;

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

                amountDiscount.Text = $"Сумма скидки: {_amountDiscount} ({(_amountDiscount * 100)/_costAllProducts}%)";
                amountDiscount.Visibility = Visibility.Visible;

                readyAmount.Text = $"К оплате: {_costWithDiscount}";
            }
            else
            {
                readyAmount.Text = $"К оплате: {_costWithDiscount}";
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
            Products product = DataBaseClass.dataBaseEntities.Products.FirstOrDefault(x => x.ProductArticleNumber == index);

            if (countBox.Text == "0")
            {
                MessageBox.Show("");
                _checkDelete.Remove(product);
            }

            DataBaseClass.dataBaseEntities.SaveChanges();

            CalculateCostOrder();
        }
    }
}
