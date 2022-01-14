using Rg.Plugins.Popup.Services;
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
    public partial class ProfilePage : ContentPage
    {
        public ProfilePage()
        {
            InitializeComponent();
        }

        private void btnEditProfile_Clicked(object sender, EventArgs e)
        {
            PopupNavigation.Instance.PushAsync(new EditProfilePopup());
        }

        private void Button_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new HistoryOrderPage());
        }

        private void btnLogout_Clicked(object sender, EventArgs e)
        {
            App.loginID = "";
            Navigation.PushAsync(new Views.HomePage());
        }

        private void btnhome_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new Views.HomePage());
        }
    }
}