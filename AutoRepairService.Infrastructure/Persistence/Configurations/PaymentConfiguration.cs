using AutoRepairService.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AutoRepairService.Infrastructure.Configurations;

public class PaymentConfiguration : IEntityTypeConfiguration<PayMent>
{
    public void Configure(EntityTypeBuilder<PayMent> builder)
    {
        

        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id)
            .HasDefaultValueSql("NEWID()");

        builder.Property(x => x.CustomerId)
            .HasColumnName("Customer_Id")
            .IsRequired();

        builder.Property(x => x.MechanicId)
            .HasColumnName("Mechanic_Id")
            .IsRequired();

        builder.Property(x => x.ServiceId)
            .HasColumnName("Service_Id")
            .IsRequired();

        builder.Property(x => x.CustomerCardId)
            .HasColumnName("Customer_Card_Id")
            .IsRequired();

        builder.Property(x => x.MechanicAccountId)
            .HasColumnName("Mechanic_Account_Id")
            .IsRequired();

        builder.Property(x => x.Amount)
            .HasPrecision(18, 2)
            .IsRequired();

        builder.Property(x => x.Status)
            .HasColumnType("varchar(50)")
            .IsRequired();

        builder.Property(x => x.TransactionId)
            .HasColumnType("varchar(50)")
            .IsRequired();

        builder.Property(x => x.PaidAt)
            .HasColumnName("Paid_At")
            .HasDefaultValueSql("SYSDATETIME()");

        builder.HasOne(x => x.Customer)
            .WithMany(x => x.Payments)
            .HasForeignKey(x => x.CustomerId)
            .HasPrincipalKey(x => x.UserId).OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(x => x.Mechanic)
            .WithMany(x => x.Payments)
            .HasForeignKey(x => x.MechanicId)
            .HasPrincipalKey(x => x.UserId).OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(x => x.Service)
            .WithMany(x => x.Payments)
            .HasForeignKey(x => x.ServiceId).OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(x => x.CustomerCard)
            .WithMany(x => x.Payments)
            .HasForeignKey(x => x.CustomerCardId).OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(x => x.MechanicAccount)
            .WithMany(x => x.PayMents)
            .HasForeignKey(x => x.MechanicAccountId).OnDelete(DeleteBehavior.Restrict);

        builder.ToTable("PayMents_Table", table =>
        {
            table.HasCheckConstraint(
                "CK_Payment_Status",
                "LOWER([Status]) IN " +
                "('Pending', 'Done', 'Rejected', 'CoudlNotMake')");
        });
    }
}
