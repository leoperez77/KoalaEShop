using GalaSoft.MvvmLight.Command;
using Shop.Common.Models;
using Shop.UiForms.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Windows.Input;

namespace Shop.UiForms.ViewModels
{
    public class MainViewModel : BaseViewModel
    {
        private static MainViewModel instance;
        private User user;

        public LoginViewModel Login { get; set; }

        public ProductsViewModel Products { get; set; }


        public AddProductViewModel AddProduct { get; set; }

        public EditProductViewModel EditProduct { get; set; }

        public ObservableCollection<MenuItemViewModel> Menus { get; set; }


        public RegisterViewModel Register { get; set; }

        public RememberPasswordViewModel RememberPassword { get; set; }

        public ProfileViewModel Profile { get; set; }

        public ChangePasswordViewModel ChangePassword { get; set; }

        public TokenResponse Token { get; set; }

        public string UserEmail { get; set; }

        public string UserPassword { get; set; }

        public User User
        {
            get => user;
            set => this.SetValue(ref this.user, value);
        }


        public ICommand AddProductCommand { get { return new RelayCommand(GoAddProduct); } }

        private async void GoAddProduct()
        {
            this.AddProduct = new AddProductViewModel();
            await App.Navigator.PushAsync(new AddProductPage());
        }

        public MainViewModel()
        {
            //this.Login = new LoginViewModel();
            instance = this;
            this.LoadMenus();
        }

        public static MainViewModel GetInstance()
        {
            if (instance == null)
            {
                return new MainViewModel();
            }

            return instance;
        }

        private void LoadMenus()
        {
            var menus = new List<Menu>
            {
                new Menu
                {
                    Icon = "ic_info",
                    PageName = "AboutPage",
                    Title = "About"
                },

                new Menu
                {
                    Icon = "ic_insert_chart",
                    PageName = "SetupPage",
                    Title = "Setup"
                },

                 new Menu
                {
                    Icon = "ic_insert_chart",
                    PageName = "ProfilePage",
                    Title = "Modify user"
                },

                new Menu
                {
                    Icon = "ic_exit_to_app",
                    PageName = "LoginPage",
                    Title = "Close session"
                }
            };

            this.Menus = new ObservableCollection<MenuItemViewModel>(menus.Select(m => new MenuItemViewModel
            {
                Icon = m.Icon,
                PageName = m.PageName,
                Title = m.Title
            }).ToList());
        }

    }
}
