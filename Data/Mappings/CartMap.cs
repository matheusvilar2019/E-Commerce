using E_Commerce.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace E_Commerce.Data.Mappings
{
    public class CartMap : IEntityTypeConfiguration<Cart>
    {
        public void Configure(EntityTypeBuilder<Cart> builder)
        {
            // Table
            builder.ToTable("Cart");

            // Primary Key
            builder.HasKey(x => x.Id);

            // Identity
            builder.Property(x => x.Id)
                .ValueGeneratedOnAdd()
                .UseIdentityColumn();

            // Relations
            builder
                .HasMany(x => x.Products)
                .WithMany(x => x.Carts)
                .UsingEntity<Dictionary<string, object>>(
                    "CartProduct",
                    cart => cart
                        .HasOne<Product>()
                        .WithMany()
                        .HasForeignKey("CartId")
                        .HasConstraintName("FK_CartProduct_CartId")
                        .OnDelete(DeleteBehavior.Cascade),
                    product => product
                        .HasOne<Cart>()
                        .WithMany()
                        .HasForeignKey("ProductId")
                        .HasConstraintName("FK_CartProduct_ProductId")
                        .OnDelete(DeleteBehavior.Cascade));

        }
    }
}
