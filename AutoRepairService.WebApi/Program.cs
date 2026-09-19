
using AutoRepairService.Application.ServiceInterfaces;
using AutoRepairService.Application.Services;
using AutoRepairService.Domain.Interfaces;
using AutoRepairService.Domain.Interfaces.RepositoryInterfaces;
using AutoRepairService.Infrastructure;
using AutoRepairService.Infrastructure.Persistence;
using AutoRepairService.Infrastructure.Repositories;
using AutoRepairService.Infrastructure.Services;
using AutoRepairService.Infrastructure.Settings;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace AutoRepairService.WebApi
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            // Add services to the container.


            //პოულობს მისამართს: appsettings.json-იდან ამოაქვს მონაცემთა ბაზის მისამართი (DefaultConnection).
            //ირჩევს ბაზის ტიპს: პროგრამას ეუბნება, რომ გამოიყენოს Microsoft SQL Server-ი.
            //ხდის ხელმისაწვდომს: ბაზის მართვის ხელსაწყოს (AppDbContext) ამზადებს მთელ პროგრამაში გამოსაყენებლად. პროგრამა ავტომატურად მართავს AppDbContext-ის შექმნას და გადაცემას იქ, სადაც ის გჭირდებათ (ამას .NET-ში Dependency Injection ჰქვია).
            builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("DefaultConnection")));   // AppDbContext მა იცის ყველა სია. ამ კოდით ვეუბნები, რომ როდესაც რეპოზიტორში დამჭირდება ეს ობიექტი რადგან მისი სია გამოვიყენო, თავისით შემნას და არ დამჭირდეს new()


            //ეს ხაზი ეუბნება შენს აპლიკაციას,
            //    რომ ავტომატურად იპოვოს და დაიმახსოვროს 
            //    შენი დაწერილი ყველა Mapping წესი
            //    (რომელი ობიექტი რომელზე გადავიდეს).
           

            builder.Services.Configure<EmailSettings>(
    builder.Configuration.GetSection("EmailSettings"));


            
            builder.Services.Configure<JwtSettings>(
                builder.Configuration.GetSection("JwtSettings"));



            builder.Services.AddScoped<IUserRepository, UserRepository>();
            builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
            builder.Services.AddScoped<IEmailService, EmailService>();
            builder.Services.AddScoped<IAuthentication, AuthenticationService>();
            builder.Services.AddScoped<IRoleRepository, RoleRepository>();
            builder.Services.AddScoped<ITokenService, JwtTokenService>();


             var jwtSettings = builder.Configuration.GetSection("JwtSettings").Get<JwtSettings>()
                ?? throw new InvalidOperationException("JwtSettings section is missing.");

            builder.Services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = jwtSettings.Issuer,
                    ValidAudience = jwtSettings.Audience,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings.SecretKey)),
                    ClockSkew = TimeSpan.Zero
                };
            });

            builder.Services.AddAuthorization();
            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();


            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
           

            var app = builder.Build();

           
            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthentication();
            app.UseAuthorization();
            
            app.MapControllers();

            app.Run();
        }
    }
}
