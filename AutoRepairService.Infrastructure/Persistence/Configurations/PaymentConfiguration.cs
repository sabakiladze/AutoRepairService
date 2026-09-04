using AutoRepairService.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AutoRepairService.Infrastructure.Configurations;

public class PaymentConfiguration : IEntityTypeConfiguration<PayMent>
{
    public void Configure(EntityTypeBuilder<PayMent> builder)
    {
        builder.ToTable("PayMents_Table");

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

        builder.Property(x => x.ClientCardId)
            .HasColumnName("Client_Card_Id")
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
            .HasPrincipalKey(x => x.UserId);

        builder.HasOne(x => x.Mechanic)
            .WithMany(x => x.Payments)
            .HasForeignKey(x => x.MechanicId)
            .HasPrincipalKey(x => x.UserId);

        builder.HasOne(x => x.Service)
            .WithMany(x => x.Payments)
            .HasForeignKey(x => x.ServiceId);

        builder.HasOne(x => x.ClientCard)
            .WithMany(x => x.Payments)
            .HasForeignKey(x => x.ClientCardId);

        builder.HasOne(x => x.MechanicAccount)
            .WithMany(x => x.PayMents)
            .HasForeignKey(x => x.MechanicAccountId);

        builder.ToTable("PayMents_Table", table =>
        {
            table.HasCheckConstraint(
                "CK_Payment_Status",
                "LOWER([Status]) IN " +
                "('pending', 'done', 'rejected', 'coudnotmake')");
        });
    }
}