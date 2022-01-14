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
    public partial class HomeMasterPage : ContentPage
    {
        public HomeMasterPage()
        {
            InitializeComponent();
            if (App.loginID != "")
            {

                loginBtn.Text = "";
                registerBtn.Text = "";
            }    
            else
            {
                Profile.Text = "";
            }    
        }
        private async void loginBtn_Clicked(object sender, EventArgs e)
        {
            App.MasterDet.IsPresented = false;
            await App.MasterDet.Detail.Navigation.PushAsync(new LoginPage());
        }

        private async void registerBtn_Clicked(object sender, EventArgs e)
        {
            App.MasterDet.IsPresented = false;
            await App.MasterDet.Detail.Navigation.PushAsync(new RegisterPage());
        }

        private void Profile_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new Views.ProfilePage());
        }
    }
}