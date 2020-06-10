using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace EFCoreAppMigration
{
    public class CatPrdDbContext : DbContext
    {
        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }

        public CatPrdDbContext()
        {

        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=.;Initial Catalog=VodafoneDb; Integrated Security=SSPI");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // define one-to-many ralationship and hasOne relationship across Category and Product
            modelBuilder.Entity<Product>()
                         .HasOne(p => p.Category) // hasone, one product is a part of one category
                         .WithMany(c => c.Products) // one category contains multiple products
                         .HasForeignKey(p => p.CategoryRowId); // product contains reference for CatgoryRowId
        }
    }
}
