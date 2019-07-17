﻿using Shop.UiForms.Interfaces;
using Shop.UiForms.Resources;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace Shop.Common.Helpers
{
    public static class Languages
    {
        static Languages()
        {
            var ci = DependencyService.Get<ILocalize>().GetCurrentCultureInfo();
            Resource.Culture = ci;
            DependencyService.Get<ILocalize>().SetLocale(ci);
        }

        public static string Accept => Resource.Accept;

        public static string Error => Resource.Error;

        public static string EmailError => Resource.EmailError;

        public static string PesswordError => Resource.PesswordError;

        public static string LoginError => Resource.LoginError;

        public static string Login => Resource.Login;
        public static string Email => Resource.Email;
        public static string EmailPlaceholder => Resource.EmailPlaceholder;
        public static string Password => Resource.Password;
        public static string PasswordPlaceholder => Resource.PasswordPlaceholder;
        public static string Remember => Resource.Remember;

        public static string RegisterNewUser => Resource.RegisterNewUser;
    }

}
