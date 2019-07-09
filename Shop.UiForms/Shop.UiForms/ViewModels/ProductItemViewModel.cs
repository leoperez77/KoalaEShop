﻿using GalaSoft.MvvmLight.Command;
using Shop.Common.Models;
using Shop.UiForms.Views;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;

namespace Shop.UiForms.ViewModels
{
    public class ProductItemViewModel: Product
    {
        public ICommand SelectProductCommand => new RelayCommand(this.SelectProduct);

        private async void SelectProduct()
        {
            MainViewModel.GetInstance().EditProduct = new EditProductViewModel((Product)this);
            await App.Navigator.PushAsync(new EditProductPage());
        }

    }
}
