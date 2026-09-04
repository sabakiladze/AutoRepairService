using AutoRepairService.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.Identity.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoRepairService.Infrastructure.Persistence.Configurations
{
    public class CustomerProfileConfiguration:IEntityTypeConfiguration<CustomerProfile>
    {
        public void Configure(EntityTypeBuilder<CustomerProfile> builder)
        {
            builder.ToTable("Customer_Profile_Table");
            builder.HasKey(x => x.Id);
            builder.Property(x=>x.Id).HasDefaultValue("NEWID()");
            builder.Property(x => x.UserId).HasColumnName("Users_Id").IsRequired();
            builder.HasIndex(x => x.UserId).IsUnique();
            builder.Property(x => x.DefaultAddress).HasMaxLength(500).IsRequired();

            builder.HasOne(x=>x.User).WithOne(x=>x.CustomerProfile).HasForeignKey<CustomerProfile>(x => x.UserId);
        }
    }
}
