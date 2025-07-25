﻿using E_Commerce.Model;
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

            // Relationships
            builder
                .HasMany(x => x.Items)
                .WithOne(x => x.Cart)
                .HasConstraintName("FK_Cart_CartItems")
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
