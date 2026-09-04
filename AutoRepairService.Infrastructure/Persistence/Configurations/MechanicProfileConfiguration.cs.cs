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
    public  class MechanicProfileConfiguration
        :IEntityTypeConfiguration<MechanicProfile>
    {
        public void Configure(EntityTypeBuilder<MechanicProfile> builder)
        {
            builder.ToTable("Mechanic_Profile_Table");

            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).HasDefaultValue("NEWID()");

            builder.Property(x => x.UserId).HasColumnName("Users_Id").IsRequired();
            builder.HasIndex(x => x.UserId).IsUnique();

            builder.Property(x=>x.Specialization).HasMaxLength(100).IsRequired();
            builder.Property(x => x.ExperienceYears).HasDefaultValue(0);
            builder.Property(x => x.HourlyRate).HasPrecision(18, 2).HasDefaultValue(0.00m);
            builder.Property(x => x.Bio).HasMaxLength(250);
            builder.Property(x=>x.IsVerified).HasDefaultValue(false);
            builder.Property(x => x.Rating).HasPrecision(3, 2).HasDefaultValue(0.00m);
            builder.Property(x => x.IsAvailable).HasDefaultValue(false);
            builder.Property(x => x.Latitde).HasPrecision(9,6).IsRequired();
            builder.Property(x=>x.Longitde).HasPrecision(9,6).IsRequired();
            builder.Property(x => x.CmpletedJobsCount).HasDefaultValue(0);


            builder.HasOne(x => x.User).WithOne(x => x.MechanicProfile).HasForeignKey<MechanicProfile>(x => x.UserId);
            /// ეს ვერ გავიგე რა.

        }
    }
}
