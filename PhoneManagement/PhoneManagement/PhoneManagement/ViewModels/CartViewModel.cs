﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Net.Http;
using System.Text;
using System.Windows.Input;
using Newtonsoft.Json;
using PhoneManagement.Models;
using PhoneManagement.Views;
using Xamarin.Forms;
namespace PhoneManagement.ViewModels
{
    class CartViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        ObservableCollection<CartDetail> listCartDetail;
        ObservableCollection<Product> listProduct;
        Cart cart;

        public ObservableCollection<CartDetail> LISTCARTDETAIL
        {
            get { return listCartDetail; }
            set
            {
                listCartDetail = value;
                OnPropertyChanged("LISTCARTDETAIL");
            }
        }

        public ObservableCollection<Product> LISTPRODUCT
        {
            get { return listProduct; }
            set
            {
                listProduct = value;
                OnPropertyChanged("LISTPRODUCT");
            }
        }

        ObservableCollection<CartProduct> cartProducts;
        public ObservableCollection<CartProduct> CARTPRODUCTS
        {
            get { return cartProducts; }
            set
            {
                cartProducts = value;
                OnPropertyChanged("CARTPRODUCTS");
            }
        }
        public Cart CART
        {
            get { return cart; }
            set
            {
                cart = value;
                OnPropertyChanged("CART");
            }
        }

        ObservableCollection<Shipping> listAddress;
        public ObservableCollection<Shipping> LISTADDRESS
        {
            get
            {
                return listAddress;
            }
            set
            {
                listAddress = value;
                OnPropertyChanged("LISTADDRESS");
            }
        }
        async void GetCart()
        {
            HttpClient http = new HttpClient();
            var list = await http.GetStringAsync("http://www.wjbu-project.somee.com/api/CartController/GetCart?accountID=" + App.loginID);
            var listConvert = JsonConvert.DeserializeObject<List<Cart>>(list);
            CART = new Cart();
            if (listConvert.Count > 0)
            {
                CART = listConvert[0];
            }
          
        }

        async void RenderCartPage()
        {
            HttpClient http = new HttpClient();
            var list = await http.GetStringAsync("http://www.wjbu-project.somee.com/api/CartController/GetCartDetail?accountID=" + App.loginID);
            var listConvert = JsonConvert.DeserializeObject<List<CartDetail>>(list);
            CARTPRODUCTS = new ObservableCollection<CartProduct>();
            for (int i = 0; i < listConvert.Count; i++)
            {
                var pID = listConvert[i].ProductID;
                var product = await http.GetStringAsync("http://www.wjbu-project.somee.com/api/ProductController/GetProductWithID?id=" + pID);
                var productConvert = JsonConvert.DeserializeObject<List<Product>>(product);
                CartProduct temp = new CartProduct
                {
                    CARTDETAILID = listConvert[i].Cart_DetailID,
                    CARTID = listConvert[i].CartID,
                    PRODUCTID = listConvert[i].ProductID,
                    PRODUCTQUANITY = listConvert[i].ProductQuanity,
                    PRODUCTNAME = productConvert[0].ProductName,
                    PRODUCTIMG = productConvert[0].ProductImg,
                    PRODUCTPRICE = productConvert[0].ProductPrice,
                    PRODUCTSTATUS = productConvert[0].ProductStatus,
                    CATEGORYID = productConvert[0].CategoryID,
                };
                CARTPRODUCTS.Add(temp);
            }
        }
        public ICommand plusCommand { get; set; }
        public ICommand minusCommand { get; set; }

        async void plusFunc(object cartDetailID)
        {
            for (int i = 0; i < CARTPRODUCTS.Count; i++)
            {
                if (CARTPRODUCTS[i].CARTDETAILID.ToString() == cartDetailID.ToString())
                {
                    CARTPRODUCTS[i].PRODUCTQUANITY++;
                    CART.CartTotal += int.Parse(CARTPRODUCTS[i].PRODUCTPRICE.ToString());
                    HttpClient http = new HttpClient();
                    var list = await http.GetStringAsync("http://www.wjbu-project.somee.com/api/CartController/UpdateCart?cartID=" + CART.CartID + "&total=" + CART.CartTotal + "&cartDetailID=" + CARTPRODUCTS[i].CARTDETAILID + "&quanity=" + CARTPRODUCTS[i].PRODUCTQUANITY);
                }
            }

        }
        async void minusFunc(object cartDetailID)
        {
            HttpClient http = new HttpClient();

            for (int i = 0; i < CARTPRODUCTS.Count; i++)
            {
                if (CARTPRODUCTS[i].CARTDETAILID.ToString() == cartDetailID.ToString())
                {
                    if (CARTPRODUCTS[i].PRODUCTQUANITY - 1 <= 0)
                    {
                        CART.CartTotal -= int.Parse(CARTPRODUCTS[i].PRODUCTPRICE.ToString());

                        var list = await http.GetStringAsync("http://www.wjbu-project.somee.com/api/CartController/DeleteProductInCart?pID=" + CARTPRODUCTS[i].PRODUCTID + "&cartDetailID=" + CARTPRODUCTS[i].CARTDETAILID + "&total=" + CART.CartTotal + "&cartID=" + CART.CartID);
                        CARTPRODUCTS.Remove(CARTPRODUCTS[i]);
                    }
                    else
                    {
                        CARTPRODUCTS[i].PRODUCTQUANITY--;
                        CART.CartTotal -= int.Parse(CARTPRODUCTS[i].PRODUCTPRICE.ToString());
                        var list = await http.GetStringAsync("http://www.wjbu-project.somee.com/api/CartController/UpdateCart?cartID=" + CART.CartID + "&total=" + CART.CartTotal + "&cartDetailID=" + CARTPRODUCTS[i].CARTDETAILID + "&quanity=" + CARTPRODUCTS[i].PRODUCTQUANITY);
                    }
                }
            }
        }
        async void GetShipping()
        {
            HttpClient http = new HttpClient();
            var list = await http.GetStringAsync("http://www.wjbu-project.somee.com/api/ShippingController/GetListShipping?accountID=" + App.loginID);
            var listConvert = JsonConvert.DeserializeObject<List<Shipping>>(list);
            LISTADDRESS = new ObservableCollection<Shipping>();
            for (int i = 0; i < listConvert.Count; i++)
            {
                LISTADDRESS.Add(listConvert[i]);
            }
        }

        public ICommand AddNewAddress { get; set; }
        async void AddNewAddressFunc(string[] arr)
        {
            Shipping newShipping = new Shipping
            {
                SHIPPINGID = 1,
                SHIPPINGNAME = arr[0],
                SHIPPINGADDRESS = arr[1],
                SHIPPINGPHONE = arr[2],
                ACCOUNTID = 1,
            };
            HttpClient http = new HttpClient();
            string json = JsonConvert.SerializeObject(newShipping);
            StringContent content = new StringContent(json, Encoding.UTF8, "application/json");
            var list = await http.PostAsync("http://www.wjbu-project.somee.com/api/ShippingController/AddNewAddress", content);
            await Application.Current.MainPage.DisplayAlert("Thong bao", "Them dia chi moi thanh cong", "OK");
            await Application.Current.MainPage.Navigation.PushAsync(new CheckoutPage());
        }

        public ICommand Checkout { get; set; }
        async void CheckoutFunc(string[] note)
        {
            HttpClient http = new HttpClient();
            var list = await http.GetStringAsync("http://www.wjbu-project.somee.com/api/OrderController/Checkout?accountID=" + App.loginID + "&cartID=" + CART.CartID + "&shippingID=" + note[1] + "&orderNote=" + note[0]);
            await Application.Current.MainPage.DisplayAlert("Thông báo", "Đặt hàng thành công", "OK");
            await Application.Current.MainPage.Navigation.PushAsync(new HomePage());
        }

        public CartViewModel()
        {
            RenderCartPage();
            GetCart();
            plusCommand = new Command(plusFunc);
            minusCommand = new Command(minusFunc);
            LISTADDRESS = new ObservableCollection<Shipping>();
            AddNewAddress = new Command<string[]>(AddNewAddressFunc);
            Checkout = new Command<string[]>(CheckoutFunc);
            GetShipping();
        }
    }
}