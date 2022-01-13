using PhoneManagement.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PhoneManagement.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ProductDetailPage : ContentPage
    {
        public ProductDetailPage()
        {
            InitializeComponent();
        }
        public ProductDetailPage(Product details)
        {
            InitializeComponent();
            CultureInfo cul = CultureInfo.GetCultureInfo("vi-VN");
            string a = double.Parse(details.ProductPrice.ToString()).ToString("#,###", cul.NumberFormat);
            image.Source = details.ProductImg.ToString();
            price.Text = a + "đ";
            name.Text = details.ProductName;
            addBtn.CommandParameter = details.ProductID.ToString();
        }

        private void CartShoppingIcon_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new CartPage());
        }
    }
}