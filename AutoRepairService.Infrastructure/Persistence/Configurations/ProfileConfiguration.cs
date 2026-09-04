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
    public class ProfileConfiguration:IEntityTypeConfiguration<Profile>
    {
        public void Configure(EntityTypeBuilder<Profile> builder)
        {
            builder.ToTable("Profile_Table");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).HasDefaultValue("NEWID()");
            builder.Property(x => x.UserId).HasColumnName("Users_Id").IsRequired();
            builder.HasIndex(x => x.UserId).IsUnique();
            builder.Property(x => x.FirstName)
          .HasMaxLength(50)
          .IsRequired();

            builder.Property(x => x.LastName)
                .HasMaxLength(50)
                .IsRequired();

            builder.Property(x => x.PhoneNumber)
                .HasMaxLength(20)
                .IsRequired();

            builder.Property(x => x.CreatedAt)
                .HasDefaultValueSql("SYSDATETIME()");

            builder.Property(x => x.ProfileImage)
                .HasColumnType("varbinary(max)");

            builder.HasOne(x=>x.User).WithOne(x=>x.Profile).HasForeignKey<Profile>(x => x.UserId);
        }
    }
}
