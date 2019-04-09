using Microsoft.EntityFrameworkCore;
using Shop.Web.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace Shop.Web.Data
{
    public class DataContext: IdentityDbContext<User>

    {
        public DbSet<Product> Products { get; set; }

        public DbSet<Country> Countries { get; set; }

        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Product>()
                .Property(b => b.Price).HasColumnType("money");
        }
    }
}
