using AutoRepairService.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

public class ServicePartConfiguration
    : IEntityTypeConfiguration<ServicePart>
{
    public void Configure(EntityTypeBuilder<ServicePart> builder)
    {
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id)
            .HasDefaultValueSql("NEWID()");

        builder.Property(x => x.PartId)
            .HasColumnName("Part_Id")
            .IsRequired();

        builder.Property(x => x.ServiceId)
            .HasColumnName("Service_Id")
            .IsRequired();

        builder.Property(x => x.Quantity)          
            .HasDefaultValue(1)
            .IsRequired();

        builder.HasIndex(x => new { x.PartId, x.ServiceId })   
            .IsUnique();

        builder.HasOne(x => x.Part)
            .WithMany(x => x.ServiceParts)
            .HasForeignKey(x => x.PartId);

        builder.HasOne(x => x.Service)
            .WithMany(x => x.ServiceParts)
            .HasForeignKey(x => x.ServiceId);

    builder.ToTable("Service_Parts", table => {           
            table.HasCheckConstraint("CK_ServiceParts_Quantity", "[Quantity] > 0");
        });
    }
}
