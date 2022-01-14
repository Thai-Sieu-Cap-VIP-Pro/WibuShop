using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PhoneManagement.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CartPage : ContentPage
    {
        public CartPage()
        {
            InitializeComponent();
          
        }

        private void Button_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new NewAddressPage());
          

        }

        private void Button_Clicked_1(object sender, EventArgs e)
        {
        
            if (App.loginID == "")
            {
                DisplayAlert("OK", "Vui lòng đăng nhập để tiến hành thanh toán!", "OK");
                Navigation.PushAsync(new LoginPage());
             
            }
            else if ((ProductList.ItemsSource as ObservableCollection<Models.CartProduct>).Count > 0)
            {
                Navigation.PushAsync(new CheckoutPage());
            }    
            else
            {
                DisplayAlert("Thông báo", "Giỏ hàng chưa có sản phẩm nào!", "OK");
                Navigation.PushAsync(new HomePage());
            }    
        
          
        }
    }
}