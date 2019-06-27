using Shop.Common.Models;
using Shop.Common.Services;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Xamarin.Forms;

namespace Shop.UiForms.ViewModels
{
    public class ProductsViewModel:BaseViewModel
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


        public ProductsViewModel()
        {
            this.apiService = new ApiService();
            this.LoadProducts();
        }

        private async void LoadProducts()
        {
            this.IsRefreshing = true;
            //var response = await this.apiService.GetListAsync<Product>(
            //    "https://koalaeshop.azurewebsites.net",
            //    "/api",
            //    "/products");

            var url = Application.Current.Resources["UrlAPI"].ToString();
            var response = await this.apiService.GetListAsync<Product>(
                url,
                "/api",
                "/Products",
                "bearer",
                MainViewModel.GetInstance().Token.Token);

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
            this.ProductsCollection = new ObservableCollection<Product>(obj.OrderBy(p => p.Name));
        }
    }
}
