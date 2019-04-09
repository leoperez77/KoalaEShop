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
        private readonly IUserHelper user;
        private readonly Random random;

        public Seeder(DataContext context, IUserHelper userHelper)
        {
            this.context = context;
            this.user = userHelper;
            this.random = new Random();
        }

        public async Task SeedAsync()
        {
            await this.context.Database.EnsureCreatedAsync();

            var user = await this.user.GetUserByEmailAsync("leonardo_perez@hotmail.com");
            if (user == null)
            {
                user = new User
                {
                    FirstName = "Leonardo",
                    LastName = "Perez",
                    Email = "leonardo_perez@hotmail.com",
                    UserName = "leonardo_perez@hotmail.com", 
                    PhoneNumber = "3017216927"
                };

                var result = await this.user.AddUserAsync(user, "123456");
                if (result != IdentityResult.Success)
                {
                    throw new InvalidOperationException("Could not create the user in seeder");
                }
            }

            if (!this.context.Products.Any())
            {
                this.AddProduct("First Product", user);
                this.AddProduct("Second Product", user);
                this.AddProduct("Third Product", user);
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
