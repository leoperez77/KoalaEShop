using Shop.Common.Models;
using Shop.Common.Services;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Xamarin.Forms;

namespace Shop.UiForms.ViewModels
{
    public class ProductsViewModels:BaseViewModel
    {
        private ObservableCollection<Product> productsCollection;
        private ApiService apiService;
        private bool isRefreshing;

        public ObservableCollection<Product> ProductsCollection
        {
            get { return this.productsCollection; }
            set { this.SetValue(ref this.productsCollection, value); }
        }

        

        public bool IsRefreshing
        {
            get { return this.isRefreshing; }
            set { this.SetValue(ref this.isRefreshing, value); }
        }


        public ProductsViewModels()
        {
            this.apiService = new ApiService();
            this.LoadProducts();
        }

        private async void LoadProducts()
        {
            this.IsRefreshing = true;
            var response = await this.apiService.GetListAsync<Product>(
                "https://koalaeshop.azurewebsites.net",
                "/api",
                "/products");

            this.IsRefreshing = false;

            if (!response.IsSuccess)
            {
                await Application.Current.MainPage.DisplayAlert(
                    "Error",
                    response.Message,
                    "Accept");

                return;
            }

            var obj = (List<Product>)response.Result;
            this.ProductsCollection = new ObservableCollection<Product>(obj);
        }
    }
}
