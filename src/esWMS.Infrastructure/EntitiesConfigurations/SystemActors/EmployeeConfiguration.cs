using esWMS.Domain.Entities.SystemActors;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace esWMS.Infrastructure.EntitiesConfigurations.SystemActors
{
    internal class EmployeeConfiguration : IEntityTypeConfiguration<Employee>
    {
        public void Configure(EntityTypeBuilder<Employee> builder)
        {
            builder.HasKey(e => e.EmployeeId);

            builder.Property(e => e.EmployeeId)
                .IsRequired()
                .HasMaxLength(60);

            builder.Property(e => e.RoleId)
                .IsRequired()
                .HasMaxLength(3);

            builder.Property(e => e.FirstName)
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(e => e.LastName)
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(e => e.PasswordHash)
                .HasMaxLength(255);

            builder.Property(e => e.Country)
                .HasMaxLength(100);

            builder.Property(e => e.City)
                .HasMaxLength(100);

            builder.Property(e => e.Region)
                .HasMaxLength(100);

            builder.Property(e => e.PostalCode)
                .HasMaxLength(25);

            builder.Property(e => e.Address)
                .HasMaxLength(250);

            builder.Property(e => e.EmailAddress)
                .HasMaxLength(255);

            builder.Property(e => e.PhoneNumber)
                .HasMaxLength(20);

            builder.Property(e => e.IsActive)
                .IsRequired();

            builder.Property(e => e.CreatedAt)
                .IsRequired();

            builder.Property(e => e.CreatedBy)
                .HasMaxLength(60);

            builder.Property(e => e.ModifiedBy)
                .HasMaxLength(60);
        }
    }
}
