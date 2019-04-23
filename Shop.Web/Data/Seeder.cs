using Microsoft.AspNetCore.Identity;
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

            await this.userHelper.CheckRoleAsync("Admin");
            await this.userHelper.CheckRoleAsync("Customer");

            if (!this.context.Countries.Any())
            {
                var cities = new List<City>();
                cities.Add(new City { Name = "Medellín" });
                cities.Add(new City { Name = "Bogotá" });
                cities.Add(new City { Name = "Calí" });

                this.context.Countries.Add(new Country
                {
                    Cities = cities,
                    Name = "Colombia"
                });

                await this.context.SaveChangesAsync();
            }


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

            }

            var isInRole = await this.userHelper.IsUserInRoleAsync(user, "Admin");
            if (!isInRole)
            {
                await this.userHelper.AddUserToRoleAsync(user, "Admin");
            }


            if (!this.context.Products.Any())
            {
                for (int i = 0; i < 5; i++)
                {
                    this.AddProduct("First Product", user);
                    this.AddProduct("Second Product", user);
                    this.AddProduct("Third Product", user);
                    this.AddProduct("DJI Mavic2", user);
                }
                await this.context.SaveChangesAsync();
            }
        }
   

        private void AddProduct(string name, User user)
        {
            this.context.Products.Add(new Product
            {
                User = user,
                Name = name,
                Price = this.random.Next(100),
                IsAvailabe = true,
                Stock = this.random.Next(100)
            });
        }
    }

}
