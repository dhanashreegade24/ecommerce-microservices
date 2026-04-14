using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace ProductService.Domain.Entities;

public partial class ProductDbContext : DbContext
{
    public ProductDbContext()
    {
    }

    public ProductDbContext(DbContextOptions<ProductDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Product> Products { get; set; }

    //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    //{
    //    // If options have already been configured (e.g. by AddDbContext in Program.cs),
    //    // we should not override them here. Keep this method empty or use environment-based fallback if needed.
    //    if (!optionsBuilder.IsConfigured)
    //    {
    //        // Optional: load a fallback from env var or leave empty to force configuration via DI
    //        // var conn = Environment.GetEnvironmentVariable("PRODUCT_DB_CONNECTION");
    //        // if (!string.IsNullOrEmpty(conn))
    //        // {
    //        //     optionsBuilder.UseNpgsql(conn);
    //        // }
    //    }
    //}

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Product>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("pk_product");

            entity.ToTable("product");

            entity.Property(e => e.Id)
                .UseIdentityAlwaysColumn()
                .HasColumnName("id");
            entity.Property(e => e.Name)
                .HasMaxLength(255)
                .HasColumnName("name");
            entity.Property(e => e.Price)
                .HasPrecision(18, 2)
                .HasColumnName("price");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
