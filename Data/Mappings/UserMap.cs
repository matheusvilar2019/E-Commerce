using E_Commerce.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace E_Commerce.Data.Mappings
{
    public class UserMap : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            // Table
            builder.ToTable("User");

            // Primary key
            builder.HasKey(x => x.Id);

            // Identity
            builder.Property(x => x.Id)
                .ValueGeneratedOnAdd()
                .UseIdentityColumn();

            // Proprieties
            builder.Property(x => x.Name)
                .IsRequired()
                .HasColumnName("Name")
                .HasColumnType("NVARCHAR")
                .HasMaxLength(80);

            builder.Property(x => x.CPF);
            builder.Property(x => x.Email)
                .IsRequired()
                .HasColumnName("Email")
                .HasColumnType("VARCHAR")
                .HasMaxLength(160);
            builder.Property(x => x.PasswordHash)
                .IsRequired()
                .HasColumnName("PasswordHash")
                .HasColumnType("VARCHAR")
                .HasMaxLength(255);
            builder.Property(x => x.BirthDate);
            builder.Property(x => x.CEP);
            builder.Property(x => x.Address);

            // Index
            builder
                .HasIndex(x => x.Email)
                .IsUnique(); // Email Validation: Unique email

            // Relationships
            builder
                .HasOne(x => x.Cart)
                .WithOne(x => x.User)
                .HasConstraintName("FK_User_Cart")
                .OnDelete(DeleteBehavior.Cascade);

            builder
                .HasMany(x => x.Roles)
                .WithMany(x => x.Users)
                .UsingEntity<Dictionary<string, object>>(
                    "UserRole",
                    role => role
                        .HasOne<Role>()
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .HasConstraintName("FK_UserRole_RoleId")
                        .OnDelete(DeleteBehavior.Cascade),
                    user => user
                        .HasOne<User>()
                        .WithMany()
                        .HasForeignKey("UserId")
                        .HasConstraintName("FK_UserRole_UserId")
                        .OnDelete(DeleteBehavior.Cascade));

            builder
                .HasMany(x => x.Payments)
                .WithOne(x => x.User)
                .HasConstraintName("FK_User_Payment")
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
