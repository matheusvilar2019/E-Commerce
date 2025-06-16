using E_Commerce.Data.Mappings;
using E_Commerce.Model;
using Microsoft.EntityFrameworkCore;
namespace E_Commerce.Data;

public class ECommerceDataContext : DbContext
{
    public DbSet<Cart> Carts { get; set; }
    public DbSet<Payment> Payments { get; set; }
    public DbSet<Product> Products { get; set; }
    public DbSet<User> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder options)
    {
        options.UseSqlServer("Server=localhost;Database=ECommerce;Trusted_Connection=True;TrustServerCertificate=True;");
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new CartMap());
        modelBuilder.ApplyConfiguration(new PaymentMap());
        modelBuilder.ApplyConfiguration(new ProductMap());
        modelBuilder.ApplyConfiguration(new UserMap());
    }
}