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
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {

        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable("User_table");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).HasDefaultValueSql("NEWID()");
            builder.Property(x => x.Email).HasMaxLength(250).IsRequired();
            builder.HasIndex(x => x.Email).IsUnique();
            builder.Property(x => x.PasswordHash).HasColumnType("varchar(250)").IsRequired();
            builder.Property(x => x.Token).HasColumnType("varchar(max)");
            builder.Property(x => x.RefreshToken).HasColumnType("varchar(500)");
            builder.Property(x => x.IsActive).HasDefaultValue(true);
            builder.Property(x => x.CreatedAt).HasDefaultValue("SYSDATETIME()");

            // 
        }
        
        
    }
    
}
