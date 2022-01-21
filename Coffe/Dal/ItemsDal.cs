using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using Coffe.Models;

namespace Coffe.Dal
{
    public class ItemsDal : DbContext
    {
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Items>().ToTable("Items");

        }

        public DbSet<Items> Items { get; set; }

    }
}
