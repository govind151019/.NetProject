using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace web_application_product.Models.data
{
    public partial class YourDbContext : DbContext
    {
        public YourDbContext(DbContextOptions<YourDbContext> options) : base(options)
        {
        }

        public DbSet<Product> GProducts { get; set; }
        public DbSet<ProductCategory> ProductCategory { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Product>(entity =>
            {
                entity.HasKey(e => e.Id)
                    .HasName("FK__GProducts__Produ__3864608B");

                entity.Property(e => e.Name)
                    .HasMaxLength(30)
                    .IsUnicode(false);

                entity.Property(e => e.Price)
                   .HasMaxLength(30)
                   .IsUnicode(false);

                entity.Property(e => e.Color)
                    .HasMaxLength(15)
                    .IsUnicode(false);

                entity.Property(e => e.ProductCategory)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

        
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);

    }
}
