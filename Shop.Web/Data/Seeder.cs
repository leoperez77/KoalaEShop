﻿using Microsoft.AspNetCore.Identity;
using Shop.Web.Data.Entities;
using Shop.Web.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shop.Web.Data
{
  

    public class Seeder
    {
        private readonly DataContext context;
        private readonly IUserHelper userHelper;
        private readonly Random random;

        public Seeder(DataContext context, IUserHelper userHelper)
        {
            this.context = context;
            this.userHelper = userHelper;
            this.random = new Random();
        }

        public async Task SeedAsync()
        {
            await this.context.Database.EnsureCreatedAsync();

            await this.CheckRoles();

            if (!this.context.Countries.Any())
            {
                await this.AddCountriesAndCitiesAsync();
            }

            //await this.CheckUserAsync("brad@gmail.com", "Brad", "Pit", "Customer");
            //await this.CheckUserAsync("angelina@gmail.com", "Angelina", "Jolie", "Customer");
            //var user = await this.CheckUserAsync("leonardo_perez@hotmail.com", "Leonardo", "Pérez", "Admin");


            var user = await this.userHelper.GetUserByEmailAsync("leonardo_perez@hotmail.com");
            if (user == null)
            {
                user = new User
                {
                    FirstName = "Leonardo",
                    LastName = "Perez",
                    Email = "leonardo_perez@hotmail.com",
                    UserName = "leonardo_perez@hotmail.com",
                    PhoneNumber = "3017216927",
                    Address = "Cll 23 #41-70",
                    CityId = this.context.Countries.FirstOrDefault().Cities.FirstOrDefault().Id,
                    City = this.context.Countries.FirstOrDefault().Cities.FirstOrDefault()

                };

                var result = await this.userHelper.AddUserAsync(user, "123456");
                if (result != IdentityResult.Success)
                {
                    throw new InvalidOperationException("Could not create the user in seeder");
                }

                await this.userHelper.AddUserToRoleAsync(user, "Admin");

                var token = await this.userHelper.GenerateEmailConfirmationTokenAsync(user);
                await this.userHelper.ConfirmEmailAsync(user, token);


            }

            var isInRole = await this.userHelper.IsUserInRoleAsync(user, "Admin");
            if (!isInRole)
            {
                await this.userHelper.AddUserToRoleAsync(user, "Admin");
            }

            // Add products
            if (!this.context.Products.Any())
            {
                this.AddProduct("AirPods", 159, user);
                this.AddProduct("Blackmagic eGPU Pro", 1199, user);
                this.AddProduct("iPad Pro", 799, user);
                this.AddProduct("iMac", 1398, user);
                this.AddProduct("iPhone X", 749, user);
                this.AddProduct("iWatch Series 4", 399, user);
                this.AddProduct("Mac Book Air", 789, user);
                this.AddProduct("Mac Book Pro", 1299, user);
                this.AddProduct("Mac Mini", 708, user);
                this.AddProduct("Mac Pro", 2300, user);
                this.AddProduct("Magic Mouse", 47, user);
                this.AddProduct("Magic Trackpad 2", 140, user);
                this.AddProduct("USB C Multiport", 145, user);
                this.AddProduct("Wireless Charging Pad", 67.67M, user);
                await this.context.SaveChangesAsync();
            }

            //await this.context.Database.EnsureCreatedAsync();

            //await this.userHelper.CheckRoleAsync("Admin");
            //await this.userHelper.CheckRoleAsync("Customer");

            //if (!this.context.Countries.Any())
            //{
            //    var cities = new List<City>();
            //    cities.Add(new City { Name = "Medellín" });
            //    cities.Add(new City { Name = "Bogotá" });
            //    cities.Add(new City { Name = "Calí" });

            //    this.context.Countries.Add(new Country
            //    {
            //        Cities = cities,
            //        Name = "Colombia"
            //    });

            //    await this.context.SaveChangesAsync();
            //}




            //if (!this.context.Products.Any())
            //{
            //    for (int i = 0; i < 5; i++)
            //    {
            //        this.AddProduct("First Product", user);
            //        this.AddProduct("Second Product", user);
            //        this.AddProduct("Third Product", user);
            //        this.AddProduct("DJI Mavic2", user);
            //    }
            //    await this.context.SaveChangesAsync();
            //}
        }


        //private void AddProduct(string name, User user)
        //{
        //    this.context.Products.Add(new Product
        //    {
        //        User = user,
        //        Name = name,
        //        Price = this.random.Next(100),
        //        IsAvailabe = true,
        //        Stock = this.random.Next(100)
        //    });
        //}

        private async Task<User> CheckUserAsync(string userName, string firstName, string lastName, string role)
        {
            // Add user
            var user = await this.userHelper.GetUserByEmailAsync(userName);
            if (user == null)
            {
                user = await this.AddUser(userName, firstName, lastName, role);
            }

            var isInRole = await this.userHelper.IsUserInRoleAsync(user, role);
            if (!isInRole)
            {
                await this.userHelper.AddUserToRoleAsync(user, role);
            }

            return user;
        }

        private async Task<User> AddUser(string userName, string firstName, string lastName, string role)
        {
            var user = new User
            {
                FirstName = firstName,
                LastName = lastName,
                Email = userName,
                UserName = userName,
                Address = "Calle Luna Calle Sol",
                PhoneNumber = "350 634 2747",
                CityId = this.context.Countries.FirstOrDefault().Cities.FirstOrDefault().Id,
                City = this.context.Countries.FirstOrDefault().Cities.FirstOrDefault()
            };

            var result = await this.userHelper.AddUserAsync(user, "123456");
            if (result != IdentityResult.Success)
            {
                throw new InvalidOperationException("Could not create the user in seeder");
            }

            await this.userHelper.AddUserToRoleAsync(user, role);
            var token = await this.userHelper.GenerateEmailConfirmationTokenAsync(user);
            await this.userHelper.ConfirmEmailAsync(user, token);
            return user;
        }

        private async Task AddCountriesAndCitiesAsync()
        {
            this.AddCountry("Colombia", new string[] { "Medellín", "Bogota", "Calí", "Barranquilla", "Bucaramanga", "Cartagena", "Pereira" });
            this.AddCountry("Argentina", new string[] { "Córdoba", "Buenos Aires", "Rosario", "Tandil", "Salta", "Mendoza" });
            this.AddCountry("Estados Unidos", new string[] { "New York", "Los Ángeles", "Chicago", "Washington", "San Francisco", "Miami", "Boston" });
            this.AddCountry("Ecuador", new string[] { "Quito", "Guayaquil", "Ambato", "Manta", "Loja", "Santo" });
            this.AddCountry("Peru", new string[] { "Lima", "Arequipa", "Cusco", "Trujillo", "Chiclayo", "Iquitos" });
            this.AddCountry("Chile", new string[] { "Santiago", "Valdivia", "Concepcion", "Puerto Montt", "Temucos", "La Sirena" });
            this.AddCountry("Uruguay", new string[] { "Montevideo", "Punta del Este", "Colonia del Sacramento", "Las Piedras" });
            this.AddCountry("Bolivia", new string[] { "La Paz", "Sucre", "Potosi", "Cochabamba" });
            this.AddCountry("Venezuela", new string[] { "Caracas", "Valencia", "Maracaibo", "Ciudad Bolivar", "Maracay", "Barquisimeto" });
            this.AddCountry("Paraguay", new string[] { "Asunción", "Ciudad del Este", "Encarnación", "San  Lorenzo", "Luque", "Areguá" });
            this.AddCountry("Brasil", new string[] { "Rio de Janeiro", "São Paulo", "Salvador", "Porto Alegre", "Curitiba", "Recife", "Belo Horizonte", "Fortaleza" });
            this.AddCountry("Panamá", new string[] { "Chitré", "Santiago", "La Arena", "Agua Dulce", "Monagrillo", "Ciudad de Panamá", "Colón", "Los Santos" });
            this.AddCountry("México", new string[] { "Guadalajara", "Ciudad de México", "Monterrey", "Ciudad Obregón", "Hermosillo", "La Paz", "Culiacán", "Los Mochis" });
            await this.context.SaveChangesAsync();
        }

        private void AddCountry(string country, string[] cities)
        {
            var theCities = cities.Select(c => new City { Name = c }).ToList();
            this.context.Countries.Add(new Country
            {
                Cities = theCities,
                Name = country
            });
        }

        private async Task CheckRoles()
        {
            await this.userHelper.CheckRoleAsync("Admin");
            await this.userHelper.CheckRoleAsync("Customer");
        }

        private void AddProduct(string name, decimal price, User user)
        {
            this.context.Products.Add(new Product
            {
                Name = name,
                Price = price,
                IsAvailabe = true,
                Stock = this.random.Next(100),
                User = user,
                ImageUrl = $"~/images/Products/{name}.png"
            });
        }

    }

}
