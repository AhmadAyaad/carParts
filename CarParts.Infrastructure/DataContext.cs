using CarParts.Model;
using Microsoft.EntityFrameworkCore;
using System;

namespace CarParts.Infrastructure
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> dbContextOptions) : base(dbContextOptions)
        {

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            base.OnModelCreating(modelBuilder);


            modelBuilder.Entity<User>()
                        .OwnsOne(u => u.Address);
            modelBuilder.Entity<User>(entity =>
            {
                entity.HasKey(u => u.UserId);
                entity.Property(u => u.Name).IsRequired().HasMaxLength(250);
                //entity.Property(u => u.Address).IsRequired();
                //entity.Property(u => u.Address.City).HasMaxLength(250);
                //entity.Property(u => u.Address.Country).HasMaxLength(125);
                //entity.Property(u => u.Address.Street).HasMaxLength(200);
                //entity.Property(u => u.Address.ZipCode).HasMaxLength(4);
                entity.Property(u => u.Email).IsRequired().HasMaxLength(250);
            });


            modelBuilder.Entity<User>()
                        .HasMany(u => u.Orders)
                        .WithOne(u => u.User);
           
            ////////////////////////

            modelBuilder.Entity<Category>(entity =>
            {
                entity.HasKey(c => c.CategoryId);
                entity.Property(c => c.Name).IsRequired().HasMaxLength(250);
            });

            modelBuilder.Entity<Category>()
                        .HasMany(c => c.Products)
                        .WithOne(c => c.Category);

            //////////////////////

            modelBuilder.Entity<Product>(entity =>
            {

                entity.HasKey(p => p.ProductId);
                entity.Property(p => p.ProductName).IsRequired().HasMaxLength(255);
                entity.Property(p => p.PercentageOfDiscount).HasColumnType("float");
                entity.Property(p => p.Descirption).IsRequired().HasMaxLength(500);
                entity.Property(p => p.Price).HasColumnType("decimal(10,3)");

            });

            modelBuilder.Entity<Product>()
                        .HasMany(p => p.Photos)
                        .WithOne(p => p.Product);

            ///////////////

            modelBuilder.Entity<Photo>(entity =>
            {
                entity.HasKey(p => p.Id);
            });

            ///////

            modelBuilder.Entity<Order>(entity =>
            {
                entity.HasKey(o => o.OrderId);
                entity.Property(o => o.Item).IsRequired().HasMaxLength(300);
            });
            modelBuilder.Entity<Order>()
                        .HasMany(o => o.Products)
                        .WithOne(o => o.Order);
        }
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<Category> Categories { get; set; }
        public virtual DbSet<Cart> Carts { get; set; }
        public virtual DbSet<Order> Orders { get; set; }
        public virtual DbSet<Product> Products { get; set; }
        public virtual DbSet<Photo> Photos { get; set; }
    }
}
