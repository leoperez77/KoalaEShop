using GalaSoft.MvvmLight.Command;
using Shop.UiForms.Views;
using System;
using System.Windows.Input;
using Xamarin.Forms;

namespace Shop.UiForms.ViewModels
{
    public class LoginViewModel
    {
        public string Email { get; set; }

        public string Password { get; set; }

        public ICommand LoginCommand
        {
            get
            {
                return new RelayCommand(Login);
            }
        }

        public LoginViewModel()
        {
            this.Email = "leonardo_perez@hotmail.com";
            this.Password = "123456";
        }

        private async void Login()
        {
            if(string.IsNullOrEmpty(Email))
            {
                await Application.Current.MainPage.DisplayAlert("Error",
                    "You must enter an Email",
                    "Accept");

                return;
            }

            if (string.IsNullOrEmpty(Password))
            {
                await Application.Current.MainPage.DisplayAlert("Error",
                    "You must enter a Password",
                    "Accept");

                return;
            }

            MainViewModel.GetInstance().Products = new ProductsViewModels();
            await Application.Current.MainPage.Navigation.PushAsync(new ProductsPage());

            //await Application.Current.MainPage.DisplayAlert("Shop",
            //       "Welcome to the App",
            //       "Accept");


        }
    }
}
