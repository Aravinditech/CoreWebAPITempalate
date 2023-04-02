using CoreTempalate.Models.EntityModels;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace CoreTempalate.EntityConfiguration
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable("Users");

            builder.HasKey(e => e.Id);

            builder.Property(e => e.Id)
                .HasColumnName("Id")
                .HasColumnType("uniqueidentifier");

            builder.Property(e => e.Username)
                .IsRequired()
                .HasColumnType("varchar(100)");

            builder.Property(e => e.Email)
                .IsRequired()
                .HasColumnType("varchar(100)");

            builder.Property(e => e.RoleId)
                .IsRequired().HasColumnType("uniqueidentifier");

            builder.HasOne(d => d.Role)
                .WithMany(p => p.Users)
                .HasForeignKey(d => d.RoleId)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("FK_Users_Roles");
        }
    }
}
