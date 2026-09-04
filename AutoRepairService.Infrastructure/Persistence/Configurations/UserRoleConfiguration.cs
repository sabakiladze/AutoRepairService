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
    public class UserRoleConfiguration:IEntityTypeConfiguration<UserRole>
    {
        public void Configure(EntityTypeBuilder<UserRole> builder)
        {
            builder.ToTable("Users_Roles_Table");

            builder.HasKey(x => x.Id); // primarykey
            builder.Property(x => x.Id).HasDefaultValueSql("NEWID()");

            builder.Property(x => x.UserId).HasColumnName("User_Id").IsRequired();

            builder.Property(x => x.RoleId).HasColumnName("Role_Id").IsRequired(); // აქვს property- იგივე column

            builder.HasIndex(x => new { x.UserId, x.RoleId }).IsUnique(); // არის უნიკალური

            builder.HasOne(x => x.User).WithMany(x => x.UserRoles).HasForeignKey(x => x.UserId).OnDelete(DeleteBehavior.Cascade); 

            builder.HasOne(x => x.Role).WithMany(x => x.UserRoles).HasForeignKey(x => x.RoleId).OnDelete(DeleteBehavior.Cascade);
          // კავშირები..
          //
        }
    }
}
