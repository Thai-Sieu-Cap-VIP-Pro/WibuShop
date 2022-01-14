using Newtonsoft.Json;
using PhoneManagement.Models;
using PhoneManagement.Views;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Net.Http;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace PhoneManagement.ViewModels
{
    class LoginViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        private ProfileModel info;
        public ProfileModel INFO
        {
            get { return info; }
            set
            {
                info = value;
                OnPropertyChanged("INFO");
            }
        }
  
        public ICommand LoginCommand { get; set; }
        async void CheckLoginFunction(string[] arr)
        {
           
            ProfileModel loginUser = new ProfileModel
            {
                ACCOUNTID = 0,
                ACCOUNTNAME = arr[0],
                ACCOUNTPASSWORD = arr[1],
                ACCOUNTEMAIL = "",
                ACCOUNTPHONE = "",
            };
        
            HttpClient http = new HttpClient();

            string json = JsonConvert.SerializeObject(loginUser);
            StringContent content = new StringContent(json, Encoding.UTF8, "application/json");

            var chuoi = await http.PostAsync("http://www.wjbu-project.somee.com/api/AccountController/CheckAccount", content);
            var result = await chuoi.Content.ReadAsStringAsync();
            App.loginID = result;
            await Application.Current.MainPage.Navigation.PushAsync(new HomePage());

        }

        public LoginViewModel()
        {
            LoginCommand = new Command<string[]>(CheckLoginFunction);
        }

    }
}