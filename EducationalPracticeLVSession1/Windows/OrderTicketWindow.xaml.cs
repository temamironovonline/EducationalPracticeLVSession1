using PdfSharp.Drawing;
using PdfSharp.Pdf;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing.Design;
using System.Linq;
using System.Windows;

namespace EducationalPracticeLVSession1
{
    /// <summary>
    /// Логика взаимодействия для OrderTicketWindow.xaml
    /// </summary>
    public partial class OrderTicketWindow : Window
    {

        private DateTime _dateOrder, _dateDelivery;
        private List<Products> _orderProducts;
        private int _daysDelivery, _numberOrder, _getCode;
        private float _costOrder, _amountDiscount;
        private string _deliveryPoint;
        private Users _user;

        public OrderTicketWindow(DateTime dateOrder, int daysDelivery, int numberOrder, DateTime dateDelivery, List<Products> orderProducts, float costOrder, float amountDiscount, string deliveryPoint, int getCode, Users user)
        {
            InitializeComponent();

            _dateOrder = dateOrder;
            _dateDelivery = dateDelivery;
            _orderProducts = orderProducts;
            _daysDelivery = daysDelivery;
            _numberOrder = numberOrder;
            _getCode = getCode;
            _costOrder = costOrder;
            _amountDiscount = amountDiscount;
            _deliveryPoint = deliveryPoint;
            _user = user;

            productList.ItemsSource = orderProducts.ToList();

            if (user != null)
            {
                userName.Text = $"Заказчик: {user.UserSurname} {user.UserName} {user.UserPatronymic}";
            }
            else
            {
                userName.Visibility = Visibility.Visible;
            }

            this.dateOrder.Text = $"Дата заказа: {dateOrder.ToShortDateString()}";
            this.numberOrder.Text = $"Номер заказа: {numberOrder}";
            this.costOrder.Text = $"Стоимость заказа: {costOrder}";
            if (amountDiscount > 0)
            {
                this.amountDiscount.Text = $"Скидка: {amountDiscount} ({amountDiscount*100/costOrder}%)";
            }
            else this.amountDiscount.Visibility = Visibility.Collapsed;

            this.deliveryPoint.Text = $"Пункт выдачи {deliveryPoint}";

            this.getCode.Text = $"Код получения {getCode}";
            

        }

        private void getTicketButton_Click(object sender, RoutedEventArgs e)
        {
            PdfDocument document = new PdfDocument();
            int height = 0;
            document.Info.Title = "Талон для получения заказа";
            PdfPage page = document.AddPage();
            XGraphics gfx = XGraphics.FromPdfPage(page);
            XFont fontHeader = new XFont("Comic Sans MS", 18, XFontStyle.Bold);
            gfx.DrawString("Талон для получения заказа", fontHeader, XBrushes.Black,
                new XRect(10, height, page.Width, page.Height),
                XStringFormats.TopCenter);
            XFont font = new XFont("Comic Sans MS", 14);
            height += 30;
            gfx.DrawString("Номер: " + _numberOrder, font, XBrushes.Black,
                new XRect(10, height, page.Width, page.Height),
                XStringFormats.TopLeft);
            height += 30;
            gfx.DrawString("Дата заказа: " + _dateOrder.ToShortDateString(), font, XBrushes.Black,
                new XRect(10, height, page.Width, page.Height),
                XStringFormats.TopLeft);
            height += 30;
            if (_daysDelivery == 3)
            {
                gfx.DrawString("Заказ будет готов через 3 дня", font, XBrushes.Black,
                    new XRect(10, height, page.Width, page.Height),
                    XStringFormats.TopLeft);
            }
            else
            {
                gfx.DrawString("Заказ будет готов через 6 дней", font, XBrushes.Black,
                    new XRect(10, height, page.Width, page.Height),
                    XStringFormats.TopLeft);
            }
            height += 30;
            gfx.DrawString("Дата получения заказа: " + _dateDelivery.ToShortDateString(), font, XBrushes.Black,
                new XRect(10, height, page.Width, page.Height),
                XStringFormats.TopLeft);
            height += 30;
            gfx.DrawString("Состав заказа: ", font, XBrushes.Black,
                new XRect(10, height, page.Width, page.Height),
                XStringFormats.TopLeft);
            for (int i = 0; i < _orderProducts.Count; i++)
            {
                height += 30;
                if (i != _orderProducts.Count - 1)
                {
                    gfx.DrawString("" + _orderProducts[i].ProductName + " Количество: " + _orderProducts[i].Count + ";", font, XBrushes.Black,
                        new XRect(30, height, page.Width, page.Height),
                        XStringFormats.TopLeft);
                }
                else
                {
                    gfx.DrawString("" + _orderProducts[i].ProductName + " Количество: " + _orderProducts[i].Count + ".", font, XBrushes.Black,
                        new XRect(30, height, page.Width, page.Height),
                        XStringFormats.TopLeft);
                }
            }
            height += 30;
            gfx.DrawString("Сумма заказа: " + _costOrder + " руб.", font, XBrushes.Black,
                new XRect(10, height, page.Width, page.Height),
                XStringFormats.TopLeft);
            height += 30;
            gfx.DrawString("Сумма скидки: " + _amountDiscount + " руб.", font, XBrushes.Black,
                new XRect(10, height, page.Width, page.Height),
                XStringFormats.TopLeft);
            height += 30;
            gfx.DrawString("Пункт выдачи: " + _deliveryPoint, font, XBrushes.Black,
                new XRect(10, height, page.Width, page.Height),
                XStringFormats.TopLeft);
            height += 30;
            gfx.DrawString("Код для получения: " + _getCode, fontHeader, XBrushes.Black,
                new XRect(10, height, page.Width, page.Height),
                XStringFormats.TopLeft);
            string filename = "TicketPDF.pdf";
            document.Save(filename);
            Process.Start(filename);
        }
    }
}
