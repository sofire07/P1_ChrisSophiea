using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using Models;

namespace DataAccess
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        
        public DbSet<ItemCategory> ItemCategory { get; set; }
        public DbSet<ApplicationUser> ApplicationUser { get; set; }
        public DbSet<Store> Store { get; set; }
        public DbSet<Item> Item { get; set; }
        public DbSet<ItemPurchase> ItemPurchase { get; set; }
        public DbSet<Purchase> Purchase { get; set; }
        public DbSet<Inventory> Inventory { get; set; }


    }
}
