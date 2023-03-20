using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;

namespace EducationalPracticeLVSession1
{
    /// <summary>
    /// Логика взаимодействия для ProductsList.xaml
    /// </summary>
    public partial class ProductsList : Page
    {
        private MainWindow _mainWindow;
        public ProductsList()
        {
            InitializeComponent();
            _mainWindow = (MainWindow)Application.Current.MainWindow;
            _mainWindow.Title = "ПИШИ-СТИРАЙ - Каталог товаров";
            _mainWindow.userName.Text = "Гость";
            _mainWindow.navigationBar.Height = new GridLength(0.15, GridUnitType.Star);
            SetProductList();
        }

        public static Users currentUser;

        public ProductsList(Users user)
        {
            InitializeComponent();
            currentUser = user;
            _mainWindow = (MainWindow)Application.Current.MainWindow;
            _mainWindow.Title = "ПИШИ-СТИРАЙ - Каталог товаров";
            _mainWindow.userName.Text = $"{user.UserSurname} {user.UserName} {user.UserPatronymic}";
            _mainWindow.navigationBar.Height = new GridLength(0.15, GridUnitType.Star);
            SetProductList();
        }

        private void SetProductList()
        {
            productList.ItemsSource = DataBaseClass.dataBaseEntities.Products.ToList();
            priceSorting.SelectedIndex = 0;
            discountFiltrating.SelectedIndex = 0;
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
                priceBlock.Text = $"Цена: {_product.ProductCost - (_product.ProductCost * (_product.ProductCurrentDiscount/100))}";
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

        private List<Products> _productsSorting;

        private void DataFiltrationAndSorting()
        {

            _productsSorting = DataBaseClass.dataBaseEntities.Products.ToList();

            int allRecords = _productsSorting.Count;

            foreach(Products product in _productsSorting)
            {
                if (product.ProductCurrentDiscount != 0)
                {
                    product.ProductCost = product.ProductCost - (product.ProductCost * (product.ProductCurrentDiscount / 100));
                }
            }

            // Сортировка по цене
            if (priceSorting.SelectedIndex == 1)
            {
                _productsSorting = _productsSorting.OrderBy(x => x.ProductCost).ToList();
            }
            else if (priceSorting.SelectedIndex == 2)
            {
                _productsSorting = _productsSorting.OrderByDescending(x => x.ProductCost).ToList();
            }
            else
            {
                _productsSorting = _productsSorting.ToList();
            }

            //Фильтрация по скидке
            if (discountFiltrating.SelectedIndex == 1)
            {
                _productsSorting = _productsSorting.Where(x => x.ProductCurrentDiscount <= 9.99).ToList();
            }
            else if (discountFiltrating.SelectedIndex == 2)
            {
                _productsSorting = _productsSorting.Where(x => x.ProductCurrentDiscount >= 10 && x.ProductCurrentDiscount <= 14.99).ToList();
            }
            else if (discountFiltrating.SelectedIndex == 3)
            {
                _productsSorting = _productsSorting.Where(x => x.ProductCurrentDiscount >= 15).ToList();
            }

            Regex regexName = new Regex($@".*{nameProduct.Text.ToLower()}.*");

            _productsSorting = _productsSorting.Where(x => regexName.IsMatch(x.ProductName.ToLower())).ToList();

            int currentRecords = _productsSorting.Count;

            countRecords.Text = $"{currentRecords} из {allRecords} записей";

            productList.ItemsSource = _productsSorting;
        }


        private void priceSorting_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            DataFiltrationAndSorting();
        }

        private void discountFiltrating_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            DataFiltrationAndSorting();
        }

        private void nameService_TextChanged(object sender, TextChangedEventArgs e)
        {
            DataFiltrationAndSorting();
        }

        private void clearButton_Click(object sender, RoutedEventArgs e)
        {
            priceSorting.SelectedIndex = 0;
            discountFiltrating.SelectedIndex = 0;
            nameProduct.Text = "";
        }

        public static List<Products> productsOrder = new List<Products>();

        private void addOrder_Click(object sender, RoutedEventArgs e)
        {
            Products product = _productsSorting.FirstOrDefault(x => x.ProductID == productList.SelectedIndex + 1);

            if (productsOrder.Count > 0)
            {
                bool checkSame = false;

                foreach (Products p in productsOrder)
                {
                    if (p.ProductArticleNumber == product.ProductArticleNumber)
                    {
                        p.Count += 1;
                        checkSame = true;
                        break;
                    }
                    else
                    {
                        checkSame = false;
                    }
                }

                if (!checkSame)
                {
                    product.Count = 1;
                    productsOrder.Add(product);
                }
            }
            else
            {
                product.Count = 1;
                productsOrder.Add(product);
            }

            if (_mainWindow.openOrder.Visibility == Visibility.Collapsed)
            {
                _mainWindow.openOrder.Visibility = Visibility.Visible;
            }
        }
    }
}
