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

        public ObservableCollection<Product> ProductsCollection
        {
            get { return this.productsCollection; }
            set { this.SetValue(ref this.productsCollection, value); }
        }

        private ApiService apiService;
        
        public ProductsViewModels()
        {
            this.apiService = new ApiService();
            this.LoadProducts();
        }

        private async void LoadProducts()
        {
            var response = await this.apiService.GetListAsync<Product>(
                "https://koalaeshop.azurewebsites.net",
                "/api",
                "/products");

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
