﻿using Rg.Plugins.Popup.Extensions;
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
    public partial class EditProfilePopup : Rg.Plugins.Popup.Pages.PopupPage
    {
        public EditProfilePopup()
        {
            InitializeComponent();
        }

        private void btnClosePopupEditProfile_Clicked(object sender, EventArgs e)
        {
            Navigation.PopPopupAsync();
        }
    }
}