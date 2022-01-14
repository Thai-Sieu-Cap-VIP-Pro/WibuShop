using PhoneManagement.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PhoneManagement.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class OrderDetailPage : ContentPage
    {
 
        public OrderDetailPage()
        {
            InitializeComponent();
        }
        public OrderDetailPage(ObservableCollection<OrderProductDetailModel> x, OrderModel y, ShippingModel b)
        {
            InitializeComponent();
            ((NavigationPage)Application.Current.MainPage).BarBackgroundColor = Color.FromHex("#F78700");
            CultureInfo cul = CultureInfo.GetCultureInfo("vi-VN");
            string a = double.Parse(y.ORDERTOTAL.ToString()).ToString("#,###", cul.NumberFormat);
            lstdetail.ItemsSource = x;
            lblnote.Text = y.ORDERNOTE;
            lblname.Text = b.SHIPPINGNAME;
            lbladdress.Text = b.SHIPPINGADDRESS;
            lblphone.Text = b.SHIPPINGPHONE;
            lbltongtien.Text = a + "đ";

        }
    }
}