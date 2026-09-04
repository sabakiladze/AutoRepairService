using AutoRepairService.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoRepairService.Infrastructure.Persistence.Configurations
{
    public class VehicleConfiguration:IEntityTypeConfiguration<Vehicle>
    {
        public void Configure(EntityTypeBuilder<Vehicle> builder)
        {
            builder.ToTable("Vehicles_Table");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id).HasDefaultValue("NEWID()");

            builder.Property(x => x.UserId).HasColumnName("Users_Id").IsRequired(); //ანუ რადგან sql and c# name არ ემთხვევა ამიტომ უნდა დავუწეროთ?

            builder.Property(x=>x.Brand).HasMaxLength(50).IsRequired();

            builder.Property(x => x.Model).HasMaxLength(50).IsRequired();

            builder.Property(x => x.Year).IsRequired();

            builder.Property(x=>x.Engine).HasMaxLength(50).IsRequired();

            builder.Property(x => x.Transmission).HasMaxLength(50).IsRequired();

            builder.Property(x => x.PlateNumber).HasMaxLength(20).IsRequired();
            builder.HasIndex(x => x.PlateNumber).IsUnique();

            builder.HasOne(x => x.User).WithMany(x => x.Vehicles).HasForeignKey(x => x.UserId).OnDelete(DeleteBehavior.Cascade);




            // checks
            builder.ToTable("Vehicle_Table", table =>
            {
                table.HasCheckConstraint(
               "CK_Vehicle_Year",
               "[Year] <= YEAR(GETDATE())");

                table.HasCheckConstraint(
                    "CK_Vehicle_Engine",
                    "LOWER([Engine]) IN ('gasoline', 'diesel', 'hybrid', 'electric', 'plug-in hybrid')");

                table.HasCheckConstraint(
                    "CK_Vehicle_Transmission",
                    "LOWER([Transmission]) IN ('automatic', 'manual', 'cvt', 'semi-automatic')");

                table.HasCheckConstraint(
                    "CK_Vehicle_PlateNumber",
                    "[PlateNumber] LIKE '[A-Z][A-Z]-[0-9][0-9][0-9]-[A-Z][A-Z]' OR " +
                    "[PlateNumber] LIKE '[A-Z][A-Z][A-Z]-[0-9][0-9][0-9]'");
            });
        }
    }
}
