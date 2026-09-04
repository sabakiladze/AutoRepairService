using AutoRepairService.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AutoRepairService.Infrastructure.Configurations;

public class CardConfiguration : IEntityTypeConfiguration<Card>
{
    public void Configure(EntityTypeBuilder<Card> builder)
    {
        builder.ToTable("Card_Table");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id)
            .HasDefaultValueSql("NEWID()");

        builder.Property(x => x.CustomerId)
            .HasColumnName("Customer_Id")
            .IsRequired();

        builder.Property(x => x.CardHolderName)
            .HasMaxLength(50)
            .IsRequired();

        builder.Property(x => x.Last4Digits)
            .HasColumnType("char(4)")
            .IsRequired();

        builder.Property(x => x.CardBrand)
            .HasMaxLength(50)
            .IsRequired();

        builder.Property(x => x.ProcessorToken)
            .HasMaxLength(50)
            .IsRequired();

        builder.HasIndex(x => x.ProcessorToken)
            .IsUnique();

        builder.Property(x => x.IsDefault)
            .HasDefaultValue(false);

        /// ამას უნდა მივაქციო ყურადღება რატომ....
        builder.HasOne(x => x.Customer)
            .WithMany(x => x.Cards)
            .HasForeignKey(x => x.CustomerId)
            .HasPrincipalKey(x => x.UserId);

        /// ამასაც უნდა მივაქციო ყურადღება რატომ.
        builder.ToTable("Card_Table", table =>
        {
            table.HasCheckConstraint(
                "CK_Card_Last4Digits",
                "[Last4Digits] LIKE '[0-9][0-9][0-9][0-9]'");
        });
    }
}