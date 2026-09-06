
using AutoRepairService.Application.Mapping;
using AutoRepairService.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace AutoRepairService.WebApi
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.


            //პოულობს მისამართს: appsettings.json-იდან ამოაქვს მონაცემთა ბაზის მისამართი (DefaultConnection).
            //ირჩევს ბაზის ტიპს: პროგრამას ეუბნება, რომ გამოიყენოს Microsoft SQL Server-ი.
            //ხდის ხელმისაწვდომს: ბაზის მართვის ხელსაწყოს (AppDbContext) ამზადებს მთელ პროგრამაში გამოსაყენებლად. პროგრამა ავტომატურად მართავს AppDbContext-ის შექმნას და გადაცემას იქ, სადაც ის გჭირდებათ (ამას .NET-ში Dependency Injection ჰქვია).
            builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("DefaultConnection")));


            //ეს ხაზი ეუბნება შენს აპლიკაციას,
            //    რომ ავტომატურად იპოვოს და დაიმახსოვროს 
            //    შენი დაწერილი ყველა Mapping წესი
            //    (რომელი ობიექტი რომელზე გადავიდეს).
            builder.Services.AddAutoMapper(
    typeof(MappingProfile).Assembly);


            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
