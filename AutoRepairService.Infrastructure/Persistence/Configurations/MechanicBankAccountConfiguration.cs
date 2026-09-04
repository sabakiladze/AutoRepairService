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
    public class MechanicBankAccountConfiguration:IEntityTypeConfiguration<MechanicBankAccount>

    {
        public void Configure(EntityTypeBuilder <MechanicBankAccount> builder)
        {
            builder.ToTable("Mechanic_Bank_Accounts_Table");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id)
                .HasDefaultValueSql("NEWID()");

            builder.Property(x => x.MechanicId)
                .HasColumnName("Mechanic_Id")
                .IsRequired();

            builder.Property(x => x.IBAN)
                .HasMaxLength(50)
                .IsRequired();

            builder.HasIndex(x => x.IBAN)
                .IsUnique();

            builder.Property(x => x.BankName)
                .HasColumnName("Bank_Name")
                .HasMaxLength(50)
                .IsRequired();

            builder.Property(x => x.HolderName)
                .HasColumnName("Holder_Name")
                .HasMaxLength(50)
                .IsRequired();

            builder.Property(x => x.IsDefault)
                .HasDefaultValue(false);


            // ამასაც ყურადღება. რატომ
            // არ აქვს <> აქ ტიპი??
            builder.HasOne(x => x.MechanicProfile)
                .WithMany(x => x.BankAccounts)
                .HasForeignKey(x => x.MechanicId)
                .HasPrincipalKey(x => x.UserId);
        }
    }
}
