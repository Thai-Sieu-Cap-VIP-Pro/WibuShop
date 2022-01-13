using PhoneManagement.Models;
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
    public partial class CheckoutPage : ContentPage
    {
        int shippingID;
        public CheckoutPage()
        {
            InitializeComponent();
        }

        private void Button_Clicked(object sender, EventArgs e)
        {
            if (Note.Text != null)
            {
                string[] arr = new string[]
           {
                    Note.Text,
                    shippingID.ToString()
           };
                dathang.CommandParameter = arr;
            }
            else
            {
                string[] arr = new string[]
           {
                "Default",
                shippingID.ToString()
           };
                dathang.CommandParameter = arr;
            }

        }

        private void picker_SelectedIndexChanged(object sender, EventArgs e)
        {

            var num = (Shipping)picker.SelectedItem;
            shippingID = num.SHIPPINGID;
        }

        private void Button_Clicked_1(object sender, EventArgs e)
        {
            Navigation.PushAsync(new NewAddressPage());
        }
    }
}