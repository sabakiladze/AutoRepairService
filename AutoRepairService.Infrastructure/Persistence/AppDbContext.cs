using AutoRepairService.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace AutoRepairService.Infrastructure.Persistence;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {
    }

    //AppDbContext ის მთავარი დანიშნულებაა რომ ცხრილების მონაცემები აქციოს სიად.

    public DbSet<User> Users => Set<User>();
    public DbSet<Role> Roles => Set<Role>();
    public DbSet<UserRole> UserRoles => Set<UserRole>();
    public DbSet<MechanicProfile> MechanicProfiles => Set<MechanicProfile>();
    public DbSet<CustomerProfile> CustomerProfiles => Set<CustomerProfile>();
    public DbSet<Profile> Profiles => Set<Profile>();
    public DbSet<Vehicle> Vehicles => Set<Vehicle>();
    public DbSet<Card> Cards => Set<Card>();
    public DbSet<MechanicBankAccount> MechanicBankAccounts => Set<MechanicBankAccount>();
    public DbSet<Service> Services => Set<Service>();
    public DbSet<Part> Parts => Set<Part>();
    public DbSet<ServicePart> ServiceParts => Set<ServicePart>();
    public DbSet<PayMent> Payments => Set<PayMent>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(
            typeof(AppDbContext).Assembly);

        base.OnModelCreating(modelBuilder);

        //"Infrastructure assembly-ში მოძებნე ყველა IEntityTypeConfiguration<T> და გამოიყენე."
        //    აღარაა საჭირო new ... შექმნა მათი გამოყენებისთვის
    }
}