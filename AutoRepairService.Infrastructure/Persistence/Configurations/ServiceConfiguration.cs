using AutoRepairService.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AutoRepairService.Infrastructure.Configurations;

public class ServiceConfiguration : IEntityTypeConfiguration<Service>
{
    public void Configure(EntityTypeBuilder<Service> builder)
    {
        builder.ToTable("Service_Table");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id)
            .HasDefaultValueSql("NEWID()");

        builder.Property(x => x.CustomerId)
            .HasColumnName("Customer_Id")
            .IsRequired();

        builder.Property(x => x.MechanicId)
            .HasColumnName("Mechanic_Id")
            .IsRequired();

        builder.Property(x => x.VehicleId)
            .HasColumnName("Vehicle_Id")
            .IsRequired();

        builder.Property(x => x.Description)
            .HasColumnType("nvarchar(max)");

        builder.Property(x => x.RequestedAt)
            .HasColumnName("Requested_At")
            .HasDefaultValueSql("SYSDATETIME()")
            .IsRequired();

        builder.Property(x => x.AcceptedByMechanicAt)
            .HasColumnName("Accepted_by_mechanic_at");

        builder.Property(x => x.DoneAt)
            .HasColumnName("Done_At");

        builder.Property(x => x.Total)
            .HasPrecision(18, 2)
            .IsRequired();

        builder.Property(x => x.IsPaid)
            .HasDefaultValue(false)
            .IsRequired();

        builder.Property(x => x.EstimatedHours)
            .HasColumnName("EstimatedHours")
            .HasPrecision(5, 2);

        builder.Property(x => x.PaymentMethod)
            .HasMaxLength(50);

        builder.Property(x => x.ServicePrice)
            .HasPrecision(18, 2)
            .IsRequired();

        builder.Property(x => x.PartsPrice)
            .HasPrecision(18, 2)
            .IsRequired();

        builder.HasOne(x => x.Customer)
            .WithMany(x => x.Services)
            .HasForeignKey(x => x.CustomerId)
            .HasPrincipalKey(x => x.UserId);

        builder.HasOne(x => x.Mechanic)
            .WithMany(x => x.Services)
            .HasForeignKey(x => x.MechanicId)
            .HasPrincipalKey(x => x.UserId);

        builder.HasOne(x => x.Vehicle)
            .WithMany(x => x.Services)
            .HasForeignKey(x => x.VehicleId);
    }
}