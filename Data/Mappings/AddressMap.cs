using E_Commerce.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace E_Commerce.Data.Mappings
{
    public class AddressMap : IEntityTypeConfiguration<Address>
    {
        public void Configure(EntityTypeBuilder<Address> builder)
        {
            // Table
            builder.ToTable("Address");

            // Primary key
            builder.HasKey(x => x.Id);

            // Identity
            builder.Property(x => x.Id)
                .ValueGeneratedOnAdd()
                .IsRequired();

            // Properties
            builder.Property(x => x.ZipCode)
                .IsRequired()
                .HasColumnName("ZipCode")
                .HasColumnType("INT")
                .HasMaxLength(8);

            builder.Property(x => x.Street)
                .IsRequired()
                .HasColumnName("Street")
                .HasColumnType("VARCHAR")
                .HasMaxLength(60);

            builder.Property(x => x.Number)
                .IsRequired()
                .HasColumnName("Number")
                .HasColumnType("VARCHAR")
                .HasMaxLength(20);

            builder.Property(x => x.District)
                .IsRequired()
                .HasColumnName("District")
                .HasColumnType("VARCHAR")
                .HasMaxLength(40);

            builder.Property(x => x.State)
                .IsRequired()
                .HasColumnName("State")
                .HasColumnType("VARCHAR")
                .HasMaxLength(40);

            builder.Property(x => x.City)
                .IsRequired()
                .HasColumnName("City")
                .HasColumnType("VARCHAR")
                .HasMaxLength(40);
        }
    }
}
