﻿using Shop.Common.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Shop.UiForms.ViewModels
{
    public class MainViewModel
    {
        private static MainViewModel instance;

        public LoginViewModel Login { get; set; }

        public ProductsViewModel Products { get; set; }

        public TokenResponse Token { get; set; }

        public MainViewModel()
        {
            //this.Login = new LoginViewModel();
            instance = this;
        }

        public static MainViewModel GetInstance()
        {
            if(instance==null)
            {
                return new MainViewModel();
            }

            return instance;
        }
    }
}
