using System;
using System.Collections.Generic;
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
            Navigation.PushAsync(new CheckoutPage());
        }
    }
}